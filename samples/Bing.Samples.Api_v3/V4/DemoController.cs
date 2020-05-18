using Bing.Samples.Api.Models;
using Bing.Samples.Api.V4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.V4
{
    /// <summary>
    /// 案例 控制器
    /// </summary>
    [ApiController]
    [ApiVersion("4.0")]
    [Route("api/[controller]")]
    public class DemoController: ControllerBase
    {
        /// <summary>
        /// 测试 Post 请求枚举
        /// </summary>
        /// <param name="sample">Post枚举例子</param>
        [HttpPost("testPostEnum")]
        public Result TestPostEnum([FromBody] PostEnumSample sample)
        {
            return Result.Success(sample);
        }
    }
}
