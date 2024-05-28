using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Operations;

/// <summary>
/// 添加安全请求 操作过滤器
/// </summary>
public class SecurityRequirementsOperationFilter : IOperationFilter
{
    /// <summary>
    /// 过滤器
    /// </summary>
    private readonly SecurityRequirementsOperationFilter<AuthorizeAttribute> _filter;

    /// <summary>
    /// 初始化一个<see cref="SecurityRequirementsOperationFilter"/>类型的实例
    /// </summary>
    /// <param name="includeUnauthorizedAndForbiddenResponses">是否包含未授权或被禁止的响应，如果为true,则将为每个操作添加401、403响应</param>
    /// <param name="securitySchemaName">安全架构名称</param>
    public SecurityRequirementsOperationFilter(bool includeUnauthorizedAndForbiddenResponses = true, string securitySchemaName = "oauth2")
    {
        Func<IEnumerable<AuthorizeAttribute>, IEnumerable<string>> policySelector = authAttributes =>
            authAttributes.Where(x => !string.IsNullOrEmpty(x.Policy))
                .Select(x => x.Policy);
        _filter = new SecurityRequirementsOperationFilter<AuthorizeAttribute>(policySelector,
            includeUnauthorizedAndForbiddenResponses, securitySchemaName);
    }

    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        _filter.Apply(operation, context);
    }
}

/// <summary>
/// 添加安全请求 操作过滤器
/// </summary>
/// <typeparam name="TAttribute">特性类型</typeparam>
public class SecurityRequirementsOperationFilter<TAttribute> : IOperationFilter where TAttribute : Attribute
{
    /// <summary>
    /// 是否包含未授权或被禁止的响应
    /// </summary>
    private readonly bool _includeUnauthorizedAndForbiddenResponses;

    /// <summary>
    /// 授权策略选择器，从授权特性中选择授权策略
    /// </summary>
    private readonly Func<IEnumerable<TAttribute>, IEnumerable<string>> _policySelector;

    /// <summary>
    /// 安全架构名称
    /// </summary>
    private readonly string _securitySchemaName;

    /// <summary>
    /// 初始化一个<see cref="SecurityRequirementsOperationFilter{TAttribute}"/>类型的实例
    /// </summary>
    /// <param name="policySelector">授权策略选择器，从授权特性中选择授权策略。范例：(t => t.Policy)</param>
    /// <param name="includeUnauthorizedAndForbiddenResponses">是否包含未授权或被禁止的响应，如果为true,则将为每个操作添加401、403响应</param>
    /// <param name="securitySchemaName">安全架构名称</param>
    public SecurityRequirementsOperationFilter(Func<IEnumerable<TAttribute>, IEnumerable<string>> policySelector,
        bool includeUnauthorizedAndForbiddenResponses = true, string securitySchemaName = "oauth2")
    {
        _policySelector = policySelector;
        _includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
        _securitySchemaName = securitySchemaName;
    }

    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.GetControllerAndActionAttributes<AllowAnonymousAttribute>().Any())
            return;
        var actionAttributes = context.GetControllerAndActionAttributes<TAttribute>().ToList();
        if (!actionAttributes.Any())
            return;
        if (_includeUnauthorizedAndForbiddenResponses)
        {
            if (!operation.Responses.ContainsKey("401"))
                operation.Responses.Add("401", new OpenApiResponse() { Description = "Unauthorized" });
            if (!operation.Responses.ContainsKey("403"))
                operation.Responses.Add("403", new OpenApiResponse() { Description = "Forbidden" });
        }
        var policies = _policySelector(actionAttributes) ?? Enumerable.Empty<string>();
        operation.Security.Add(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                        {Type = ReferenceType.SecurityScheme, Id = _securitySchemaName}
                },
                policies.ToList()
            }
        });
    }
}