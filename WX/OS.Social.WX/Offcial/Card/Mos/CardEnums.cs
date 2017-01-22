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

namespace OS.Social.WX.Offcial.Card.Mos
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
        [Description("二维码显示code")] CODE_TYPE_QRCODE,

        /// <summary>
        /// 一维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        [Description("一维码显示code")] CODE_TYPE_BARCODE,

        /// <summary>
        /// 二维码不显示code
        /// CODE_TYPE_ONLY_QRCODE
        /// </summary>
        [Description("二维码不显示code")] CODE_TYPE_ONLY_QRCODE,

        /// <summary>
        /// 仅code类型
        /// 仅适用于输码核销
        /// </summary>
        [Description("仅code类型")] CODE_TYPE_TEXT,

        /// <summary>
        /// 无code类型
        /// 仅适用于线上核销，开发者须自定义跳转链接跳转至H5页面，允许用户核销掉卡券，自定义cell的名称可以命名为“立即使用"
        /// </summary>
        [Description("无code类型")] CODE_TYPE_NONE
    }

    /// <summary>
    ///  微信卡券类型
    /// </summary>
    public enum WxCardType
    {
        /// <summary>
        /// 团购券
        /// </summary>
        [Description("团购券")] GROUPON,

        /// <summary>
        /// 代金券
        /// </summary>
        [Description("代金券")] CASH,

        /// <summary>
        /// 折扣券
        /// </summary>
        [Description("折扣券")] DISCOUNT,


        /// <summary>
        /// 兑换券
        /// </summary>
        [Description("兑换券")] GIFT,


        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")] GENERAL_COUPON
    }

    /// <summary>
    ///   卡券背景颜色值
    /// </summary>
    public enum WxCardColor
    {
        [Description("#63b359")] Color010,
        [Description("#2c9f67")] Color020,
        [Description("#509fc9")] Color030,
        [Description("#5885cf")] Color040,
        [Description("#9062c0")] Color050,
        [Description("#d09a45")] Color060,
        [Description("#e4b138")] Color070,
        [Description("#ee903c")] Color080,
        [Description("#f08500")] Color081,
        [Description("#a9d92d")] Color082,
        [Description("#dd6549")] Color090,
        [Description("#cc463d")] Color100,
        [Description("#cf3e36")] Color101,
        [Description("#5E6671")] Color102,
    }


    /// <summary>
    ///   卡券时效类型
    /// </summary>
    public enum WxCardDateType
    {
        [Description("固定日期区间")]
        DATE_TYPE_FIX_TIME_RANGE=1,
        [Description("固定时长")]
        DATE_TYPE_FIX_TERM =2
    }



    public enum WxCardCustomCodeMode
    {
        GET_CUSTOM_CODE_MODE_DEPOSIT
    }

    /// <summary>
    ///   商家服务类型
    /// </summary>
    public enum WxCardBusinessService
    {
        [Description("外卖服务")] BIZ_SERVICE_DELIVER,
        [Description("停车位")] BIZ_SERVICE_FREE_PARK,
        [Description("可带宠物")] BIZ_SERVICE_WITH_PET,
        [Description("免费wifi")] BIZ_SERVICE_FREE_WIFI,
    }


    public enum WxCardTimeLimitType
    {
        [Description("周一")] MONDAY,
        [Description("周二")] TUESDAY,
        [Description("周三")] WEDNESDAY,
        [Description("周四")] THURSDAY,
        [Description("周五")] FRIDAY,
        [Description("周六")] SATURDAY,
        [Description("周日")] SUNDAY,
        [Description("假日")] HOLIDAY
    }

}
