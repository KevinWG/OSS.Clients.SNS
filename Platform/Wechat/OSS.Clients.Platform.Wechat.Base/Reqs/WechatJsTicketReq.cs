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

using System.Net.Http;

namespace OSS.Clients.Platform.Wechat
{
    public class WechatJsTicketReq:WechatBaseTokenReq<WechatJsTicketResp>
    {
        public WechatJsTicketReq(WechatJsTicketType type=WechatJsTicketType.jsapi) : base(HttpMethod.Get)
        {
            _jsType = type;
        }

        private WechatJsTicketType _jsType;

        public override string GetApiPath()
        {
            return string.Concat("/cgi-bin/ticket/getticket?type=", _jsType.ToString());
        }
    }

    public class WechatJsTicketResp : WechatBaseResp
    {
        /// <summary>   
        ///   签名所需凭证
        /// </summary>  
        public string ticket { get; set; }

        /// <summary>   
        ///   有效时间
        /// </summary>  
        public int expires_in { get; set; }
    }
}
