using Bing.Extensions.Swashbuckle.Configs;
using Bing.Extensions.Swashbuckle.Filters.Operations;
using Bing.Extensions.Swashbuckle.Internal;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 注册自定义Swagger
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="options">自定义Swagger选项</param>
        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services,
            CustomSwaggerOptions options)
        {
            services.AddSwaggerGen(o =>
            {
                BuildContext.Instance.Options.SwaggerGenOptions = o;
                if (options.ApiVersions == null)
                {
                    options.AddSwaggerGenAction?.Invoke(o);
                    return;
                }

                //foreach (var version in options.ApiVersions)
                //{
                //    if (o.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(version.Version))
                //    {
                //        continue;
                //    }
                //    o.SwaggerDoc(version.Version, new Info() {Title = options.ProjectName, Version = version.Version});
                //}
                
                o.OperationFilter<ApiVersionDefaultValueOperationFilter>();
                options.AddSwaggerGenAction?.Invoke(o);
                var apiContext = BuildContext.Instance.GetApiGroupContext();
                foreach (var info in apiContext.GetInfos())
                {
                    apiContext.Options.SwaggerGenOptions.SwaggerDoc(info.Key, info.Value);
                }

                foreach (var endpoint in apiContext.GetEndpoints())
                {
                    apiContext.Options.SwaggerUiOptions.AddInfo(endpoint.Key, endpoint.Value);
                }
            });

            return services;
        }

    }
}
