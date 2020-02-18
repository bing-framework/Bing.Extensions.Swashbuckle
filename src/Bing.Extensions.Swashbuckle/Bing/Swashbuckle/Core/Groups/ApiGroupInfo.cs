using System.Collections.Generic;
using System.Linq;

namespace Bing.Swashbuckle.Core.Groups
{
    /// <summary>
    /// Api分组信息
    /// </summary>
    public class ApiGroupInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否自定义分组
        /// </summary>
        public bool IsCustomGroup { get; set; }

        /// <summary>
        /// Api版本列表
        /// </summary>
        public List<ApiVersionInfo> ApiVersions { get; set; } = new List<ApiVersionInfo>();

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="apiVersion">Api版本</param>
        public void AddItem(ApiVersionInfo apiVersion)
        {
            if (apiVersion == null)
                return;
            if (ApiVersions.Any(x => x.Name == apiVersion.Name && x.Version == apiVersion.Version))
                return;
            apiVersion.Group = this;
            ApiVersions.Add(apiVersion);
        }
    }
}
