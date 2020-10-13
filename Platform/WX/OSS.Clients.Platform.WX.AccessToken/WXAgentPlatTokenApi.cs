#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.AccessToken
{
    /// <summary>
    ///  微信开放平台相关接口基类
    /// </summary>
    public class WXAgentPlatTokenApi : WXPlatBaseApi
    {
        /// <inheritdoc />
        public WXAgentPlatTokenApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
        {
        }

        ///// <summary>
        /////   获取公众号的AccessToken
        /////     【首先从缓存中获取，如果没有再从远程获取】
        ///// </summary>
        ///// <returns></returns>
        //public async Task<WXGetAgentAccessTokenResp> GetAgentAccessTokenFromCacheAsync()
        //{
        //    var m_OffcialAccessTokenKey = string.Format(WXCacheKeysHelper.OffcialAgentAccessTokenKey, appConfig.AppId);
        //    var tokenResp =await CacheHelper.GetAsync<WXGetAgentAccessTokenResp>(m_OffcialAccessTokenKey, WXPlatConfigProvider.CacheSourceName);

        //    if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
        //        return tokenResp;

        //    tokenResp = await GetAgentAccessTokenFromWXAsync();

        //    if (!tokenResp.IsSuccess())
        //        return tokenResp;

        //    tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

        //    await CacheHelper.SetAbsoluteAsync(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in-600), WXPlatConfigProvider.CacheSourceName);
        //    return tokenResp;
        //}


        /// <summary>
        /// 从微信服务器获取AccessToken，请注意访问速率控制，正常情况请访问： 【GetAgentAccessTokenFromCacheAsync】
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetAgentAccessTokenResp> GetAgentAccessTokenFromWXAsync()
        {
            if (WXPlatConfigProvider.AgentAccessTokenHub==null)
                throw new NullReferenceException("WXPlatConfigProvider 下 AgentAccessTokenHub 接口属性不能为空，VerifyTicket由微信通过消息接口主动推送，需通过接口设置 获取VerifyTicket 实现。");

            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetAgentAccessTokenResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var verifyTicketRes =await WXPlatConfigProvider.AgentAccessTokenHub.GetAgentVerifyTicket(appConfig);
            if(!verifyTicketRes.IsSuccess())
                return new WXGetAgentAccessTokenResp().WithResp(verifyTicketRes);

            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"component_appsecret\":\"").Append(appConfig.AppSecret).Append("\",");
            strContent.Append("\"component_verify_ticket\":\"").Append(verifyTicketRes.data).Append("\" }");

            var req = new OssHttpRequest
            {
                AddressUrl =$"{m_ApiUrl}/cgi-bin/component/api_component_token",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonJson<WXGetAgentAccessTokenResp>(req);
        }

    }
}
