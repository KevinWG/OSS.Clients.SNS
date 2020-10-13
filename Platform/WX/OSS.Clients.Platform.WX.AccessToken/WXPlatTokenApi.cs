#region Copyright (C) 2020 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能，获取AccessToken
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2020-10-7
*       
*****************************************************************************/

#endregion

using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Mos;
using System.Net.Http;
using System.Threading.Tasks;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.Platform.WX.AccessToken
{
    /// <summary>
    /// 微信公众号AccessToken接口实现
    /// </summary>
    public class WXPlatTokenApi : WXPlatBaseApi
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="configProvider"></param>
        public WXPlatTokenApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
        {
        }

        //        public async Task<StrResp> GetAccessToken(AppConfig config)
        //        {

        //            var m_OffcialAccessTokenKey = string.Format(WXCacheKeysHelper.OffcialAccessTokenKey, config.AppId);
        //            var tokenResp = await CacheHelper.GetAsync<WXPlatAccessTokenResp>(m_OffcialAccessTokenKey, WXPlatConfigProvider.CacheSourceName);

        //            if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
        //                return tokenResp;

        //            tokenResp = await GetAccessTokenFromWXAsync();

        //            if (!tokenResp.IsSuccess())
        //                return tokenResp;

        //            tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

        //            await CacheHelper.SetAbsoluteAsync(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in - 600), WXPlatConfigProvider.CacheSourceName);

        //            return tokenResp;
        //        }

        /// <summary>
        /// 从微信服务器获取AccessToken，请注意访问速率控制
        /// </summary>
        /// <returns></returns>
        public async Task<WXPlatAccessTokenResp> GetAccessTokenFromWXAsync()
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXPlatAccessTokenResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var req = new OssHttpRequest
            {
                AddressUrl =
                    $"{m_ApiUrl}/cgi-bin/token?grant_type=client_credential&appid={appConfig.AppId}&secret={appConfig.AppSecret}",
                HttpMethod = HttpMethod.Get
            };
            return await RestCommonJson<WXPlatAccessTokenResp>(req);
        }



        ///// <summary>
        /////  获取js 接口 Ticket
        ///// 内部已经处理缓存
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public async Task<WXGetJsTicketResp> GetJsTicketFromCacheAsync(WXJsTicketType type)
        //{
        //    var key = string.Format(WXCacheKeysHelper.OffcialJsTicketKey, appConfig.AppId, type);

        //    var ticket =await CacheHelper.GetAsync<WXGetJsTicketResp>(key, WXPlatConfigProvider.CacheSourceName);
        //    if (ticket != null && ticket.expires_time > DateTime.Now)
        //        return ticket;

        //    var ticketRes =await GetJsTicketFromWXAsync(type);
        //    if (!ticketRes.IsSuccess())
        //        return ticketRes;

        //    ticketRes.expires_time = DateTime.Now.AddSeconds(ticketRes.expires_in);

        //    await CacheHelper.SetAbsoluteAsync(key, ticketRes, TimeSpan.FromSeconds(ticketRes.expires_in - 10), WXPlatConfigProvider.CacheSourceName);
        //    return ticketRes;
        //}

        /// <summary>
        ///  获取js 接口 Ticket,请注意访问速率控制
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<WXGetJsTicketResp> GetJsTicketFromWXAsync(WXJsTicketType type)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/ticket/getticket?type=", type.ToString())
            };

            return await RestCommonPlatAsync<WXGetJsTicketResp>(req);
        }
    }
}