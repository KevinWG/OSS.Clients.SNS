#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 微信开放平台相关接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion

using System;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Common.Plugs.CachePlug;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Basic.Mos;
using OSS.SnsSdk.Official.Wx.Platform.Mos;
using OSS.SnsSdk.Official.Wx.SysTools;

namespace OSS.SnsSdk.Official.Wx.Platform
{
    /// <summary>
    ///  微信开放平台相关接口
    /// </summary>
    public class WxOffPlatformApi: WxOffBaseApi
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxOffPlatformApi(AppConfig config=null) : base(config)
        {
        }

        ///// <summary>
        /////   获取公众号的AccessToken
        /////     【首先从缓存中获取，如果没有再从远程获取】
        ///// </summary>
        ///// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        ///// <returns></returns>
        //public virtual async Task<WxGetPlatformAccessTokenResp> GetPlatformAccessTokenFromCacheAsync(string verifyTicket)
        //{
        //    var m_OffcialAccessTokenKey = string.Format(WxCacheKeysUtil.OffcialPlatformAccessTokenKey, ApiConfig.AppId);
        //    var tokenResp = CacheUtil.Get<WxGetPlatformAccessTokenResp>(m_OffcialAccessTokenKey, ModuleName);

        //    if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
        //        return tokenResp;

        //    tokenResp = await GetPlatformAccessTokenFromWxAsync();

        //    if (!tokenResp.IsSuccess())
        //        return tokenResp;

        //    tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

        //    CacheUtil.AddOrUpdate(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in),
        //        null, ModuleName);
        //    return tokenResp;
        //}

        ///// <summary>
        ///// 从微信服务器获取AccessToken，请注意访问速率控制，正常情况请访问： 【GetAccessTokenFromCacheAsync】
        ///// </summary>
        ///// <returns></returns>
        //public async Task<WxGetPlatformAccessTokenResp> GetPlatformAccessTokenFromWxAsync()
        //{
        //    var req = new OsHttpRequest
        //    {
        //        AddressUrl =
        //            $"{m_ApiUrl}/cgi-bin/token?grant_type=client_credential&appid={ApiConfig.AppId}&secret={ApiConfig.AppSecret}",
        //        HttpMothed = HttpMothed.GET
        //    };
        //    return await RestCommonJson<WxOffAccessTokenResp>(req);
        //}

    }
}
