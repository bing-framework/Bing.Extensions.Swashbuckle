using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bing.Swashbuckle.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Operations;

/// <summary>
/// 添加文件参数 操作过滤器。支持<see cref="SwaggerUploadAttribute"/>特性
/// </summary>
public class FileParameterOperationFilter : IOperationFilter
{
    /// <summary>
    /// 重写操作处理
    /// </summary>
    /// <param name="operation">当前操作</param>
    /// <param name="context">操作过滤器上下文</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
            !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            return;
        UploadByAttribute(operation, context);
    }

    /// <summary>
    /// 通过特性进行上传
    /// </summary>
    /// <param name="operation">当前操作</param>
    /// <param name="context">操作过滤器上下文</param>
    private void UploadByAttribute(OpenApiOperation operation, OperationFilterContext context)
    {
        var swaggerUpload = context.MethodInfo.GetCustomAttributes<SwaggerUploadAttribute>().FirstOrDefault();
        if (swaggerUpload == null)
            return;
        var requestBody = operation.RequestBody;
        if (swaggerUpload.ClearOtherParameters)
        {
            operation.Parameters.Clear();
            requestBody = new OpenApiRequestBody();
        }
        requestBody = requestBody ?? new OpenApiRequestBody();
        if (!requestBody.Content.TryGetValue("multipart/form-data", out var uploadFileMediaType))
            requestBody.Content["multipart/form-data"] = uploadFileMediaType = new OpenApiMediaType();
        if (uploadFileMediaType.Schema == null)
            uploadFileMediaType.Schema = new OpenApiSchema();
        uploadFileMediaType.Schema.Type = "object";
        if (uploadFileMediaType.Schema.Properties == null)
            uploadFileMediaType.Schema.Properties = new Dictionary<string, OpenApiSchema>(StringComparer.OrdinalIgnoreCase);
        uploadFileMediaType.Schema.Properties[swaggerUpload.FieldName] = new OpenApiSchema()
        {
            Description = swaggerUpload.Description,
            Type = "string",
            Format = "binary"
        };
        if (uploadFileMediaType.Schema.Required == null)
            uploadFileMediaType.Schema.Required = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        uploadFileMediaType.Schema.Required.Add(swaggerUpload.FieldName);
        operation.RequestBody = requestBody;
    }
}