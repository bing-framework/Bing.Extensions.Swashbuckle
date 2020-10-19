using System.Collections.Generic;
using System.IO;
using Bing.Swashbuckle;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Samples.Api
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
                o.ProjectName = "Bing.Sample.Api �����ĵ�����";
                o.EnableCustomIndex = true;
                o.RoutePrefix = "swagger";
                o.EnableApiVersion = true;
                o.AddSwaggerGenAction = config =>
                {
                    //config.SwaggerDoc("v1", new Info() { Title = "Bing.Samples.Api", Version = "v1" });
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
                    config.IncludeXmlComments(xmlPath, true);
                    config.UseInlineDefinitionsForEnums();
                    //config.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
                    //    {{"oauth2", new string[] { }}});
                    config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Description = "Token����",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                    });

                    //config.AddSecurityDefinition("oauth2", new ApiKeyScheme()
                    //{
                    //    Description = "Token����",
                    //    In = "header",
                    //    Name = "Authorization",
                    //    Type = "apiKey",
                    //});

                    //config.OperationFilter<ApiVersionDefaultValueOperationFilter>();

                    // ��������ͷ����������ʾSwagger�Զ�������ͷ
                    config.EnableRequestHeader();

                    // ������Ӧ�ɹ���������ʾSwagger�Զ�����Ӧͷ
                    config.EnableResponseHeader();

                    // ��ʾ�ļ�����
                    config.ShowFileParameter();

                    //// ��ʾ��Ȩ��Ϣ
                    //config.ShowAuthorizeInfo();
                    // ��ʾö������
                    config.ShowEnumDescription();

                    // ����������
                    config.OrderByController();

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
                    // �����Զ��������ʶ
                    config.CustomOperationIds(apiDesc =>
                        apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                    config.MapType<IFormFile>(() => new OpenApiSchema() { Type = "file" });
                };
            });
            // ���� JSON.NET
            services.AddSwaggerGenNewtonsoftSupport();

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
                    // ʹ��Ĭ��SwaggerUI
                    config.UseDefaultSwaggerUI();
                    // ����Token�洢
                    config.UseTokenStorage("oauth2");
                };
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
