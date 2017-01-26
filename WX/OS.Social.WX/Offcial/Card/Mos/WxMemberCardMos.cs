#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券实体中 会员卡自己的实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-26
*       
*****************************************************************************/

#endregion


using System.ComponentModel;
using Newtonsoft.Json;

namespace OS.Social.WX.Offcial.Card.Mos
{
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
        [JsonConverter(typeof(StringConverter))]
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


   /// <summary>
   ///   修改会员卡请求实体
   /// </summary>
    public class WxUpdateMemberCardReq : WxUpdateCardBaseReq
    {
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
        ///    必填 string（3072） 会员卡特权说明。
        /// </summary>  
        public string prerogative { get; set; }

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
        ///    可空 JSON结构 自定义会员信息类目，会员卡激活后显示。
        /// </summary>  
        public WxMemberCardCustomCellMo custom_cell1 { get; set; }
    }
}
