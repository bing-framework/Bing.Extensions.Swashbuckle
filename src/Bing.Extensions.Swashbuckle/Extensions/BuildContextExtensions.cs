﻿using System.Collections.Generic;
using Bing.Extensions.Swashbuckle.Core.Groups;
using Bing.Extensions.Swashbuckle.Internal;
using Swashbuckle.AspNetCore.Swagger;

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
        public static void SetApiGroups(this BuildContext context,List<ApiGroupInfo> apiGroups) => context.SetItem(DocsMap,apiGroups);

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
        public static IDictionary<string, Info> GetInfoMaps(this BuildContext context)
        {
            if (!context.Items.ContainsKey(InfoMap))
            {
                var dictionary=new Dictionary<string,Info>();
                context.SetInfoMaps(dictionary);
            }

            return context.GetItem<IDictionary<string, Info>>(InfoMap);
        }

        /// <summary>
        /// 设置信息映射字典
        /// </summary>
        /// <param name="context">构建上下文</param>
        /// <param name="dictionary">字典</param>
        public static void SetInfoMaps(this BuildContext context, IDictionary<string, Info> dictionary)
        {
            if (dictionary == null)
                return;
            context.SetItem(InfoMap, dictionary);
        }
    }
}