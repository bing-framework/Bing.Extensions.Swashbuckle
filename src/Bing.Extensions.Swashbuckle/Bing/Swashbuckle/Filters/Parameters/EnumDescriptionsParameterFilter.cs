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
            if (context.PropertyInfo != null)
            {
                var type = context.PropertyInfo?.PropertyType;
                SetDescription(parameter,type);
            }
            else
            {
                var type = context.ParameterInfo?.ParameterType;
                SetDescription(parameter, type);
            }
        }

        /// <summary>
        /// 设置API参数描述信息
        /// </summary>
        /// <param name="parameter">API参数</param>
        /// <param name="type">类型</param>
        private void SetDescription(OpenApiParameter parameter, Type type)
        {
            if (type == null)
                return;
            if (type.IsEnum)
                parameter.Description = FormatDescription(parameter.Description, type);
        }
    }
}
