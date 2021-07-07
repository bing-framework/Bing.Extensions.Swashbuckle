using System.ComponentModel;

namespace Bing.Samples.Api.V4.Models
{
    /// <summary>
    /// Post枚举例子
    /// </summary>
    public class PostEnumSample
    {
        /// <summary>
        /// 枚举样例
        /// </summary>
        public PostEnumType EnumSample { get; set; }
    }

    /// <summary>
    /// Post枚举类型
    /// </summary>
    public enum PostEnumType
    {
        /// <summary>
        /// 老大
        /// </summary>
        [Description("老大")]
        One = 1,
        /// <summary>
        /// 老二
        /// </summary>
        [Description("老二")]
        Two = 2,
        /// <summary>
        /// 老三
        /// </summary>
        [Description("老三")]
        Three = 3
    }
}
