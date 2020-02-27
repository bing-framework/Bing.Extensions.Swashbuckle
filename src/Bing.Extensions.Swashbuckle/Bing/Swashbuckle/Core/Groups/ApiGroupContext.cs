using System.Collections.Generic;
using System.Linq;
using Bing.Swashbuckle.Internals;
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
        /// Swagger扩展选项配置
        /// </summary>
        public SwaggerExtensionOptions Options { get; set; }

        /// <summary>
        /// 获取Api分组列表
        /// </summary>
        public IList<ApiGroupInfo> GetApiGroups() => ApiGroups;

        /// <summary>
        /// 添加分组。仅添加分组，不添加版本
        /// </summary>
        /// <param name="name">名称</param>
        public void AddGroup(string name)
        {
            AddGroup(name, name, string.Empty);
        }

        /// <summary>
        /// 添加分组。仅添加分组，不添加版本
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        public void AddGroup(string title, string name, string description)
        {
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo { Title = title, Name = name, Description = string.Empty, IsCustomGroup = true };
            if (!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
        }

        /// <summary>
        /// 添加APi分组
        /// </summary>
        /// <param name="name">名称</param>
        public void AddApiGroupByCustomGroup(string name)
        {
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo { Title = name, Name = name, Description = string.Empty, IsCustomGroup = true };
            apiGroup.AddItem(new ApiVersionInfo()
            {
                Name = name,
                Version = string.Empty
            });
            if (!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
        }

        /// <summary>
        /// 添加API分组
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="versionName">版本名称</param>
        /// <param name="version">版本号</param>
        public void AddApiGroupByCustomGroup(string title, string name, string description, string versionName,
            string version)
        {
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo { Title = title, Name = name, Description = description, IsCustomGroup = true };
            apiGroup.AddItem(new ApiVersionInfo()
            {
                Name = versionName,
                Version = version
            });
            if (!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
        }

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
        public void AddApiGroup(string name, string description)
        {
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo { Title = name, Name = name, Description = description };
            apiGroup.AddItem(new ApiVersionInfo()
            {
                Name = name,
                Version = string.Empty
            });
            if (!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
        }

        /// <summary>
        /// 添加API分组
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="versionName">版本名称</param>
        /// <param name="version">版本号</param>
        public void AddApiGroup(string title, string name, string description, string versionName, string version)
        {
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo { Title = title, Name = name, Description = description };
            apiGroup.AddItem(new ApiVersionInfo()
            {
                Name = versionName,
                Version = version
            });
            if (!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
        }

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
                apiGroup.AddItem(new ApiVersionInfo()
                {
                    Name = name,
                    Version = version
                });
            }
        }

        /// <summary>
        /// 是否存在API分组
        /// </summary>
        /// <param name="name">名称</param>
        public bool ExistsApiGroup(string name) => ApiGroups.Any(x => x.Name == name);

        /// <summary>
        /// 获取API分组
        /// </summary>
        /// <param name="name">名称</param>
        public ApiGroupInfo GetApiGroup(string name) => ApiGroups.FirstOrDefault(x => x.Name == name);

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
                    dict[apiVersion.Title] = $"/swagger/{apiVersion.GetName()}/swagger.json";
                }
            }

            return dict;
        }
    }
}
