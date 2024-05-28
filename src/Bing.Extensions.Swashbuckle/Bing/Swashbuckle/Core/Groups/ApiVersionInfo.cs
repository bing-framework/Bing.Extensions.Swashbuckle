namespace Bing.Swashbuckle.Core.Groups;

/// <summary>
/// Api版本信息
/// 下拉列表：自定义名称(名称) - 版本号
/// 下拉列表地址：/swagger/{Name}-v1.0/swagger.json
/// </summary>
internal class ApiVersionInfo
{
    /// <summary>
    /// 初始化一个<see cref="ApiVersionInfo"/>类型的实例
    /// </summary>
    /// <param name="group">API分组信息</param>
    /// <param name="name">名称</param>
    /// <param name="version">版本号</param>
    public ApiVersionInfo(ApiGroupInfo group, string name, string version)
    {
        Group = group;
        Name = name;
        Version = version;
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title => GetTitle();

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 分组信息
    /// </summary>
    public ApiGroupInfo Group { get; set; }

    /// <summary>
    /// 获取标题
    /// </summary>
    private string GetTitle()
    {
        if (string.IsNullOrEmpty(Version))
            return Group.Title;
        if (Group.Title == Name)
            return Group.Title;
        if (Group.Name == Name)
            return Group.Title;
        return $"{Group.Title} - {Name}";
    }

    /// <summary>
    /// 获取名称
    /// </summary>
    public string GetName() => Name == Group.Name ? Name : $"{Group.Name}{Name}";
}