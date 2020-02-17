using Bing.Swashbuckle.Internals;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 首字母大写Url 文档过滤器
    /// </summary>
    public class FirstUpperUrlDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) => swaggerDoc.Paths =
            UrlConvert.ConvertKeys(swaggerDoc.Paths, UrlConvert.FirstUpper);
    }
}
