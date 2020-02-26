using Bing.Extensions.Swashbuckle.Internal;
using Bing.Swashbuckle.Internals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace Bing.Swashbuckle
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
                options.AddSwaggerGenAction?.Invoke(o);
                BuildContext.Instance.Build();
            });

            return services;
        }

        /// <summary>
        /// 注册缓存Swagger
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="options">自定义Swagger选项</param>
        public static IServiceCollection AddCachedSwaggerGen(this IServiceCollection services,
            CustomSwaggerOptions options)
        {
            services.AddSwaggerGen();
            services.Replace(ServiceDescriptor.Transient<ISwaggerProvider, CacheSwaggerProvider>());
            services.ConfigureSwaggerGen(o =>
            {
                BuildContext.Instance.Options.SwaggerGenOptions = o;
                options.AddSwaggerGenAction?.Invoke(o);
                BuildContext.Instance.Build();
            });
            return services;
        }
    }
}
