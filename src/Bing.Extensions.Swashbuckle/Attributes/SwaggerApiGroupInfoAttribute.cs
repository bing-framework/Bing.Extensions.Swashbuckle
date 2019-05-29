using System;

namespace Bing.Extensions.Swashbuckle.Attributes
{
    /// <summary>
    /// Swagger：Api分组信息。用于显示在界面版本选择名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property| AttributeTargets.Enum)]
    public class SwaggerApiGroupInfoAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

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
