using Newtonsoft.Json;
using System;

namespace OSS.Clients.Platform.WX.Base.Mos
{
    public class WXGetJsTicketResp : WXBaseResp
    {
        /// <summary>   
        ///   签名所需凭证
        /// </summary>  
        public string ticket { get; set; }

        /// <summary>   
        ///   有效时间
        /// </summary>  
        public int expires_in { get; set; }

        /// <summary>
        ///   过期时间
        /// </summary>
        [JsonIgnore]
        public DateTime expires_time { get; set; }
    }
}
