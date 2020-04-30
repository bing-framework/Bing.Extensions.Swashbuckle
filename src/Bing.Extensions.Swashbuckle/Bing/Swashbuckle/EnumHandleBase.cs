using System;
using System.Text;
using Enum = Bing.Swashbuckle.Internals.Enum;

namespace Bing.Swashbuckle
{
    /// <summary>
    /// 枚举处理基类
    /// </summary>
    internal abstract class EnumHandleBase
    {
        /// <summary>
        /// 格式化描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        protected virtual string FormatDescription(Type type)
        {
            var sb = new StringBuilder();
            var result = Enum.GetDescriptions(type);
            foreach (var item in result)
                sb.AppendLine($"{item.Value} = {(string.IsNullOrEmpty(item.Description) ? item.Name : item.Description)}");
            return sb.ToString();
        }
    }
}
