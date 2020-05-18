using Bing.Samples.Api.V1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Api.V1.Controllers
{
    /// <summary>
    /// 订单 控制器
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("0.9", Deprecated = true)]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id">订单标识</param>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id) => Ok(new Order() { Id = id, Customer = "隔壁老王" });

        /// <summary>
        /// 创建一个新的订单
        /// </summary>
        /// <param name="order">订单</param>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Order order)
        {
            order.Id = 42;
            return CreatedAtAction(nameof(Get), new {id = order.Id}, order);
        }
    }
}
