using System;
using System.Collections.Generic;
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
        var controllerAttributes = context.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes<TAttribute>();
        var actionAttributes = context.MethodInfo.GetCustomAttributes<TAttribute>();

        var result = new List<TAttribute>(controllerAttributes);
        result.AddRange(actionAttributes);
        return result;
    }
}