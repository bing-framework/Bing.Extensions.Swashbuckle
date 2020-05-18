using System.Collections.Generic;
using System.IO;
using Bing.Swashbuckle;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Bing.Samples.MultipleVersion
{
    /// <summary>
    /// ��������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ��ʼ��һ��<see cref="Startup"/>���͵�ʵ��
        /// </summary>
        /// <param name="configuration">����</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ����
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services">���񼯺�</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerEx(o =>
            {
                o.ProjectName = "Bing.Samples.MultipleVersion �����ĵ�����";
                o.EnableCustomIndex = true;
                o.RoutePrefix = "swagger";
                o.EnableApiVersion = true;
                o.AddSwaggerGenAction = config =>
                {
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath, "Bing.Samples.MultipleVersion.xml");
                    config.IncludeXmlComments(xmlPath, true);

                    config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Description = "Token����",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                    });

                    // ��������ͷ����������ʾSwagger�Զ�������ͷ
                    config.EnableRequestHeader();

                    // ������Ӧ�ɹ���������ʾSwagger�Զ�����Ӧͷ
                    config.EnableResponseHeader();

                    // ��ʾ�ļ�����
                    config.ShowFileParameter();

                    // ��ʾö������
                    config.ShowEnumDescription();

                    // ��ʾUrlģʽ������ĸСд������ĸ��д��ȫСд��ȫ��д��Ĭ��
                    config.ShowUrlMode();

                    // ��������
                    config.SchemaFilter<IgnorePropertySchemaFilter>();

                    // ���ͨ�ò���
                    config.AddCommonParameter(new List<OpenApiParameter>()
                    {
                        new OpenApiParameter()
                        {
                            Name = "Test",
                            In = ParameterLocation.Header,
                            Schema = new OpenApiSchema() {Type = "string", Default = new OpenApiString("")}
                        }
                    });

                    // ����Ĭ��ֵ
                    config.EnableDefaultValue();
                };
            });

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

        /// <summary>
        /// ��������ܵ�
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerEx(o =>
            {
                o.UseSwaggerUIAction = config =>
                {
                    config.InjectJavascript("resources/jquery");
                    config.InjectJavascript("resources/translator");
                    config.InjectStylesheet("resources/swagger-common");

                    // ʹ��Ĭ��SwaggerUI
                    config.UseDefaultSwaggerUI();
                };
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
