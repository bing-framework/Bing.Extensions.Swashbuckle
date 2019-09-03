using System.Collections.Generic;
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
        /// 获取列表
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">标识</param>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value">值</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
