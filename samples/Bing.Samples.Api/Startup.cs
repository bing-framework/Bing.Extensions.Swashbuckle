using System;
using System.Collections.Generic;
using System.IO;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bing.Extensions.Swashbuckle.Configs;
using Bing.Extensions.Swashbuckle.Core;
using Bing.Extensions.Swashbuckle.Extensions;
using Bing.Extensions.Swashbuckle.Filters.Operations;
using Bing.Extensions.Swashbuckle.Filters.Schemas;
using Bing.Samples.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Bing.Samples.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            //services.AddAuthorization(o => { o.AddPolicy("Admin", policy => policy.RequireClaim("Admin")); });
            var builder = new ContainerBuilder();
            builder.RegisterDynamicProxy(config =>
            {
                config.EnableParameterAspect();
                
            });
            builder.RegisterType<ScopeAspectScheduler>().As<IAspectScheduler>().InstancePerLifetimeScope();
            builder.RegisterType<ScopeAspectBuilderFactory>().As<IAspectBuilderFactory>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ScopeAspectContextFactory>().As<IAspectContextFactory>()
                .InstancePerLifetimeScope();
            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            //app.UseApiVersioning();
        }

        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        private CustomSwaggerOptions CurrentSwaggerOptions = new CustomSwaggerOptions()
        {
            ProjectName = "Bing.Sample.Api 在线文档调试",
            UseCustomIndex = true,
            RoutePrefix = "swagger",
            //ApiVersions = new List<ApiVersion>() {new ApiVersion(){Description = "通用接口",Version = "v1"},new ApiVersion(){Description = "测试接口",Version = "v2"}},
            //SwaggerAuthorizations = new List<CustomSwaggerAuthorization>()
            //{
            //    new CustomSwaggerAuthorization("admin","123456")
            //},
            AddSwaggerGenAction = config =>
            {
                //config.SwaggerDoc("v1", new Info() { Title = "Bing.Samples.Api", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
                config.IncludeXmlComments(xmlPath, true);

                //config.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
                //    {{"oauth2", new string[] { }}});

                config.AddSecurityDefinition("oauth2", new ApiKeyScheme()
                {
                    Description = "Token令牌",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey",
                });

                //config.OperationFilter<ApiVersionDefaultValueOperationFilter>();

                // 启用请求头过滤器。显示Swagger自定义请求头
                config.EnableRequestHeader();

                // 启用响应由过滤器。显示Swagger自定义响应头
                config.EnableResponseHeader();

                // 显示文件参数
                config.ShowFileParameter();

                //// 显示授权信息
                //config.ShowAuthorizeInfo();

                // 显示枚举描述
                config.ShowEnumDescription();

                // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                config.ShowUrlMode();

                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();

                // 使用API分组
                config.ApiGroup<GroupSample>();

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
            },
            UseSwaggerAction = config =>
            {
            },
            UseSwaggerUIAction = config =>
            {
                //config.IndexStream = () =>
                //    GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Bing.Samples.Api.Swagger.index.html");
                //config.SwaggerEndpoint("/swagger/v1/swagger.json", "Bing.Samples.Api v1");
                config.InjectJavascript("/swagger/resources/jquery");
                config.InjectJavascript("/swagger/resources/translator");
                //config.InjectJavascript("/swagger/resources/export");
                config.InjectStylesheet("/swagger/resources/swagger-common");

                // 使用默认SwaggerUI
                config.UseDefaultSwaggerUI();

                // 使用API分组
                config.EnableApiGroup<GroupSample>();
            }
        };
    }
}
