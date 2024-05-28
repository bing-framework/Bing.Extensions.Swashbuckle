using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bing.Swashbuckle.Attributes;
using Bing.Swashbuckle.Core.Groups;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// 构建上下文
/// </summary>
internal class BuildContext
{
    /// <summary>
    /// 对象锁
    /// </summary>
    private static readonly object Lock = new object();

    /// <summary>
    /// Swagger扩展选项配置
    /// </summary>
    public SwaggerExOptions ExOptions { get; set; } = new SwaggerExOptions();

    /// <summary>
    /// 服务提供程序
    /// </summary>
    public IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// 对象
    /// </summary>
    /// <param name="key">键</param>
    public object this[string key]
    {
        get => Items[key];
        set => Items[key] = value;
    }

    /// <summary>
    /// 对象字典
    /// </summary>
    public IDictionary<string, object> Items { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="TItem">类型</typeparam>
    /// <param name="key">键</param>
    public TItem GetItem<TItem>(string key) => (TItem)Items[key];

    /// <summary>
    /// 设置对象
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="item">项</param>
    public void SetItem(string key, object item) => Items[key] = item;

    /// <summary>
    /// 实例
    /// </summary>
    public static BuildContext Instance = new BuildContext();

    /// <summary>
    /// 构建
    /// </summary>
    public void Build() => BuildApiDoc();

    /// <summary>
    /// 构建API文档
    /// </summary>
    private void BuildApiDoc()
    {
        lock (Lock)
        {
            Debug.WriteLine("BuildApiDoc");
            var apiGroupContext = this.GetApiGroupContext();
            BuildSwaggerDoc(apiGroupContext);
            BuildSwaggerEndpoint(apiGroupContext);
            BuildDocInclusionPredicate(apiGroupContext);
        }
    }

    /// <summary>
    /// 构建SwaggerDoc
    /// </summary>
    /// <param name="context">API分组上下文</param>
    private void BuildSwaggerDoc(ApiGroupContext context)
    {
        foreach (var info in context.GetInfos())
        {
            // 锁住对象，防止多线程
            lock (Lock)
            {
                Debug.WriteLine($"Build Swagger Document Key: {info.Key}");
                ExOptions.SwaggerGenOptions.SwaggerGeneratorOptions.SwaggerDocs[info.Key] = info.Value;
            }
        }
    }

    /// <summary>
    /// 构建Swagger入口点
    /// </summary>
    /// <param name="context">API分组上下文</param>
    private void BuildSwaggerEndpoint(ApiGroupContext context)
    {
        if (!context.HasApiGroups)
            return;
        // TODO: 由于Swashbuckle多管闲事初始化Urls，导致初始化Endpoints v1 问题
        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/v6.2.3/src/Swashbuckle.AspNetCore.SwaggerUI/SwaggerUIBuilderExtensions.cs#L41
        ExOptions.SwaggerUiOptions.ConfigObject.Urls = null;
        foreach (var endpoint in context.GetEndpoints())
            ExOptions.SwaggerUiOptions.AddInfo(endpoint.Key, endpoint.Value);
    }

    /// <summary>
    /// 构建Swagger入口选择
    /// </summary>
    /// <param name="context">API分组上下文</param>
    private void BuildDocInclusionPredicate(ApiGroupContext context)
    {
        BuildDocInclusionPredicateByApiGroup();
        BuildDocInclusionPredicateByApiVersion();
        BuildDocInclusionPredicateByApiVersionWithGroup();
    }

    /// <summary>
    /// 构建Swagger文档入口选择 - 根据分组
    /// </summary>
    private void BuildDocInclusionPredicateByApiGroup()
    {
        if (ExOptions.EnableApiVersion)
            return;
        if (!ExOptions.EnableApiGroup())
            return;
        ExOptions.SwaggerGenOptions.DocInclusionPredicate((docName, apiDescription) =>
        {
            if (docName == "NoGroup")
            {
                if (ExistsApiGroupAttribute(apiDescription.ActionDescriptor))
                    return false;
                return string.IsNullOrWhiteSpace(apiDescription.GroupName);
            }

            foreach (var obj in apiDescription.ActionDescriptor.EndpointMetadata)
            {
                if (!(obj is SwaggerApiGroupAttribute swaggerApiGroup))
                    continue;
                if (swaggerApiGroup.GroupName == docName)
                    return true;
            }
            return false;
        });
    }

    /// <summary>
    /// 构建Swagger文档入口选择 - 根据API版本
    /// </summary>
    private void BuildDocInclusionPredicateByApiVersion()
    {
        if (ExOptions.EnableApiGroup())
            return;
        if (!ExOptions.EnableApiVersion)
            return;
        ExOptions.SwaggerGenOptions.DocInclusionPredicate((docName, apiDescription) => docName == apiDescription.GroupName);
    }

    /// <summary>
    /// 构建Swagger文档入口选择 - 根据API版本以及分组
    /// </summary>
    private void BuildDocInclusionPredicateByApiVersionWithGroup()
    {
        if (!ExOptions.EnableApiGroup())
            return;
        if (!ExOptions.EnableApiVersion)
            return;
        ExOptions.SwaggerGenOptions.DocInclusionPredicate((docName, apiDescription) =>
        {
            // 无分组处理
            if (docName.StartsWith("NoGroup"))
            {
                if (ExistsApiGroupAttribute(apiDescription.ActionDescriptor))
                    return false;
                if (docName == $"NoGroup{apiDescription.GroupName}")
                    return true;
                return false;
            }
            // 有分组处理

            foreach (var obj in apiDescription.ActionDescriptor.EndpointMetadata)
            {
                if (!(obj is SwaggerApiGroupAttribute swaggerApiGroup))
                    continue;
                if ($"{swaggerApiGroup.GroupName}{apiDescription.GroupName}" == docName)
                    return true;
            }

            return false;
        });
    }

    /// <summary>
    /// 是否存在Api分组特性
    /// </summary>
    /// <param name="actionDescriptor">操作描述器</param>
    private bool ExistsApiGroupAttribute(ActionDescriptor actionDescriptor) => actionDescriptor.EndpointMetadata.OfType<SwaggerApiGroupAttribute>().Any();
}