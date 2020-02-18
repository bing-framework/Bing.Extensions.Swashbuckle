using System.Collections.Generic;
using System.Linq;
using Bing.Swashbuckle.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Operations
{
    /// <summary>
    /// 添加请求头 操作过滤器
    /// </summary>
    public class RequestHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 重写操作处理
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var swaggerRequestHeaders = context.GetControllerAndActionAttributes<SwaggerRequestHeaderAttribute>().ToList();
            if (!swaggerRequestHeaders.Any())
                return;
            foreach (var requestHeader in swaggerRequestHeaders)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();
                var request = operation.Parameters.FirstOrDefault(x =>
                    x.In == ParameterLocation.Header && x.Name == requestHeader.Name);
                if (request != null)
                    operation.Parameters.Remove(request);
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = requestHeader.Name,
                    In = ParameterLocation.Header,
                    Required = requestHeader.Required,
                    Description = requestHeader.Description,
                    Schema = new OpenApiSchema()
                    {
                        Type = "string",
                        Default = new OpenApiString(requestHeader.Default?.ToString())
                    }
                });

            }
        }
    }
}
