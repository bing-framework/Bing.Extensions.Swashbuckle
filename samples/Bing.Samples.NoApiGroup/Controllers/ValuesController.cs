using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.NoApiGroup.Controllers
{
    /// <summary>
    /// 值 控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 获取列表【同步】
        /// </summary>
        [HttpGet("getBySource")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 获取列表【同步】
        /// </summary>
        [HttpGet("getByActionResult")]
        public ActionResult<IEnumerable<string>> GetByActionResult()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 获取列表【异步】
        /// </summary>
        [HttpGet("getByActionResultAsync")]
        public Task<ActionResult<IEnumerable<string>>> GetByActionResultAsync()
        {
            var result = new string[] { "value1", "value2" };
            return Task.FromResult<ActionResult<IEnumerable<string>>>(result);
        }

        /// <summary>
        /// 获取详情【同步】
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet("getBySource/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 获取详情【同步】
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet("getByActionResult/{id}")]
        public ActionResult<string> GetByActionResult(int id)
        {
            return "value";
        }

        /// <summary>
        /// 获取详情【异步】
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet("getByActionResultAsync/{id}")]
        public Task<ActionResult<string>> GetAsync(int id)
        {
            return Task.FromResult<ActionResult<string>>("value");
        }

        /// <summary>
        /// 新增【同步】
        /// </summary>
        /// <param name="value">值</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// 新增【异步】
        /// </summary>
        /// <param name="value">值</param>
        [HttpPost("postByAsync")]
        public Task PostAsync([FromBody] string value)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="value">值</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">标识</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
