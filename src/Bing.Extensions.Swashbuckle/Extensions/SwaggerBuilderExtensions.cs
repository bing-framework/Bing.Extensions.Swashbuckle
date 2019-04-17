using System;
using System.Linq;
using Bing.Extensions.Swashbuckle.Configs;
using Bing.Extensions.Swashbuckle.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// Swagger构建器扩展
    /// </summary>
    public static class SwaggerBuilderExtensions
    {
        /// <summary>
        /// 使用自定义Swagger
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="options">自定义Swagger选项</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerCustom(this IApplicationBuilder app, CustomSwaggerOptions options)
        {
            app.UseSwaggerCustomAuthorization(options)
                .UseSwagger(o => { options.UseSwaggerAction?.Invoke(o); })
                .UseSwaggerUI(o =>
                {
                    o.RoutePrefix = options.RoutePrefix;
                    o.DocumentTitle = options.ProjectName;
                    if (options.UseCustomIndex)
                    {
                        o.UseCustomSwaggerIndex();
                    }

                    if (options.SwaggerAuthorizations.Count > 0)
                    {
                        o.ConfigObject.AdditionalItems["customAuth"] = true;
                        o.ConfigObject.AdditionalItems["loginUrl"] = $"/{options.RoutePrefix}/login.html";
                        o.ConfigObject.AdditionalItems["logoutUrl"] = $"/{options.RoutePrefix}/logout";
                    }

                    if (options.ApiVersions == null)
                    {
                        return;
                    }

                    foreach (var item in options.ApiVersions)
                    {
                        o.SwaggerEndpoint($"/swagger/{item}/swagger.json", $"{item}");
                    }

                    options.UseSwaggerUIAction?.Invoke(o);
                });
            return app;
        }

        /// <summary>
        /// Swagger授权Cookie名称
        /// </summary>
        private const string SWAGGER_AUTH_COOKIE = nameof(SWAGGER_AUTH_COOKIE);

        /// <summary>
        /// 添加Swagger自定义授权访问
        /// </summary>
        private static IApplicationBuilder UseSwaggerCustomAuthorization(this IApplicationBuilder app,
            CustomSwaggerOptions options)
        {
            if (options?.SwaggerAuthorizations.Count == 0)
            {
                return app;
            }

            app.Use(async (context, next) =>
            {
                var method = context.Request.Method.ToLower();
                var path = context.Request.Path.Value;
                if (path.IndexOf($"/{options.RoutePrefix}", StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    await next();
                    return;
                }

                if (path == $"/{options.RoutePrefix}/login.html")
                {
                    // 登录
                    if (method == "get")
                    {
                        context.Response.WriteSwaggerPage("login");
                        return;
                    }

                    if (method == "post")
                    {
                        var user = new CustomSwaggerAuthorization(context.Request.Form["userName"],
                            context.Request.Form["password"]);
                        if (!options.SwaggerAuthorizations.Any(x =>
                            x.UserName == user.UserName && x.Password == user.Password))
                        {
                            await context.Response.WriteAsync("Login error!");
                            return;
                        }

                        context.Response.Cookies.Append(SWAGGER_AUTH_COOKIE, user.Token,
                            new CookieOptions() {Expires = DateTimeOffset.Now.AddMonths(1)});
                        context.Response.Redirect($"/{options.RoutePrefix}");
                        return;
                    }
                }

                if (path == $"/{options.RoutePrefix}/logout")
                {
                    // 退出
                    context.Response.Cookies.Delete(SWAGGER_AUTH_COOKIE);
                    context.Response.Redirect($"/{options.RoutePrefix}/login.html");
                    return;
                }

                if (!options.SwaggerAuthorizations.Any(x =>
                    !string.IsNullOrWhiteSpace(x.Token) && x.Token == context.Request.Cookies[SWAGGER_AUTH_COOKIE]))
                {
                    context.Response.Redirect($"/{options.RoutePrefix}/login.html");
                    return;
                }

                await next();
            });
            return app;
        }
    }
}
