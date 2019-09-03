using Bing.Extensions.Swashbuckle.Attributes;

namespace Bing.Samples.MultipleVersionWithGroup
{
    /// <summary>
    /// Api分组例子
    /// </summary>
    public enum ApiGroupSample
    {
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
