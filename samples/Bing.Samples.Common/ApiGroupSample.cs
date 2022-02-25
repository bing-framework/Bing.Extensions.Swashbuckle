using Bing.Swashbuckle.Attributes;

namespace Bing.Samples.Common
{
    /// <summary>
    /// Api分组例子
    /// </summary>
    public enum ApiGroupSample
    {
        /// <summary>
        /// 案例
        /// </summary>
        [SwaggerApiGroupInfo(Title = "Demo模块", Description = "案例模块相关接口")]
        Demo,
        /// <summary>
        /// 测试
        /// </summary>
        [SwaggerApiGroupInfo(Title = "测试模块", Description = "测试相关接口")]
        Test,
        /// <summary>
        /// 订单
        /// </summary>
        [SwaggerApiGroupInfo(Title = "订单模块", Description = "订单模块相关接口")]
        Order,
        /// <summary>
        /// 人员
        /// </summary>
        [SwaggerApiGroupInfo(Title = "人员模块", Description = "人员模块相关接口")]
        Person,
    }
}
