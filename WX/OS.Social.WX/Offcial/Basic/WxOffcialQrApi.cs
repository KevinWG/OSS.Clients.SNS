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

using System.Text;
using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Basic.Mos;

namespace OS.Social.WX.Offcial.Basic
{
    /// <summary>
    ///   二维码处理
    /// </summary>
    public partial class WxOffcialApi
    {
        /// <summary>
        /// 获取二维码ticket
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public WxQrCodeTicketResp GetQrCodeTicket(WxQrCodeTicketReq req)
        {
            var reqest = new OsHttpRequest();
            reqest.HttpMothed = HttpMothed.POST;
            reqest.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/qrcode/create");

            StringBuilder strParas = new StringBuilder("{");

            if (req.expire_seconds > 0)
                strParas.Append("\"expire_seconds\":").Append(req.expire_seconds).Append(",");

            strParas.Append("\"action_name\":\"");
            strParas.Append(req.expire_seconds > 0 ? "QR_SCENE" : "QR_LIMIT_STR_SCENE").Append("\",");
            strParas.Append("\"action_info\":{\"scene\": {");

            if (req.scene_id > 0)
                strParas.Append("\"scene_id\": ").Append(req.scene_id);
            else
                strParas.Append("\"scene_str\": \"").Append(req.scene_str).Append("\"");

            reqest.CustomBody = strParas.ToString();
            return RestCommonOffcial<WxQrCodeTicketResp>(reqest);
        }


        /// <summary>
        /// 获取二维码图片地址
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public string GetQrCodeUrl(string ticket)
        {
            return string.Concat("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=",ticket);
        }

        /// <summary>
        /// 生成短链
        /// </summary>
        /// <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <returns></returns>
        public WxShortUrlResp GetShortUrl(string longUrl)
        {
            var req=new OsHttpRequest();
            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/shorturl");
            req.CustomBody = $"{{\"action\":\"long2short\",\"long_url\":\"{longUrl}\"}}";

            return RestCommonOffcial<WxShortUrlResp>(req);
        }

    }
}
