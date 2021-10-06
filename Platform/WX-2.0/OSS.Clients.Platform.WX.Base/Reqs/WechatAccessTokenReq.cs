using System.Net.Http;

namespace OSS.Clients.Platform.WX.Base.Reqs
{
    public class WechatAccessTokenReq:WechatBaseReq
    {
        public WechatAccessTokenReq() : base(HttpMethod.Get)
        {
        }

        protected override string GetApiUrl()
        {
            return
                $"{WechatPlatformHelper.ApiHost}/cgi-bin/token?grant_type=client_credential&appid={app_config.app_id}&secret={app_config.app_secret}";
        }
    }


    /// <summary>
    ///   公众号功能接口accesstoken信息
    /// </summary>
    public class WXPlatAccessTokenResp : WechatBaseResp
    {
        /// <summary>
        ///   token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 	凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 【UTC】过期时间，接口获取数据后根据expires_in 计算的值(可以扣除十分钟，作为中间的缓冲值)
        /// </summary>
        public long expires_date { get; set; }
    }
}
