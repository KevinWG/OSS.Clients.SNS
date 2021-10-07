using System;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;
using OSS.Tools.Cache;

namespace OSS.Clients.Platform.WX.Base.Interface.Impls
{
    internal class InterJsTicketProvider : IJsTicketProvider
    {
        private const string jsTicketCacheKey = "OSS_Wechat_JSTicket_";

        public async Task<StrResp> GetJsTicket(IAppSecret config, WechatJsTicketType type)
        {
            var key            = string.Concat(jsTicketCacheKey, config.app_id);
            var jsTicketRes = await CacheHelper.GetAsync<WechatJsTicketResp>(key);

            if (jsTicketRes != null)
                return new StrResp(jsTicketRes.ticket);

            jsTicketRes = await new WechatJsTicketReq(type)
                .SetContextConfig(config)
                .SendAsync();

            if (!jsTicketRes.IsSuccess())
                return new StrResp().WithResp(jsTicketRes);

            // 按照返回的过期时间提前5分钟过期
            await CacheHelper.SetAbsoluteAsync(key, jsTicketRes, TimeSpan.FromSeconds(jsTicketRes.expires_in - 60 * 5), "OSS.Clients.Platform.WX");

            return new StrResp(jsTicketRes.ticket);

        }
    }
}
