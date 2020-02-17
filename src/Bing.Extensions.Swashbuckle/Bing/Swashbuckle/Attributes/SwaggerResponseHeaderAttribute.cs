using System;

namespace Bing.Swashbuckle.Attributes
{
    /// <summary>
    /// Swagger: 响应请求头，用于标识接口响应请求头参数信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseHeaderAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="SwaggerResponseHeaderAttribute"/>类型的实例
        /// </summary>
        /// <param name="statusCode">状态码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="description">描述</param>
        /// <param name="format">格式化</param>
        public SwaggerResponseHeaderAttribute(int statusCode, string name, string type, string description, string format = "")
        {
            StatusCodes = new[] { statusCode };
            Name = name;
            Type = type;
            Description = description;
            Format = format;
        }

        /// <summary>
        /// 初始化一个<see cref="SwaggerResponseHeaderAttribute"/>类型的实例
        /// </summary>
        /// <param name="statusCode">状态码</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="description">描述</param>
        /// <param name="format">格式化</param>
        public SwaggerResponseHeaderAttribute(int[] statusCode, string name, string type, string description,
            string format = "")
        {
            StatusCodes = statusCode;
            Name = name;
            Type = type;
            Description = description;
            Format = format;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public int[] StatusCodes { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; }
    }
}
