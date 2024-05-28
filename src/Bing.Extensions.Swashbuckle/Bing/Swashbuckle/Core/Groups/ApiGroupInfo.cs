using System.Collections.Generic;
using System.Linq;

namespace Bing.Swashbuckle.Core.Groups;

/// <summary>
/// Api分组信息
/// </summary>
internal class ApiGroupInfo
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
    /// <param name="name">名称</param>
    /// <param name="version">版本号</param>
    public void AddItem(string name, string version)
    {
        if (string.IsNullOrEmpty(name))
            return;
        if (ApiVersions.Any(x => x.Name == name && x.Version == version))
            return;
        AddItem(new ApiVersionInfo(this, name, version));
    }

    /// <summary>
    /// 添加明细
    /// </summary>
    /// <param name="apiVersion">Api版本</param>
    private void AddItem(ApiVersionInfo apiVersion)
    {
        if (apiVersion == null)
            return;
        ApiVersions.Add(apiVersion);
    }
}