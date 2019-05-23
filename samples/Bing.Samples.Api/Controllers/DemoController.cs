using Bing.Samples.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// 案例 控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DemoController:Controller
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sample">上传信息</param>
        /// <returns></returns>
        [HttpPost]
        public Result Upload([FromForm]UploadSample sample)
        {
            return Result.Success(sample.Name);
        }
    }
}