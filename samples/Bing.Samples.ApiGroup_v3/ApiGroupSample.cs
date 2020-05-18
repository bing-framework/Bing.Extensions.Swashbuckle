using Bing.Swashbuckle.Attributes;

namespace Bing.Samples.ApiGroup
{
    /// <summary>
    /// Api分组例子
    /// </summary>
    public enum ApiGroupSample
    {
        /// <summary>
        /// 案例
        /// </summary>
        [SwaggerApiGroupInfo(Title = "Demo模块",Description = "案例模块相关接口")]
        Demo,
        /// <summary>
        /// 测试
        /// </summary>
        [SwaggerApiGroupInfo(Title = "测试模块",Description = "测试相关接口")]
        Test
    }
}
