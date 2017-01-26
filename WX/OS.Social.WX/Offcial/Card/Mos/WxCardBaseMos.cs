#region Copyright (C) 2017 Kevin (OS系列开源项目)
/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券实体
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
using Newtonsoft.Json.Converters;

namespace OS.Social.WX.Offcial.Card.Mos
{
    /// <summary>
    ///   添加卡券响应实体
    /// </summary>
    public class WxAddCardResp:WxBaseResp
    {
        /// <summary>
        ///   卡券Id
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    ///   卡券实体基类
    /// </summary>
    public class WxCardTypeBaseMo
    {
        /// <summary>
        /// 卡券类型
        /// </summary>
        [JsonConverter(typeof (StringConverter))]
        public WxCardType card_type { get; set; }

    }

    #region  卡券基本信息
    
    /// <summary>
    ///   卡券基本信息
    /// </summary>
    public class WxCardBasicMo
    {
        /// <summary>
        ///   卡Id  添加时不做处理
        /// </summary>
        public string id { get; set; }

        /// <summary>   
        ///   必填    string(128)http://mmbiz.qpic.cn/卡券的商户logo，建议像素为300*300。
        /// </summary>  
        public string logo_url { get; set; }

        /// <summary>   
        ///   必填    string(16)CODE_TYPE_TEXT码型：
        /// "CODE_TYPE_TEXT"文本；
        /// "CODE_TYPE_BARCODE"一维码"CODE_TYPE_QRCODE"二维码
        /// "CODE_TYPE_ONLY_QRCODE",二维码无code显示；
        /// "CODE_TYPE_ONLY_BARCODE",一维码无code显示；
        /// CODE_TYPE_NONE，不显示code和条形码类型
        /// </summary>  
        [JsonConverter(typeof (StringEnumConverter))]
        public WxCardCodeType code_type { get; set; }

        /// <summary>   
        ///   必填    string（36）商户名字,字数上限为12个汉字。
        /// </summary>  
        public string brand_name { get; set; }

        /// <summary>   
        ///   必填    string（27）卡券名，字数上限为9个汉字。(建议涵盖卡券属性、服务及金额)。
        /// </summary>  
        public string title { get; set; }

        /// <summary>   
        ///   必填    string（16）Color010券颜色。按色彩规范标注填写Color010-Color100。
        /// </summary>  
        [JsonConverter(typeof (StringEnumConverter))]
        public WxCardColor color { get; set; }

        /// <summary>   
        ///   必填    string（48）卡券使用提醒，字数上限为16个汉字。
        /// </summary>  
        public string notice { get; set; }

        /// <summary>   
        ///   必填    string(3072）卡券使用说明，字数上限为1024个汉字。
        /// </summary>  
        public string description { get; set; }

        /// <summary>   
        ///   必填    JSON结构商品信息。
        /// </summary>  
        public WxCardBasicSkuMo sku { get; set; }


        /// <summary>   
        ///   必填    JSON结构使用日期，有效期的信息。
        /// </summary>  
        public WxCardBasicDateMo date_info { get; set; }
        
        #region  可选字段
        /// <summary>   
        ///   【选填】    bool是否自定义Code码。填写true或false，默认为false。
        ///    通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见是否自定义Code码。
        /// </summary>  
        public bool use_custom_code { get; set; }

        /// <summary>   
        ///  【选填】    填入GET_CUSTOM_CODE_MODE_DEPOSIT表示该卡券为预存code模式卡券，
        /// 须导入超过库存数目的自定义code后方可投放，填入该字段后，quantity字段须为0,须导入code后再增加库存
        /// </summary>  

        public string get_custom_code_mode { get; set; }

        /// <summary>   
        ///   【选填】   是否指定用户领取，填写true或false。默认为false。
        /// 通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
        /// </summary>  
        public bool bind_openid { get; set; }

        /// <summary>   
        ///   【选填】   客服电话。
        /// </summary>  
        public string service_phone { get; set; }

        /// <summary>   
        ///  【选填】 门店位置poiid。调用POI门店管理接口获取门店位置poiid。具备线下门店的商户为必填。
        /// </summary>  
        public List<long> location_id_list { get; set; }

        /// <summary>   
        ///    【选填】 设置本卡券支持全部门店，与location_id_list互斥
        /// </summary>  
        public bool use_all_locations { get; set; }

        /// <summary>   
        ///   【选填】   第三方来源名，例如同程旅游、大众点评。
        /// </summary>  
        public string source { get; set; }

