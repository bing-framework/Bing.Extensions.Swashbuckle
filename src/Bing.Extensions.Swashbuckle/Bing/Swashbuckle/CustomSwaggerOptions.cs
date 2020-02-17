using System;
using System.Collections.Generic;
using Bing.Extensions.Swashbuckle.Core;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bing.Swashbuckle
{
    /// <summary>
    /// 自定义Swagger选项
    /// </summary>
    public class CustomSwaggerOptions
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = "My Api";

        /// <summary>
        /// Api版本列表
        /// </summary>
        public List<ApiVersion> ApiVersions { get; set; } = new List<ApiVersion>();

        /// <summary>
        /// 接口文档访问路由前缀
        /// </summary>
        public string RoutePrefix { get; set; } = "swagger";

        /// <summary>
        /// 是否使用自定义首页
        /// </summary>
        public bool UseCustomIndex { get; set; }

        /// <summary>
        /// 是否启用Api版本号
        /// </summary>
        public bool EnableApiVersion { get; set; }

        /// <summary>
        /// Api分组类型
        /// </summary>
        public Type ApiGroupType { get; set; }

        /// <summary>
        /// Swagger授权登录账号，未指定则不启用
        /// </summary>
        public List<CustomSwaggerAuthorization> SwaggerAuthorizations { get; set; } =
            new List<CustomSwaggerAuthorization>();

        /// <summary>
        /// UseSwagger 操作
        /// </summary>
        public Action<SwaggerOptions> UseSwaggerAction { get; set; }

        /// <summary>
        /// UseSwaggerUI 操作
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public Action<SwaggerUIOptions> UseSwaggerUIAction { get; set; }

        /// <summary>
        /// AddSwaggerGen 操作
        /// </summary>
        public Action<SwaggerGenOptions> AddSwaggerGenAction { get; set; }

        /// <summary>
        /// 初始化一个<see cref="CustomSwaggerOptions"/>类型的实例
        /// </summary>
        public CustomSwaggerOptions() { }

        /// <summary>
        /// 初始化一个<see cref="CustomSwaggerOptions"/>类型的实例
        /// </summary>
        /// <param name="projectName">项目名称</param>
        /// <param name="apiVersions">Api版本列表</param>
        public CustomSwaggerOptions(string projectName, List<ApiVersion> apiVersions)
        {
            ProjectName = projectName;
            ApiVersions = apiVersions;
        }
    }

    /// <summary>
    /// Api版本
    /// </summary>
    public class ApiVersion
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
