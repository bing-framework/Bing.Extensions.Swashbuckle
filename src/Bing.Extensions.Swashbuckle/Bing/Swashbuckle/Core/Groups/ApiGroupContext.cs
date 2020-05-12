using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Bing.Swashbuckle.Core.Groups
{
    /// <summary>
    /// Api分组上下文
    /// </summary>
    internal class ApiGroupContext
    {
        /// <summary>
        /// Api分组列表
        /// </summary>
        private IList<ApiGroupInfo> ApiGroups { get; set; } = new List<ApiGroupInfo>();

        /// <summary>
        /// 添加分组。仅添加分组，不添加版本
        /// </summary>
        /// <param name="name">名称</param>
        public void AddGroup(string name) => AddGroup(name, name, string.Empty);

        /// <summary>
        /// 添加分组。仅添加分组，不添加版本
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        public void AddGroup(string title, string name, string description) =>
            SaveApiGroup(name, initAction: o =>
            {
                o.Title = title;
                o.Name = name;
                o.Description = description;
                o.IsCustomGroup = true;
            });

        /// <summary>
        /// 添加APi分组
        /// </summary>
        /// <param name="name">名称</param>
        public void AddApiGroupByCustomGroup(string name) =>
            SaveApiGroup(name, o =>
            {
                o.AddItem(name, string.Empty);
            }, o =>
            {
                o.Title = name;
                o.Name = name;
                o.Description = string.Empty;
                o.IsCustomGroup = true;
            });

        /// <summary>
        /// 添加API分组
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="versionName">版本名称</param>
        /// <param name="version">版本号</param>
        public void AddApiGroupByCustomGroup(string title, string name, string description, string versionName,
            string version) =>
            SaveApiGroup(name, o =>
            {
                o.AddItem(versionName, version);
            }, o =>
            {
                o.Title = title;
                o.Name = name;
                o.Description = description;
                o.IsCustomGroup = true;
            });

        /// <summary>
        /// 添加无分组
        /// </summary>
        public void AddNoGroup() => AddGroup("无分组", "NoGroup", string.Empty);

        /// <summary>
        /// 添加无分组，带API版本
        /// </summary>
        public void AddNoGroupWithVersion() => AddApiGroupByCustomGroup("无分组", "NoGroup", string.Empty, "NoGroup", string.Empty);

        /// <summary>
        /// 添加API分组
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        public void AddApiGroup(string name, string description) =>
            SaveApiGroup(name, o =>
            {
                o.AddItem(name, string.Empty);
            }, o =>
            {
                o.Title = name;
                o.Name = name;
                o.Description = description;
            });

        /// <summary>
        /// 添加API分组
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="versionName">版本名称</param>
        /// <param name="version">版本号</param>
        public void AddApiGroup(string title, string name, string description, string versionName, string version) =>
            SaveApiGroup(name, o =>
            {
                o.AddItem(versionName, version);
            }, o =>
            {
                o.Title = title;
                o.Name = name;
                o.Description = description;
            });

        /// <summary>
        /// 添加API版本
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="version">版本号</param>
        public void AddApiVersion(string name, string version)
        {
            foreach (var apiGroup in ApiGroups)
            {
                if (!apiGroup.IsCustomGroup)
                    continue;
                apiGroup.AddItem(name,version);
            }
        }

        /// <summary>
        /// 保存API分组
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <param name="setupAction">配置操作</param>
        /// <param name="initAction">初始化操作</param>
        private void SaveApiGroup(string name, Action<ApiGroupInfo> setupAction = null, Action<ApiGroupInfo> initAction = null)
        {
            var exists = true;
            var apiGroup = ApiGroups.FirstOrDefault(x => x.Name == name);
            if (apiGroup == null)
            {
                apiGroup = new ApiGroupInfo();
                initAction?.Invoke(apiGroup);
                exists = false;
            }
            setupAction?.Invoke(apiGroup);
            if(!exists)
                ApiGroups.Add(apiGroup);
        }

        /// <summary>
        /// 获取信息列表
        /// </summary>
        public IDictionary<string, OpenApiInfo> GetInfos()
        {
            var dict = new Dictionary<string, OpenApiInfo>();
            foreach (var apiGroup in ApiGroups)
            {
                foreach (var apiVersion in apiGroup.ApiVersions)
                    dict[apiVersion.GetName()] = CreateInfo(apiVersion);
            }

            return dict;
        }

        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="apiVersion">API版本</param>
        private OpenApiInfo CreateInfo(ApiVersionInfo apiVersion) =>
            new OpenApiInfo
            {
                Title = apiVersion.Title,
                Version = apiVersion.Version,
                Description = apiVersion.Group.Description,
            };

        /// <summary>
        /// 获取入口点列表
        /// </summary>
        public IDictionary<string, string> GetEndpoints()
        {
            var dict = new Dictionary<string, string>();
            foreach (var apiGroup in ApiGroups)
            {
                foreach (var apiVersion in apiGroup.ApiVersions)
                {
                    dict[apiVersion.Title] = $"{apiVersion.GetName()}/swagger.json";
                }
            }

            return dict;
        }
    }
}
