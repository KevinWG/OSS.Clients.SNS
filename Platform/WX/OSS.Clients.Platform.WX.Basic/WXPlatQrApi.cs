#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：公号二维码管理部分
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-16
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.Extension;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Mos;
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Common.BasicImpls;

namespace OSS.Clients.Platform.WX.Basic
{
    /// <summary>
    ///   二维码处理
    /// </summary>
    public  class WXPlatQrApi:WXPlatBaseApi
    {
        /// <inheritdoc />
        public WXPlatQrApi(IMetaProvider<AppConfig> configProvider=null) : base(configProvider)
        {

        }

        /// <summary>
        /// 获取二维码ticket
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<WXQrCodeResp> GetQrCodeTicketAsync(WXCreateSenceQrReq req)
        {
            var reqest = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/qrcode/create"),
                CustomBody = JsonConvert.SerializeObject(req)
            };

            return await RestCommonPlatAsync<WXQrCodeResp>(reqest);
        }


        /// <summary>
        /// 获取二维码图片地址
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public string GetQrCodeUrl(string ticket)
        {
            return string.Concat("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=",ticket.UrlEncode());
        }

        /// <summary>
        /// 生成短链
        /// </summary>
        /// <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <returns></returns>
        public async Task<WXShortUrlResp> GetShortUrl(string longUrl)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/shorturl"),
                CustomBody = $"{{\"action\":\"long2short\",\"long_url\":\"{longUrl}\"}}"
            };

            return await RestCommonPlatAsync<WXShortUrlResp>(req);
        }
        
    }
}
