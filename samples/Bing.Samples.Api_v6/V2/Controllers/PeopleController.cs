using System.Collections.Generic;
using Bing.Samples.Api.V2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.V2.Controllers
{
    /// <summary>
    /// 人员 控制器
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PeopleController : ControllerBase
    {
        /// <summary>
        /// 获取人员列表
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var peoples = new[]
            {
                new Person()
                {
                    Id = 1,
                    FirstName = "隔壁",
                    LastName = "老王",
                    Email = "gebilaowang@somewhere.com"
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "隔壁",
                    LastName = "老萌",
                    Email = "gebilaomeng@somewhere.com"
                },
                new Person()
                {
                    Id = 3,
                    FirstName = "隔壁",
                    LastName = "张三",
                    Email = "gebizhangsan@somewhere.com"
                },
            };
            return Ok(peoples);
        }

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
            LastName = "老萌",
            Email = "gebilaomeng@somewhere.com"
        });
    }
}
