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
            using (var stream = typeof(Common).GetTypeInfo().Assembly.GetManifestResourceStream($"Bing.Extensions.Swashbuckle.Resources.{resourceFile}"))
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
    }
}
