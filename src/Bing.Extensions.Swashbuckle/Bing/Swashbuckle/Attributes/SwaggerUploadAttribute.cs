using System;

namespace Bing.Swashbuckle.Attributes
{
    /// <summary>
    /// Swagger: 上传，用于标识接口是否包含上传信息参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SwaggerUploadAttribute : Attribute
    {
        /// <summary>
        /// 清空其它参数
        /// </summary>
        public bool ClearOtherParameters { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; } = "file";

        /// <summary>
        /// 是否必填项
        /// </summary>
        public bool Required { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = "上传文件";
    }
}
