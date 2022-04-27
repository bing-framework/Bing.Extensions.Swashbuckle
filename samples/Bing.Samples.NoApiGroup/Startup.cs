using System;
using System.Linq;
using Bing.Samples.Common;
using Bing.Swashbuckle;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;

namespace Bing.Samples.NoApiGroup
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化一个<see cref="Startup"/>类型的实例
        /// </summary>
        /// <param name="configuration">配置</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services">服务集合</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 配置跨域
            services.AddCors();
            #region Swagger相关操作

            services.AddSwaggerEx(o =>
            {
                o.GlobalResponseWrapperAction = (action, type, isVoid) =>
                {
                    var apiResultType = typeof(ApiResult);
                    var genericApiResultType = typeof(ApiResult<>);

                    var okReturnType = isVoid ? apiResultType : genericApiResultType.MakeGenericType(type);
                    var apiErrorReturnType = apiResultType;
                    var errorReturnType = apiResultType;

                    var has200 = false;
                    var has400 = false;
                    var has500 = false;
                    foreach (var p in action.Filters.Where(x => x is ProducesResponseTypeAttribute))
                    {
                        if (p is ProducesResponseTypeAttribute producesResponseTypeAttribute)
                        {
                            var statusCode = producesResponseTypeAttribute.StatusCode;
                            if (!has200 && 200 <= statusCode && statusCode < 300)
                            {
                                has200 = true;
                                if (producesResponseTypeAttribute.Type == null)
                                {
                                    producesResponseTypeAttribute.Type = okReturnType;
                                }
                                continue;
                            }

                            if (!has400 && 400 <= statusCode && statusCode < 500)
                            {
                                has400 = true;
                                if (producesResponseTypeAttribute.Type == null)
                                {
                                    producesResponseTypeAttribute.Type = apiErrorReturnType;
                                }
                                continue;
                            }

                            if (!has500 && 500 <= statusCode && statusCode < 600)
                            {
                                has500 = true;
                                if (producesResponseTypeAttribute.Type == null)
                                {
                                    producesResponseTypeAttribute.Type = errorReturnType;
                                }
                                continue;
                            }
                        }
                    }

                    if (!has200)
                    {
                        action.Filters.Add(new ProducesResponseTypeAttribute(okReturnType, StatusCodes.Status200OK));
                    }
                    if (!has400)
                    {
                        action.Filters.Add(new ProducesResponseTypeAttribute(apiErrorReturnType, StatusCodes.Status400BadRequest));
                    }
                    if (!has500)
                    {
                        action.Filters.Add(new ProducesResponseTypeAttribute(errorReturnType, StatusCodes.Status500InternalServerError));
                    }
                };
                StartupConfig.ConfigureServicesByNoGroup(o);

            });

            // 启用 JSON.NET
            services.AddSwaggerGenNewtonsoftSupport();

            #endregion
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(o => { o.SerializerSettings.ContractResolver = new DefaultContractResolver(); })
                .AddControllersAsServices();
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 配置请求管道
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            #region Swagger相关操作

            app.UseSwaggerEx(o =>
            {
                StartupConfig.ConfigureByNoGroup(o);
            });

            #endregion
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    /// <summary>
    /// API结果
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
    }

    /// <summary>
    /// API结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
