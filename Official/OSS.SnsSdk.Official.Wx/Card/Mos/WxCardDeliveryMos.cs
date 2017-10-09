#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  卡券投放实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using OSS.SnsSdk.Official.Wx.Basic.Mos;


namespace OSS.SnsSdk.Official.Wx.Card.Mos
{
    #region  投放二维码相关实体
    /// <summary>
    /// 微信卡券二维码请求
    /// </summary>
    internal class WxCreateCardQrReq
    {
        /// <summary>
        /// 过期时间，永久二维码请设置为0
        /// </summary>
        public int expire_seconds { get; set; }

        /// <summary>
        /// 生成二维码性质   
        /// 可以通过 typeof(WxQrCodeType).ToEnumDirs() 获取对应的枚举字典列表
        /// </summary>
        public string action_name { get; set; }

        /// <summary>
        ///   场景二维码信息
        /// </summary>
        public object action_info { get; set; }
    }


    public class WxCardQrCodeResp : WxQrCodeResp
    {

        /// <summary>
        /// 二维码显示地址，点击后跳转二维码页面   也可通过ticket接口获取
        /// </summary>
        public string show_qrcode_url { get; set; }
    }

    public class WxCardQrMo
    {
        /// <summary>   
        ///   可空    卡券ID。
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///   可空    指定领取者的openid，只有该用户能领取。bind_openid字段为true的卡券必须填写，非指定openid不必填写。
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///   可空    指定二维码的有效时间，范围是60~1800秒。不填默认为365天有效
        /// </summary>  
        public string expire_seconds { get; set; }

        /// <summary>   
        ///   可空    指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。填写true或false。默认false，注意填写该字段时，卡券须通过审核且库存不为0。
        /// </summary>  
        public string is_unique_code { get; set; }

        /// <summary>   
        ///   可空    领取场景值，用于领取渠道的数据统计，默认值为0，字段类型为整型，长度限制为60位数字。用户领取卡券后触发的事件推送中会带上此自定义场景值。
        /// </summary>  
        public string outer_id { get; set; }

        /// <summary>   
        ///   可空    outer_id字段升级版本，字符串类型，用户首次领卡时，会通过领取事件推送给商户；对于会员卡的二维码，用户每次扫码打开会员卡后点击任何url，会将该值拼入url中，方便开发者定位扫码来源
        /// </summary>  
        public string outer_str { get; set; }
    }
    #endregion


    #region  投放货架相关实体
    /// <summary>
    ///  创建卡券投放货架请求实体
    /// </summary>
    public class WxCreateCardLandPageReq
    {
        /// <summary>   
        ///   页面的banner图片链接，须调用，建议尺寸为640*300。必填    
        /// </summary>  
        public string banner { get; set; }

        /// <summary>   
        ///   页面的title。必填    
        /// </summary>  
        public string title { get; set; }

        /// <summary>   
        ///   页面是否可以分享,填入true/false必填    
        /// </summary>  
        public string can_share { get; set; }

        /// <summary>   
        ///   投放页面的场景值；SCENE_NEAR_BY附近 SCENE_MENU自定义菜单 SCENE_QRCODE二维码 SCENE_ARTICLE公众号文章 SCENE_H5h5页面SCENE_IVR自动回复 SCENE_CARD_CUSTOM_CELL卡券自定义 cell
        ///   typeof(WxCardLandPageSence).ToEnumDirs() 获取对应的枚举字典列表
        /// </summary>  
        public string scene { get; set; }
        
        /// <summary>   
        ///   卡券列表，每个item有两个字段必填    
        /// </summary>  
        public List<WxCardLandPageItemMo> card_list { get; set; }

    }

    /// <summary>
    /// 创建卡券投放货架响应实体
    /// </summary>
    public class WxCreateCardLandPageResp:WxBaseResp
    {
        /// <summary>
        /// 货架链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 货架ID。货架的唯一标识
        /// </summary>
        public long page_id { get; set; }

    }

    public class WxCardLandPageItemMo
    {

        /// <summary>   
        ///   所要在页面投放的card_id必填    
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///   缩略图url
        /// </summary>  
        public string thumb_url { get; set; }
    }

    #endregion

    /// <summary>
    ///   获取卡券图文推送内容响应实体
    /// </summary>
    public class WxGetCardArticleContentResp : WxBaseResp
    {
        /// <summary>
        /// 返回一段html代码，可以直接嵌入到图文消息的正文里。即可以把这段代码嵌入到上传图文消息素材接口中的content字段里
        /// </summary>
        public string content { get; set; }
    }


    #region  导入卡券code 相关实体

    public class WxImportCardCodeResp : WxBaseResp
    {
        /// <summary>   
        ///   成功个数
        /// </summary>  
        public int succ_code { get; set; }

        /// <summary>   
        ///   重复导入的code会自动被过滤。
        /// </summary>  
        public int duplicate_code { get; set; }

        /// <summary>   
        ///   失败个数。
        /// </summary>  
        public int fail_code { get; set; }

    }

    public class WxGetImportCodeCountResp : WxBaseResp
    {
        /// <summary>
        /// 已经成功存入的code数目。
        /// </summary>
        public int count { get; set; }
    }

    public class WxCheckImportCodeResp : WxBaseResp
    {
        /// <summary>
        /// 已经成功存入的code。
        /// </summary>
        public List<string> exist_code { get; set; }

        /// <summary>
        /// 没有存入的code。
        /// </summary>
        public List<string> not_exist_code { get; set; }
    }

    #endregion
}
