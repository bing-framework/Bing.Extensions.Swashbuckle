using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bing.Samples.Api.V1.Models
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 客户
        /// </summary>
        [Required]
        public string Customer { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    /// <summary>
    /// 订单项
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 产品标识
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
    }
}