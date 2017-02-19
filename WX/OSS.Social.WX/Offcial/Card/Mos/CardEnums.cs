#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：微信的卡券枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using OSS.Common.Extention;

namespace OSS.Social.WX.Offcial.Card.Mos
{

    /// <summary>
    /// 卡券状态
    /// </summary>
    public enum WxCardStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [OSDescript("待审核")]
        CARD_STATUS_NOT_VERIFY = 10,

        /// <summary>
        /// 审核失败
        /// </summary>
        [OSDescript("审核失败")]
        CARD_STATUS_VERIFY_FAIL = 20,

        /// <summary>
        /// 通过审核
        /// </summary>
        [OSDescript("通过审核")]
        CARD_STATUS_VERIFY_OK = 30,
        /// <summary>
        /// 卡券被商户删除
        /// </summary>
        [OSDescript("卡券被商户删除")]
        CARD_STATUS_DELETE = 40,
        /// <summary>
        /// 在公众平台投放过的卡券
        /// </summary>
        [OSDescript("在公众平台投放过的卡券")]
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
        [OSDescript("二维码显示code")] CODE_TYPE_QRCODE = 10,

        /// <summary>
        /// 一维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        [OSDescript("一维码显示code")] CODE_TYPE_BARCODE = 20,

        /// <summary>
        /// 二维码不显示code
        /// CODE_TYPE_ONLY_QRCODE
        /// </summary>
        [OSDescript("二维码不显示code")] CODE_TYPE_ONLY_QRCODE = 30,

        /// <summary>
        /// 仅code类型
        /// 仅适用于输码核销
        /// </summary>
        [OSDescript("仅code类型")] CODE_TYPE_TEXT = 40,

