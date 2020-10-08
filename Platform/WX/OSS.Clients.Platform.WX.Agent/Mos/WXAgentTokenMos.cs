#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 获取平台授权token相关实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion

using OSS.Clients.Platform.WX.Base.Mos;
using System.Collections.Generic;

namespace OSS.Clients.Platform.WX.Agent.Mos
{
    /// <summary>
    /// 获取预授权code
    /// </summary>
    public class WXGetPreAuthCodeResp : WXBaseResp
    {
        /// <summary>   
        ///   预授权码
        /// </summary>  
        public string pre_auth_code { get; set; }

        /// <summary>   
        ///   有效期，为10分钟
        /// </summary>  
        public string expires_in { get; set; }
    }

    
    #region 获取授权AccessToken相关信息


    /// <summary>
    ///  获取当前授权账号的AccessToken
    /// </summary>
    public class WXGetGrantedAccessTokenResp : WXBaseResp
    {
        /// <summary>   
        ///   授权信息
        /// </summary>  
        public WXGrantedTokenInfoMo authorization_info { get; set; }
    }

    /// <summary>
    ///    授权相关的权限和Token信息
    /// </summary>
    public class WXGrantedTokenInfoMo
    {
        /// <summary>   
        ///   授权方appid
        /// </summary>  
        public string authorizer_appid { get; set; }


        /// <summary>   
        ///   授权方接口调用凭据（在授权的公众号或小程序具备API权限时，才有此返回值），也简称为令牌
        /// </summary>  
        public string authorizer_access_token { get; set; }

        /// <summary>   
        ///   有效期（在授权的公众号或小程序具备API权限时，才有此返回值）
        /// </summary>  
        public int expires_in { get; set; }

        /// <summary>   
        ///   接口调用凭据刷新令牌（在授权的公众号具备API权限时，才有此返回值）
        ///   刷新令牌主要用于第三方平台获取和刷新已授权用户的access_token，只会在授权时刻提供，请妥善保存。
        ///   一旦丢失，只能让用户重新授权，才能再次拿到新的刷新令牌
        /// </summary>  
        public string authorizer_refresh_token { get; set; }

        /// <summary>
        /// 公众号授权给开发者的权限集列表，ID为1到15时分别代表：
        /// 消息管理权限， 用户管理权限， 帐号服务权限
        /// 网页服务权限， 微信小店权限， 微信多客服权限
        /// 群发与通知权限，微信卡券权限， 微信扫一扫权限
        /// 微信连WIFI权限，素材管理权限，微信摇周边权限
        /// 微信门店权限， 微信支付权限， 自定义菜单权限
        /// </summary>
        public List<WXFuncInfoItem> func_info { get; set; }

    }


    #endregion

    
    /// <summary>
    /// 刷新当前授权账号的AccessToken
    /// </summary>
    public class WXRefreshGrantedAccessTokenResp : WXBaseResp
    {
        /// <summary>   
        ///   授权方令牌
        /// </summary>  
        public string authorizer_access_token { get; set; }

        /// <summary>   
        ///   有效期（秒），为2小时
        /// </summary>  
        public int expires_in { get; set; }

        /// <summary>   
        ///   刷新令牌
        /// </summary>  
        public string authorizer_refresh_token { get; set; }

    }


    /// <summary>
    ///  获取授权账号列表
    /// </summary>
    public class WXGetGrantorListResp : WXBaseResp
    {
        /// <summary>
        ///  授权总数量
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        ///  授权账号信息
        /// </summary>
        public List<WXGrantorListItemMo> list { get; set; }
    }

    public class WXGrantorListItemMo
    {
        /// <summary>   
        ///   
        /// </summary>  
        public string authorizer_appid { get; set; }

        /// <summary>   
        ///   
        /// </summary>  
        public string refresh_token { get; set; }

        /// <summary>   
        ///   
        /// </summary>  
        public string auth_time { get; set; }
    }
}
