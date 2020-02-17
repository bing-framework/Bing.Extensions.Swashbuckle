using Bing.Extensions.Swashbuckle.Internal;

namespace Bing.Extensions.Swashbuckle.Core
{
    /// <summary>
    /// 自定义Swagger授权
    /// </summary>
    public class CustomSwaggerAuthorization
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 授权令牌
        /// </summary>
        public string Token => Encrypt.HmacSha256($"{UserName}{Password}", "Bing.Extensions.Swashbuckle");

        /// <summary>
        /// 初始化一个<see cref="CustomSwaggerAuthorization"/>类型的实例
        /// </summary>
        public CustomSwaggerAuthorization() { }

        /// <summary>
        /// 初始化一个<see cref="CustomSwaggerAuthorization"/>类型的实例
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public CustomSwaggerAuthorization(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
