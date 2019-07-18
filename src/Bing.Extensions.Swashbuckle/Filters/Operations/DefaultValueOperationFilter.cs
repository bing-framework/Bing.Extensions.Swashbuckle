using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Extensions.Swashbuckle.Filters.Operations
{
    /// <summary>
    /// 添加 默认值 操作过滤器
    /// </summary>
    public class DefaultValueOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation?.Parameters == null || !operation.Parameters.Any())
                return;
            var parameterValuePairs = context.ApiDescription.ParameterDescriptions
                .Where(parameter => GetDefaultValueAttribute(parameter) != null || GetParameterInfo(parameter).HasDefaultValue)
                .ToDictionary(parameter => parameter.Name, GetDefaultValue);
            foreach (var parameter in operation.Parameters)
            {
                
                if (parameterValuePairs.TryGetValue(parameter.Name, out var defaultValue))
                {
                    if (parameter is NonBodyParameter nonBodyParameter)
                    {
                        nonBodyParameter.Default = defaultValue;
                        nonBodyParameter.Required = false;
                    }
                    //parameter.Extensions["default"] = defaultValue;
                    //parameter.Required = false;
                }
            }
        }

        /// <summary>
        /// 获取默认值特性
        /// </summary>
        /// <param name="parameter">API参数</param>
        private DefaultValueAttribute GetDefaultValueAttribute(ApiParameterDescription parameter)
        {
            if (!(parameter.ModelMetadata is DefaultModelMetadata metadata) || metadata.Attributes.PropertyAttributes == null)
                return null;
            return metadata.Attributes.PropertyAttributes.OfType<DefaultValueAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// 获取参数信息
        /// </summary>
        /// <param name="parameter">API参数</param>
        private ParameterInfo GetParameterInfo(ApiParameterDescription parameter) => ((ControllerParameterDescriptor)parameter.ParameterDescriptor).ParameterInfo;

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="parameter">API参数</param>
        private object GetDefaultValue(ApiParameterDescription parameter)
        {
            var parameterInfo = GetParameterInfo(parameter);
            if (parameterInfo.HasDefaultValue)
            {
                if (parameter.Type.IsEnum)
                    return null;
                return parameterInfo.DefaultValue;
            }
            var defaultValueAttribute = GetDefaultValueAttribute(parameter);
            return defaultValueAttribute.Value;
        }

        /// <summary>
        /// 转换为首字母驼峰式字符串
        /// </summary>
        /// <param name="name">名称</param>
        private string ToCamelCase(string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}
