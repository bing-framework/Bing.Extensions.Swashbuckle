using System.Collections.Generic;
using Bing.Swashbuckle.Core;
using Bing.Swashbuckle.Filters.Documents;
using Bing.Swashbuckle.Filters.Operations;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// ReSharper disable once CheckNamespace
namespace Bing.Swashbuckle
{
    /// <summary>
    /// SwaggerGen 扩展
    /// </summary>
    public static class SwaggerGenExtensions
    {
        /// <summary>
        /// 显示枚举描述
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void ShowEnumDescription(this SwaggerGenOptions options)
        {
            if (!options.ParameterFilterDescriptors.Exists(x => x.Type == typeof(EnumDescriptionsDocumentFilter)))
                options.ParameterFilter<EnumDescriptionsDocumentFilter>();
            if (!options.SchemaFilterDescriptors.Exists(x => x.Type == typeof(EnumDescriptionSchemaFilter)))
                options.SchemaFilter<EnumDescriptionSchemaFilter>();
        }

        /// <summary>
        /// 显示文件参数
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void ShowFileParameter(this SwaggerGenOptions options)
        {
            if (options.OperationFilterDescriptors.Exists(x => x.Type == typeof(FileParameterOperationFilter)))
                return;
            options.OperationFilter<FileParameterOperationFilter>();
        }

        /// <summary>
        /// 添加通用参数参数
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        /// <param name="parameters">参数列表</param>
        public static void AddCommonParameter(this SwaggerGenOptions options, IEnumerable<OpenApiParameter> parameters)
        {
            if (options.OperationFilterDescriptors.Exists(x => x.Type == typeof(CommonParametersOperationFilter)))
                return;
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
                    options.AddDocumentFilter<FirstLowerUrlDocumentFilter>();
                    break;
                case UrlShowMode.AllLowercase:
                    options.AddDocumentFilter<LowerUrlDocumentFilter>();
                    break;
                case UrlShowMode.FirstUppercase:
                    options.AddDocumentFilter<FirstUpperUrlDocumentFilter>();
                    break;
                case UrlShowMode.AllUppercase:
                    options.AddDocumentFilter<UpperUrlDocumentFilter>();
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
                return;
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
                return;
            options.OperationFilter<TOperationFilter>(arguments);
        }

        /// <summary>
        /// 启用请求头过滤器
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void EnableRequestHeader(this SwaggerGenOptions options) => options.AddOperationFilter<RequestHeaderOperationFilter>();

        /// <summary>
        /// 启用响应头过滤器
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void EnableResponseHeader(this SwaggerGenOptions options) => options.AddOperationFilter<ResponseHeadersOperationFilter>();

        /// <summary>
        /// 显示授权信息
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void ShowAuthorizeInfo(this SwaggerGenOptions options)
        {
            options.AddOperationFilter<SecurityRequirementsOperationFilter>();
            options.AddOperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        }

        /// <summary>
        /// 启用默认值过滤器
        /// </summary>
        /// <param name="options">Swagger生成选项</param>
        public static void EnableDefaultValue(this SwaggerGenOptions options) => options.AddOperationFilter<DefaultValueOperationFilter>();

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
