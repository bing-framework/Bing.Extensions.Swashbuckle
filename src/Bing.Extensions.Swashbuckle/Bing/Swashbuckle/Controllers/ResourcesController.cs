using Bing.Swashbuckle.Internals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Swashbuckle.Controllers
{
    /// <summary>
    /// 资源 控制器
    /// </summary>
    [AllowAnonymous]
    [Route("swagger/resources")]
    public class ResourcesController: Controller
    {
        /// <summary>
        /// 获取翻译器资源
        /// </summary>
        /// <returns></returns>
        [HttpGet("translator")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public JavaScriptResult Translator()
        {
            return new JavaScriptResult(Common.LoadContent("translator.js"));
        }

        /// <summary>
        /// 获取jquery
        /// </summary>
        /// <returns></returns>
        [HttpGet("jquery")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public JavaScriptResult JQuery()
        {
            return new JavaScriptResult(Common.LoadContent("jquery.min.js"));
        }

        /// <summary>
        /// 获取导出资源
        /// </summary>
        /// <returns></returns>
        [HttpGet("export")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public JavaScriptResult Export()
        {
            return new JavaScriptResult(Common.LoadContent("export.js"));
        }

        /// <summary>
        /// 获取导出资源
        /// </summary>
        /// <returns></returns>
        [HttpGet("swagger-common")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public CssResult SwaggerCommon()
        {
            return new CssResult(Common.LoadContent("swagger-common.css"));
        }
    }
}
