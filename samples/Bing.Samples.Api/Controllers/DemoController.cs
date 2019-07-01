using Bing.Extensions.Swashbuckle.Attributes;
using Bing.Samples.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// 案例 控制器
    /// </summary>
    [ApiController]
    [SwaggerApiGroup(GroupSample.Demo)]
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sample">查询</param>
        /// <returns></returns>
        [HttpGet]
        public virtual Result Query([FromQuery] QuerySample sample)
        {
            return Result.Success(sample);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Post([FromBody] QuerySample sample)
        {
            return Result.Success(sample);
        }
    }
}