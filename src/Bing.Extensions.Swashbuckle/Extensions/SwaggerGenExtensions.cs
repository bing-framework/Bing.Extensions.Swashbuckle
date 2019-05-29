using System;
using System.Collections.Generic;
using System.Linq;
using Bing.Extensions.Swashbuckle.Attributes;
using Bing.Extensions.Swashbuckle.Core;
using Bing.Extensions.Swashbuckle.Filters.Documents;
using Bing.Extensions.Swashbuckle.Filters.Operations;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// SwaggerGen 扩展
    /// </summary>
    public static class SwaggerGenExtensions
    {
        /// <summary>
        /// Api分组
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="options">Swagger生成选项</param>
        public static void ApiGroup<TEnum>(this SwaggerGenOptions options) where TEnum : struct
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
            {
                return;
            }
            // 遍历 TEnum 所有枚举值生成接口文档，Skip(1)是因为Enum第一个FileInfo是内置的一个int值
            type.GetFields().Skip(1).ToList().ForEach(x =>
            {
                var info = x.GetCustomAttributes(typeof(SwaggerApiGroupInfoAttribute), false)
                    .OfType<SwaggerApiGroupInfoAttribute>().FirstOrDefault();
                options.SwaggerDoc(x.Name, new Info()
                {
                    Title = info?.Title,
                    Version = info?.Version,
                    Description = info?.Description
                });
            });

            // 没有加特性的分到这个NoGroup上
            options.SwaggerDoc("NoGroup", new Info()
            {
                Title = "无分组"
            });

            // 判断接口归于哪个分组
            options.DocInclusionPredicate((docName, apiDescription) =>
            {
                if (docName == "NoGroup")
                {
                    // 当分组为NoGroup时，只要没加特性的都属于这个组
                    return string.IsNullOrWhiteSpace(apiDescription.GroupName);
                }
                else
                {
                    foreach (var obj in apiDescription.ActionDescriptor.EndpointMetadata)
                    {
                        if (obj is SwaggerApiGroupAttribute swaggerApiGroup)
                        {
                            if (docName == swaggerApiGroup.GroupName)
                            {
                                return true;
                            }
                        }
                    }

                    return false;
                }
            });
        }

        /// <summary>
        /// 显示枚举描述
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void ShowEnumDescription(this SwaggerGenOptions options)
        {
            if (options.DocumentFilterDescriptors.Exists(x => x.Type == typeof(AddEnumDescriptionsDocumentFilter)))
            {
                return;
            }
            options.DocumentFilter<AddEnumDescriptionsDocumentFilter>();
        }

        /// <summary>
        /// 显示文件参数
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void ShowFileParameter(this SwaggerGenOptions options)
        {
            if (options.OperationFilterDescriptors.Exists(x => x.Type == typeof(FileParameterOperationFilter)))
            {
                return;
            }
            options.OperationFilter<FileParameterOperationFilter>();
        }

        /// <summary>
        /// 添加通用参数参数
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        /// <param name="parameters">参数列表</param>
        public static void AddCommonParameter(this SwaggerGenOptions options, IEnumerable<IParameter> parameters)
        {
            if (options.OperationFilterDescriptors.Exists(x => x.Type == typeof(CommonParametersOperationFilter)))
            {
                return;
            }
            options.OperationFilter<CommonParametersOperationFilter>(parameters);
        }

        /// <summary>
        /// 显示Url模式
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        /// <param name="mode">显示Url模式</param>
        public static void ShowUrlMode(this SwaggerGenOptions options, UrlShowMode mode = UrlShowMode.FirstLowercase)
        {
            switch (mode)
            {
                case UrlShowMode.FirstLowercase:
                    options.AddDocumentFilter<FirstLowercaseUrlDocumentFilter>();
                    break;
                case UrlShowMode.AllLowercase:
                    options.AddDocumentFilter<AllLowercaseUrlDocumentFilter>();
                    break;
                case UrlShowMode.FirstUppercase:
                    options.AddDocumentFilter<FirstUppercaseUrlDocumentFilter>();
                    break;
                case UrlShowMode.AllUppercase:
                    options.AddDocumentFilter<AllUppercaseUrlDocumentFilter>();
                    break;
            }
        }

        /// <summary>
        /// 添加文档过滤器
        /// </summary>
        /// <typeparam name="TDocumentFilter">文档过滤器类型</typeparam>
        /// <param name="options">Swagger生成选项</param>
        /// <param name="arguments">参数</param>
        internal static void AddDocumentFilter<TDocumentFilter>(this SwaggerGenOptions options,
            params object[] arguments)
            where TDocumentFilter : IDocumentFilter
        {
            if (options.DocumentFilterDescriptors.Exists(x => x.Type == typeof(TDocumentFilter)))
            {
                return;
            }

            options.DocumentFilter<TDocumentFilter>(arguments);
        }

        /// <summary>
        /// 添加操作过滤器
        /// </summary>
        /// <typeparam name="TOperationFilter">操作过滤器类型</typeparam>
        /// <param name="options">Swagger生成选项</param>
        /// <param name="arguments">参数</param>
        internal static void AddOperationFilter<TOperationFilter>(this SwaggerGenOptions options,
            params object[] arguments)
            where TOperationFilter : IOperationFilter
        {
            if (options.OperationFilterDescriptors.Exists(x => x.Type == typeof(TOperationFilter)))
            {
                return;
            }

            options.OperationFilter<TOperationFilter>(arguments);
        }

        /// <summary>
        /// 启用请求头过滤器
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void EnableRequestHeader(this SwaggerGenOptions options)
        {
            options.AddOperationFilter<RequestHeaderOperationFilter>();
        }

        /// <summary>
        /// 启用响应头过滤器
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void EnableResponseHeader(this SwaggerGenOptions options)
        {
            options.AddOperationFilter<ResponseHeadersOperationFilter>();
        }

        /// <summary>
        /// 显示授权信息
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void ShowAuthorizeInfo(this SwaggerGenOptions options)
        {
            options.AddOperationFilter<SecurityRequirementsOperationFilter>();
            options.AddOperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        }

        ///// <summary>
        ///// 显示授权信息
        ///// </summary>
        ///// <typeparam name="TAttribute">授权特性类型</typeparam>
        ///// <param name="options">Swagger生成选项</param>
        ///// <param name="policySelectors">策略选择器</param>
        //public static void ShowAuthorizeInfo<TAttribute>(this SwaggerGenOptions options,
        //    IEnumerable<PolicySelectorWithLabel<TAttribute>> policySelectors) where TAttribute : Attribute
        //{
        //    options.AddOperationFilter<SecurityRequirementsOperationFilter<TAttribute>>();
        //    options.AddOperationFilter<AppendAuthorizeToSummaryOperationFilter<TAttribute>>(policySelectors);
        //}
    }
}
