using Bing.Extensions.Swashbuckle.Attributes;

namespace Bing.Samples.Api.Models
{
    /// <summary>
    /// 分组例子
    /// </summary>
    public enum GroupSample
    {
        /// <summary>
        /// 登录
        /// </summary>
        [SwaggerApiGroupInfo(Title = "登录模块",Description = "登录模块相关接口",Version = "v1")]
        Login,
        /// <summary>
        /// 测试
        /// </summary>
        [SwaggerApiGroupInfo(Title = "测试模块",Description = "测试相关接口")]
        Test,
        /// <summary>
        /// 案例
        /// </summary>
        [SwaggerApiGroupInfo(Title = "Demo模块",Description = "案例模块相关接口")]
        Demo
    }
}
