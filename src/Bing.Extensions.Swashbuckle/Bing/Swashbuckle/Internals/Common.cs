using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bing.Extensions.Swashbuckle.Internal
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
                {
                    return string.Empty;
                }
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
        /// <returns></returns>
        public static Type GetType<T>()
        {
            return GetType(typeof(T));
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
