using System.Net.Http;

namespace OSS.Clients.Platform.WX.User
{
    /// <summary>
    ///  获取被拉黑用户列表
    /// </summary>
    public class WechatBlackedOpenIdListReq : WechatBaseTokenReq<WechatOpenIdListResp>
    {
        public WechatBlackedOpenIdListReq() : base(HttpMethod.Post)
        {
        }

        public string next_openid { get; set; }
        
        public override string GetApiPath()
        {
            return "/cgi-bin/tags/members/getblacklist";
        }
        protected override void PrepareSend()
        {
            custom_body = $"{{\"begin_openid\":\"{next_openid}\"}}";
        }
    }
    
}
