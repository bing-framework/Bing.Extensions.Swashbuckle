using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Operations;

/// <summary>
/// 添加 Api接口版本默认值 操作过滤器
/// </summary>
public class ApiVersionDefaultValueOperationFilter : IOperationFilter
{
    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1147
        if (operation.Parameters == null)
            return;
        foreach (var parameter in operation.Parameters)
        {
            var description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);
            var routeInfo = description.RouteInfo;
            if (string.IsNullOrEmpty(parameter.Name))
                parameter.Name = description.ModelMetadata?.Name;
            if (parameter.Description == null)
                parameter.Description = description.ModelMetadata?.Description;
            if (routeInfo == null)
                continue;
            parameter.Required |= !routeInfo.IsOptional;
        }
    }
}