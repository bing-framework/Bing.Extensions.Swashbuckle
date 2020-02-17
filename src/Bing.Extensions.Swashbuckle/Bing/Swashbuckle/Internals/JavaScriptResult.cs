using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Extensions.Swashbuckle.Internal
{
    /// <summary>
    /// Js结果
    /// </summary>
    public class JavaScriptResult:ContentResult
    {
        /// <summary>
        /// 初始化一个<see cref="JavaScriptResult"/>类型的实例
        /// </summary>
        /// <param name="script">js</param>
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }
}
