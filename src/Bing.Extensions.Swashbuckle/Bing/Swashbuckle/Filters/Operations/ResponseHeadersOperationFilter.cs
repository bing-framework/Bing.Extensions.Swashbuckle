using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Bing.Swashbuckle.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Operations;

/// <summary>
/// 添加响应请求头 操作过滤器
/// </summary>
public class ResponseHeadersOperationFilter : IOperationFilter
{
    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionAttributes = context.MethodInfo.GetCustomAttributes<SwaggerResponseHeaderAttribute>();
        if (!actionAttributes.Any())
            return;
        foreach (var attr in actionAttributes)
        {
            foreach (var statusCode in attr.StatusCodes)
            {
                var response = operation.Responses
                    .FirstOrDefault(x => x.Key == (statusCode).ToString(CultureInfo.InvariantCulture)).Value;
                if (response == null)
                    continue;
                if (response.Headers == null)
                    response.Headers = new Dictionary<string, OpenApiHeader>();
                response.Headers.Add(attr.Name,
                    new OpenApiHeader()
                    {
                        Description = attr.Description,
                        Schema = new OpenApiSchema {Description = attr.Description, Type = attr.Type, Format = attr.Format}
                    });
            }
        }
    }
}