using System;
using Bing.Swashbuckle.Core.Authorization;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Builder;

// ReSharper disable once CheckNamespace
namespace Bing.Swashbuckle;

/// <summary>
/// Swagger构建器扩展
/// </summary>
public static class SwaggerBuilderExtensions
{
    /// <summary>
    /// 启用SwaggerEx
    /// </summary>
    /// <param name="app">应用构建器</param>
    /// <param name="setupAction">配置操作</param>
    public static IApplicationBuilder UseSwaggerEx(this IApplicationBuilder app, Action<SwaggerExOptions> setupAction = null)
    {
        BuildContext.Instance.ServiceProvider = app.ApplicationServices;
        // 初始化配置信息
        setupAction?.Invoke(BuildContext.Instance.ExOptions);
        if (BuildContext.Instance.ExOptions.EnableAuthorization())
            app.UseMiddleware<SwaggerAuthorizeMiddleware>();
        app.UseSwagger(o =>
            {
                BuildContext.Instance.ExOptions.InitSwaggerOptions(o);
            })
            .UseSwaggerUI(o =>
            {
                BuildContext.Instance.ExOptions.InitSwaggerUiOptions(o);
            });
        return app;
    }
}