using System;
using System.Collections.Generic;
using Bing.Samples.Common;
using Bing.Samples.MultipleVersionWithGroup.V3.Models;
using Bing.Swashbuckle.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.MultipleVersionWithGroup.V3.Controllers
{
    /// <summary>
    /// 订单 控制器
    /// </summary>
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/[controller]")]
    [SwaggerApiGroup(ApiGroupSample.Order)]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var orders = new[]
            {
                new Order() {Id = 1, Customer = "隔壁老王"},
                new Order() {Id = 2, Customer = "隔壁老萌"},
                new Order() {Id = 3, Customer = "隔壁张三", EffectiveDate = DateTimeOffset.UtcNow.AddDays(7d)},
            };
            return Ok(orders);
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id">订单标识</param>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id) => Ok(new Order() { Id = id, Customer = "隔壁张三" });

        /// <summary>
        /// 创建一个新的订单
        /// </summary>
        /// <param name="order">订单</param>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(404)]
        public IActionResult Post([FromBody] Order order)
        {
            order.Id = 42;
            return CreatedAtAction(nameof(Get), new {id = order.Id}, order);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id">订单标识</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id) => NoContent();
    }
}
