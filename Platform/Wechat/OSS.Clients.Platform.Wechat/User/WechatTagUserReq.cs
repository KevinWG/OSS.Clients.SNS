

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace OSS.Clients.Platform.WX.User
{

    public class WechatUnTagUserReq : WechatTagUserReq
    {

        public WechatUnTagUserReq(int tagId, IList<string> openIds) : base(tagId, openIds)
        {
        }

        public override string GetApiPath()
        {
            return "/cgi-bin/tags/members/batchuntagging";
        }


    }


    public class WechatTagUserReq:WechatBaseTokenReq<WechatBaseResp>
    {
        public WechatTagUserReq(int tagId,IList<string> openIds) : base(HttpMethod.Post)
        {
            _tagId   = tagId;
            _openIds = openIds;
        }
        
        private int           _tagId;
        private IList<string> _openIds;


        public override string GetApiPath()
        {
            return "/cgi-bin/tags/members/batchtagging";
        }

        protected override void PrepareSend()
        {
            custom_body = JsonConvert.SerializeObject(new { tagid = _tagId, openid_list = _openIds });
        }
    }
}
