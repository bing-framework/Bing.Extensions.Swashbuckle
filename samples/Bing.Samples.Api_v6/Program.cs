using Bing.Swashbuckle;
using Bing.Swashbuckle.Filters.Schemas;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerEx(o =>
{
    o.ProjectName = "Bing.Sample.Api �����ĵ�����";
    o.EnableCustomIndex = false;
    o.RoutePrefix = "swagger";
    o.EnableApiVersion = true;
    o.AddSwaggerGenAction = config =>
    {
        //config.SwaggerDoc("v1", new Info() { Title = "Bing.Samples.Api", Version = "v1" });
        var basePath = AppContext.BaseDirectory;
        var xmlPath = Path.Combine(basePath, "Bing.Samples.Api.xml");
        config.IncludeXmlComments(xmlPath, true);
        config.UseInlineDefinitionsForEnums();
        //config.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
        //    {{"oauth2", new string[] { }}});
        //config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        //{
        //    Type = SecuritySchemeType.ApiKey,
        //    Description = "Token����",
        //    Name = "Authorization",
        //    In = ParameterLocation.Header,
        //});

        config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        {
            Type = SecuritySchemeType.ApiKey,
            Description = "Token����",
            Name = "web.auth.yzw",
            In = ParameterLocation.Cookie,
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
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = false;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVVV";
    options.SubstituteApiVersionInUrl = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

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

app.Run();
