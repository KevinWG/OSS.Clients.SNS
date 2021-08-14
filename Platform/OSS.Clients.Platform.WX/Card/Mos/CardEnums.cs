#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信的卡券枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

namespace OSS.Clients.Platform.WX.Card.Mos
{

    /// <summary>
    /// 卡券状态
    /// </summary>
    public enum WXCardStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>

        CARD_STATUS_NOT_VERIFY = 10,

        /// <summary>
        /// 审核失败
        /// </summary>

        CARD_STATUS_VERIFY_FAIL = 20,

        /// <summary>
        /// 通过审核
        /// </summary>

        CARD_STATUS_VERIFY_OK = 30,

        /// <summary>
        /// 卡券被商户删除
        /// </summary>

        CARD_STATUS_DELETE = 40,

        /// <summary>
        /// 在公众平台投放过的卡券
        /// </summary>

        CARD_STATUS_DISPATCH = 50,

    }

    /// <summary>
    ///   卡券code类型
    /// </summary>
    public enum WXCardCodeType
    {
        /// <summary>
        /// 二维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        CODE_TYPE_QRCODE = 10,

        /// <summary>
        /// 一维码显示code
        /// 适用于扫码/输码核销
        /// </summary>
        CODE_TYPE_BARCODE = 20,

        /// <summary>
        /// 二维码不显示code
        /// CODE_TYPE_ONLY_QRCODE
        /// </summary>
        CODE_TYPE_ONLY_QRCODE = 30,

        /// <summary>
        /// 仅code类型
        /// 仅适用于输码核销
        /// </summary>
        CODE_TYPE_TEXT = 40,

        /// <summary>
        /// 无code类型
        /// 仅适用于线上核销，开发者须自定义跳转链接跳转至H5页面，允许用户核销掉卡券，自定义cell的名称可以命名为“立即使用"
        /// </summary>
        CODE_TYPE_NONE = 50
    }

    /// <summary>
    ///  微信卡券类型
    /// </summary>
    public enum WXCardType
    {
        /// <summary>
        /// 折扣券
        /// </summary>
        DISCOUNT = 0, //  官方数值

        /// <summary>
        /// 代金券
        /// </summary>
        CASH = 1, //  官方数值

        /// <summary>
        /// 礼品券
        /// </summary>
        GIFT = 2, //  官方数值

        /// <summary>
        /// 优惠券
        /// </summary>
        GENERAL_COUPON = 3, //  官方数值

        /// <summary>
        /// 团购券
        /// </summary>
        GROUPON = 4, //  官方数值

        /// <summary>
        /// 会员卡
        /// </summary>
        MEMBER_CARD = 60,

        /// <summary>
        /// 景点门票
        /// </summary>
        SCENIC_TICKET = 70,

        /// <summary>
        /// 电影票
        /// </summary>
        MOVIE_TICKET = 80,

        /// <summary>
        /// 飞机票
        /// </summary>
        BOARDING_PASS = 90,

        /// <summary>
        /// 会议门票
        /// </summary>
        MEETING_TICKET = 100,

        /// <summary>
        /// 汽车票
        /// </summary>
        BUS_TICKET = 110

    }

    /// <summary>
    ///   卡券背景颜色值
    /// </summary>
    public enum WXCardColor
    {
        /// <summary>
        /// #63b359
        /// </summary>
        Color010 = 10,

        /// <summary>
        /// #2c9f67
        /// </summary>
        Color020 = 20,

        /// <summary>
        /// #509fc9
        /// </summary>
        Color030 = 30,

        /// <summary>
        /// #5885cf
        /// </summary>
        Color040 = 40,

        /// <summary>
        /// #9062c0
        /// </summary>
        Color050 = 50,

        /// <summary>
        /// #d09a45
        /// </summary>
        Color060 = 60,

        /// <summary>
        /// #e4b138
        /// </summary>
        Color070 = 70,

        /// <summary>
        /// #ee903c
        /// </summary>
        Color080 = 80,

        /// <summary>
        /// #f08500
        /// </summary>
        Color081 = 81,

        /// <summary>
        /// #a9d92d
        /// </summary>
        Color082 = 82,

        /// <summary>
        /// #dd6549
        /// </summary>
        Color090 = 90,

        /// <summary>
        /// #cc463d
        /// </summary>
        Color100 = 100,

        /// <summary>
        /// #cf3e36
        /// </summary>
        Color101 = 101,

        /// <summary>
        /// #5E6671
        /// </summary>
        Color102 = 102
    }


    /// <summary>
    ///   卡券时效类型
    /// </summary>
    public enum WXCardDateType
    {
        /// <summary>
        /// 固定日期区间
        /// </summary>
        DATE_TYPE_FIX_TIME_RANGE = 1,

        /// <summary>
        /// 固定时长
        /// </summary>
        DATE_TYPE_FIX_TERM = 2
    }



    public enum WXCardCustomCodeMode
    {
        GET_CUSTOM_CODE_MODE_DEPOSIT
    }

    /// <summary>
    ///   商家服务类型
    /// </summary>
    public enum WXCardBusinessService
    {
        /// <summary>
        /// 外卖服务
        /// </summary>
        BIZ_SERVICE_DELIVER = 10,

        /// <summary>
        /// 停车位
        /// </summary>
        BIZ_SERVICE_FREE_PARK = 20,

        /// <summary>
        /// 可带宠物
        /// </summary>
        BIZ_SERVICE_WITH_PET = 30,

        /// <summary>
        /// 免费wifi
        /// </summary>
        BIZ_SERVICE_FREE_WIFI = 40,
    }


    public enum WXCardTimeLimitType
    {
        /// <summary>
        /// 周一
        /// </summary>
        MONDAY = 10,

        /// <summary>
        /// 周二
        /// </summary>
        TUESDAY = 20,

        /// <summary>
        /// 周三
        /// </summary>
        WEDNESDAY = 30,

        /// <summary>
        /// 周四
        /// </summary>
        THURSDAY = 40,

        /// <summary>
        /// 周五
        /// </summary>
        FRIDAY = 50,

        /// <summary>
        /// 周六
        /// </summary>
        SATURDAY = 60,

        /// <summary>
        /// 周日
        /// </summary>
        SUNDAY = 70,

        /// <summary>
        /// 假日
        /// </summary>
        HOLIDAY = 80
    }


    public enum WXCardLandPageSence
    {
        /// <summary>
        /// 附近
        /// </summary>
        SCENE_NEAR_BY = 10,

        /// <summary>
        /// 自定义菜单
        /// </summary>
        SCENE_MENU = 20,

        /// <summary>
        /// 二维码
        /// </summary>
        SCENE_QRCODE = 30,

        /// <summary>
        /// 公众号文章
        /// </summary>
        SCENE_ARTICLE = 40,

        /// <summary>
        /// h5页面
        /// </summary>
        SCENE_H5 = 50,

        /// <summary>
        /// 自动回复
        /// </summary>
        SCENE_IVR = 60,

        /// <summary>
        /// 卡券自定义cell
        /// </summary>
        SCENE_CARD_CUSTOM_CELL = 70

    }


    public enum WXMemberCardCustomNameType
    {
        /// <summary>
        /// 等级
        /// </summary>
        FIELD_NAME_TYPE_LEVEL = 10,

        /// <summary>
        /// 优惠券
        /// </summary>
        FIELD_NAME_TYPE_COUPON = 20,

        /// <summary>
        /// 印花
        /// </summary>
        FIELD_NAME_TYPE_STAMP = 30,

        /// <summary>
        /// 折扣
        /// </summary>
        FIELD_NAME_TYPE_DISCOUNT = 40,

        /// <summary>
        /// 成就
        /// </summary>
        FIELD_NAME_TYPE_ACHIEVEMEN = 50,

        /// <summary>
        /// 里程
        /// </summary>
        FIELD_NAME_TYPE_MILEAGE = 60,

        /// <summary>
        /// 集点
        /// </summary>
        FIELD_NAME_TYPE_SET_POINTS = 70,

        /// <summary>
        /// 次数
        /// </summary>
        FIELD_NAME_TYPE_TIMS = 80,
    }


    public enum WXCardCodeUseState
    {
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL = 10,

        /// <summary>
        /// 已核销
        /// </summary>
        CONSUMED = 20,

        /// <summary>
        /// 已过期
        /// </summary>
        EXPIRE = 30,

        /// <summary>
        /// 转赠中
        /// </summary>
        GIFTING = 15,

        /// <summary>
        /// 转赠超时
        /// </summary>
        GIFT_TIMEOUT = 18,

        /// <summary>
        /// 已删除
        /// </summary>
        DELETE = -10,

        /// <summary>
        /// 已失效
        /// </summary>

        UNAVAILABLE = -1

    }



    /// <summary>
    ///  微信会员卡激活表单通用字段
    /// </summary>
    public enum WXActiveFormCommonField
    {
        /// <summary>
        /// 手机号
        /// </summary>
        USER_FORM_INFO_FLAG_MOBILE = 10,

        /// <summary>
        /// 性别
        /// </summary>
        USER_FORM_INFO_FLAG_SEX = 20,

        /// <summary>
        /// 姓名
        /// </summary>
        USER_FORM_INFO_FLAG_NAME = 30,

        /// <summary>
        /// 生日
        /// </summary>
        USER_FORM_INFO_FLAG_BIRTHDAY = 40,

        /// <summary>
        /// 身份证
        /// </summary>
        USER_FORM_INFO_FLAG_IDCARD = 50,

        /// <summary>
        /// 邮箱
        /// </summary>
        USER_FORM_INFO_FLAG_EMAIL = 60,

        /// <summary>
        /// 姓名
        /// </summary>
        USER_FORM_INFO_FLAG_LOCATION = 70,

        /// <summary>
        /// 姓名
        /// </summary>
        USER_FORM_INFO_FLAG_EDUCATION_BACKGRO = 80,

        /// <summary>
        /// 姓名
        /// </summary>
        USER_FORM_INFO_FLAG_INDUSTRY = 90,

        /// <summary>
        /// 姓名
        /// </summary>
        USER_FORM_INFO_FLAG_INCOME = 100,

        /// <summary>
        /// 兴趣爱好
        /// </summary>
        USER_FORM_INFO_FLAG_HABIT = 110
    }


}
