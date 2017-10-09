#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券实体中 会员卡对应的用户实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-30
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;


namespace OSS.SnsSdk.Official.Wx.Card.Mos
{
    #region 获取会员卡用户信息部分

    /// <summary>
    ///  获取会员卡用户信息响应实体
    /// </summary>
    public class WxGetMemberCardUserInfoResp : WxBaseResp
    {
        /// <summary>   
        ///    用户在本公众号内唯一识别码
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///    用户昵称
        /// </summary>  
        public string nickname { get; set; }

        /// <summary>   
        ///    积分信息
        /// </summary>  
        public int bonus { get; set; }

        /// <summary>   
        ///    余额信息
        /// </summary>  
        public int balance { get; set; }

        /// <summary>   
        ///    用户性别
        /// </summary>  
        public string sex { get; set; }

        /// <summary>   
        ///    会员信息
        /// </summary>  
        public WxMemberCardUserInfoMo user_info { get; set; }

        /// <summary>   
        ///    当前用户的会员卡状态，NORMAL 正常 EXPIRE 已过期 GIFTING 转赠中 GIFT_SUCC 转赠成功 GIFT_TIMEOUT 转赠超时 DELETE 已删除，UNAVAILABLE 已失效
        ///   typeof(WxCardStatus).ToEnumDirs() 获取对应的枚举字典列表
        /// </summary>  
        public string user_card_status { get; set; }

        /// <summary>   
        ///    该卡是否已经被激活，true表示已经被激活，false表示未被激活
        /// </summary>  
        public bool has_active { get; set; }
    }

    /// <summary>
    ///  会员卡用户信息部分
    /// </summary>
    public class WxMemberCardUserInfoMo
    {
        /// <summary>   
        ///    微信默认格式的会员卡会员信息类目，如等级。
        /// </summary>  
        public List<WxMemberCardCommonFieldItemMo> common_field_list { get; set; }

        /// <summary>   
        ///    开发者设置的会员卡会员信息类目，如等级。
        /// </summary>  
        public List<WxMemberCardCustomFieldItemMo> custom_field_list { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class WxMemberCardCommonFieldItemMo
    {
        /// <summary>   
        ///    会员信息类目名称
        /// 可以通过 typeof(WxActiveFormCommonField).ToEnumDirs() 获取对应的枚举字典列表
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    会员卡信息类目值，比如等级值等
        /// </summary>  
        public string value { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WxMemberCardCustomFieldItemMo
    {
        /// <summary>   
        ///    会员信息类目名称
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///    会员卡信息类目值，比如等级值等
        /// </summary>  
        public string value { get; set; }

        /// <summary>   
        ///    填写项目为多选时的返回
        /// </summary>  
        public List<string> value_list { get; set; }
    }


    /// <summary>
    ///  获取填写激活表单的临时信息
    /// </summary>
    public class WxGetActiveTempInfoResp : WxBaseResp
    {
        public WxGetActiveTempItemsMo info { get; set; }
    }

    public class WxGetActiveTempItemsMo
    {
        /// <summary>   
        ///    微信默认格式的会员卡会员信息类目，如等级。
        /// </summary>  
        public List<WxMemberCardCommonFieldItemMo> common_field_list { get; set; }

        /// <summary>   
        ///    开发者设置的会员卡会员信息类目，如等级。
        /// </summary>  
        public List<WxMemberCardCustomFieldItemMo> custom_field_list { get; set; }
    }

    #endregion

    /// <summary>
    /// 更新会员卡对应的会员信息接口
    /// </summary>
    public class WxUpdateMemberCardUserInfoReq
    {
        /// <summary>   
        ///    必填 string(20) 1231123 卡券Code码。
        /// </summary>  
        public string code { get; set; }

        /// <summary>   
        ///    必填 string（32） 卡券ID。
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///    可空 string（128） 支持商家激活时针对单个会员卡分配自定义的会员卡背景。
        /// </summary>  
        public string background_pic_url { get; set; }

        /// <summary>   
        ///    可空 int 100 需要设置的积分全量值，传入的数值会直接显示
        /// </summary>  
        public int bonus { get; set; }

        /// <summary>   
        ///    可空 int 100 本次积分变动值，传负数代表减少
        /// </summary>  
        public int add_bonus { get; set; }

        /// <summary>   
        ///    可空 string(42) 消费30元，获得3积分 商家自定义积分消耗记录，不超过14个汉字
        /// </summary>  
        public string record_bonus { get; set; }

        /// <summary>   
        ///    可空 int 100 需要设置的余额全量值，传入的数值会直接显示在卡面
        /// </summary>  
        public int balance { get; set; }

        /// <summary>   
        ///    可空 int 100 本次余额变动值，传负数代表减少
        /// </summary>  
        public int add_balance { get; set; }

        /// <summary>   
        ///    可空 string(42) 商家自定义金额消耗记录，不超过14个汉字。
        /// </summary>  
        public string record_balance { get; set; }

        /// <summary>   
        ///    可空 string（12） 白金 创建时字段custom_field1定义类型的最新数值，限制为4个汉字，12字节。
        /// </summary>  
        public string custom_field_value1 { get; set; }

        /// <summary>   
        ///    可空 string（12） 8折 创建时字段custom_field2定义类型的最新数值，限制为4个汉字，12字节。
        /// </summary>  
        public string custom_field_value2 { get; set; }

        /// <summary>   
        ///    可空 string（12） 500 创建时字段custom_field3定义类型的最新数值，限制为4个汉字，12字节。
        /// </summary>  
        public string custom_field_value3 { get; set; }

        /// <summary>   
        ///    可空 JSON -- 控制原生消息结构体，包含各字段的消息控制字段
        /// </summary>  
        public WxMemberCardNotifyMo notify_optional { get; set; }

     
    }
    /// <summary>
    /// 更新会员卡信息后需要通知的选项
    /// </summary>
    public class WxMemberCardNotifyMo
    {
        /// <summary>   
        ///    可空 bool true 积分变动时是否触发系统模板消息，默认为true
        /// </summary>  
        public bool is_notify_bonus { get; set; }

        /// <summary>   
        ///    可空 bool true 余额变动时是否触发系统模板消息，默认为true
        /// </summary>  
        public bool is_notify_balance { get; set; }

        /// <summary>   
        ///    可空 bool false 自定义group1变动时是否触发系统模板消息，默认为false。（2、3同理）
        /// </summary>  
        public bool is_notify_custom_field1 { get; set; }

    }

    /// <summary>
    /// 更新会员卡用户信息后的响应实体
    /// </summary>
    public class WxUpdateMemberCardUserInfoResp : WxBaseResp
    {
        /// <summary>   
        ///    当前用户积分总额
        /// </summary>  
        public int result_bonus { get; set; }

        /// <summary>   
        ///    当前用户预存总金额
        /// </summary>  
        public int result_balance { get; set; }

        /// <summary>   
        ///    用户openid
        /// </summary>  
        public string openid { get; set; }
    }
}
