using Microsoft.AspNetCore.Mvc;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// Css结果
/// </summary>
public class CssResult : ContentResult
{
    /// <summary>
    /// 初始化一个<see cref="CssResult"/>类型的实例
    /// </summary>
    /// <param name="css">Css</param>
    public CssResult(string css)
    {
        this.Content = css;
        this.ContentType = "text/css";
    }
}