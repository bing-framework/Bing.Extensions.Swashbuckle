using System;
using System.Collections.Generic;
using System.IO;
using Bing.Extensions.Swashbuckle.Configs;
using Bing.Extensions.Swashbuckle.Extensions;
using Bing.Extensions.Swashbuckle.Filters.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Bing.Samples.ApiGroup
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
            services.AddSwaggerCustom(CurrentSwaggerOptions);
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
            app.UseSwaggerCustom(CurrentSwaggerOptions);
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        private CustomSwaggerOptions CurrentSwaggerOptions = new CustomSwaggerOptions()
        {
            ProjectName = "Bing.Sample.ApiGroup 在线文档调试",
            UseCustomIndex = true,
            RoutePrefix = "swagger",
            ApiGroupType = typeof(ApiGroupSample),
            AddSwaggerGenAction = config =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.ApiGroup.xml");
                config.IncludeXmlComments(xmlPath, true);

                config.AddSecurityDefinition("oauth2", new ApiKeyScheme()
                {
                    Description = "Token令牌",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey",
                });

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 添加通用参数
                config.AddCommonParameter(new List<IParameter>()
                {
                    new NonBodyParameter()
                    {
                        Name = "Test",
                        In = "header",
                        Default = "",
                        Type = "string"
                    }
                });

                // 启用默认值
                config.EnableDefaultValue();
            },
            UseSwaggerAction = config =>
            {
            },
            UseSwaggerUIAction = config =>
            {
                config.InjectJavascript("/swagger/resources/jquery");
                config.InjectJavascript("/swagger/resources/translator");
                config.InjectStylesheet("/swagger/resources/swagger-common");

                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();
            }
        };
    }
}
