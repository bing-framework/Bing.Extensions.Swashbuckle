using System.Linq;
using Bing.Swashbuckle.Attributes;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Swashbuckle.Core.Groups;

/// <summary>
/// Api分组上下文构建器
/// </summary>
internal class ApiGroupContextBuilder
{
    /// <summary>
    /// 构建上下文
    /// </summary>
    public ApiGroupContext Build()
    {
        var context = new ApiGroupContext();
        var buildContext = BuildContext.Instance;
        BuildGroup(context, buildContext);
        BuildCustomVersion(context, buildContext);
        BuildNoGroup(context, buildContext);
        BuildApiVersion(context, buildContext);
        return context;
    }

    /// <summary>
    /// 构建分组
    /// </summary>
    /// <param name="context">Api分组上下文</param>
    /// <param name="buildContext">构建上下文</param>
    private void BuildGroup(ApiGroupContext context, BuildContext buildContext)
    {
        if (!buildContext.ExOptions.EnableApiGroup())
            return;
        if (!buildContext.ExOptions.ApiGroupType.IsEnum)
            return;
        buildContext.ExOptions.ApiGroupType.GetFields().Skip(1).ToList().ForEach(x =>
        {
            var attribute = x.GetCustomAttributes(typeof(SwaggerApiGroupInfoAttribute), false)
                .OfType<SwaggerApiGroupInfoAttribute>().FirstOrDefault();
            if (attribute == null)
            {
                if (buildContext.ExOptions.EnableApiVersion)
                {
                    context.AddGroup(x.Name);
                    return;
                }
                context.AddApiGroupByCustomGroup(x.Name);
                return;
            }

            if (buildContext.ExOptions.EnableApiVersion)
            {
                context.AddGroup(attribute.Title, x.Name, attribute.Description);
                return;
            }
            context.AddApiGroupByCustomGroup(attribute.Title, x.Name, attribute.Description, x.Name, x.Name);
        });
    }

    /// <summary>
    /// 构建自定版本
    /// </summary>
    /// <param name="context">Api分组上下文</param>
    /// <param name="buildContext">构建上下文</param>
    private void BuildCustomVersion(ApiGroupContext context, BuildContext buildContext)
    {
        if (buildContext.ExOptions.EnableApiGroup() || buildContext.ExOptions.EnableApiVersion)
            return;
        if (!buildContext.ExOptions.HasCustomVersion())
            return;
        foreach (var apiVersion in buildContext.ExOptions.ApiVersions)
            context.AddApiGroup(apiVersion.Version, apiVersion.Description);
    }

    /// <summary>
    /// 构建API多版本
    /// </summary>
    /// <param name="context">Api分组上下文</param>
    /// <param name="buildContext">构建上下文</param>
    private void BuildApiVersion(ApiGroupContext context, BuildContext buildContext)
    {
        if (!buildContext.ExOptions.EnableApiVersion)
            return;
        var provider = buildContext.ServiceProvider.GetService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            if (buildContext.ExOptions.EnableApiGroup())
            {
                context.AddApiVersion(description.GroupName, description.ApiVersion.ToString());
                continue;
            }
            context.AddApiGroup(description.GroupName, description.GroupName, string.Empty, description.GroupName,
                description.ApiVersion.ToString());
        }
    }

    /// <summary>
    /// 构建无分组
    /// </summary>
    /// <param name="context">Api分组上下文</param>
    /// <param name="buildContext">构建上下文</param>
    private void BuildNoGroup(ApiGroupContext context, BuildContext buildContext)
    {
        if (!buildContext.ExOptions.EnableApiGroup())
            return;
        if (buildContext.ExOptions.EnableApiVersion)
            context.AddNoGroup();
        else
            context.AddNoGroupWithVersion();
    }
}