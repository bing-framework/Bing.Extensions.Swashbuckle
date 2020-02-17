using System.Linq;
using System.Reflection;
using Bing.Extensions.Swashbuckle.Internal;
using Bing.Swashbuckle;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// SwaggerUI 扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SwaggerUIExtensions
    {
        /// <summary>
        /// 使用默认SwggerUI
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

        /// <summary>
        /// 使用自定义首页
        /// </summary>
        /// <param name="options">SwaggerUI选项</param>
        public static void UseCustomSwaggerIndex(this SwaggerUIOptions options)
        {
            var currentAssembly = typeof(CustomSwaggerOptions).GetTypeInfo().Assembly;
            //options.IndexStream = () =>
            //    currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.Resources.index.html");
            options.IndexStream = () =>
                currentAssembly.GetManifestResourceStream($"Bing.Swashbuckle.Resources.index.html");
        }

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
