namespace Bing.Swashbuckle;

/// <summary>
/// 令牌存储参数。用于拦截登录后存储令牌，解决刷新页面导致令牌丢失问题
/// </summary>
internal class TokenStorageParameter
{
    /// <summary>
    /// 授权定义。对应于 AddSecurityDefinition 中的 name
    /// </summary>
    public string SecurityDefinition { get; set; }

    /// <summary>
    /// 网页缓存类型
    /// </summary>
    public WebCacheType CacheType { get; set; }
}

/// <summary>
/// 网页缓存方式
/// </summary>
public enum WebCacheType
{
    /// <summary>
    /// 本地存储
    /// </summary>
    Local,
    /// <summary>
    /// 会话
    /// </summary>
    Session,
}