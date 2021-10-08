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
