using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bing.Swashbuckle.Filters.Operations;

/// <summary>
/// 添加 追加授权信息到注释 操作过滤器
/// </summary>
public class AppendAuthorizeToSummaryOperationFilter : IOperationFilter
{
    /// <summary>
    /// 过滤器
    /// </summary>
    private readonly AppendAuthorizeToSummaryOperationFilter<AuthorizeAttribute> _filter;

    /// <summary>
    /// 初始化一个<see cref="AppendAuthorizeToSummaryOperationFilter"/>类型的实例
    /// </summary>
    public AppendAuthorizeToSummaryOperationFilter()
    {
        var policySelector = new PolicySelectorWithLabel<AuthorizeAttribute>()
        {
            Label = "policies",
            Selector = authAttributes =>
                authAttributes
                    .Where(x => !string.IsNullOrWhiteSpace(x.Policy))
                    .Select(x => x.Policy)
        };

        var rolesSelector = new PolicySelectorWithLabel<AuthorizeAttribute>()
        {
            Label = "roles",
            Selector = authAttributes =>
                authAttributes
                    .Where(x => !string.IsNullOrWhiteSpace(x.Roles))
                    .Select(x => x.Roles)
        };
        _filter = new AppendAuthorizeToSummaryOperationFilter<AuthorizeAttribute>(new[] { policySelector, rolesSelector }.AsEnumerable());
    }

    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context) => _filter.Apply(operation, context);
}

/// <summary>
/// 添加 追加授权信息到注释 操作过滤器
/// </summary>
/// <typeparam name="TAttribute">特性类型</typeparam>
public class AppendAuthorizeToSummaryOperationFilter<TAttribute> : IOperationFilter where TAttribute : Attribute
{
    /// <summary>
    /// 授权选择器标签列表
    /// </summary>
    private readonly IEnumerable<PolicySelectorWithLabel<TAttribute>> _policySelectors;

    /// <summary>
    /// 初始化一个<see cref="AppendAuthorizeToSummaryOperationFilter{TAttribute}"/>类型的实例
    /// </summary>
    /// <param name="policySelectors">授权选择器标签列表。例如：(a => a.Policy)</param>
    public AppendAuthorizeToSummaryOperationFilter(IEnumerable<PolicySelectorWithLabel<TAttribute>> policySelectors) => _policySelectors = policySelectors;

    /// <summary>
    /// 重写操作处理
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.GetControllerAndActionAttributes<AllowAnonymousAttribute>().Any())
            return;
        var authorizeAttributes = context.GetControllerAndActionAttributes<TAttribute>().ToList();
        if (!authorizeAttributes.Any())
            return;
        var authorizationDescription = new StringBuilder(" (Auth");
        foreach (var policySelector in _policySelectors)
            AppendPolicies(authorizeAttributes, authorizationDescription, policySelector);
        operation.Summary += authorizationDescription.ToString().TrimEnd(';') + ")";
    }

    /// <summary>
    /// 追加授权信息
    /// </summary>
    /// <param name="authorizeAttributes">授权特性列表</param>
    /// <param name="authorizationDescription">授权说明</param>
    /// <param name="policySelector">授权选择器</param>
    private void AppendPolicies(IEnumerable<TAttribute> authorizeAttributes, StringBuilder authorizationDescription,
        PolicySelectorWithLabel<TAttribute> policySelector)
    {
        var policies = policySelector.Selector(authorizeAttributes).OrderBy(policy => policy).ToList();
        if (policies.Any())
            authorizationDescription.Append($" {policySelector.Label}: {string.Join(", ", policies)};");
    }
}

/// <summary>
/// 授权选择器标签
/// </summary>
/// <typeparam name="T">特性类型</typeparam>
public class PolicySelectorWithLabel<T> where T : Attribute
{
    /// <summary>
    /// 选择器
    /// </summary>
    public Func<IEnumerable<T>, IEnumerable<string>> Selector { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string Label { get; set; }
}