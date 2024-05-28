using System;
using System.Text;
using Bing.Swashbuckle.Internals;
using Enum = Bing.Swashbuckle.Internals.Enum;

namespace Bing.Swashbuckle;

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
                sb.Append($"{item.Value} = {(string.IsNullOrEmpty(item.Description) ? item.Name : item.Description)}{Environment.NewLine}");
            return sb.ToString();
        }

    /// <summary>
    /// 格式化描述
    /// </summary>
    /// <param name="description">描述</param>
    /// <param name="type">枚举类型</param>
    protected virtual string FormatDescription(string description, Type type)
    {
            var sb = new StringBuilder(description);
            var enumPrefix = BuildContext.Instance.ExOptions.EnumPrefix;
            if (!string.IsNullOrEmpty(enumPrefix))
                sb.Append(enumPrefix);
            sb.AppendLine("<ul>");
            foreach (var item in Enum.GetDescriptions(type))
            {
                var itemDesc = string.Format(BuildContext.Instance.ExOptions.EnumItemFormat, item.Value, item.Name, item.Description);
                sb.AppendLine($"<li>{itemDesc}</li>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
}