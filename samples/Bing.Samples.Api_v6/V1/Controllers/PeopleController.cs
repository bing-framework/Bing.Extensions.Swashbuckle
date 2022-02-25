using Bing.Samples.Api.V1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.V1.Controllers
{
    /// <summary>
    /// 人员 控制器
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("0.9", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PeopleController : ControllerBase
    {
        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="id">人标识</param>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id) => Ok(new Person()
        {
            Id = id,
            FirstName = "隔壁",
            LastName = "老王"
        });
    }
}
