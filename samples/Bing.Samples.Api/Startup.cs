using System;
using System.Collections.Generic;
using System.IO;
using Bing.Swashbuckle;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Samples.Api
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
            services.AddSwaggerEx(o =>
            {
                o.ProjectName = "Bing.Sample.Api 在线文档调试";
                o.EnableCustomIndex = true;
                o.RoutePrefix = "swagger";
                o.EnableApiVersion = true;
                o.EnableCached = true;
                o.AddSwaggerGenAction = config =>
                {
                    //config.SwaggerDoc("v1", new Info() { Title = "Bing.Samples.Api", Version = "v1" });
                    var basePath = AppContext.BaseDirectory;
                    var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
                    config.IncludeXmlComments(xmlPath, true);
                    config.UseInlineDefinitionsForEnums();
                    //config.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
                    //    {{"oauth2", new string[] { }}});
                    config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Description = "Token令牌",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                    });

                    //config.AddSecurityDefinition("oauth2", new ApiKeyScheme()
                    //{
                    //    Description = "Token令牌",
                    //    In = "header",
                    //    Name = "Authorization",
                    //    Type = "apiKey",
                    //});

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

                    // 控制器排序
                    config.OrderByController();

                    // 显示Url模式：首字母小写、首字母大写、全小写、全大写、默认
                    config.ShowUrlMode();

                    // 隐藏属性
                    config.SchemaFilter<IgnorePropertySchemaFilter>();

                    // 添加通用参数
                    config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                    // 启用默认值
                    config.EnableDefaultValue();
                    // 配置自定义操作标识
                    config.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                    // 显示方法计数
                    config.ShowActionCount();
                    config.MapType<IFormFile>(() => new OpenApiSchema() {Type = "file"});
                };
            });
            services.AddSwaggerGenNewtonsoftSupport();
            //services.AddCachedSwaggerGen(CurrentSwaggerOptions);
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddControllersAsServices();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = false;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            
            //services.AddAuthorization(o => { o.AddPolicy("Admin", policy => policy.RequireClaim("Admin")); });
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
            app.UseSwaggerEx(o =>
            {
                o.UseSwaggerAction = config =>
                {
                    config.SerializeAsV2 = true;
                };
                o.UseSwaggerUIAction = config =>
                {
                    //config.IndexStream = () =>
                    //    GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Bing.Samples.Api.Swagger.index.html");
                    //config.SwaggerEndpoint("/swagger/v1/swagger.json", "Bing.Samples.Api v1");
                    config.UseInternalResources();
                    config.UseTranslate();
                    // 使用默认SwaggerUI
                    config.UseDefaultSwaggerUI();
                    // 启用Token存储
                    config.UseTokenStorage("oauth2");
                };
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
