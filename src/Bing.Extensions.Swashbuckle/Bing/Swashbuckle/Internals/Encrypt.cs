using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bing.Swashbuckle.Internals;

/// <summary>
/// 加密操作
/// </summary>
internal static class Encrypt
{
    #region HmacSha256加密

    /// <summary>
    /// HMACSHA256加密
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="key">密钥</param>
    public static string HmacSha256(string value, string key) => HmacSha256(value, key, Encoding.UTF8);

    /// <summary>
    /// HMACSHA256加密
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="key">密钥</param>
    /// <param name="encoding">字符编码</param>
    public static string HmacSha256(string value, string key, Encoding encoding)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            return string.Empty;
        var sha256 = new HMACSHA256(encoding.GetBytes(key));
        var hash = sha256.ComputeHash(encoding.GetBytes(value));
        return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
    }

    #endregion
}