using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 首字母大写Url 文档过滤器
    /// </summary>
    public class FirstUppercaseUrlDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(entry => FirstLowerEverythingButParameters(entry.Key),
                entry => entry.Value);
        }

        /// <summary>
        /// 除参数为，任何值首字母小写
        /// </summary>
        private static string FirstLowerEverythingButParameters(string key)
        {
            return string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : FirstUpper(x)));
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        private static string FirstUpper(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }
    }
}
