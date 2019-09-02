using System;
using System.ComponentModel.DataAnnotations;

namespace Bing.Samples.Api.V2.Models
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
        /// 生效日期
        /// </summary>
        public DateTimeOffset EffectiveDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 客户
        /// </summary>
        [Required]
        public string Customer { get; set; }
    }
}