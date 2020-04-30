using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Documents
{
    /// <summary>
    /// 枚举描述 文档过滤器。支持Url查询参数
    /// </summary>
    internal class EnumDescriptionsDocumentFilter : EnumHandleBase, IParameterFilter
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
            {
                parameter.Description = $"{parameter.Description}\r\n{FormatDescription(type)}";
            }
        }
    }
}
