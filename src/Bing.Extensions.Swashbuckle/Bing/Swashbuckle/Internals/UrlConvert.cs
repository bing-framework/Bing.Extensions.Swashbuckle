using System;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// Url转换
/// </summary>
internal class UrlConvert
{
    /// <summary>
    /// 转换键
    /// </summary>
    /// <param name="paths">路径字典</param>
    /// <param name="func">转换函数</param>
    public static OpenApiPaths ConvertKeys(OpenApiPaths paths, Func<string, string> func)
    {
        var result = new OpenApiPaths();
        foreach (var path in paths)
            result[func(path.Key)] = path.Value;
        return result;
    }

    /// <summary>
    /// 大写
    /// </summary>
    public static string Upper(string key) => string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToUpper()));

    /// <summary>
    /// 小写
    /// </summary>
    public static string Lower(string key) => string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower()));

    /// <summary>
    /// 首字母大写
    /// </summary>
    public static string FirstUpper(string key) => string.Join("/",
        key.Split('/').Select(x => x.Contains("{") ? x : ConvertCase(x, false)));

    /// <summary>
    /// 首字母小写
    /// </summary>
    public static string FirstLower(string key) => string.Join("/",
        key.Split('/').Select(x => x.Contains("{") ? x : ConvertCase(x, true)));

    /// <summary>
    /// 转换大小写
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="lower">是否小写</param>
    private static string ConvertCase(string value, bool lower) => string.IsNullOrWhiteSpace(value)
        ? string.Empty
        : lower
            ? $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}"
            : $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
}