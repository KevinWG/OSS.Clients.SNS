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
using OSS.Clients.Platform.WX.Agent.Mos;
using OSS.Common.Extention;
using OSS.Clients.Platform.WX.Helpers;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Cache;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX
{
    /// <summary>
    ///  微信开放平台相关接口基类
    /// </summary>
    public class WXAgentBaseApi : WXBaseApi
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WXAgentBaseApi(AppConfig config) : base(config)
        {
        }

        /// <summary>
        ///   获取公众号的AccessToken
        ///     【首先从缓存中获取，如果没有再从远程获取】
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetAgentAccessTokenResp> GetAgentAccessTokenFromCacheAsync()
        {
            var m_OffcialAccessTokenKey = string.Format(WXCacheKeysHelper.OffcialAgentAccessTokenKey, ApiConfig.AppId);
            var tokenResp =await CacheHelper.GetAsync<WXGetAgentAccessTokenResp>(m_OffcialAccessTokenKey, WXPlatConfigProvider.CacheSourceName);

            if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
                return tokenResp;

            tokenResp = await GetAgentAccessTokenFromWXAsync();

            if (!tokenResp.IsSuccess())
                return tokenResp;

            tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

            await CacheHelper.SetAbsoluteAsync(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in-600), WXPlatConfigProvider.CacheSourceName);
            return tokenResp;
        }


        /// <summary>
        /// 从微信服务器获取AccessToken，请注意访问速率控制，正常情况请访问： 【GetAgentAccessTokenFromCacheAsync】
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetAgentAccessTokenResp> GetAgentAccessTokenFromWXAsync()
        {
            var verifyTicket = WXPlatConfigProvider.AgentVerifyTicketGetFunc?.Invoke(ApiConfig);
            if (string.IsNullOrEmpty(verifyTicket))
            {
                throw new ArgumentNullException("verifyticket", "verifyticket未发现，请检查 WXPlatConfigProvider 下 AgentVerifyTicketGetFunc 委托是否为空或者返回值不正确！");
            }

            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"component_appsecret\":\"").Append(ApiConfig.AppSecret).Append("\",");
            strContent.Append("\"component_verify_ticket\":\"").Append(verifyTicket).Append("\" }");

            var req = new OssHttpRequest
            {
                AddressUrl =$"{m_ApiUrl}/cgi-bin/component/api_component_token",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonJson<WXGetAgentAccessTokenResp>(req);
        }

        /// <summary>
        ///   第三方代理平台的请求方法
        ///      预处理component_access_token赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="client">自定义 HttpClient </param>
        /// <returns></returns>
        protected async Task<T> RestCommonAgentAsync<T>(OssHttpRequest req,
            HttpClient client = null)
            where T : WXBaseResp, new()
        {
            var tokenRes = await GetAgentAccessTokenFromCacheAsync();

            if (!tokenRes.IsSuccess())
                return new T().WithResp(tokenRes);// tokenRes.ConvertToResultInherit<T>();
            
            req.AddressUrl = string.Concat(req.AddressUrl,// req.AddressUrl.IndexOf('?') > 0 ? "&" : "?",
                "?component_access_token=",tokenRes.component_access_token);

            return await RestCommonJson<T>(req);
        }
        
    }
}
