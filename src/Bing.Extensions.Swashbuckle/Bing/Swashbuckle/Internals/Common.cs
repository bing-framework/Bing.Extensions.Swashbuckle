using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Swashbuckle.Internals
{
    /// <summary>
    /// 通用类
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// 加载内容
        /// </summary>
        /// <param name="resourceFile">资源文件</param>
        public static async Task<string> LoadContentAsync(string resourceFile)
        {
            using var stream = typeof(Common).GetTypeInfo().Assembly.GetManifestResourceStream($"Bing.Swashbuckle.Resources.{resourceFile}");
            if (stream == null)
                return string.Empty;
            using var sr = new StreamReader(stream, Encoding.UTF8);
            return await sr.ReadToEndAsync();
        }

        /// <summary>
        /// 获取语言资源文件
        /// </summary>
        /// <param name="language">语言</param>
        public static async Task<string> GetLanguageAsync(string language)
        {
            using var stream = typeof(Common).GetTypeInfo().Assembly.GetManifestResourceStream($"Bing.Swashbuckle.Resources.lang.{language}.js");
            if (stream == null)
                return string.Empty;
            using var sr = new StreamReader(stream, Encoding.UTF8);
            return await sr.ReadToEndAsync();
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>() => GetType(typeof(T));

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetType(Type type) => Nullable.GetUnderlyingType(type) ?? type;
    }
}
