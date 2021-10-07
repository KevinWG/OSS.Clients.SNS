using System.Net.Http;

namespace OSS.Clients.Platform.WX
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