        /// <summary>   
        ///  【选填】    立即使用自定义跳转外链的入口名字。详情见活用自定义入口
        /// </summary>  
        public string custom_url_name { get; set; }

        /// <summary>   
        ///   【选填】   立即使用卡券顶部居中的按钮，仅在卡券状态正常(可以核销)时显示
        /// </summary>  
        public string center_title { get; set; }

        /// <summary>   
        ///   【选填】   立即享受优惠显示在入口下方的提示语，仅在卡券状态正常(可以核销)时显示。
        /// </summary>  
        public string center_sub_title { get; set; }

        /// <summary>   
        ///   【选填】   顶部居中的url，仅在卡券状态正常(可以核销)时显示。
        /// </summary>  
        public string center_url { get; set; }

        /// <summary>   
        ///   【选填】   自定义跳转的URL。
        /// </summary>  
        public string custom_url { get; set; }

        /// <summary>   
        ///   【选填】   显示在入口右侧的提示语。
        /// </summary>  
        public string custom_url_sub_title { get; set; }

        /// <summary>   
        ///  【选填】    产品介绍营销场景的自定义入口名称。
        /// </summary>  
        public string promotion_url_name { get; set; }

        /// <summary>   
        ///   【选填】   入口跳转外链的地址链接。
        /// </summary>  
        public string promotion_url { get; set; }

        /// <summary>   
        ///  【选填】    卖场大优惠。显示在营销入口右侧的提示语。
        /// </summary>  
        public string promotion_url_sub_title { get; set; }

        /// <summary>   
        ///   【选填】   每人可领券的数量限制,不填写默认为50。
        /// </summary>  
        public int get_limit { get; set; }

        /// <summary>   
        ///   【选填】   每人可核销的数量限制,不填写默认为50。
        /// </summary>  
        public int use_limit { get; set; }

        /// <summary>   
        ///  【选填】    卡券领取页面是否可分享。
        /// </summary>  
        public bool can_share { get; set; }

        /// <summary>   
        ///   【选填】   卡券是否可转赠。
        /// </summary>  
        public bool can_give_friend { get; set; }
        
        #endregion

        /// <summary>
        ///   状态  添加卡券时不做处理
        /// </summary>
        [JsonConverter(typeof(StringConverter))]
        public WxCardStatus status { get; set; }

    }

    /// <summary>
    ///  卡券sku信息
    /// </summary>
    public class WxCardBasicSkuMo
    {
        /// <summary>   
        ///   必填    int卡券库存的数量，上限为100000000。
        /// </summary>  
        public int quantity { get; set; }

    }

    /// <summary>
    ///   卡券时间限制实体
    /// </summary>
    public class WxCardBasicDateMo
    {
        /// <summary>   
        ///   必填    stringDATE_TYPE_FIX_TIME_RANGE表示固定日期区间，DATE_TYPE_FIX_TERM表示固定时长（自领取后按天算。
        /// </summary>  
        [JsonConverter(typeof(StringEnumConverter))]
        public WxCardDateType type { get; set; }

        /// <summary>   
        ///  unsignedinttype为DATE_TYPE_FIX_TIME_RANGE时【必填】，表示起用时间。从1970年1月1日00:00:00至起用时间的秒数，最终需转换为字符串形态传入。（东八区时间,UTC+8，单位为秒）
        /// </summary>  
        public uint begin_timestamp { get; set; }

        /// <summary>   
        ///   固定时间段时必填    表示结束时间，建议设置为截止日期的23:59:59过期。（东八区时间,UTC+8，单位为秒）
        ///   固定时长时选填   表示过期时间
        /// </summary>  
        public uint end_timestamp { get; set; }

        /// <summary>   
        ///   【固定】时长专用【必填】    inttype为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天内有效，不支持填写0。
        /// </summary>  
        public int fixed_term { get; set; }

        /// <summary>   
        ///   【固定】时长专用【必填】    int   type为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天开始生效，领取后当天生效填写0。（单位为天）
        /// </summary>  
        public int fixed_begin_term { get; set; }
    }

    #endregion

    #region  卡券高级信息

    public class WxCardAdvancedMo
    {
        /// <summary>   
        ///   可空    JSON结构使用门槛（条件）字段，若不填写使用条件则在券面拼写：无最低消费限制，全场通用，不限品类；并在使用说明显示：可与其他优惠共享
        /// </summary>  
        public WxCardAdUseConditionMo use_condition { get; set; }

