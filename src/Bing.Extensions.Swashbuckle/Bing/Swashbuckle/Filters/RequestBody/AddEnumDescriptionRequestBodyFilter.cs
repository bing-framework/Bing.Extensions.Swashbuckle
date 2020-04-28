using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.RequestBody
{
    public class AddEnumDescriptionRequestBodyFilter:IRequestBodyFilter
    {
        public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
        {
            var bodyParameterDescription = context.BodyParameterDescription;
            if (bodyParameterDescription == null)
                return;
            var propertyInfo = bodyParameterDescription.PropertyInfo();
            if (propertyInfo!=null)
            {
                var type = bodyParameterDescription.Type;
                return;
            }

            var parameterInfo = bodyParameterDescription.ParameterInfo();
            if (parameterInfo != null&&parameterInfo.ParameterType.IsEnum)
            {
                var result = parameterInfo.ParameterType;
            }
        }

        private static string GetEnumDescription(IEnumerable<object> enums, bool isWrap = true)
        {
            var enumDescriptions = new List<string>();
            Type type = null;
            foreach (var enumOption in enums)
            {
                if (type == null)
                    type = enumOption.GetType();
                enumDescriptions.Add($"{Convert.ChangeType(enumOption, type.GetEnumUnderlyingType())} = {Internals.Enum.GetDescription(type, enumOption)}");
            }
            var separator = isWrap ? Environment.NewLine : "; ";
            return $"{Environment.NewLine}{string.Join(separator, enumDescriptions)}";
        }

        /// <summary>
        /// 应用于Swagger请求内容特性
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="description">描述</param>
        private void ApplySwaggerRequestBodyAttribute(OpenApiRequestBody parameter, string description)
        {
            if (!string.IsNullOrEmpty(description))
                parameter.Description = description;
        }
    }
}
