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
