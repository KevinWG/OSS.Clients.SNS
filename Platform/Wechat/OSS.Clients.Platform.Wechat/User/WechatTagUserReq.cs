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

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace OSS.Clients.Platform.Wechat.User
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
