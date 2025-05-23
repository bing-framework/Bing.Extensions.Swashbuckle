using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

// ReSharper disable once CheckNamespace
namespace Bing.Swashbuckle;

/// <summary>
/// 操作过滤器上下文(<see cref="OperationFilterContext"/>) 扩展
/// </summary>
internal static class OperationFilterContextExtensions
{
    /// <summary>
    /// 获取控制器以及操作中指定类型的所有特性
    /// </summary>
    /// <typeparam name="TAttribute">特性类型</typeparam>
    /// <param name="context">操作过滤器上下文</param>
    public static IEnumerable<TAttribute> GetControllerAndActionAttributes<TAttribute>(this OperationFilterContext context)
        where TAttribute : Attribute
    {
        var result = new List<TAttribute>();

        if (context.MethodInfo != null)
        {
            var controllerAttributes = context.MethodInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes<TAttribute>();
            if (controllerAttributes != null)
                result.AddRange(controllerAttributes);
            var actionAttributes = context.MethodInfo.GetCustomAttributes<TAttribute>();
            result.AddRange(actionAttributes);
        }
#if NETCOREAPP3_1_OR_GREATER
        if (context.ApiDescription.ActionDescriptor.EndpointMetadata != null)
        {
            var endpointAttributes = context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<TAttribute>();
            result.AddRange(endpointAttributes);
        }
#endif

        return result.Distinct();
    }
}