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
        [SwaggerApiGroupInfo(Title = "登录模块",Name = "v1.0",Description = "登录模块相关接口", Version = "1.0")]
        Login,
        /// <summary>
        /// 测试
        /// </summary>
        [SwaggerApiGroupInfo(Title = "测试模块", Name = "v2.0", Description = "测试相关接口", Version = "2.0")]
        Test,
        /// <summary>
        /// 案例
        /// </summary>
        [SwaggerApiGroupInfo(Title = "Demo模块", Name = "v3.0", Description = "案例模块相关接口", Version = "3.0")]
        Demo
    }
}
