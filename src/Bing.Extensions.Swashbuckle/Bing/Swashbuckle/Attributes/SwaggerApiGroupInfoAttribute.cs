using System;

namespace Bing.Swashbuckle.Attributes
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
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 初始化一个<see cref="SwaggerApiGroupInfoAttribute"/>类型的实例
        /// </summary>
        public SwaggerApiGroupInfoAttribute()
        {
        }

        /// <summary>
        /// 初始化一个<see cref="SwaggerApiGroupInfoAttribute"/>类型的实例
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        public SwaggerApiGroupInfoAttribute(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
