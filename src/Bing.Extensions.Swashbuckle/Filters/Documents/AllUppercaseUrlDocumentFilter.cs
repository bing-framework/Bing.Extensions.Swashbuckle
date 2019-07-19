using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 全大写Url 文档过滤器
    /// </summary>
    public class AllUppercaseUrlDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(entry => LowercaseEverythingButParameters(entry.Key), entry => entry.Value);
        }

        /// <summary>
        /// 大写
        /// </summary>
        private static string LowercaseEverythingButParameters(string key) => string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToUpper()));
    }
}
