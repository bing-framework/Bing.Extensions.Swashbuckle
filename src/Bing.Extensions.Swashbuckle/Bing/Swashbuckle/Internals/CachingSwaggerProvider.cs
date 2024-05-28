using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// Swagger缓存提供程序
/// </summary>
internal class CachingSwaggerProvider : ISwaggerProvider
{
    /// <summary>
    /// 缓存字典
    /// </summary>
    private static readonly ConcurrentDictionary<string, OpenApiDocument> Cache =
        new ConcurrentDictionary<string, OpenApiDocument>();

    /// <summary>
    /// Swagger文档生成器
    /// </summary>
    private readonly SwaggerGenerator _swaggerGenerator;

    /// <summary>
    /// 初始化一个<see cref="CachingSwaggerProvider"/>类型的实例
    /// </summary>
    /// <param name="optionsAccessor">Swagger生成器选项配置</param>
    /// <param name="apiDescriptionGroupCollectionProvider">Api描述提供程序</param>
    /// <param name="schemaGenerator">架构生成器</param>
    public CachingSwaggerProvider(IOptions<SwaggerGeneratorOptions> optionsAccessor
        , IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider
        , ISchemaGenerator schemaGenerator) =>
        _swaggerGenerator = new SwaggerGenerator(optionsAccessor.Value, apiDescriptionGroupCollectionProvider, schemaGenerator);

    /// <summary>
    /// 获取Swagger文档
    /// </summary>
    /// <param name="documentName">文档名称</param>
    /// <param name="host">主机</param>
    /// <param name="basePath">基础路径</param>
    public OpenApiDocument GetSwagger(string documentName, string host, string basePath) =>
        Cache.GetOrAdd($"{documentName}_{host}_{basePath}",
            (_) => _swaggerGenerator.GetSwagger(documentName, host, basePath));
}