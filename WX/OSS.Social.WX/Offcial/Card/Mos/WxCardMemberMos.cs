#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券实体中 会员卡自己的实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-26
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace OSS.Social.WX.Offcial.Card.Mos
{

    #region  会员卡实体相关部分

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
        ///    必填 string（128） 激活会员卡的url。
        /// </summary>  
        public string activate_url { get; set; }

        /// <summary>   
        ///    可空 bool 设置为true时会员卡支持一键开卡，不允许同时传入activate_url字段，否则设置wx_activate失效。填入该字段后仍需调用接口设置开卡项方可生效，详情见一键开卡。
        /// </summary>  
        public bool wx_activate { get; set; }

        /// <summary>   
        ///    可空 bool 是否支持跳转型一键激活，填true或lse
        /// </summary>  
        public bool wx_activate_after_submit { get; set; }

        /// <summary>   
        ///    可空 跳转型一键激活跳转的地址链接，请填写http://或者https://开头的链接
        /// </summary>  
        public string wx_activate_after_submit_url { get; set; }



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
        /// typeof(WxMemberCardCustomNameType).ToEnumDirs  获取对应的枚举字典列表
        /// </summary>  
        public string name_type { get; set; }

        /// <summary>   
        ///    可空 string(24) 会员信息类目自定义名称，当开发者变更这类类目信息的value值时不会触发系统模板消息通知用户
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    可空 string（128） 点击类目跳转外链url
        /// </summary>  
        public string url { get; set; }
    }

    /// <summary>
    ///   会员卡自定义入口
    /// </summary>
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

    /// <summary>
    ///   积分规则
    /// </summary>
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

    /// <summary>
    ///   修改会员卡请求实体
    /// </summary>
    public class WxUpdateMemberCardReq : WxUpdateCardBaseReq
    {
        /// <summary>   
        ///    可空 string(128) 会员卡自定义卡面背景图
        /// </summary>  
        public string background_pic_url { get; set; }

        /// <summary>   
        ///    必填 bool 是否支持积分，仅支持从false变为true，默认为false
        /// </summary>  
        public bool supply_bonus { get; set; }

        /// <summary>   
        ///    可空 string（128） 积分清零规则。
        /// </summary>  
        public string bonus_cleared { get; set; }

        /// <summary>   
        ///    可空 string（128） 积分规则。
        /// </summary>  
        public string bonus_rules { get; set; }

        /// <summary>   
        ///    必填 bool 是否支持储值，仅支持从false变为true，默认为false
        /// </summary>  
        public string supply_balance { get; set; }

        /// <summary>   
        ///    可空 string（128） 储值说明。
        /// </summary>  
        public string balance_rules { get; set; }

        /// <summary>   
        ///    必填 string（3072） 会员卡特权说明。
        /// </summary>  
        public string prerogative { get; set; }


        /// <summary>   
        ///    可空 bool 设置为true时用户领取会员卡后系统自动将其激活，无需调用激活接口，详情见自动激活。
        /// </summary>  
        public bool auto_activate { get; set; }

        /// <summary>   
        ///    必填 string（128） 激活会员卡的url。
        /// </summary>  
        public string activate_url { get; set; }

        /// <summary>   
        ///    可空 bool 设置为true时会员卡支持一键开卡，不允许同时传入activate_url字段，否则设置wx_activate失效。填入该字段后仍需调用接口设置开卡项方可生效，详情见一键开卡。
        /// </summary>  
        public bool wx_activate { get; set; }

        /// <summary>   
        ///    可空 bool 是否支持跳转型一键激活，填true或lse
        /// </summary>  
        public bool wx_activate_after_submit { get; set; }

        /// <summary>   
        ///    可空 跳转型一键激活跳转的地址链接，请填写http://或者https://开头的链接
        /// </summary>  
        public string wx_activate_after_submit_url { get; set; }


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
    ///   激活会员卡请求
    /// </summary>
    public class WxActiveMemberCardReq
    {
        /// <summary>   
        ///    必填 string(20) 会员卡编号，由开发者填入，作为序列号显示在用户的卡包里。可与Code码保持等值。
        /// </summary>  
        public string membership_number { get; set; }

        /// <summary>   
        ///    必填 string(20) 领取会员卡用户获得的code
        /// </summary>  
        public string code { get; set; }

        /// <summary>   
        ///    可空 string（32） 卡券ID,自定义code卡券必填
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///    可空 string（128） 商家自定义会员卡背景图，须先调用上传图片接口将背景图上传至CDN，否则报错，卡面设计请遵循微信会员卡自定义背景设计规范
        /// </summary>  
        public string background_pic_url { get; set; }

        /// <summary>   
        ///    可空 unsigned int 激活后的有效起始时间。若不填写默认以创建时的 data_info 为准。Unix时间戳格式。
        /// </summary>  
        public long activate_begin_time { get; set; }

        /// <summary>   
        ///    可空 unsigned int 激活后的有效截至时间。若不填写默认以创建时的 data_info 为准。Unix时间戳格式。
        /// </summary>  
        public long activate_end_time { get; set; }

        /// <summary>   
        ///    可空 int 初始积分，不填为0。
        /// </summary>  
        public int init_bonus { get; set; }

        /// <summary>   
        ///    可空 string(32) 积分同步说明。
        /// </summary>  
        public string init_bonus_record { get; set; }

        /// <summary>   
        ///    可空 int 初始余额，不填为0。
        /// </summary>  
        public int init_balance { get; set; }

        /// <summary>   
        ///    可空 string（12） 创建时字段custom_field1定义类型的初始值，限制为4个汉字，12字节。
        /// </summary>  
        public string init_custom_field_value1 { get; set; }

        /// <summary>   
        ///    可空 string（12） 创建时字段custom_field2定义类型的初始值，限制为4个汉字，12字节。
        /// </summary>  
        public string init_custom_field_value2 { get; set; }

        /// <summary>   
        ///    可空 string（12） 创建时字段custom_field3定义类型的初始值，限制为4个汉字，12字节。
        /// </summary>  
        public string init_custom_field_value3 { get; set; }
    }

    #region  设置微信自动激活开卡表单

    /// <summary>
    /// 设置开卡微信填写字段
    /// </summary>
    public class WxSetActiveFormReq
    {
        /// <summary>   
        ///    必填 string(32) 卡券ID。
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///    可空 JSON结构 会员卡激活时的必填选项。
        /// </summary>  
        public WxActiveFormFieldsMo required_form { get; set; }

        /// <summary>   
        ///    可空 JSON结构 会员卡激活时的选填项。
        /// </summary>  
        public WxActiveFormFieldsMo optional_form { get; set; }

        /// <summary>   
        ///    可空 JSON结构 服务声明，用于放置商户会员卡守则
        /// </summary>  
        public WxActiveFormNavMo service_statement { get; set; }

        /// <summary>   
        ///    可空 JSON结构 绑定老会员链接
        /// </summary>  
        public WxActiveFormNavMo bind_old_card { get; set; }
    }

    /// <summary>
    /// 激活表单里的导航项
    /// </summary>
    public class WxActiveFormNavMo
    {
        /// <summary>   
        ///    可空 string(32) 链接名称
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    可空 string(128) 自定义url，请填写http://或者https://开头的链接
        /// </summary>  
        public string url { get; set; }
    }

    /// <summary>
    ///  激活表单里的填写字段
    /// </summary>
    public class WxActiveFormFieldsMo
    {
        /// <summary>   
        /// 可空 bool 当前结构（required_form或者optional_form ）
        /// 内的字段是否允许用户激活后再次修改，商户设置为true时，
        /// 需要接收相应事件通知处理修改事件
        /// </summary>  
        public bool can_modify { get; set; }

        /// <summary>   
        ///    可空 arry 微信格式化的选项类型。
        ///  通过 typeof(WxActiveFormCommonField).ToEnumDirs(false) 获取键值列表
        /// </summary>  
        public List<string> common_field_id_list { get; set; }

        /// <summary>   
        ///    可空 arry 自定义选项名称，开发者可以分别在必填和选填中至多定义五个自定义选项
        /// </summary>  
        public List<string> custom_field_list { get; set; }

        /// <summary>   
        ///    可空 arry 自定义富文本类型，包含以下三个字段，开发者可以分别在必填和选填中至多定义五个自定义选项
        /// </summary>  
        public List<WxActiveFormRichFieldMo> rich_field_list { get; set; }
    }

    /// <summary>
    /// 激活字段信息
    /// </summary>
    public class WxActiveFormRichFieldMo
    {
        /// <summary>   
        ///    可空 string(21) 富文本类型
        /// FORM_FIELD_RADIO 自定义单选 
        /// FORM_FIELD_SELECT 自定义选择项 
        /// FORM_FIELD_CHECK_BOX 自定义多选
        /// </summary>  
        public string type { get; set; }

        /// <summary>   
        ///    可空 string(21) 字段名
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    可空 arry 选择项
        /// </summary>  
        public List<string> values { get; set; }
    }

    #endregion


  


}
