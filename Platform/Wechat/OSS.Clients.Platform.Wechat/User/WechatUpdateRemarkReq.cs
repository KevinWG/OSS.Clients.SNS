
using System.Net.Http;

namespace OSS.Clients.Platform.WX.User
{
    public class WechatUpdateRemarkReq:WechatBaseTokenReq<WechatBaseResp>
    {
        public WechatUpdateRemarkReq(string openid, string remark) : base(HttpMethod.Post)
        {
            _openid = openid;
            _remark = remark;
        }

        public override string GetApiPath()
        {
            return "/cgi-bin/user/info/updateremark";
        }

        private string _openid;
        private string _remark;

        protected override void PrepareSend()
        {
            custom_body = $"{{\"openid\":\"{_openid}\",\"remark\":\"{_remark}\"}}";
        }
    }


}
