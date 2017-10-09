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
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Plugs.CachePlug;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Assist.Mos;
using OSS.SnsSdk.Official.Wx.SysTools;
using OSS.SnsSdk.Official.Wx.SysTools.Mos;

namespace OSS.SnsSdk.Official.Wx.Assist
{
    /// <summary>
    ///   微信公众号辅助接口
    /// </summary>
    public class WxOffAssistApi : WxOffBaseApi
    {

        /// <summary>
        ///   辅助类Api
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOffAssistApi(AppConfig config=null) : base(config)
        {
        }

        /// <summary>
        ///  获取js 接口 Ticket
        /// 内部已经处理缓存
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<WxGetJsTicketResp> GetJsTicketFromCacheAsync(WxJsTicketType type)
        {
            var key = string.Format(WxCacheKeysUtil.OffcialJsTicketKey, ApiConfig.AppId, type);

            var ticket = CacheUtil.Get<WxGetJsTicketResp>(key, ModuleName);
            if (ticket != null && ticket.expires_time > DateTime.Now)
                return ticket;

            var ticketRes =await GetJsTicketFromWxAsync(type);
            if (!ticketRes.IsSuccess())
                return ticketRes;

            ticketRes.expires_time = DateTime.Now.AddSeconds(ticketRes.expires_in);

            CacheUtil.AddOrUpdate(key, ticketRes, TimeSpan.FromSeconds(ticketRes.expires_in - 10), null,
                ModuleName);
            return ticketRes;
        }

        /// <summary>
        ///  获取js 接口 Ticket
        /// 内部已经处理缓存
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<WxGetJsTicketResp> GetJsTicketFromWxAsync(WxJsTicketType type)
        {
            var req = new OsHttpRequest
            {
                HttpMothed = HttpMothed.GET,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/ticket/getticket?type=", type.ToString())
            };

            return await RestCommonOffcialAsync<WxGetJsTicketResp>(req);
        }
    }
}
