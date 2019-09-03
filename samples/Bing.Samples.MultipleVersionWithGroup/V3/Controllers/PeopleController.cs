using System.Collections.Generic;
using Bing.Extensions.Swashbuckle.Attributes;
using Bing.Samples.MultipleVersionWithGroup.V3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.MultipleVersionWithGroup.V3.Controllers
{
    /// <summary>
    /// 人员 控制器
    /// </summary>
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [SwaggerApiGroup(ApiGroupSample.Person)]
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
                    Email = "gebilaowang@somewhere.com",
                    Phone = "12345678901"
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "隔壁",
                    LastName = "老萌",
                    Email = "gebilaomeng@somewhere.com",
                    Phone = "12345678902"
                },
                new Person()
                {
                    Id = 3,
                    FirstName = "隔壁",
                    LastName = "张三",
                    Email = "gebizhangsan@somewhere.com",
                    Phone = "12345678903"
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
            LastName = "张三",
            Email = "gebizhangsan@somewhere.com",
            Phone = "12345678903"
        });

        /// <summary>
        /// 创建人员
        /// </summary>
        /// <param name="person">人员</param>
        /// <param name="apiVersion">API版本</param>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Person), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Person person, ApiVersion apiVersion)
        {
            person.Id = 42;
            return CreatedAtAction(nameof(Get), new {id = person.Id, version = apiVersion.ToString()}, person);
        }
    }
}
