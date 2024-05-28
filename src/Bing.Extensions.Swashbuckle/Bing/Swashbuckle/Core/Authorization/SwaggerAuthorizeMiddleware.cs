using System;
using System.Linq;
using System.Threading.Tasks;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Http;

namespace Bing.Swashbuckle.Core.Authorization;

/// <summary>
/// Swagger 授权中间件
/// </summary>
internal class SwaggerAuthorizeMiddleware
{
    /// <summary>
    /// 方法
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// SwaggerEx 选项配置
    /// </summary>
    private readonly SwaggerExOptions _options;

    /// <summary>
    /// Swagger授权Cookie名称
    /// </summary>
    private const string SWAGGER_AUTH_COOKIE = nameof(SWAGGER_AUTH_COOKIE);

    /// <summary>
    /// 初始化一个<see cref="SwaggerAuthorizeMiddleware"/>类型的实例
    /// </summary>
    /// <param name="next">方法</param>
    public SwaggerAuthorizeMiddleware(RequestDelegate next)
    {
        _next = next;
        _options = BuildContext.Instance.ExOptions;
    }

    /// <summary>
    /// 执行方法
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        if (!_options.EnableAuthorization())
            await _next(context);
        var method = context.Request.Method.ToLower();
        var path = context.Request.Path.Value;
        if (path.IndexOf($"/{_options.RoutePrefix}", StringComparison.InvariantCultureIgnoreCase) != 0)
        {
            await _next(context);
            return;
        }

        if (path == $"/{_options.RoutePrefix}/login.html")
        {
            if (method == "get")
            {
                context.Response.WriteSwaggerPage("login");
                return;
            }

            if (method == "post")
            {
                var user = new SwaggerAuthorizationUser(context.Request.Form["userName"],
                    context.Request.Form["password"]);
                if (!_options.SwaggerAuthorizations.Any(x =>
                        x.UserName == user.UserName && x.Password == user.Password))
                {
                    await context.Response.WriteAsync("Login error!");
                    return;
                }

                context.Response.Cookies.Append(SWAGGER_AUTH_COOKIE, user.Token,
                    new CookieOptions() { Expires = DateTimeOffset.Now.AddMonths(1) });
                context.Response.Redirect($"/{_options.RoutePrefix}");
            }
        }

        if (path == $"/{_options.RoutePrefix}/logout")
        {
            context.Response.Cookies.Delete(SWAGGER_AUTH_COOKIE);
            context.Response.Redirect($"/{_options.RoutePrefix}/login.html");
            return;
        }

        if (!_options.SwaggerAuthorizations.Any(x =>
                !string.IsNullOrWhiteSpace(x.Token) && x.Token == context.Request.Cookies[SWAGGER_AUTH_COOKIE]))
        {
            context.Response.Redirect($"/{_options.RoutePrefix}/login.html");
            return;
        }

        await _next(context);
    }
}