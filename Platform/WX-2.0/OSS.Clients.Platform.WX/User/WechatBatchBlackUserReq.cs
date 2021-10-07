
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace OSS.Clients.Platform.WX.User
{

    public class WechatBatchUnBlackUserReq : WechatBatchBlackUserReq
    {

        public override string GetApiPath()
        {
            return "/cgi-bin/tags/members/batchunblacklist";
        }


        public WechatBatchUnBlackUserReq(IList<string> openIds) : base(openIds)
        {
        }
    }

    public class WechatBatchBlackUserReq : WechatBaseTokenReq<WechatBaseResp>
    {
        public WechatBatchBlackUserReq(IList<string> openIds) : base(HttpMethod.Post)
        {
            _openIds = openIds;
        }

        private IList<string> _openIds;

        public override string GetApiPath()
        {
            return "/cgi-bin/tags/members/batchblacklist";
        }

        protected override void PrepareSend()
        {
            custom_body = JsonConvert.SerializeObject(new {opened_list = _openIds });
        }

    
    }
}
