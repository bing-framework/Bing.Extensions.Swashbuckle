using Bing.Swashbuckle.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bing.Swashbuckle.Filters.Operations;

/// <summary>
/// API最后修改操作过滤器
/// </summary>
public class LastModifiedOperationFilter : IOperationFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attribute = context.MethodInfo.GetCustomAttributes<ApiLastModifiedAttribute>(true).FirstOrDefault();
        if (attribute == null)
            return;
        // 将最后修改信息添加到扩展属性中（供前端 JavaScript 插件使用）
        if (!string.IsNullOrWhiteSpace(attribute.Date))
            operation.Extensions.Add("x-lastModified", new OpenApiString(attribute.Date));
        if (!string.IsNullOrWhiteSpace(attribute.Author))
            operation.Extensions.Add("x-author", new OpenApiString(attribute.Author));
        AppendToDescription(operation, attribute);
    }

    /// <summary>
    /// 将最后修改信息追加到操作描述中
    /// </summary>
    /// <param name="operation">OpenAPI 操作</param>
    /// <param name="attribute">最后修改属性</param>
    private static void AppendToDescription(OpenApiOperation operation, ApiLastModifiedAttribute attribute)
    {
        if (string.IsNullOrWhiteSpace(attribute.Date) && string.IsNullOrWhiteSpace(attribute.Author))
            return;
        var stringBuilder = new StringBuilder();
        // 保留原有的描述
        if (!string.IsNullOrWhiteSpace(operation.Description))
        {
            stringBuilder.AppendLine(operation.Description);
            stringBuilder.AppendLine(); // 添加空行分隔
        }
        // 添加最后修改信息
        stringBuilder.AppendLine("---");
        stringBuilder.AppendLine("**最后修改信息**");

        if (!string.IsNullOrWhiteSpace(attribute.Date)) 
            stringBuilder.AppendLine($"- **修改日期**: {attribute.Date}");
        if (!string.IsNullOrWhiteSpace(attribute.Author)) 
            stringBuilder.AppendLine($"- **修改作者**: {attribute.Author}");
        operation.Description = stringBuilder.ToString().TrimEnd();
    }
}