using System.Collections.Generic;
using System.ComponentModel;

namespace Bing.Samples.Api.Models.Responses
{
    /// <summary>
    /// 枚举字典响应
    /// </summary>
    public class EnumDictionaryResponse
    {
        /// <summary>
        /// 单据状态
        /// </summary>
        public Dictionary<SendNoticeOrderStatus, int> StatusDic { get; set; }

        /// <summary>
        /// 异常状态
        /// </summary>
        public Dictionary<SendOrderErrorCode, int> ErrorCodeDic { get; set; }
    }

    /// <summary>
    /// 发货状态枚举
    /// </summary>
    public enum SendNoticeOrderStatus
    {
        /// <summary>
        /// 待组合
        /// </summary>
        [Description("待组合")]
        WaitForCombination = 0,

        /// <summary>
        /// 待拣货
        /// </summary>
        [Description("待拣货")]
        WaitForPicking = 1,

        /// <summary>
        /// 拣货中
        /// </summary>
        [Description("拣货中")]
        Picking = 2,

        /// <summary>
        /// 待打包
        /// </summary>
        [Description("待打包")]
        WaitForPacking = 3,

        /// <summary>
        /// 待发货
        /// </summary>
        [Description("待发货")]
        Packed = 4,

        /// <summary>
        /// 部分发货
        /// </summary>
        [Description("部分发货")]
        PartialSent = 6,

        /// <summary>
        /// 全部发货
        /// </summary>
        [Description("全部发货")]
        Sent = 7,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Cancel = 8,

        /// <summary>
        /// 通知电商中
        /// </summary>
        [Description("通知电商中")]
        NoticingEShop = 9,

        /// <summary>
        /// 通知TMS中
        /// </summary>
        [Description("通知TMS中")]
        NoticingTms = 10

    }

    /// <summary>
    /// 发货单异常枚举
    /// </summary>
    public enum SendOrderErrorCode
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 系统缺货
        /// </summary>
        [Description("系统缺货")]
        SysQtyShort = 1,

        /// <summary>
        /// 实物缺货
        /// </summary>
        [Description("实物缺货")]
        RealQtyShort = 2,
    }
}