        /// <summary>   
        ///   可空    JSON结构封面摘要结构体名称
        /// </summary>  
        [JsonProperty("abstract")]
        public WxCardAdAbstractMo Abstract { get; set; }

        /// <summary>   
        ///   可空 显示在详情内页，优惠券券开发者须至少传入一组图文列表
        /// </summary>  
        public WxCardAdTextImgMo text_image_list { get; set; }

        /// <summary>   
        ///   可空    arry商家服务类型，从 WxCardBusinessService 枚举中获取 可多选 
        /// </summary>  
        public List<string> business_service { get; set; }

        /// <summary>   
        ///   可空    JSON结构使用时段限制，包含以下字段
        /// </summary>  
        public WxCardAdTimeLimitMo time_limit { get; set; }
    }


    /// <summary>
    /// 卡券高级信息中的使用条件实体
    /// </summary>
    public class WxCardAdUseConditionMo
    {
        /// <summary>   
        ///   可空    string（512）指定可用的商品类目，仅用于代金券类型，填入后将在券面拼写适用于xxx
        /// </summary>  
        public string accept_category { get; set; }

        /// <summary>   
        ///   可空    string（512）指定不可用的商品类目，仅用于代金券类型，填入后将在券面拼写不适用于xxxx
        /// </summary>  
        public string reject_category { get; set; }

        /// <summary>   
        ///   可空    int满减门槛字段，可用于兑换券和代金券，填入后将在全面拼写消费满xx元可用。
        /// </summary>  
        public int least_cost { get; set; }

        /// <summary>   
        ///   可空    string（512）购买xx可用类型门槛，仅用于兑换，填入后自动拼写购买xxx可用。
        /// </summary>  
        public string object_use_for { get; set; }
        
        /// <summary>   
        ///   可空    bool不可以与其他类型共享门槛，
        /// 填写false时系统将在使用须知里拼写“不可与其他优惠共享”，
        /// 填写true时系统将在使用须知里拼写“可与其他优惠共享”，默认为true
        /// </summary>  
        public bool can_use_with_other_discount { get; set; } = true;
    }


    /// <summary>
    ///   卡券封面摘要实体
    /// </summary>
    public class WxCardAdAbstractMo
    {
        /// <summary>   
        ///   可空  封面摘要简介
        /// </summary>  
        [JsonProperty("abstract")]
        public string Abstract { get; set; }

        /// <summary>   
        ///   可空    string（128）封面图片列表，仅支持填入一个封面图片链接，上传图片接口上传获取图片获得链接，填写非CDN链接会报错，并在此填入。建议图片尺寸像素850*350
        /// </summary>  
        public List<string> icon_url_list { get; set; }
    }



    public class WxCardAdTextImgMo
    {
        /// <summary>   
        ///   可空    string（128）图片链接，必须调用上传图片接口上传图片获得链接，并在此填入，否则报错
        /// </summary>  
        public string image_url { get; set; }

        /// <summary>   
        ///   可空    string（512）图文描述
        /// </summary>  
        public string text { get; set; }
    }


    public class WxCardAdTimeLimitMo
    {
        /// <summary>   
        ///   可空    string（24）限制类型枚举值：支持填入MONDAY周一TUESDAY周二WEDNESDAY周三THURSDAY周四FRIDAY周五SATURDAY周六SUNDAY周日此处只控制显示，不控制实际使用逻辑，不填默认不显示
        /// </summary>  
        public WxCardTimeLimitType type { get; set; }

        /// <summary>   
        ///   可空    int当前type类型下的起始时间（小时），如当前结构体内填写了MONDAY，此处填写了10，则此处表示周一10:00可用
        /// </summary>  
        public int begin_hour { get; set; }

        /// <summary>   
        ///   可空    int当前type类型下的起始时间（分钟），如当前结构体内填写了MONDAY，begin_hour填写10，此处填写了59，则此处表示周一10:59可用
        /// </summary>  
        public int begin_minute { get; set; }

        /// <summary>   
        ///   可空    int当前type类型下的结束时间（小时），如当前结构体内填写了MONDAY，此处填写了20，则此处表示周一10:00-20:00可用
        /// </summary>  
        public int end_hour { get; set; }

        /// <summary>   
        ///   可空    int当前type类型下的结束时间（分钟），如当前结构体内填写了MONDAY，begin_hour填写10，此处填写了59，则此处表示周一10:59-00:59可用
        /// </summary>  
        public int end_minute { get; set; }
    }

    #endregion
}
