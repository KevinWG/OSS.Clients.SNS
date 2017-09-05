#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 授权相关实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion


using System.Collections.Generic;

namespace OSS.SnsSdk.Official.Wx.Agent.Mos
{
    /// <summary>
    ///   获取授权
    /// </summary>
    public class WxGetGrantorInfoResp : WxBaseResp
    {
        /// <summary>
        ///  授权权限的信息
        /// </summary>
        public WxGrantedInfoMo authorization_info { get; set; }

        /// <summary>
        ///  授权者本身的信息
        /// </summary>
        public WxGrantorInfoMo authorizer_info { get; set; }
    }


    #region 授权的权限相关信息

    /// <summary>
    ///  授权的权限相关信息
    /// </summary>
    public class WxGrantedInfoMo
    {
        /// <summary>   
        ///   授权方appid
        /// </summary>  
        public string appid { get; set; }

        /// <summary>
        /// 公众号授权给开发者的权限集列表，ID为1到15时分别代表：
        /// 消息管理权限， 用户管理权限， 帐号服务权限
        /// 网页服务权限， 微信小店权限， 微信多客服权限
        /// 群发与通知权限，微信卡券权限， 微信扫一扫权限
        /// 微信连WIFI权限，素材管理权限，微信摇周边权限
        /// 微信门店权限， 微信支付权限， 自定义菜单权限
        /// </summary>
        public List<WxFuncInfoItem> func_info { get; set; }
    }
    
    /// <summary>
    ///  授权的权限类别信息
    /// </summary>
    public class WxAuthIdItemMo
    {
        public int id { get; set; }
    }

    /// <summary>
    ///  授权的权限信息
    /// </summary>
    public class WxFuncInfoItem
    {

        public WxAuthIdItemMo funcscope_category { get; set; }
    }

    #endregion


    #region  授权账号信息
    /// <summary>
    /// 授权者信息
    /// </summary>
    public class WxGrantorInfoMo
    {
        /// <summary>   
        ///   授权方昵称
        /// </summary>  
        public string nick_name { get; set; }

        /// <summary>   
        ///   授权方头像
        /// </summary>  
        public string head_img { get; set; }

        /// <summary>   
        ///   授权方公众号类型，0代表订阅号，1代表由历史老帐号升级后的订阅号，2代表服务号
        /// </summary>  
        public WxAuthIdItemMo service_type_info { get; set; }

        /// <summary>   
        ///   授权方认证类型，
        /// -1代表未认证，0代表微信认证，1代表新浪微博认证，
        /// 2代表腾讯微博认证，3代表已资质认证通过但还未通过名称认证，
        /// 4代表已资质认证通过、还未通过名称认证，但通过了新浪微博认证，
        /// 5代表已资质认证通过、还未通过名称认证，但通过了腾讯微博认证
        /// </summary>  
        public WxAuthIdItemMo verify_type_info { get; set; }

        /// <summary>   
        ///   授权方公众号的原始ID
        /// </summary>  
        public string user_name { get; set; }

        /// <summary>   
        ///   公众号的主体名称
        /// </summary>  
        public string principal_name { get; set; }

        /// <summary>   
        ///   授权方公众号所设置的微信号，可能为空
        /// </summary>  
        public string alias { get; set; }

        /// <summary>   
        ///   用以了解以下功能的开通状况（0代表未开通，1代表已开通）
        /// </summary>  
        public WxGrantorBusinessMo business_info { get; set; }

        /// <summary>   
        ///   二维码图片的URL，开发者最好自行也进行保存
        /// </summary>  
        public string qrcode_url { get; set; }

        /// <summary>   
        ///   帐号介绍[小程序]
        /// </summary>  
        public string signature { get; set; }

        /// <summary>
        ///  小程序信息，公众号没有此信息
        /// </summary>
        public WxSmallProgramMo MiniProgramInfo { get; set; }
    }


    /// <summary>
    ///  授权者的业务信息
    ///    0代表未开通，1代表已开通
    /// </summary>
    public class WxGrantorBusinessMo
    {
        /// <summary>   
        ///   是否开通微信门店功能
        /// </summary>  
        public bool open_store { get; set; }

        /// <summary>   
        ///   是否开通微信扫商品功能
        /// </summary>  
        public bool open_scan { get; set; }

        /// <summary>   
        ///   是否开通微信支付功能
        /// </summary>  
        public bool open_pay { get; set; }

        /// <summary>   
        ///   是否开通微信卡券功能
        /// </summary>  
        public bool open_card { get; set; }

        /// <summary>   
        ///   是否开通微信摇一摇功能
        /// </summary>  
        public bool open_shake { get; set; }
    }

    public class WxSmallProgramMo
    {
        public WxSmallAppNetworkMo network { get; set; }
        public List<WxSmallAppCategorItemMo> categories { get; set; }
        public int  visit_status { get; set; }
    }

    /// <summary>
    ///  小程序网络设置模块
    /// </summary>
    public class WxSmallAppNetworkMo
    {
        /// <summary>   
        ///   接口域名
        /// </summary>  
        public string RequestDomain { get; set; }
        /// <summary>   
        ///   websocket域名
        /// </summary>  
        public string WsRequestDomain { get; set; }
        /// <summary>   
        ///   上传域名
        /// </summary>  
        public string UploadDomain { get; set; }
        /// <summary>   
        ///   下载域名
        /// </summary>  
        public string DownloadDomain { get; set; }
    }


    public class WxSmallAppCategorItemMo
    { 
        public string first { get; set; } 
        public string second { get; set; }
    }

    #endregion




}
