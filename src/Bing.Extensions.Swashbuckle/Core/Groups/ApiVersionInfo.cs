namespace Bing.Extensions.Swashbuckle.Core.Groups
{
    /// <summary>
    /// Api版本信息
    /// 下拉列表：自定义名称(名称) - 版本号
    /// 下拉列表地址：/swagger/{Name}-v1.0/swagger.json
    /// </summary>
    public class ApiVersionInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title => $"{Group.Title} - {Version}";

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
    }
}
