using System.Collections.Generic;
using Bing.Extensions.Swashbuckle.Core.Groups;
using Bing.Extensions.Swashbuckle.Internal;
using Microsoft.OpenApi.Models;

namespace Bing.Extensions.Swashbuckle.Extensions
{
    /// <summary>
    /// 构建上下文(<see cref="BuildContext"/>) 扩展
    /// </summary>
    internal static class BuildContextExtensions
    {
        /// <summary>
        /// Swagger 地址映射
        /// </summary>
        public const string UrlMap = "SwaggerUrlMap";

        /// <summary>
        /// Swagger 文档映射
        /// </summary>
        public const string DocsMap = "SwaggerDocsMap";

        /// <summary>
        /// Swagger 信息映射
        /// </summary>
        public const string InfoMap = "SwaggerInfoMap";

        /// <summary>
        /// Api分组上下文
        /// </summary>
        public const string ApiGroupContext = "ApiGroupContext";

        /// <summary>
        /// 获取API分组
        /// </summary>
        /// <param name="context">构建上下文</param>
        public static List<ApiGroupInfo> GetApiGroups(this BuildContext context)
        {
            if (!context.Items.ContainsKey(DocsMap))
            {
                var list = new List<ApiGroupInfo>();
                context.SetApiGroups(list);
            }
            return context.GetItem<List<ApiGroupInfo>>(DocsMap);
        }

        /// <summary>
        /// 设置API分组
        /// </summary>
        /// <param name="context">构建上下文</param>
        /// <param name="apiGroups">Api分组</param>
        public static void SetApiGroups(this BuildContext context, List<ApiGroupInfo> apiGroups) => context.SetItem(DocsMap, apiGroups);

        /// <summary>
        /// 获取地址映射字典
        /// </summary>
        /// <param name="context">构建上下文</param>
        public static IDictionary<string, string> GetUrlMaps(this BuildContext context)
        {
            if (!context.Items.ContainsKey(UrlMap))
            {
                var dictionary = new Dictionary<string, string>();
                context.SetUrlMaps(dictionary);
            }
            return context.GetItem<IDictionary<string, string>>(UrlMap);
        }

        /// <summary>
        /// 设置地址映射字典
        /// </summary>
        /// <param name="context">构建上下文</param>
        /// <param name="dictionary">字典</param>
        public static void SetUrlMaps(this BuildContext context, IDictionary<string, string> dictionary)
        {
            if (dictionary == null)
                return;
            context.SetItem(UrlMap, dictionary);
        }

        /// <summary>
        /// 获取信息映射字典
        /// </summary>
        /// <param name="context">构建上下文</param>
        public static IDictionary<string, OpenApiInfo> GetInfoMaps(this BuildContext context)
        {
            if (!context.Items.ContainsKey(InfoMap))
            {
                var dictionary = new Dictionary<string, OpenApiInfo>();
                context.SetInfoMaps(dictionary);
            }

            return context.GetItem<IDictionary<string, OpenApiInfo>>(InfoMap);
        }

        /// <summary>
        /// 设置信息映射字典
        /// </summary>
        /// <param name="context">构建上下文</param>
        /// <param name="dictionary">字典</param>
        public static void SetInfoMaps(this BuildContext context, IDictionary<string, OpenApiInfo> dictionary)
        {
            if (dictionary == null)
                return;
            context.SetItem(InfoMap, dictionary);
        }

        /// <summary>
        /// 获取API分组上下文
        /// </summary>
        /// <param name="context">构建上下文</param>
        public static ApiGroupContext GetApiGroupContext(this BuildContext context)
        {
            if (!context.Items.ContainsKey(ApiGroupContext))
            {
                var builder = new ApiGroupContextBuilder();
                context.SetApiGroupContext(builder.Build());
            }
            return context.GetItem<ApiGroupContext>(ApiGroupContext);
        }

        /// <summary>
        /// 设置API分组上下文
        /// </summary>
        /// <param name="context">构建上下文</param>
        /// <param name="apiGroupContext">API分组上下文</param>
        public static void SetApiGroupContext(this BuildContext context, ApiGroupContext apiGroupContext)
        {
            if (apiGroupContext == null)
                return;
            context.SetItem(ApiGroupContext, apiGroupContext);
        }
    }
}
