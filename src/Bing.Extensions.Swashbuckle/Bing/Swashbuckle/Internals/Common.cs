using System;
using System.IO;
using System.Reflection;
using System.Text;

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
        /// <returns></returns>
        public static string LoadContent(string resourceFile)
        {
            using (var stream = typeof(Common).GetTypeInfo().Assembly.GetManifestResourceStream($"Bing.Swashbuckle.Resources.{resourceFile}"))
            {
                if (stream == null)
                    return string.Empty;
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
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
