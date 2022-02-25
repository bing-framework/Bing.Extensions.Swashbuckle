using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// Swagger资源控制器
    /// </summary>
    [ApiController]
    [Route("swagger-resources")]
    public class SwaggerResourcesController : Controller
    {
        /// <summary>
        /// 获取接口列表
        /// </summary>
        [HttpGet]
        public IActionResult GetList()
        {
            return new JsonResult(new List<dynamic>()
            {
                new
                {
                    name="0.9默认接口",
                    url="/swagger/v0.9/swagger.json",
                    swaggerVersion="2.0",
                    location="/swagger/v0.9/swagger.json"
                },
                new
                {
                    name="默认接口",
                    url="/swagger/v1.0/swagger.json",
                    swaggerVersion="2.0",
                    location="/swagger/v1.0/swagger.json"
                },
                new
                {
                    name="2.0默认接口",
                    url="/swagger/v2.0/swagger.json",
                    swaggerVersion="2.0",
                    location="/swagger/v2.0/swagger.json"
                },
                new
                {
                    name="3.0默认接口",
                    url="/swagger/v3.0/swagger.json",
                    swaggerVersion="2.0",
                    location="/swagger/v3.0/swagger.json"
                },
            });
        }

        /// <summary>
        /// 获取UI配置
        /// </summary>
        [HttpGet("configuration/ui")]
        // ReSharper disable once InconsistentNaming
        public IActionResult GetUIConfig()
        {
            return new JsonResult(new
            {
                deepLinking = true,
                displayOperationId = false,
                defaultModelsExpandDepth = 1,
                defaultModelExpandDepth = 1,
                defaultModelRendering = "example",
                displayRequestDuration = false,
                docExpansion = "none",
                filter = false,
                operationsSorter = "alpha",
                showExtensions = false,
                tagsSorter = "alpha",
                validatorUrl = "",
                apisSorter = "alpha",
                jsonEditor = false,
                showRequestHeaders = false,
                supportedSubmitMethods = new[] { "get", "put", "post", "delete", "options", "head", "patch", "trace" }
            });
        }
    }
}
