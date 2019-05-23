using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Schemas
{
    /// <summary>
    /// 忽略属性架构过滤
    /// </summary>
    public class IgnorePropertySchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }
        }
    }
}
