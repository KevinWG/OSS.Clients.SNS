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

    public enum WxCardStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        CARD_STATUS_NOT_VERIFY = 10,

        /// <summary>
        /// 审核失败
        /// </summary>
        [Description("审核失败")]
        CARD_STATUS_VERIFY_FAIL = 20,

        /// <summary>
        /// 通过审核
        /// </summary>
        [Description("通过审核")]
        CARD_STATUS_VERIFY_OK = 30,
        /// <summary>
        /// 卡券被商户删除
        /// </summary>
        [Description("卡券被商户删除")]
        CARD_STATUS_DELETE = 40,
        /// <summary>
        /// 在公众平台投放过的卡券
        /// </summary>
        [Description("在公众平台投放过的卡券")]
        CARD_STATUS_DISPATCH = 50,

    }

    /// <summary>
    ///   卡券code类型
    /// </summary>
    public enum WxCardCodeType
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
        [Description("优惠券")] GENERAL_COUPON = 50,

        /// <summary>
        /// 会员卡
        /// </summary>
        [Description("会员卡")] MEMBER_CARD = 60,

        /// <summary>
        /// 景点门票
        /// </summary>
        [Description("景点门票")] SCENIC_TICKET = 70,

        /// <summary>
        /// 电影票
        /// </summary>
        [Description("电影票")] MOVIE_TICKET = 80,

        /// <summary>
        /// 飞机票
        /// </summary>
        [Description("飞机票")] BOARDING_PASS = 90,

        /// <summary>
        /// 会议门票
        /// </summary>
        [Description("会议门票")] MEETING_TICKET = 100,

        /// <summary>
        /// 会议门票
        /// </summary>
        [Description("汽车票")] BUS_TICKET = 110

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
    {     /// <summary>
          /// 固定日期区间
          /// </summary>
        [Description("固定日期区间")] DATE_TYPE_FIX_TIME_RANGE = 1,
        /// <summary>
        /// 固定时长
        /// </summary>
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
        /// <summary>
        /// 外卖服务
        /// </summary>
        [Description("外卖服务")] BIZ_SERVICE_DELIVER = 10,

        /// <summary>
        /// 停车位
        /// </summary>
        [Description("停车位")] BIZ_SERVICE_FREE_PARK = 20,

        /// <summary>
        /// 可带宠物
        /// </summary>
        [Description("可带宠物")] BIZ_SERVICE_WITH_PET = 30,

        /// <summary>
        /// 免费wifi
        /// </summary>
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


    public enum WxCardLandPageSence
    {
        /// <summary>
        /// 附近
        /// </summary>
        [Description("附近")] SCENE_NEAR_BY = 10,

        /// <summary>
        /// 自定义菜单
        /// </summary>
        [Description("自定义菜单")] SCENE_MENU = 20,

        /// <summary>
        /// 二维码
        /// </summary>
        [Description("二维码")] SCENE_QRCODE = 30,

        /// <summary>
        /// 公众号文章
        /// </summary>
        [Description("公众号文章")] SCENE_ARTICLE = 40,

        /// <summary>
        /// h5页面
        /// </summary>
        [Description("h5页面")] SCENE_H5 = 50,

        /// <summary>
        /// 自动回复
        /// </summary>
        [Description("自动回复")] SCENE_IVR = 60,

        /// <summary>
        /// 卡券自定义cell
        /// </summary>
        [Description("卡券自定义cell")] SCENE_CARD_CUSTOM_CELL = 70

    }


    public enum WxMemberCardCustomNameType
    {
        /// <summary>
        /// 等级
        /// </summary>
        [Description("等级")] FIELD_NAME_TYPE_LEVEL = 10,

        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")] FIELD_NAME_TYPE_COUPON = 20,

        /// <summary>
        /// 印花
        /// </summary>
        [Description("印花")] FIELD_NAME_TYPE_STAMP = 30,

        /// <summary>
        /// 折扣
        /// </summary>
        [Description("折扣")] FIELD_NAME_TYPE_DISCOUNT = 40,

        /// <summary>
        /// 成就
        /// </summary>
        [Description("成就")] FIELD_NAME_TYPE_ACHIEVEMEN = 50,

        /// <summary>
        /// 里程
        /// </summary>
        [Description("里程")] FIELD_NAME_TYPE_MILEAGE = 60,

        /// <summary>
        /// 集点
        /// </summary>
        [Description("集点")] FIELD_NAME_TYPE_SET_POINTS = 70,

        /// <summary>
        /// 次数
        /// </summary>
        [Description("次数")] FIELD_NAME_TYPE_TIMS = 80,
    }


    public enum WxCardCodeUseState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        NORMAL = 10,

        /// <summary>
        /// 已核销
        /// </summary>
        [Description("已核销")]
        CONSUMED = 20,

        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        EXPIRE = 30,

        /// <summary>
        /// 转赠中
        /// </summary>
        [Description("转赠中")]
        GIFTING = 15,

        /// <summary>
        /// 转赠超时
        /// </summary>
        [Description("转赠超时")]
        GIFT_TIMEOUT = 18,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        DELETE = -10,

        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        UNAVAILABLE = -1

    }


}
