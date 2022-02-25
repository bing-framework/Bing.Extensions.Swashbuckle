using System;
using System.Collections.Generic;
using System.IO;
using Bing.Samples.Common;
using Bing.Swashbuckle;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ApiVersion = Bing.Swashbuckle.ApiVersion;

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
}
