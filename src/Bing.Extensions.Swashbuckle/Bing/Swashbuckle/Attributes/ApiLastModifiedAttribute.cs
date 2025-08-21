using System;

namespace Bing.Swashbuckle.Attributes;

/// <summary>
/// API最后修改信息
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class ApiLastModifiedAttribute : Attribute
{
    /// <summary>
    /// 日期
    /// </summary>
    public string Date { get; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; }

    /// <summary>
    /// 初始化一个<see cref="ApiLastModifiedAttribute"/>类型的实例
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="author">作者</param>
    public ApiLastModifiedAttribute(string date, string author = null)
    {
        Date = date;
        Author = author;
    }
}