#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  现金券实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace OS.Social.WX.Offcial.Card.Mos
{

    public class WxCardSmallBaseMo
    {
        /// <summary>
        ///   卡券基本信息
        /// </summary>
        public WxCardBasicMo base_info { get; set; }
    }

    /// <summary>
    ///   卡券信息的基类
    /// </summary>
    public class WxCardBaseMo : WxCardSmallBaseMo
    {
        /// <summary>
        ///   卡券高级信息
        /// </summary>
        public WxCardAdvancedMo advanced_info { get; set; }
    }


    #region  团购券

    /// <summary>
    ///  添加团购券请求实体
    /// </summary>
    public class WxGroupCardBigMo : WxCardTypeBaseMo
    {
        public WxGroupCardMo groupon { get; set; }
    }

    /// <summary>
    ///   团购券信息
    /// </summary>
    public class WxGroupCardMo : WxCardBaseMo
    {
        /// <summary>
        ///  必填 团购券专用，团购详情。
        /// </summary>
        public string deal_detail { get; set; }
    }

    #endregion

    #region  现金券

    /// <summary>
    ///  添加现金券请求实体
    /// </summary>
    public class WxAddCashCardReq : WxCardTypeBaseMo
    {
        public WxCashCardMo cash { get; set; }
    }

    /// <summary>
    ///   现金券信息
    /// </summary>
    public class WxCashCardMo : WxCardBaseMo
    {
        /// <summary>
        ///  必填 代金券专用，表示起用金额（单位为分）,如果无起用门槛则填0
        /// </summary>
        public int least_cost { get; set; }

        /// <summary>
        ///  必填 代金券专用，表示减免金额。（单位为分）
        /// </summary>
        public int reduce_cost { get; set; }
    }

    #endregion

    #region   折扣券

    /// <summary>
    ///  添加折扣券请求实体
    /// </summary>
    public class WxDiscountCardBig : WxCardTypeBaseMo
    {
        public WxDiscountCardMo discount { get; set; }
    }

    /// <summary>
    ///   折扣券信息
    /// </summary>
    public class WxDiscountCardMo : WxCardBaseMo
    {

        /// <summary>
        ///  必填 折扣券专用，表示打折额度（百分比）。填30就是七折
        /// </summary>
        public int discount { get; set; }

    }

    #endregion

    #region   兑换券

    /// <summary>
    ///  添加兑换券请求实体
    /// </summary>
    public class WxGiftCardBigMo : WxCardTypeBaseMo
    {
        public WxGiftCardMo gift { get; set; }
    }

    /// <summary>
    ///   兑换券信息
    /// </summary>
    public class WxGiftCardMo : WxCardBaseMo
    {
        /// <summary>
        ///  必填  string(3072) 兑换券专用，填写兑换内容的名称
        /// </summary>
        public string gift { get; set; }

    }

    #endregion

    #region   优惠券

    /// <summary>
    ///  添加优惠券请求实体
    /// </summary>
    public class WxCouponCardBigMo : WxCardTypeBaseMo
    {
        public WxCouponCardMo general_coupon { get; set; }
    }

    /// <summary>
    ///   优惠券信息
    /// </summary>
    public class WxCouponCardMo : WxCardBaseMo
    {

        /// <summary>
        ///  必填  string(3072) 优惠券专用，填写优惠详情
        /// </summary>
        public string default_detail { get; set; }
    }

    #endregion

    #region   会员卡

    /// <summary>
    ///  包含卡类型会员卡实体
    /// </summary>
    public class WxMemberCardBigMo : WxCardTypeBaseMo
    {
        public WxCouponCardMo member_card { get; set; }
    }

    /// <summary>
    ///   会员卡信息实体
    /// </summary>
    public class WxMemberCardMo : WxCardBaseMo
    {
        /// <summary>   
        ///    可空 string(128) 商家自定义会员卡背景图，须先调用上传图片接口将背景图上传至CDN，否则报错，卡面设计请遵循微信会员卡自定义背景设计规范 ,像素大小控制在1000像素*600像素以下
        /// </summary>  
        public string background_pic_url { get; set; }

        /// <summary>   
        ///    必填 string（3072） 会员卡特权说明。
        /// </summary>  
        public string prerogative { get; set; }

        /// <summary>   
        ///    可空 bool 设置为true时用户领取会员卡后系统自动将其激活，无需调用激活接口，详情见自动激活。
        /// </summary>  
        public bool auto_activate { get; set; }

        /// <summary>   
        ///    可空 bool 设置为true时会员卡支持一键开卡，不允许同时传入activate_url字段，否则设置wx_activate失效。填入该字段后仍需调用接口设置开卡项方可生效，详情见一键开卡。
        /// </summary>  
        public bool wx_activate { get; set; }

        /// <summary>   
        ///    必填 bool 显示积分，填写true或false，如填写true，积分相关字段均为必填。
        /// </summary>  
        public bool supply_bonus { get; set; }

        /// <summary>   
        ///    可空 string(128) 设置跳转外链查看积分详情。仅适用于积分无法通过激活接口同步的情况下使用该字段。
        /// </summary>  
        public string bonus_url { get; set; }

        /// <summary>   
        ///    必填 bool 是否支持储值，填写true或false。如填写true，储值相关字段均为必填。
        /// </summary>  
        public bool supply_balance { get; set; }

        /// <summary>   
        ///    可空 string(128) 设置跳转外链查看余额详情。仅适用于余额无法通过激活接口同步的情况下使用该字段。
        /// </summary>  
        public string balance_url { get; set; }

        /// <summary>   
        ///    可空 JSON结构 自定义会员信息类目，会员卡激活后显示,包含name_type(name)和url字段
        /// </summary>  
        public WxMemberCardCustomFieldMo custom_field1 { get; set; }

        /// <summary>   
        ///    可空 JSON结构 自定义会员信息类目，会员卡激活后显示，包含name_type(name)和url字段
        /// </summary>  
        public WxMemberCardCustomFieldMo custom_field2 { get; set; }

        /// <summary>   
        ///    可空 JSON结构 自定义会员信息类目，会员卡激活后显示，包含name_type(name)和url字段
        /// </summary>  
        public WxMemberCardCustomFieldMo custom_field3 { get; set; }

        /// <summary>   
        ///    可空 string（128） 积分清零规则。
        /// </summary>  
        public string bonus_cleared { get; set; }

        /// <summary>   
        ///    可空 string（128） 积分规则。
        /// </summary>  
        public string bonus_rules { get; set; }

        /// <summary>   
        ///    可空 string（128） 储值说明。
        /// </summary>  
        public string balance_rules { get; set; }

        /// <summary>   
        ///    必填 string（128） 激活会员卡的url。
        /// </summary>  
        public string activate_url { get; set; }

        /// <summary>   
        ///    可空 JSON结构 自定义会员信息类目，会员卡激活后显示。
        /// </summary>  
        public WxMemberCardCustomCellMo custom_cell1 { get; set; }


        /// <summary>   
        ///    可空 JSON结构 积分规则。
        /// </summary>  
        public WxMemberCardBonusRuleMo bonus_rule { get; set; }

        /// <summary>   
        ///    可空 int 折扣，该会员卡享受的折扣优惠,填10就是九折
        /// </summary>  
        public int discount { get; set; }


    }

    /// <summary>
    ///  会员卡自定义字段
    /// </summary>
    public class WxMemberCardCustomFieldMo
    {
        /// <summary>   
        ///    可空 string(24) 会员信息类目半自定义名称，当开发者变更这类类目信息的value 可以选择触发系统模板消息通知用户。
        /// FIELD_NAME_TYPE_LEVEL 等级
        /// FIELD_NAME_TYPE_COUPON 优惠券
        /// FIELD_NAME_TYPE_STAMP 印花 
        /// FIELD_NAME_TYPE_DISCOUNT 折扣 
        /// FIELD_NAME_TYPE_ACHIEVEMEN 成就 
        /// FIELD_NAME_TYPE_MILEAGE 里程 
        /// FIELD_NAME_TYPE_SET_POINTS 集点 
        /// FIELD_NAME_TYPE_TIMS 次数
        /// </summary>  
        [JsonConverter(typeof (StringConverter))]
        public WxMemberCardCustomNameType name_type { get; set; }

        /// <summary>   
        ///    可空 string(24) 会员信息类目自定义名称，当开发者变更这类类目信息的value值时不会触发系统模板消息通知用户
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    可空 string（128） 点击类目跳转外链url
        /// </summary>  
        public string url { get; set; }
    }


    public class WxMemberCardCustomCellMo
    {
        /// <summary>   
        ///    必填 string（15） 入口名称。
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    必填 string（18） 入口右侧提示语，6个汉字内。
        /// </summary>  
        public string tips { get; set; }

        /// <summary>   
        ///    必填 string（128） 入口跳转链接。
        /// </summary>  
        public string url { get; set; }
    }


    public class WxMemberCardBonusRuleMo
    {
        /// <summary>   
        ///    可空 int 消费金额。以分为单位。
        /// </summary>  
        public int cost_money_unit { get; set; }

        /// <summary>   
        ///    可空 int 对应增加的积分。
        /// </summary>  
        public int increase_bonus { get; set; }

        /// <summary>   
        ///    可空 int 用户单次可获取的积分上限。
        /// </summary>  
        public int max_increase_bonus { get; set; }

        /// <summary>   
        ///    可空 int 初始设置积分。
        /// </summary>  
        public int init_increase_bonus { get; set; }

        /// <summary>   
        ///    可空 int 每使用5积分。
        /// </summary>  
        public int cost_bonus_unit { get; set; }

        /// <summary>   
        ///    可空 int 抵扣xx元，（这里以分为单位）
        /// </summary>  
        public int reduce_money { get; set; }

        /// <summary>   
        ///    可空 int 抵扣条件，满xx元（这里以分为单位）可用。
        /// </summary>  
        public int least_money_to_use_bonus { get; set; }

        /// <summary>   
        ///    可空 int 抵扣条件，单笔最多使用xx积分。
        /// </summary>  
        public int max_reduce_bonus { get; set; }
    }

    #endregion

    #region  朋友的券

    public class WxFriendCardBigMo : WxCardTypeBaseMo
    {

        /// <summary>
        ///   朋友的券 ==  礼品券类型时需要的部分
        /// </summary>
        public WxFriendCardGiftMo gift { get; set; }


        /// <summary>
        ///   朋友的券 ==  现金券类型时需要的部分
        /// </summary>
        public WxCashCardMo cash { get; set; }

    }


    /// <summary>
    ///   朋友券中的礼品券信息
    /// </summary>
    public class WxFriendCardGiftMo : WxGiftCardMo
    {
        /// <summary>   
        ///    兑换券兑换商品名字，限6个汉字 必填
        /// </summary>  
        public string gift_name { get; set; }

        /// <summary>   
        ///    兑换券兑换商品数目，限三位数字 可空
        /// </summary>  
        public int gift_num { get; set; }

        /// <summary>   
        ///    兑换券兑换商品的数量单位，限两个汉字 可空
        /// </summary>  
        public string gift_unit { get; set; }

    }

    #endregion

    #region  会议门票


    public class WxMeetingCardBigMo : WxCardTypeBaseMo
    {
        /// <summary>
        ///   会议票实体
        /// </summary>
        public WxMeetingCardMo meeting_ticket { get; set; }
    }

    public class WxMeetingCardMo : WxCardSmallBaseMo
    {

        /// <summary>   
        ///    必填 string(3072) 本次会议于2015年5月10号在广州举行，会场地点：xxxx。 会议详情。
        /// </summary>  
        public string meeting_detail { get; set; }

        /// <summary>   
        ///    可空 string(128) xxx.com 会场导览图。
        /// </summary>  
        public string map_url { get; set; }
    }

    #endregion

    #region   景区门票

    public class WxScenicCardBigMo : WxCardTypeBaseMo
    {
        /// <summary>   
        ///    门票实体
        /// </summary>  
        public WxScenicCardMo scenic_ticket { get; set; }

    }

    public class WxScenicCardMo : WxCardSmallBaseMo
    {
        /// <summary>   
        ///    必填 string(3072) 平日全票 票类型，例如平日全票，套票等。
        /// </summary>  
        public string ticket_class { get; set; }

        /// <summary>   
        ///    可空 string(128) xxx.com 导览图url
        /// </summary>  
        public string guide_url { get; set; }

    }

    #endregion
    
    #region  电影票

    public class WxMovieCardBigMo : WxCardTypeBaseMo
    {
        /// <summary>   
        ///    电影票实体
        /// </summary>  
        public WxMovieCardMo movie_ticket { get; set; }
    }

    public class WxMovieCardMo : WxCardSmallBaseMo
    {
        /// <summary>   
        ///    必填 string(3072) 电影名：xxx，电影简介：xxx。 电影票详
        /// </summary>  
        public string detail { get; set; }
    }

    #endregion


    #region  飞机票


    public class WxBoardCardBigMo : WxCardTypeBaseMo
    {
        /// <summary>
        ///   票实体
        /// </summary>
        public WxBoardCardMo boarding_pass { get; set; }
    }

    public class WxBoardCardMo : WxCardSmallBaseMo
    {
        /// <summary>   
        ///    必填 string(54) 成都 起点，上限为18个汉字。
        /// </summary>  
        public string from { get; set; }

        /// <summary>   
        ///    必填 string(54) 广州 终点，上限为18个汉字。
        /// </summary>  
        public string to { get; set; }

        /// <summary>   
        ///    必填 string(24) CE123 航班
        /// </summary>  
        public string flight { get; set; }

        /// <summary>   
        ///    可空 string(12) A11 入口，上限为4个汉字。
        /// </summary>  
        public string gate { get; set; }

        /// <summary>   
        ///    可空 string(128) xxx.com 在线值机的链接。
        /// </summary>  
        public string check_in_url { get; set; }

        /// <summary>   
        ///    必填 string(24) 空客A320 机型，上限为8个汉字。
        /// </summary>  
        public string air_model { get; set; }

        /// <summary>   
        ///    必填 string(128) 1434507901 起飞时间。Unix时间戳格式。
        /// </summary>  
        public string departure_time { get; set; }

        /// <summary>   
        ///    必填 string(128) 1434909901 降落时间。Unix时间戳格式。
        /// </summary>  
        public string landing_time { get; set; }
    }

    #endregion




    #region  卡券列表

    public class WxGetUserCardCodeListResp : WxBaseResp
    {
        /// <summary>
        ///   卡券列表
        /// </summary>
        public List<WxCardCodeItemMo> card_list { get; set; }

        /// <summary>
        /// 是否有可用的朋友的券
        /// </summary>
        public bool has_share_card { get; set; }
    }


    public class WxCardCodeItemMo
    {
        /// <summary>
        ///   卡券的code码，单张卡券唯一标识
        /// </summary>
        public string code { get; set; }


        /// <summary>
        ///  卡券id
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// 获取卡券id列表
    /// </summary>
    public class WxGetCardIdListResp:WxBaseResp
    {
        /// <summary>
        /// 该商户名下卡券ID总数
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        ///    卡券id列表
        /// </summary>
        public List<string> card_id_list { get; set; }
    }

    #endregion




    #region  获取卡券信息相关实体

    /// <summary>
    ///   获取卡券详情响应实体
    /// </summary>
    public class WxGetCardDetailRsp : WxBaseResp
    {
        /// <summary>
        ///   返回的卡详情实体
        /// </summary>
        public WxCardPackageMo card { get; set; }
    }


    /// <summary>
    /// 卡券类型  和  卡券相关类型的包实体
    /// </summary>
    public class WxCardPackageMo : WxCardTypeBaseMo
    {
        /// <summary>
        /// 通用券
        /// </summary>
        public WxCouponCardMo general_coupon { get; set; }

        /// <summary>
        /// 团购券
        /// </summary>
        public WxGroupCardMo groupon { get; set; }

        /// <summary>
        /// 礼品券
        /// </summary>
        public WxGiftCardMo gift { get; set; }

        /// <summary>
        /// 代金券
        /// </summary>
        public WxCashCardMo cash { get; set; }

        /// <summary>
        /// 折扣券
        /// </summary>
        public WxDiscountCardMo discount { get; set; }


        /// <summary>
        /// 会员卡
        /// </summary>
        public WxMemberCardMo member_card { get; set; }

        /// <summary>
        /// 会议门票
        /// </summary>
        public WxMeetingCardMo meeting_ticket { get; set; }

        /// <summary>
        /// 门票
        /// </summary>
        public WxScenicCardMo scenic_ticket { get; set; }

        /// <summary>
        /// 电影票
        /// </summary>
        public WxMovieCardMo movie_ticket { get; set; }

        /// <summary>
        /// 飞机票
        /// </summary>
        public WxBoardCardMo boarding_pass { get; set; }


    }

    #endregion

}
