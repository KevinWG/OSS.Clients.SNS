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
        [Description("二维码显示code")] CODE_TYPE_QRCODE = 10,

        /// <summary>
        /// 一维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        [Description("一维码显示code")] CODE_TYPE_BARCODE = 20,

        /// <summary>
        /// 二维码不显示code
        /// CODE_TYPE_ONLY_QRCODE
        /// </summary>
        [Description("二维码不显示code")] CODE_TYPE_ONLY_QRCODE = 30,

        /// <summary>
        /// 仅code类型
        /// 仅适用于输码核销
        /// </summary>
        [Description("仅code类型")] CODE_TYPE_TEXT = 40,

        /// <summary>
        /// 无code类型
        /// 仅适用于线上核销，开发者须自定义跳转链接跳转至H5页面，允许用户核销掉卡券，自定义cell的名称可以命名为“立即使用"
        /// </summary>
        [Description("无code类型")] CODE_TYPE_NONE = 50
    }

    /// <summary>
    ///  微信卡券类型
    /// </summary>
    public enum WxCardType
    {
        /// <summary>
        /// 团购券
        /// </summary>
        [Description("团购券")] GROUPON = 10,

        /// <summary>
        /// 代金券
        /// </summary>
        [Description("代金券")] CASH = 20,

        /// <summary>
        /// 折扣券
        /// </summary>
        [Description("折扣券")] DISCOUNT = 30,


        /// <summary>
        /// 兑换券
        /// </summary>
        [Description("兑换券")] GIFT = 40,


        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")] GENERAL_COUPON = 50
    }

    /// <summary>
    ///   卡券背景颜色值
    /// </summary>
    public enum WxCardColor
    {
        [Description("#63b359")] Color010 = 10,
        [Description("#2c9f67")] Color020 = 20,
        [Description("#509fc9")] Color030 = 30,
        [Description("#5885cf")] Color040 = 40,
        [Description("#9062c0")] Color050 = 50,
        [Description("#d09a45")] Color060 = 60,
        [Description("#e4b138")] Color070 = 70,
        [Description("#ee903c")] Color080 = 80,
        [Description("#f08500")] Color081 = 81,
        [Description("#a9d92d")] Color082 = 82,
        [Description("#dd6549")] Color090 = 90,
        [Description("#cc463d")] Color100 = 100,
        [Description("#cf3e36")] Color101 = 101,
        [Description("#5E6671")] Color102 = 102
    }


    /// <summary>
    ///   卡券时效类型
    /// </summary>
    public enum WxCardDateType
    {
        [Description("固定日期区间")] DATE_TYPE_FIX_TIME_RANGE = 1,

        [Description("固定时长")] DATE_TYPE_FIX_TERM = 2
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
        [Description("外卖服务")] BIZ_SERVICE_DELIVER = 10,
        [Description("停车位")] BIZ_SERVICE_FREE_PARK = 20,
        [Description("可带宠物")] BIZ_SERVICE_WITH_PET = 30,
        [Description("免费wifi")] BIZ_SERVICE_FREE_WIFI = 40,
    }


    public enum WxCardTimeLimitType
    {
        [Description("周一")] MONDAY = 10,
        [Description("周二")] TUESDAY = 20,
        [Description("周三")] WEDNESDAY = 30,
        [Description("周四")] THURSDAY = 40,
        [Description("周五")] FRIDAY = 50,
        [Description("周六")] SATURDAY = 60,
        [Description("周日")] SUNDAY = 70,
        [Description("假日")] HOLIDAY = 80
    }

}
