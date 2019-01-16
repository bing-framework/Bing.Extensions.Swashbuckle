using System.IO;
using System.Reflection;
using Bing.Extensions.Swashbuckle.Filters.Operations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
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
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info() {Title = "Bing.Samples.Api", Version = "v1"});
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
                config.IncludeXmlComments(xmlPath);

                config.OperationFilter<AddRequestHeaderOperationFilter>();
                config.OperationFilter<AddResponseHeadersOperationFilter>();
                config.OperationFilter<AddFileParameterOperationFilter>();

                // 授权组合
                config.OperationFilter<AddSecurityRequirementsOperationFilter>();
                config.OperationFilter<AddAppendAuthorizeToSummaryOperationFilter>();
                config.AddSecurityDefinition("oauth2", new ApiKeyScheme()
                {
                    Description = "Token令牌",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey",
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(config => { });
            app.UseSwaggerUI(config =>
            {
                config.IndexStream = () =>
                    GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Bing.Samples.Api.Swagger.index.html");
                config.ShowExtensions();
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Bing.Samples.Api v1");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
