using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bing.Swashbuckle;
using Bing.Samples.Common;

namespace Bing.Samples.Api
{
    public class Startup
    {
        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services">���񼯺�</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            #region Swagger��ز���

            services.AddSwaggerEx(o =>
            {
                StartupConfig.ConfigureServicesByApi(o);
            });

            // ���� JSON.NET
            services.AddSwaggerGenNewtonsoftSupport();

            #endregion

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger��ز���

            app.UseSwaggerEx(o =>
            {
                StartupConfig.ConfigureByApi(o);
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
