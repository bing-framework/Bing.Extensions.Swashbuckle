using System.Collections.Generic;
using System.IO;
using Bing.Extensions.Swashbuckle.Configs;
using Bing.Extensions.Swashbuckle.Core;
using Bing.Extensions.Swashbuckle.Extensions;
using Bing.Extensions.Swashbuckle.Filters.Documents;
using Bing.Extensions.Swashbuckle.Filters.Operations;
using Bing.Extensions.Swashbuckle.Filters.Schemas;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerCustom(CurrentSwaggerOptions);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver=new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
            ProjectName = "Bing.Sample.Api 在线文档调试",
            UseCustomIndex = true,
            RoutePrefix = "swagger",
            ApiVersions = new List<string>() {"v1"},
            SwaggerAuthorizations = new List<CustomSwaggerAuthorization>()
            {
                new CustomSwaggerAuthorization("admin","123456")
            },
            AddSwaggerGenAction = config =>
            {
                //config.SwaggerDoc("v1", new Info() { Title = "Bing.Samples.Api", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
                config.IncludeXmlComments(xmlPath, true);

                config.OperationFilter<RequestHeaderOperationFilter>();
                config.OperationFilter<ResponseHeadersOperationFilter>();
                config.OperationFilter<FileParameterOperationFilter>();

                // 授权组合
                //config.OperationFilter<AddSecurityRequirementsOperationFilter>();
                config.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

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
                // 使用区域描述
                //config.TagActionsBy(apiDesc=>apiDesc.GetAreaName());
                config.DocumentFilter<AddEnumDescriptionsDocumentFilter>();
                config.DocumentFilter<FirstLowerUrlDocumentFilter>();
                // 隐藏属性
                config.SchemaFilter<IgnorePropertySchemaFilter>();
                
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
                //config.InjectJavascript("/swagger/resources/translator");
                //config.InjectJavascript("/swagger/resources/export");
                config.InjectStylesheet("/swagger/resources/swagger-common");

                config.UseDefaultSwaggerUI();
                // 其他配置
                //config.DocumentTitle = "Bing.Sample.Api 在线文档调试";
            }
        };
    }
}
