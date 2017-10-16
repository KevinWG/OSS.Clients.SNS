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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Encrypt;
using OSS.Common.Extention;
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

        /// <summary>
        ///  获取jssdk签名信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async  Task<WxJsSdkSignatureResp> GetJsSdkSignature(string url)
        {
            var ticketRes = await GetJsTicketFromCacheAsync(WxJsTicketType.jsapi);
            if (!ticketRes.IsSuccess())
            {
                return ticketRes.ConvertToResult<WxJsSdkSignatureResp>();
            }

            var resp = new WxJsSdkSignatureResp
            {
                app_id = ApiConfig.AppId,
                noncestr = GenerateNonceStr(),
                timestamp = DateTime.Now.ToLocalSeconds()
            };


            var signStr= new StringBuilder();
            signStr.Append("jsapi_ticket=").Append(ticketRes.ticket);
            signStr.Append("&noncestr=").Append(resp.noncestr);
            signStr.Append("&timestamp=").Append(resp.timestamp);
            if (!string.IsNullOrEmpty(url))
            {
                signStr.Append("&url=").Append(url);
            }
            
            resp.signature= Sha1.Encrypt(signStr.ToString());

            return resp;
        }



        private static readonly Random _rnd = new Random();
        private static readonly char[] _arrChar = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 'q', 's', 't', 'u', 'v',
            'w', 'z', 'y', 'x',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Q', 'P', 'R', 'T', 'S', 'V', 'U',
            'W', 'X', 'Y', 'Z'
        };
        /// <summary>
        /// 生成随机串
        /// </summary>
        /// <returns></returns>
        private static string GenerateNonceStr()
        {
            var num = new StringBuilder();

            for (var i = 0; i < 8; i++)
            {
                num.Append(_arrChar[_rnd.Next(0, 59)].ToString());
            }
            return num.ToString();
        }
    }
}
