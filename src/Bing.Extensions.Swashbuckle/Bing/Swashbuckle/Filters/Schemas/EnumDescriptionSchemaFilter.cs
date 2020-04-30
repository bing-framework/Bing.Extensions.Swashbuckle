using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Schemas
{
    /// <summary>
    /// 枚举描述 过滤器。支持Body参数内容
    /// </summary>
    internal class EnumDescriptionSchemaFilter : EnumHandleBase, ISchemaFilter
    {
        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Description = $"{schema.Description}\r\n{FormatDescription(context.Type)}";
            }
        }
    }
}
