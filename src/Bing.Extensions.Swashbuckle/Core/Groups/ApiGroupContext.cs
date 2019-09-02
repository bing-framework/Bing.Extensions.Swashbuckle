using System.Collections.Generic;
using System.Linq;
using Bing.Extensions.Swashbuckle.Internal;
using Swashbuckle.AspNetCore.Swagger;

namespace Bing.Extensions.Swashbuckle.Core.Groups
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
        /// 添加API分组
        /// </summary>
        /// <param name="name">名称</param>
        public void AddApiGroup(string name)
        {
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo {Title = name, Name = name, Description = string.Empty};
            apiGroup.AddItem(new ApiVersionInfo()
            {
                Name = name,
                Version = string.Empty
            });
            if(!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
        }

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
            ApiGroupInfo apiGroup = GetApiGroup(name) ?? new ApiGroupInfo {Title = title, Name = name, Description = description};
            apiGroup.AddItem(new ApiVersionInfo()
            {
                Name = versionName,
                Version = version
            });
            if (!ExistsApiGroup(name))
                ApiGroups.Add(apiGroup);
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
        public IDictionary<string, Info> GetInfos()
        {
            var dict = new Dictionary<string, Info>();
            foreach (var apiGroup in ApiGroups)
            {
                foreach (var apiVersion in apiGroup.ApiVersions)
                {
                    dict[apiVersion.Name] = CreateInfo(apiVersion);
                }
            }

            dict["NoGroup"] = new Info() {Title = "无分组"};
            return dict;
        }

        /// <summary>
        /// 创建信息
        /// </summary>
        /// <param name="apiVersion">API版本</param>
        private Info CreateInfo(ApiVersionInfo apiVersion)
        {
            return new Info()
            {
                Title = apiVersion.Title,
                Version = apiVersion.Version,
                Description = apiVersion.Group.Description,
            };
        }

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
                    dict[apiVersion.Title] = $"/swagger/{apiVersion.Name}/swagger.json";
                }
            }

            dict["无分组"] = "/swagger/NoGroup/swagger.json";

            return dict;
        }
    }
}
