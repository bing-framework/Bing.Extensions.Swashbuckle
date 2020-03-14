using System.Linq;
using Bing.Swashbuckle.Attributes;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Swashbuckle.Core.Groups
{
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
            context.Options = buildContext.Options;
            BuildGroup(context, buildContext);
            BuildCustomVersion(context, buildContext);
            BuildNoGroup(context, buildContext);
            BuildApiVersion(context,buildContext);
            return context;
        }

        /// <summary>
        /// 构建分组
        /// </summary>
        /// <param name="context">Api分组上下文</param>
        /// <param name="buildContext">构建上下文</param>
        private void BuildGroup(ApiGroupContext context, BuildContext buildContext)
        {
            if (!buildContext.Options.EnableApiGroup)
                return;
            if (!buildContext.Options.ApiGroupType.IsEnum)
                return;
            buildContext.Options.ApiGroupType.GetFields().Skip(1).ToList().ForEach(x =>
            {
                var attribute = x.GetCustomAttributes(typeof(SwaggerApiGroupInfoAttribute), false)
                    .OfType<SwaggerApiGroupInfoAttribute>().FirstOrDefault();
                if (attribute == null)
                {
                    if (buildContext.Options.EnableApiVersion)
                    {
                        context.AddGroup(x.Name);
                        return;
                    }
                    context.AddApiGroupByCustomGroup(x.Name);
                    return;
                }

                if (buildContext.Options.EnableApiVersion)
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
            if (buildContext.Options.EnableApiGroup || buildContext.Options.EnableApiVersion)
                return;
            if (!buildContext.Options.HasCustomVersion)
                return;
            foreach (var apiVersion in buildContext.Options.ApiVersions)
                context.AddApiGroup(apiVersion.Version, apiVersion.Description);
        }

        /// <summary>
        /// 构建API多版本
        /// </summary>
        /// <param name="context">Api分组上下文</param>
        /// <param name="buildContext">构建上下文</param>
        private void BuildApiVersion(ApiGroupContext context, BuildContext buildContext)
        {
            if (!buildContext.Options.EnableApiVersion)
                return;
            var provider = buildContext.ServiceProvider.GetService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                if (buildContext.Options.EnableApiGroup)
                {
                    context.AddApiVersion(description.GroupName,description.ApiVersion.ToString());
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
            if (!buildContext.Options.EnableApiGroup)
                return;
            if (buildContext.Options.EnableApiVersion)
                context.AddNoGroup();
            else
                context.AddNoGroupWithVersion();
        }
    }
}