        /// <summary>
        /// 无code类型
        /// 仅适用于线上核销，开发者须自定义跳转链接跳转至H5页面，允许用户核销掉卡券，自定义cell的名称可以命名为“立即使用"
        /// </summary>
        [OSDescript("无code类型")] CODE_TYPE_NONE = 50
    }

    /// <summary>
    ///  微信卡券类型
    /// </summary>
    public enum WxCardType
    {
        /// <summary>
        /// 折扣券
        /// </summary>
        [OSDescript("折扣券")] DISCOUNT = 0,  //  官方数值
        /// <summary>
        /// 代金券
        /// </summary>
        [OSDescript("代金券")]
        CASH = 1,    //  官方数值

        /// <summary>
        /// 礼品券
        /// </summary>
        [OSDescript("礼品券")] GIFT = 2,   //  官方数值

        /// <summary>
        /// 优惠券
        /// </summary>
        [OSDescript("优惠券")] GENERAL_COUPON = 3,  //  官方数值

        /// <summary>
        /// 团购券
        /// </summary>
        [OSDescript("团购券")]
        GROUPON = 4,     //  官方数值

        /// <summary>
        /// 会员卡
        /// </summary>
        [OSDescript("会员卡")] MEMBER_CARD = 60,

        /// <summary>
        /// 景点门票
        /// </summary>
        [OSDescript("景点门票")] SCENIC_TICKET = 70,

        /// <summary>
        /// 电影票
        /// </summary>
        [OSDescript("电影票")] MOVIE_TICKET = 80,

        /// <summary>
        /// 飞机票
        /// </summary>
        [OSDescript("飞机票")] BOARDING_PASS = 90,

        /// <summary>
        /// 会议门票
        /// </summary>
        [OSDescript("会议门票")] MEETING_TICKET = 100,

        /// <summary>
        /// 汽车票
        /// </summary>
        [OSDescript("汽车票")] BUS_TICKET = 110

    }

    /// <summary>
    ///   卡券背景颜色值
    /// </summary>
    public enum WxCardColor
    {
        [OSDescript("#63b359")] Color010 = 10,
        [OSDescript("#2c9f67")] Color020 = 20,
        [OSDescript("#509fc9")] Color030 = 30,
        [OSDescript("#5885cf")] Color040 = 40,
        [OSDescript("#9062c0")] Color050 = 50,
        [OSDescript("#d09a45")] Color060 = 60,
        [OSDescript("#e4b138")] Color070 = 70,
        [OSDescript("#ee903c")] Color080 = 80,
        [OSDescript("#f08500")] Color081 = 81,
        [OSDescript("#a9d92d")] Color082 = 82,
        [OSDescript("#dd6549")] Color090 = 90,
        [OSDescript("#cc463d")] Color100 = 100,
        [OSDescript("#cf3e36")] Color101 = 101,
        [OSDescript("#5E6671")] Color102 = 102
    }


    /// <summary>
    ///   卡券时效类型
    /// </summary>
    public enum WxCardDateType
    {     /// <summary>
          /// 固定日期区间
          /// </summary>
        [OSDescript("固定日期区间")] DATE_TYPE_FIX_TIME_RANGE = 1,
        /// <summary>
        /// 固定时长
        /// </summary>
        [OSDescript("固定时长")] DATE_TYPE_FIX_TERM = 2
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
        [OSDescript("外卖服务")] BIZ_SERVICE_DELIVER = 10,

        /// <summary>
        /// 停车位
        /// </summary>
        [OSDescript("停车位")] BIZ_SERVICE_FREE_PARK = 20,

        /// <summary>
        /// 可带宠物
        /// </summary>
        [OSDescript("可带宠物")] BIZ_SERVICE_WITH_PET = 30,

        /// <summary>
        /// 免费wifi
        /// </summary>
        [OSDescript("免费wifi")] BIZ_SERVICE_FREE_WIFI = 40,
    }


    public enum WxCardTimeLimitType
    {
        [OSDescript("周一")] MONDAY = 10,
        [OSDescript("周二")] TUESDAY = 20,
        [OSDescript("周三")] WEDNESDAY = 30,
        [OSDescript("周四")] THURSDAY = 40,
        [OSDescript("周五")] FRIDAY = 50,
        [OSDescript("周六")] SATURDAY = 60,
        [OSDescript("周日")] SUNDAY = 70,
        [OSDescript("假日")] HOLIDAY = 80
    }


    public enum WxCardLandPageSence
    {
        /// <summary>
        /// 附近
        /// </summary>
        [OSDescript("附近")] SCENE_NEAR_BY = 10,

        /// <summary>
        /// 自定义菜单
        /// </summary>
        [OSDescript("自定义菜单")] SCENE_MENU = 20,

        /// <summary>
        /// 二维码
        /// </summary>
        [OSDescript("二维码")] SCENE_QRCODE = 30,

        /// <summary>
        /// 公众号文章
        /// </summary>
        [OSDescript("公众号文章")] SCENE_ARTICLE = 40,

        /// <summary>
        /// h5页面
        /// </summary>
        [OSDescript("h5页面")] SCENE_H5 = 50,

        /// <summary>
        /// 自动回复
        /// </summary>
        [OSDescript("自动回复")] SCENE_IVR = 60,

        /// <summary>
        /// 卡券自定义cell
        /// </summary>
        [OSDescript("卡券自定义cell")] SCENE_CARD_CUSTOM_CELL = 70

    }


    public enum WxMemberCardCustomNameType
    {
        /// <summary>
        /// 等级
        /// </summary>
        [OSDescript("等级")] FIELD_NAME_TYPE_LEVEL = 10,

        /// <summary>
        /// 优惠券
        /// </summary>
        [OSDescript("优惠券")] FIELD_NAME_TYPE_COUPON = 20,

        /// <summary>
        /// 印花
        /// </summary>
        [OSDescript("印花")] FIELD_NAME_TYPE_STAMP = 30,

        /// <summary>
        /// 折扣
        /// </summary>
        [OSDescript("折扣")] FIELD_NAME_TYPE_DISCOUNT = 40,

        /// <summary>
        /// 成就
        /// </summary>
        [OSDescript("成就")] FIELD_NAME_TYPE_ACHIEVEMEN = 50,

        /// <summary>
        /// 里程
        /// </summary>
        [OSDescript("里程")] FIELD_NAME_TYPE_MILEAGE = 60,

        /// <summary>
        /// 集点
        /// </summary>
        [OSDescript("集点")] FIELD_NAME_TYPE_SET_POINTS = 70,

        /// <summary>
        /// 次数
        /// </summary>
        [OSDescript("次数")] FIELD_NAME_TYPE_TIMS = 80,
    }


    public enum WxCardCodeUseState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [OSDescript("正常")]
        NORMAL = 10,

        /// <summary>
        /// 已核销
        /// </summary>
        [OSDescript("已核销")]
        CONSUMED = 20,

        /// <summary>
        /// 已过期
        /// </summary>
        [OSDescript("已过期")]
        EXPIRE = 30,

        /// <summary>
        /// 转赠中
        /// </summary>
        [OSDescript("转赠中")]
        GIFTING = 15,

        /// <summary>
        /// 转赠超时
        /// </summary>
        [OSDescript("转赠超时")]
        GIFT_TIMEOUT = 18,

        /// <summary>
        /// 已删除
        /// </summary>
        [OSDescript("已删除")]
        DELETE = -10,

        /// <summary>
        /// 已失效
        /// </summary>
        [OSDescript("已失效")]
        UNAVAILABLE = -1

    }



    /// <summary>
    ///  微信会员卡激活表单通用字段
    /// </summary>
    public enum WxActiveFormCommonField
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [OSDescript("手机号")] USER_FORM_INFO_FLAG_MOBILE = 10,

        /// <summary>
        /// 性别
        /// </summary>
        [OSDescript("性别")] USER_FORM_INFO_FLAG_SEX = 20,

        /// <summary>
        /// 姓名
        /// </summary>
        [OSDescript("姓名")] USER_FORM_INFO_FLAG_NAME = 30,

        /// <summary>
        /// 生日
        /// </summary>
        [OSDescript("生日")] USER_FORM_INFO_FLAG_BIRTHDAY = 40,

        /// <summary>
        /// 身份证
        /// </summary>
        [OSDescript("身份证")] USER_FORM_INFO_FLAG_IDCARD = 50,

        /// <summary>
        /// 邮箱
        /// </summary>
        [OSDescript("邮箱")] USER_FORM_INFO_FLAG_EMAIL = 60,

        /// <summary>
        /// 姓名
        /// </summary>
        [OSDescript("详细地址")] USER_FORM_INFO_FLAG_LOCATION = 70,

        /// <summary>
        /// 姓名
        /// </summary>
        [OSDescript("教育背景")] USER_FORM_INFO_FLAG_EDUCATION_BACKGRO = 80,

        /// <summary>
        /// 姓名
        /// </summary>
        [OSDescript("行业")] USER_FORM_INFO_FLAG_INDUSTRY = 90,

        /// <summary>
        /// 姓名
        /// </summary>
        [OSDescript("收入")] USER_FORM_INFO_FLAG_INCOME = 100,

        /// <summary>
        /// 兴趣爱好
        /// </summary>
        [OSDescript("兴趣爱好")] USER_FORM_INFO_FLAG_HABIT = 110
    }


}
