using System.Linq;
using System.Reflection;
using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

// ReSharper disable once CheckNamespace
namespace Bing.Swashbuckle
{
    /// <summary>
    /// SwaggerUI 扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SwaggerUIExtensions
    {
        #region UseDefaultUI(使用默认的UI)

        /// <summary>
        /// 使用默认的UI
        /// </summary>
        /// <param name="options">SwaggerUI配置</param>
        // ReSharper disable once InconsistentNaming
        public static void UseDefaultSwaggerUI(this SwaggerUIOptions options)
        {
            options.DefaultModelExpandDepth(2);// 接口列表折叠配置
            options.DefaultModelRendering(ModelRendering.Example);// 控制首次呈现API时模型的显示方式（模型|示例）。
            options.ShowExtensions();// 显示扩展信息
            options.DefaultModelsExpandDepth(-1);// 隐藏model
            options.DisplayOperationId();// 显示控制器接口方法名
            options.DisplayRequestDuration();// 显示请求持续时间（以毫秒为单位）
            options.DocExpansion(DocExpansion.None);// 文档显示方式：显示控制器
            options.EnableDeepLinking();// 启用深层连接，用于指定Url自动跳转到相应标签
            options.EnableFilter();// 启用过滤文本框
        }

        #endregion

        #region UseCustomIndex(使用自定义首页)

        /// <summary>
        /// 使用自定义首页
        /// </summary>
        /// <param name="options">SwaggerUI选项</param>
        public static void UseCustomSwaggerIndex(this SwaggerUIOptions options)
        {
            var currentAssembly = typeof(SwaggerExOptions).GetTypeInfo().Assembly;
            options.IndexStream = () =>
                currentAssembly.GetManifestResourceStream($"Bing.Swashbuckle.Resources.index.html");
        }

        #endregion

        #region UseTokenStorage(使用令牌存储)

        /// <summary>
        /// 使用令牌存储。解决刷新页面导致令牌丢失问题，前提必须使用 <see cref="UseCustomSwaggerIndex"/> 方法
        /// </summary>
        /// <param name="options">SwaggerUI选项</param>
        /// <param name="securityDefinition">授权定义。对应于 AddSecurityDefinition 中的 name</param>
        /// <param name="cacheType">缓存类型</param>
        public static void UseTokenStorage(this SwaggerUIOptions options, string securityDefinition, WebCacheType cacheType = WebCacheType.Session)
        {
            options.ConfigObject.AdditionalItems["token_storage"] = new TokenStorageParameter
            {
                CacheType = cacheType,
                SecurityDefinition = securityDefinition
            };
        }

        #endregion

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="options">SwaggerUI选项配置</param>
        /// <param name="name">名称</param>
        /// <param name="url">地址</param>
        internal static void AddInfo(this SwaggerUIOptions options, string name, string url)
        {
            if (options.ExistsApiVersion(name, url))
                return;
            var urlMaps = BuildContext.Instance.GetUrlMaps();
            urlMaps[name] = url;
            options.SwaggerEndpoint(url, name);
        }

        /// <summary>
        /// 是否存在Api版本
        /// </summary>
        /// <param name="options">SwaggerUI选项</param>
        /// <param name="name">名称</param>
        /// <param name="url">地址</param>
        internal static bool ExistsApiVersion(this SwaggerUIOptions options, string name, string url)
        {
            if (options?.ConfigObject?.Urls == null)
                return false;
            return options.ConfigObject.Urls.Any(x => x.Name == name || x.Url == url);
        }
    }
}
