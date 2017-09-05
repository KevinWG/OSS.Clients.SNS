#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 微信开放平台相关接口基类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Common.Plugs.CachePlug;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Agent.Mos;
using OSS.SnsSdk.Official.Wx.SysTools;

namespace OSS.SnsSdk.Official.Wx
{
    /// <summary>
    ///  微信开放平台相关接口基类
    /// </summary>
    public class WxAgentBaseApi : WxBaseApi
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxAgentBaseApi(AppConfig config=null) : base(config)
        {
        }

        /// <summary>
        ///   获取公众号的AccessToken
        ///     【首先从缓存中获取，如果没有再从远程获取】
        /// </summary>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <returns></returns>
        public virtual async Task<WxGetAgentAccessTokenResp> GetAgentAccessTokenFromCacheAsync(string verifyTicket)
        {
            var m_OffcialAccessTokenKey = string.Format(WxCacheKeysUtil.OffcialPlatformAccessTokenKey, ApiConfig.AppId);
            var tokenResp = CacheUtil.Get<WxGetAgentAccessTokenResp>(m_OffcialAccessTokenKey, ModuleName);

            if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
                return tokenResp;

            tokenResp = await GetAgentAccessTokenFromWxAsync(verifyTicket);

            if (!tokenResp.IsSuccess())
                return tokenResp;

            tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

            CacheUtil.AddOrUpdate(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in-600),
                null, ModuleName);
            return tokenResp;
        }


        /// <summary>
        /// 从微信服务器获取AccessToken，请注意访问速率控制，正常情况请访问： 【GetAgentAccessTokenFromCacheAsync】
        /// </summary>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <returns></returns>
        public async Task<WxGetAgentAccessTokenResp> GetAgentAccessTokenFromWxAsync(string verifyTicket)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"component_appsecret\":\"").Append(ApiConfig.AppSecret).Append("\",");
            strContent.Append("\"component_verify_ticket\":\"").Append(verifyTicket).Append("\" }");

            var req = new OsHttpRequest
            {
                AddressUrl =$"{m_ApiUrl}/cgi-bin/component/api_component_token",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonJson<WxGetAgentAccessTokenResp>(req);
        }
        
        /// <summary>
        ///   第三方代理平台的请求方法
        ///      预处理component_access_token赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <param name="client">自定义 HttpClient </param>
        /// <returns></returns>
        protected async Task<T> RestCommonAgentAsync<T>(OsHttpRequest req,string verifyTicket,
            HttpClient client = null)
            where T : WxBaseResp, new()
        {
            var tokenRes = await GetAgentAccessTokenFromCacheAsync(verifyTicket);

            if (!tokenRes.IsSuccess())
                return tokenRes.ConvertToResult<T>();
            
            req.AddressUrl = string.Concat(req.AddressUrl, req.AddressUrl.IndexOf('?') > 0 ? "&" : "?",
                "component_access_token=",tokenRes.component_access_token);

            return await RestCommonJson<T>(req, client);
        }
        
    }
}
