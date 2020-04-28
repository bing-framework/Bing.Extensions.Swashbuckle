using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Schemas
{
    public class EnumDescriptionSchemaFilter:ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Description = $"{schema.Description}\r\n{GetDescription(context.Type)}";
            }
        }

        private string GetDescription(Type type)
        {
            var sb = new StringBuilder();
            foreach (var field in type.GetFields())
            {
                if(!field.FieldType.IsEnum)
                    continue;
                sb.Append(
                    $"{GetValue(type, field.Name)} = {Internals.Enum.GetDescription(type, field.Name)}{Environment.NewLine}");
            }

            return sb.ToString();
        }

        private int GetValue(Type type, object member)
        {
            var value = member.ToString();
            return (int) Enum.Parse(type, member.ToString(), true);
        }
    }
}
