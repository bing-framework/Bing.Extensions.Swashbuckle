using Bing.Swashbuckle;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bing.Samples.Common
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public static class StartupConfig
    {
        #region API

        /// <summary>
        /// Api 服务配置
        /// </summary>
        public static void ConfigureServicesByApi(SwaggerExOptions options)
        {
            options.ProjectName = "Bing.Sample.Api 在线文档调试";
            options.EnableCustomIndex = true;
            options.RoutePrefix = "swagger";
            options.EnableApiVersion = true;
            options.EnableCached = true;
            options.AddSwaggerGenAction = config =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
                config.IncludeXmlComments(xmlPath, true);
                config.UseInlineDefinitionsForEnums();
                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Token令牌",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });

                //config.OperationFilter<ApiVersionDefaultValueOperationFilter>();

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                //// 显示授权信息
                //config.ShowAuthorizeInfo();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 控制器排序
                config.OrderByController();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 添加通用参数
                config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                // 启用默认值
                config.EnableDefaultValue();
                // 配置自定义操作标识
                config.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                // 显示方法计数
                config.ShowActionCount();
                config.MapType<IFormFile>(() => new OpenApiSchema() { Type = "file" });
            };
        }

        /// <summary>
        /// Api 应用配置
        /// </summary>
        public static void ConfigureByApi(SwaggerExOptions options)
        {
            options.UseSwaggerAction = config =>
            {
                config.SerializeAsV2 = true;
            };
            options.UseSwaggerUIAction = config =>
            {
                config.UseInternalResources();
                config.UseTranslate();
                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();
                // 启用Token存储
                config.UseTokenStorage("oauth2");
            };
        }

        #endregion

        #region ApiGroup(API分组)

        /// <summary>
        /// ApiGroup 服务配置
        /// </summary>
        public static void ConfigureServicesByApiGroup(SwaggerExOptions options)
        {
            options.ProjectName = "Bing.Sample.ApiGroup 在线文档调试";
            options.EnableCustomIndex = true;
            options.RoutePrefix = "swagger";
            options.ApiGroupType = typeof(ApiGroupSample);
            options.AddSwaggerGenAction = config =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.ApiGroup.xml");
                config.IncludeXmlComments(xmlPath, true);

                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Token令牌",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 添加通用参数
                config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                // 启用默认值
                config.EnableDefaultValue();
            };
        }

        /// <summary>
        /// ApiGroup 应用配置
        /// </summary>
        public static void ConfigureByApiGroup(SwaggerExOptions options)
        {
            options.UseSwaggerUIAction = config =>
            {
                config.InjectJavascript("resources/jquery");
                config.InjectJavascript("resources/translator");
                config.InjectStylesheet("resources/swagger-common");

                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();
            };
        }

        #endregion

        #region MultipleVersionApi(多版本API)

        /// <summary>
        /// MultipleVersionApi 服务配置
        /// </summary>
        public static void ConfigureServicesByMultipleVersionApi(SwaggerExOptions options)
        {
            options.ProjectName = "Bing.Samples.MultipleVersion 在线文档调试";
            options.EnableCustomIndex = true;
            options.RoutePrefix = "swagger";
            options.EnableApiVersion = true;
            options.AddSwaggerGenAction = config =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.MultipleVersion.xml");
                config.IncludeXmlComments(xmlPath, true);

                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Token令牌",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 添加通用参数
                config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                // 启用默认值
                config.EnableDefaultValue();
            };
        }

        /// <summary>
        /// MultipleVersionApi 应用配置
        /// </summary>
        public static void ConfigureByMultipleVersionApi(SwaggerExOptions options)
        {
            options.UseSwaggerUIAction = config =>
            {
                config.InjectJavascript("resources/jquery");
                config.InjectJavascript("resources/translator");
                config.InjectStylesheet("resources/swagger-common");

                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();
            };
        }

        #endregion

        #region MultipleVersionGroupApi(多版本分组API)

        /// <summary>
        /// MultipleVersionGroupApi 服务配置
        /// </summary>
        public static void ConfigureServicesByMultipleVersionGroupApi(SwaggerExOptions options)
        {
            options.ProjectName = "Bing.Samples.MultipleVersionWithGroup 在线文档调试";
            options.EnableCustomIndex = true;
            options.RoutePrefix = "swagger";
            options.EnableApiVersion = true;
            options.ApiGroupType = typeof(ApiGroupSample);
            options.AddSwaggerGenAction = config =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.MultipleVersionWithGroup.xml");
                config.IncludeXmlComments(xmlPath, true);

                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Token令牌",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 添加通用参数
                config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                // 启用默认值
                config.EnableDefaultValue();
            };
        }

        /// <summary>
        /// MultipleVersionGroupApi 应用配置
        /// </summary>
        public static void ConfigureByMultipleVersionGroupApi(SwaggerExOptions options)
        {
            options.UseSwaggerUIAction = config =>
            {
                config.InjectJavascript("resources/jquery");
                config.InjectJavascript("resources/translator");
                config.InjectStylesheet("resources/swagger-common");

                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();
            };
        }

        #endregion

        #region NoGroup(无API分组)

        /// <summary>
        /// NoGroup 服务配置
        /// </summary>
        public static void ConfigureServicesByNoGroup(SwaggerExOptions options)
        {
            options.ProjectName = "Bing.Sample.NoApiGroup 在线文档调试";
            options.EnableCustomIndex = true;
            options.RoutePrefix = "swagger";
            //options.ApiVersions.Add(new ApiVersion()
            //{
            //    Description = "通用结果",
            //    Version = "v1"
            //});
            options.AddSwaggerGenAction = config =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.NoApiGroup.xml");
                config.IncludeXmlComments(xmlPath, true);

                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Token令牌",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 添加通用参数
                config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                // 启用默认值
                config.EnableDefaultValue();
            };
        }

        /// <summary>
        /// NoGroup 应用配置
        /// </summary>
        public static void ConfigureByNoGroup(SwaggerExOptions options)
        {
            options.UseSwaggerUIAction = config =>
            {
                config.SwaggerEndpoint("v1/swagger.json", "v1");
                config.InjectJavascript("resources/jquery");
                config.InjectJavascript("resources/translator");
                config.InjectStylesheet("resources/swagger-common");

                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();
            };
        }

        #endregion
    }
}
