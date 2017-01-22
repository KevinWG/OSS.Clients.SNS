#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：微信的卡券枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion
using System.ComponentModel;

namespace OS.Social.WX.SysUtils.Mos
{
    /// <summary>
    ///   卡券code类型
    /// </summary>
    public enum WxSCardCodeType
    {
        /// <summary>
        /// 二维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        [Description("二维码显示code")]
        CODE_TYPE_QRCODE,

        /// <summary>
        /// 一维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        [Description("一维码显示code")]
        CODE_TYPE_BARCODE,

        /// <summary>
        /// 二维码不显示code
        /// CODE_TYPE_ONLY_QRCODE
        /// </summary>
        [Description("二维码不显示code")]
        CODE_TYPE_ONLY_QRCODE,

        /// <summary>
        /// 仅code类型
        /// 仅适用于输码核销
        /// </summary>
        [Description("仅code类型")]
        CODE_TYPE_TEXT,

        /// <summary>
        /// 无code类型
        /// 仅适用于线上核销，开发者须自定义跳转链接跳转至H5页面，允许用户核销掉卡券，自定义cell的名称可以命名为“立即使用"
        /// </summary>
        [Description("无code类型")]
        CODE_TYPE_NONE
    }

    /// <summary>
    ///  微信卡券类型
    /// </summary>
    public enum WxCardType
    {
        /// <summary>
        /// 团购券
        /// </summary>
        [Description("团购券")]
        GROUPON,

        /// <summary>
        /// 代金券
        /// </summary>
        [Description("代金券")]
        CASH,

        /// <summary>
        /// 折扣券
        /// </summary>
        [Description("折扣券")]
        DISCOUNT,


        /// <summary>
        /// 兑换券
        /// </summary>
        [Description("兑换券")]
        GIFT,


        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")]
        GENERAL_COUPON
    }
}
