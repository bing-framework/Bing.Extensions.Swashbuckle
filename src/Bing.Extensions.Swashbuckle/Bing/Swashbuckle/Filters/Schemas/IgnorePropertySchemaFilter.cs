using System.Linq;
using System.Reflection;
using Bing.Swashbuckle.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Schemas;

/// <summary>
/// 忽略属性架构过滤器
/// </summary>
public class IgnorePropertySchemaFilter : ISchemaFilter
{
    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null)
            return;
        var ignoreProperties = context.Type.GetProperties()
            .Where(t => t.GetCustomAttribute<SwaggerIgnorePropertyAttribute>() != null);
        foreach (var ignoreProperty in ignoreProperties)
        {
            var propertyToRemove =
                schema.Properties.Keys.SingleOrDefault(x => x.ToLower() == ignoreProperty.Name.ToLower());
            if (propertyToRemove != null) 
                schema.Properties.Remove(propertyToRemove);
        }
    }
}