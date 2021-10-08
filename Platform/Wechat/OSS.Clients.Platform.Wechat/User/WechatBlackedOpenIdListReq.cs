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

namespace OSS.Clients.Platform.Wechat.User
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
