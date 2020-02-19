namespace Bing.Knife4jUI
{
    /// <summary>
    /// Knife4j 配置
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Knife4jUIOptions
    {
        /// <summary>
        /// 路由前缀
        /// </summary>
        public string RoutePrefix { get; set; } = "swagger";

        /// <summary>
        /// 配置对象
        /// </summary>
        public Knife4jUIConfigObject ConfigObject { get; set; } = new Knife4jUIConfigObject();
    }
}
