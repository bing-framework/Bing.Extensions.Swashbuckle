using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Internals
{
    /// <summary>
    /// Swagger缓存文档提供程序
    /// </summary>
    internal class CacheSwaggerProvider : SwaggerGenerator, ISwaggerProvider
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private static readonly ConcurrentDictionary<string, OpenApiDocument> Cache =
            new ConcurrentDictionary<string, OpenApiDocument>();

        /// <summary>
        /// 初始化一个<see cref="CacheSwaggerProvider"/>类型的实例
        /// </summary>
        /// <param name="apiDescriptionsProvider">Api描述提供程序</param>
        /// <param name="schemaGenerator">架构生成器</param>
        /// <param name="options">Swagger生成选项配置</param>
        public CacheSwaggerProvider(IApiDescriptionGroupCollectionProvider apiDescriptionsProvider, ISchemaGenerator schemaGenerator, SwaggerGeneratorOptions options) : base(apiDescriptionsProvider, schemaGenerator, options)
        {
        }

        /// <summary>
        /// 获取Swagger文档
        /// </summary>
        /// <param name="documentName">文档名称</param>
        /// <param name="host">主机</param>
        /// <param name="basePath">基础路径</param>
        public new OpenApiDocument GetSwagger(string documentName, string host, string basePath)
        {
            var cacheKey = $"{documentName}_{host}_{basePath}";
            return Cache.GetOrAdd(cacheKey, key => base.GetSwagger(documentName, host, basePath));
        }
    }
}
