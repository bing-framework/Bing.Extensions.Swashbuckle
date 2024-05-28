using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

// ReSharper disable once CheckNamespace
namespace Bing.Swashbuckle;

/// <summary>
/// SwaggerEx 选项配置扩展
/// </summary>
internal static class SwaggerExOptionsExtensions
{
    /// <summary>
    /// 是否启用授权
    /// </summary>
    /// <param name="options">SwaggerEx 选项配置</param>
    public static bool EnableAuthorization(this SwaggerExOptions options) => options.SwaggerAuthorizations.Any();

    /// <summary>
    /// 是否启用API分组
    /// </summary>
    /// <param name="options">SwaggerEx 选项配置</param>
    public static bool EnableApiGroup(this SwaggerExOptions options) => options.ApiGroupType != null;

    /// <summary>
    /// 是否自定义版本
    /// </summary>
    /// <param name="options">SwaggerEx 选项配置</param>
    public static bool HasCustomVersion(this SwaggerExOptions options) => options.ApiVersions.Any();

    /// <summary>
    /// 初始化Swagger生成选项配置
    /// </summary>
    /// <param name="options">SwaggerEx 选项配置</param>
    /// <param name="swaggerGenOptions">Swagger 生成选项配置</param>
    public static void InitSwaggerGenOptions(this SwaggerExOptions options, SwaggerGenOptions swaggerGenOptions)
    {
        options.SwaggerGenOptions = swaggerGenOptions;
        options.AddSwaggerGenAction?.Invoke(swaggerGenOptions);
    }

    /// <summary>
    /// 初始化Swagger选项配置
    /// </summary>
    /// <param name="options">SwaggerEx 选项配置</param>
    /// <param name="swaggerOptions">Swagger 选项配置</param>
    public static void InitSwaggerOptions(this SwaggerExOptions options, SwaggerOptions swaggerOptions)
    {
        options.SwaggerOptions = swaggerOptions;
        options.UseSwaggerAction?.Invoke(swaggerOptions);
    }

    /// <summary>
    /// 初始化SwaggerUI选项配置
    /// </summary>
    /// <param name="options">SwaggerEx 选项配置</param>
    /// <param name="swaggerUiOptions">Swagger 选项配置</param>
    public static void InitSwaggerUiOptions(this SwaggerExOptions options, SwaggerUIOptions swaggerUiOptions)
    {
        options.SwaggerUiOptions = swaggerUiOptions;
        swaggerUiOptions.RoutePrefix = options.RoutePrefix;
        swaggerUiOptions.DocumentTitle = options.ProjectName;
        if(options.EnableCustomIndex)
            swaggerUiOptions.UseCustomSwaggerIndex();
        if (options.EnableAuthorization())
        {
            swaggerUiOptions.ConfigObject.AdditionalItems["customAuth"] = true;
            swaggerUiOptions.ConfigObject.AdditionalItems["loginUrl"] = $"/{options.RoutePrefix}/login.html";
            swaggerUiOptions.ConfigObject.AdditionalItems["logoutUrl"] = $"/{options.RoutePrefix}/logout";
        }

        if (options.ApiVersions == null)
        {
            options.UseSwaggerUIAction?.Invoke(swaggerUiOptions);
            return;
        }
        options.UseSwaggerUIAction?.Invoke(swaggerUiOptions);
    }
}