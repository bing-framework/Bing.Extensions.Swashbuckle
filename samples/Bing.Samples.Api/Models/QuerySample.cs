﻿using System;
using System.ComponentModel;
using Bing.Extensions.Swashbuckle.Attributes;

namespace Bing.Samples.Api.Models
{
    /// <summary>
    /// 查询例子
    /// </summary>
    public class QuerySample
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SwaggerIgnoreProperty]
        public string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 枚举例子
        /// </summary>
        public EnumSample EnumSample { get; set; }
    }

    /// <summary>
    /// 枚举例子
    /// </summary>
    public enum EnumSample
    {
        /// <summary>
        /// 老大
        /// </summary>
        [Description("老大")]
        One=1,
        /// <summary>
        /// 老二
        /// </summary>
        [Description("老二")]
        Two=2,
        /// <summary>
        /// 老三
        /// </summary>
        [Description("老三")]
        Three=3
    }
}
