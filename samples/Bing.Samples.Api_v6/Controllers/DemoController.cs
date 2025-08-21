using Bing.Samples.Api.Models;
using Bing.Swashbuckle.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// 案例 控制器
    /// </summary>
    [ApiController]
    [SwaggerApiGroup(GroupSample.Demo)]
    [Route("api/[controller]/[action]")]
    public class DemoController : Controller
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="sample">上传信息</param>
        /// <remarks>
        /// 测试备注信息
        /// </remarks>
        [HttpPost]
        [ApiLastModified("2024-01-01","隔壁老汪")]
        public Result Upload([FromForm]UploadSample sample)
        {
            return Result.Success(sample.Name);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sample">查询</param>
        [HttpGet]
        [ApiLastModified("2024-09-01", "隔壁老蟹")]
        public virtual Result Query([FromQuery] QuerySample sample)
        {
            return Result.Success(sample);
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="q">字符串</param>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="enumSample">枚举例子</param>
        [HttpGet]
        [ApiLastModified("2024-09-27")]
        public virtual Result GetDefaultValue([FromQuery] string q, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery]EnumSample enumSample = EnumSample.Two)
        {
            return Result.Success(new
            {
                q,
                page,
                pageSize
            });
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sample">查询例子</param>
        [HttpPost]
        public Result Post([FromBody] QuerySample sample)
        {
            return Result.Success(sample);
        }
    }
}