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
    o.ProjectName = "Bing.Sample.Api 在线文档调试";
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
        //    Description = "Token令牌",
        //    Name = "Authorization",
        //    In = ParameterLocation.Header,
        //});

        config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
        {
            Type = SecuritySchemeType.ApiKey,
            Description = "Token令牌",
            Name = "web.auth.yzw",
            In = ParameterLocation.Cookie,
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
        // 使用默认SwaggerUI
        config.UseDefaultSwaggerUI();
        // 启用Token存储
        config.UseTokenStorage("oauth2");
    };
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
