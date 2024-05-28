using System.Threading.Tasks;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Swashbuckle.Controllers;

/// <summary>
/// 资源 控制器
/// </summary>
[AllowAnonymous]
[Route("swagger/resources")]
public class ResourcesController : Controller
{
    /// <summary>
    /// 获取资源。通过 /swagger/resources?name=
    /// </summary>
    /// <param name="name">资源文件名</param>
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ContentResult> GetAsync([FromQuery] string name)
    {
        var result = new ContentResult();
        var names = name.Split('.');
        if (names.Length < 2)
            return result;
        var suffix = names[names.Length - 1];
        result.Content = await Common.LoadContentAsync(name);
        result.ContentType = GetContentType(suffix);
        return result;
    }

    /// <summary>
    /// 获取内容类型
    /// </summary>
    /// <param name="suffix">后缀</param>
    private static string GetContentType(string suffix)
    {
        switch (suffix.ToLower())
        {
            case "css":
                return "text/css";
            case "js":
                return "application/javascript";
            default:
                return "text/plain;";
        }
    }

    /// <summary>
    /// 获取语言资源文件
    /// </summary>
    /// <param name="name">语言名称</param>
    [HttpGet("getLanguage")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ContentResult> GetLanguageAsync([FromQuery] string name)
    {
        var result = new ContentResult
        {
            Content = await Common.GetLanguageAsync(name),
            ContentType = GetContentType("js")
        };
        return result;
    }
}