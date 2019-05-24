using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 添加枚举描述 文档过滤器。支持
    /// </summary>
    public class AddEnumDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            // add enum descriptions to result models
            foreach (var schemaDictionaryItem in swaggerDoc.Definitions)
            {
                var schema = schemaDictionaryItem.Value;
                foreach (var propertyDictionaryItem in schema.Properties)
                {
                    var property = propertyDictionaryItem.Value;
                    var propertyEnums = property.Enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        property.Description += GetEnumDescription(propertyEnums);
                        property.Extensions.Add("x-enumNames", GetStringMapping(propertyEnums));
                    }
                }
            }

            if (swaggerDoc.Paths.Count <= 0) return;

            // add enum descriptions to input parameters
            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                DescribeEnumParameters(pathItem.Parameters);

                // head, patch, options, delete left out
                var possibleParameterisedOperations = new List<Operation> { pathItem.Get, pathItem.Post, pathItem.Put };
                possibleParameterisedOperations.FindAll(x => x != null)
                    .ForEach(x => DescribeEnumParameters(x.Parameters));
            }
        }

        /// <summary>
        /// 获取字符串映射
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        private string[] GetStringMapping(IList<object> enums)
        {
            var enumDescriptions = new List<string>();
            Type type = null;
            foreach (var enumOption in enums)
            {
                if (type == null) type = enumOption.GetType();
                enumDescriptions.Add(Enum.GetName(type, enumOption));
            }

            return enumDescriptions.ToArray();
        }

        private static void DescribeEnumParameters(IList<IParameter> parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
            {
                if (param is NonBodyParameter nonBodyParameter)
                {
                    if (nonBodyParameter.Enum != null && nonBodyParameter.Enum.Count > 0)
                    {
                        param.Description += GetEnumDescription(nonBodyParameter.Enum);
                    }
                }
            }
        }

        /// <summary>
        /// 获取枚举描述字符串
        /// </summary>
        /// <param name="enums">枚举值</param>
        /// <returns></returns>
        private static string GetEnumDescription(IEnumerable<object> enums)
        {
            var enumDescriptions = new List<string>();
            Type type = null;
            foreach (var enumOption in enums)
            {
                if (type == null) type = enumOption.GetType();
                enumDescriptions.Add(
                    $"{Convert.ChangeType(enumOption, type.GetEnumUnderlyingType())} = {Internal.Enum.GetDescription(type, enumOption)}");
            }

            return $"{Environment.NewLine}{string.Join(Environment.NewLine, enumDescriptions)}";
        }
    }
}
