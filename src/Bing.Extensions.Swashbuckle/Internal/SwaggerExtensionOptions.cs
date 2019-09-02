using System;
using Bing.Extensions.Swashbuckle.Core;

namespace Bing.Extensions.Swashbuckle.Internal
{
    /// <summary>
    /// Swagger扩展选项配置
    /// </summary>
    internal class SwaggerExtensionOptions
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 接口文档访问路由前缀
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// 是否启用API多版本
        /// </summary>
        public bool EnableApiVersion { get; set; }

        /// <summary>
        /// 是否启用API分组
        /// </summary>
        public bool EnableApiGroup { get; set; }

        /// <summary>
        /// API分组类型
        /// </summary>
        public Type ApiGroupType { get; set; }

        /// <summary>
        /// 是否启用自定义索引
        /// </summary>
        public bool EnableCustomIndex { get; set; }

        /// <summary>
        /// 是否启用授权访问
        /// </summary>
        public bool EnableAuthorization { get; set; }

        /// <summary>
        /// 是否启用显示枚举描述
        /// </summary>
        public bool EnableEnumDescription { get; set; }

        /// <summary>
        /// Url 显示模式
        /// </summary>
        public UrlShowMode UrlMode { get; set; }
    }
}
