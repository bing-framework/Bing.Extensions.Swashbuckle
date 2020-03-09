using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 添加枚举描述 文档过滤器。支持
    /// </summary>
    public class AddEnumDescriptionsDocumentFilter : IDocumentFilter,IParameterFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // 添加枚举描述到结果模型
            foreach (var schemaDictionaryItem in swaggerDoc.Components.Schemas)
            {
                var schema = schemaDictionaryItem.Value;
                var enums = schema.Enum;
                if (enums != null && enums.Count > 0)
                {
                    schema.Description += GetEnumDescription(enums);
                    schema.Extensions["x-enumNames"] = new OpenApiString(GetStringMapping(enums));
                }
            }

            if (swaggerDoc.Paths.Count <= 0)
                return;

            // 添加枚举描述到输入参数
            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                DescribeEnumParameters(pathItem.Parameters);
                // head, patch, options, delete left out
                //var possibleParameterisedOperations = new List<OpenApiOperation> { pathItem.Get, pathItem.Post, pathItem.Put };
                //possibleParameterisedOperations.FindAll(x => x != null)
                //    .ForEach(x => DescribeEnumParameters(x.Parameters));
            }
        }

        /// <summary>
        /// 获取字符串映射
        /// </summary>
        /// <param name="enums">枚举值</param>
        private string GetStringMapping(IList<IOpenApiAny> enums)
        {
            var sb = new StringBuilder();
            Type type = null;
            foreach (var enumOption in enums)
            {
                if (type == null)
                    type = enumOption.GetType();
                sb.AppendLine(Enum.GetName(type, enumOption));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 描述枚举参数
        /// </summary>
        /// <param name="parameters">参数</param>
        private static void DescribeEnumParameters(IList<OpenApiParameter> parameters)
        {
            if (parameters == null)
                return;
            foreach (var param in parameters)
            {
                //if (param is NonBodyParameter nonBodyParameter)
                //{
                //    if (nonBodyParameter.Enum != null && nonBodyParameter.Enum.Count > 0)
                //    {
                //        param.Description += GetEnumDescription(nonBodyParameter.Enum, false);
                //    }
                //}
            }
        }

        /// <summary>
        /// 获取枚举描述字符串
        /// </summary>
        /// <param name="enums">枚举值</param>
        /// <param name="isWrap">是否换行</param>
        private static string GetEnumDescription(IEnumerable<object> enums, bool isWrap = true)
        {
            var enumDescriptions = new List<string>();
            Type type = null;
            foreach (var enumOption in enums)
            {
                if (type == null)
                    type = enumOption.GetType();
                enumDescriptions.Add(
                    $"{Convert.ChangeType(enumOption, type.GetEnumUnderlyingType())} = {Internals.Enum.GetDescription(type, enumOption)}");
            }

            var separator = isWrap ? Environment.NewLine : "; ";
            return $"{Environment.NewLine}{string.Join(separator, enumDescriptions)}";
        }


        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = context.ParameterInfo?.ParameterType;
            if (type == null)
                return;
            if (type.IsEnum)
            {
                var names = Enum.GetNames(type);
                var values = Enum.GetValues(type);
                var desc = "";
                foreach (var value in values)
                {
                    var intValue = Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
                    desc += $"{intValue}={value},";
                }

                desc = desc.TrimEnd(',');
                if (!parameter.Extensions.ContainsKey("x-enumNames"))
                {
                    var api = new OpenApiArray();
                    foreach (var name in names) 
                        api.Add(new OpenApiString(name));
                    parameter.Extensions.Add("x-enumNames", api);
                }

                parameter.Description = $"{parameter.Description}\r\n{desc}";
            }
        }
    }
}
