#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述: 公号的功能辅助接口 —— js相关接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-24
*       
*****************************************************************************/

#endregion

using System;
using OS.Common.Modules;
using OS.Common.Modules.CacheModule;
using OS.Http;
using OS.Http.Models;
using OSS.Social.WX.Offcial.Assist.Mos;
using OSS.Social.WX.SysUtils;
using OSS.Social.WX.SysUtils.Mos;

namespace OSS.Social.WX.Offcial.Assist
{
    /// <summary>
    ///   微信公众号辅助接口
    /// </summary>
    public partial class WxOffAssistApi : WxOffBaseApi
    {
        public WxOffAssistApi(WxAppCoinfig config) : base(config)
        {
        }

        /// <summary>
        ///  获取js 接口 Ticket
        /// 内部已经处理缓存
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public WxGetJsTicketResp GetJsTicket(WxJsTicketType type)
        {
            string key = string.Format(WxCacheKeysUtil.OffcialJsTicketKey, ApiConfig.AppId, type);
            var ticket = CacheUtil.Get<WxGetJsTicketResp>(key);
            if (ticket != null && ticket.expires_time > DateTime.Now)
                return ticket;

            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, string.Concat("cgi-bin/ticket/getticket?type=", type.ToString()));

            var ticketRes = RestCommonOffcial<WxGetJsTicketResp>(req);
            if (ticketRes.IsSuccess)
            {
                ticketRes.expires_time = DateTime.Now.AddSeconds(ticketRes.expires_in);
                CacheUtil.AddOrUpdate(key, ticketRes, TimeSpan.FromSeconds(ticketRes.expires_in - 10), null,
                    ModuleNames.SnsCenter);
            }
            return ticketRes;
        }
    }
}
