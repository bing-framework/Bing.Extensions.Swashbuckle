using Bing.Samples.Api.Models;
using Bing.Swashbuckle.Attributes;
using Microsoft.AspNetCore.Http;
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
        [HttpPost]
        public Result Upload([FromForm]UploadSample sample)
        {
            return Result.Success(sample.Name);
        }

        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="collection">文件集合</param>
        [HttpPost("batchUpdate")]
        public Result BatchUpload([FromForm] IFormFileCollection collection)
        {
            return Result.Success(collection.Count);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sample">查询</param>
        [HttpGet]
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
        [SwaggerRequestHeader("clientid", "客户端ID", Default = "666", Required = true)]
        [SwaggerRequestHeader("channel", "渠道（小米、应用宝、百度等）", Default = "baidu", Required = true)]
        [SwaggerRequestHeader("platform", "平台(ios或android)", Default = "ios", Required = true)]
        [SwaggerRequestHeader("osversion", "安卓或苹果系统版本(例：8.0.1)", Default = "8.0.1", Required = true)]
        [HttpPost]
        public Result Post([FromBody] QuerySample sample)
        {
            return Result.Success(sample);
        }
    }
}