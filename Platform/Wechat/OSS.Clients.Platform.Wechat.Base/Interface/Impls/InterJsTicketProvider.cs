#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;
using OSS.Tools.Cache;

namespace OSS.Clients.Platform.Wechat.Base.Interface.Impls
{
    internal class InterJsTicketProvider : IJsTicketProvider
    {
        private const string jsTicketCacheKey = "OSS_Wechat_JSTicket_";

        public async Task<StrResp> GetJsTicket(WechatJsTicketType type, WechatBaseReq req)
        {
            var key         = string.Concat(jsTicketCacheKey, req.app_config.app_id);
            var jsTicketRes = await CacheHelper.GetAsync<WechatJsTicketResp>(key);

            if (jsTicketRes != null)
                return new StrResp(jsTicketRes.ticket);

            jsTicketRes = await new WechatJsTicketReq(type)
                .SetContextConfig(req.app_config)
                .ExecuteAsync();

            if (!jsTicketRes.IsSuccess())
                return new StrResp().WithResp(jsTicketRes);

            // 按照返回的过期时间提前5分钟过期
            await CacheHelper.SetAbsoluteAsync(key, jsTicketRes, TimeSpan.FromSeconds(jsTicketRes.expires_in - 60 * 5), "OSS.Clients.Platform.Wechat");

            return new StrResp(jsTicketRes.ticket);
        }
    }
}
