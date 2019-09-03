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
        public string GetName()
        {
            if (Name == Group.Name)
                return Name;
            return $"{Group.Name}{Name}";
        }
    }
}
