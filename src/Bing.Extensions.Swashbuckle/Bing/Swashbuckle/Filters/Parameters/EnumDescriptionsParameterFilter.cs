using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Parameters
{
    /// <summary>
    /// 枚举描述 参数过滤器。支持Url查询参数
    /// </summary>
    internal class EnumDescriptionsParameterFilter : EnumHandleBase, IParameterFilter
    {
        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = context.ParameterInfo?.ParameterType;
            if (type == null)
                return;
            if (type.IsEnum) 
                parameter.Description = $"{parameter.Description}{Environment.NewLine}{FormatDescription(type)}";
        }
    }
}
