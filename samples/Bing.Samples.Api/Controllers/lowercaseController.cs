using System.Collections.Generic;
using Bing.Extensions.Swashbuckle.Attributes;
using Bing.Samples.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// 小写控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerApiGroup(GroupSample.Demo)]
    [SwaggerApiGroup(GroupSample.Test)]
    [ApiVersionNeutral]
    public class lowercaseController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [SwaggerApiGroup(GroupSample.Login)]
        public void Delete(int id)
        {
        }
    }
}
