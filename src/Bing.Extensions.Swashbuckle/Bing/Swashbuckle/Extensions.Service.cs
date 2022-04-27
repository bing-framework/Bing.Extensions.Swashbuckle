using System;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
        /// 注册Swagger扩展
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="setupAction">操作配置</param>
        public static IServiceCollection AddSwaggerEx(this IServiceCollection services, Action<SwaggerExOptions> setupAction = null)
        {
            setupAction?.Invoke(BuildContext.Instance.ExOptions);
            // 注入缓存
            if (BuildContext.Instance.ExOptions.EnableCached)
                services.TryAddTransient<ISwaggerProvider, CachingSwaggerProvider>();
            // 注入自定义应用模型
            if (BuildContext.Instance.ExOptions.GlobalResponseWrapperAction != null)
                services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
            services.AddSwaggerGen(o =>
            {
                BuildContext.Instance.ExOptions.InitSwaggerGenOptions(o);
                BuildContext.Instance.Build();
            });
            return services;
        }

        /// <summary>
        /// 添加Swagger文档缓存。必须在 AddSwaggerGen() 方法之后使用。
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddSwaggerCaching(this IServiceCollection services) => services.Replace(ServiceDescriptor.Transient<ISwaggerProvider, CachingSwaggerProvider>());
    }
}
