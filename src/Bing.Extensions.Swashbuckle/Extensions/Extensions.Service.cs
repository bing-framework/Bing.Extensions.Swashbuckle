using Bing.Extensions.Swashbuckle.Configs;
using Bing.Extensions.Swashbuckle.Filters.Operations;
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
        /// <returns></returns>
        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services,
            CustomSwaggerOptions options)
        {
            services.AddSwaggerGen(o =>
            {
                if (options.ApiVersions == null)
                {
                    return;
                }

                foreach (var version in options.ApiVersions)
                {
                    o.SwaggerDoc(version, new Info() {Title = options.ProjectName, Version = version});
                }

                o.OperationFilter<ApiVersionDefaultValueOperationFilter>();
                options.AddSwaggerGenAction?.Invoke(o);
            });
            return services;
        }
    }
}
