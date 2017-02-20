#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 客服管理接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-2
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Http.Mos;
using OSS.Social.WX.Offcial.Basic.Mos;

namespace OSS.Social.WX.Offcial.Basic
{
    public class WxOffKfApi:WxOffBaseApi
    {
        static WxOffKfApi()
        {
            #region  客服全局错误码信息

            RegisteErrorCode(65400, "API不可用，即没有开通或升级到新版客服功能");
            RegisteErrorCode(65401, "无效客服帐号");
            RegisteErrorCode(65402, "客服帐号尚未绑定微信号，不能投入使用");
            RegisteErrorCode(65413, "不存在对应用户的会话信息");
            RegisteErrorCode(65414, "粉丝正在被其他客服接待");
            RegisteErrorCode(65415, "指定的客服不在线");
            RegisteErrorCode(40003, "非法的openid");
            RegisteErrorCode(65416, "查询参数不合法");
            RegisteErrorCode(65417, "查询时间段超出限制");

            #endregion
        }

        #region  客服账号管理部分
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public WxOffKfApi(WxAppCoinfig config) : base(config)
        {
        }

        /// <summary>
        ///   添加客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <returns></returns>
        public async Task<WxBaseResp> AddKfAccountAsync(string account, string nickname)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/add");
            req.CustomBody = $"{{\"kf_account\":\"{account}\",\"nickname\":\"{nickname}\"}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        /// 邀请绑定客服帐号
        /// </summary>
        /// <param name="account">完整客服帐号，格式为：帐号前缀@公众号微信号</param>
        /// <param name="inviteWx">接收绑定邀请的客服微信号</param>
        /// <returns></returns>
        public async Task<WxBaseResp> InviteKfWorker(string account, string inviteWx)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/inviteworker");
            req.CustomBody = $"{{\"kf_account\":\"{account}\",\"invite_wx\":\"{inviteWx}\"}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }


        /// <summary>
        ///   修改客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <returns></returns>
        public async Task<WxBaseResp> UpdateKFAccountAsync(string account, string nickname)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/update");
            req.CustomBody = $"{{\"kf_account\":\"{account}\",\"nickname\":\"{nickname}\"}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        ///   删除客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
            /// <returns></returns>
        public async Task<WxBaseResp> DeleteKFAccountAsync(string account)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/del?kf_account=", account);

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }


        /// <summary>
        ///   设置客服账号头像
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="fileReq">头像的文件信息</param>
        /// <returns></returns>
        public async Task<WxBaseResp> UploadKFHeadImgAsync(string account, WxFileReq fileReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/uploadheadimg?kf_account=", account);
            req.FileParameters.Add(new FileParameter("media", fileReq.file_stream, fileReq.file_name,
                fileReq.content_type));

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        ///  获取客服基本信息列表
        /// </summary>
        /// <returns></returns>
        public async Task<WxGetKFAccountListResp> GetKFAccountListAsync()
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/customservice/getkflist");

            return await RestCommonOffcialAsync<WxGetKFAccountListResp>(req);
        }

        /// <summary>
        ///   获取在线客服列表
        /// </summary>
        /// <returns></returns>
        public async Task<WxGetKfOnlineResp> GetKfOnlineList()
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/customservice/getonlinekflist");

            return await RestCommonOffcialAsync<WxGetKfOnlineResp>(req);
        }

        #endregion

        #region  会话管理部分
        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="account">完整客服帐号，格式为：帐号前缀@公众号微信号</param>
        /// <param name="openId">粉丝的openid</param>
        /// <returns></returns>
        public async Task<WxBaseResp> CreateKfSession(string account,string openId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfsession/create");
            req.CustomBody = $"{{\"kf_account\":\"{account}\",\"openid\":\"{openId}\"}}";

            return await RestCommonOffcialAsync<WxGetKfOnlineResp>(req);
        }

        /// <summary>
        /// 关闭会话
        /// </summary>
        /// <param name="account">完整客服帐号，格式为：帐号前缀@公众号微信号</param>
        /// <param name="openId">粉丝的openid</param>
        /// <returns></returns>
        public async Task<WxBaseResp> CloseKfSession(string account, string openId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfsession/close");
            req.CustomBody = $"{{\"kf_account\":\"{account}\",\"openid\":\"{openId}\"}}";

            return await RestCommonOffcialAsync<WxGetKfOnlineResp>(req);
        }

        /// <summary>
        ///  获取用户客服会话信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<WxGetUserKfSessionResp> GetUserKfSession(string openId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfsession/getsession?openid=", openId);

            return await RestCommonOffcialAsync<WxGetUserKfSessionResp>(req);
        }


        /// <summary>
        ///  获取客服的用户会话列表
        /// </summary>
        /// <param name="account">客服账号</param>
        /// <returns></returns>
        public async Task<WxGetKfSessionsByAccountResp> GetKfSessionsByAccount(string account)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfsession/getsessionlist?kf_account=", account);

            return await RestCommonOffcialAsync<WxGetKfSessionsByAccountResp>(req);
        }


        /// <summary>
        ///  获取未接入会话列表
        /// </summary>
        /// <returns></returns>
        public async Task<WxGetKfWaitUserListResp> GetKfWaitUserList()
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfsession/getwaitcase?");

            return await RestCommonOffcialAsync<WxGetKfWaitUserListResp>(req);
        }
        #endregion


        #region  消息部分

        /// <summary>
        ///  发送一般的客服消息
        ///   包含：text-文本，image-图片,voice-语音，mpnews-图文消息，wxcard-卡券
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="msgType">消息类型</param>
        /// <param name="mediaId">素材id，当是wxcard类型时，传入cardId</param>
        /// <param name="kfAccount">如果不为空，则以当前客服身份发送消息</param>
        /// <returns></returns>
        public async Task<WxBaseResp> SenKfMsgAsync(string openId, string msgType, string mediaId,string kfAccount="")
        {
            StringBuilder msgStr = new StringBuilder("{");

            #region 拼接内容
            // 用户信息
            msgStr.Append("\"touser\":\"").Append(openId).Append("\",");
            //  消息内容
            msgStr.Append("\"msgtype\":\"").Append(msgType).Append("\",");
            msgStr.Append("\"").Append(msgType).Append("\":");
            msgStr.Append("{\"media_id\":\"").Append(mediaId).Append("\"}");
            #endregion

            if (!string.IsNullOrEmpty(kfAccount))
                msgStr.Append(",\"customservice\":{\"kf_account\":\"").Append(kfAccount).Append("\"}");
            msgStr.Append("}");

            return await SendKfMsgAsync(msgStr.ToString());
        }

        /// <summary>
        ///  发送视频消息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="mediaId">必填，素材id</param>
        /// <param name="thumbMediaId">必填， 缩略图id</param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="kfAccount">如果不为空，则以当前客服身份发送消息</param>
        /// <returns></returns>
        public async Task<WxBaseResp> SenKfVideoMsgSync(string openId, string mediaId, string thumbMediaId, string title="", string description="", string kfAccount = "")
        {
            StringBuilder msgStr = new StringBuilder("{");

            #region 拼接内容

            // 用户信息
            msgStr.Append("\"touser\":\"").Append(openId).Append("\",");
            //  消息内容
            msgStr.Append("\"msgtype\":\"video\",");
            msgStr.Append("\"video\":{");
            msgStr.Append("\"media_id\":\"").Append(mediaId).Append("\",");
            msgStr.Append("\"thumb_media_id\":\"").Append(thumbMediaId).Append("\",");
            msgStr.Append("\"title\":\"").Append(title).Append("\",");
            msgStr.Append("\"description\":\"").Append(description).Append("\"");
            msgStr.Append("}");

            #endregion

            if (!string.IsNullOrEmpty(kfAccount))
                msgStr.Append(",\"customservice\":{\"kf_account\":\"").Append(kfAccount).Append("\"}");
      
            msgStr.Append("}");

            return await SendKfMsgAsync(msgStr.ToString());
        }


        /// <summary>
        ///  发送音乐消息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="thumbMediaId"></param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="kfAccount">如果不为空，则以当前客服身份发送消息</param>
        /// <returns></returns>
        public async Task<WxBaseResp> SenKfMusicMsgAsync(string openId, string musicurl, string hqmusicurl, string thumbMediaId,
            string title = "", string description = "", string kfAccount = "")
        {
            StringBuilder msgStr = new StringBuilder("{");

            #region 拼接内容

            // 用户信息
            msgStr.Append("\"touser\":\"").Append(openId).Append("\",");
            //  消息内容
            msgStr.Append("\"msgtype\":\"music\",");
            msgStr.Append("\"music\":{");
            msgStr.Append("\"title\":\"").Append(title).Append("\",");
            msgStr.Append("\"description\":\"").Append(description).Append("\",");
            msgStr.Append("\"thumb_media_id\":\"").Append(thumbMediaId).Append("\",");
            msgStr.Append("\"musicurl\":\"").Append(musicurl).Append("\",");
            msgStr.Append("\"hqmusicurl\":\"").Append(hqmusicurl).Append("\"");
            msgStr.Append("}");

            #endregion

            if (!string.IsNullOrEmpty(kfAccount))
                msgStr.Append(",\"customservice\":{\"kf_account\":\"").Append(kfAccount).Append("\"}");

            msgStr.Append("}");

            return await SendKfMsgAsync(msgStr.ToString());
        }


        /// <summary>
        ///  发送非素材多图文消息（不能超过8条）
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="articles">图文信息，不能超过8条</param>
        /// <param name="kfAccount">如果不为空，则以当前客服身份发送消息</param>
        /// <returns></returns>
        public async Task<WxBaseResp> SenKfArticlesMsgAsync(string openId, List<WxArticleInfo> articles, string kfAccount = "")
        {
            string msgContent = JsonConvert.SerializeObject(new
            {
                touser = openId,
                msgtype = "news",
                news = new {articles = articles},
                customservice = string.IsNullOrEmpty(kfAccount) ? new {kf_account = kfAccount} : null
            }, Formatting.Indented, new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});

            return await SendKfMsgAsync(msgContent);
        }


        /// <summary>
        ///  发送客服最终走的方法
        /// </summary>
        /// <param name="msgContent"></param>
        /// <returns></returns>
        private async Task<WxBaseResp> SendKfMsgAsync(string msgContent)
        { 
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/custom/send");
            req.CustomBody = msgContent;

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }


        /// <summary>
        ///   获取客服的聊天记录列表
        /// </summary>
        /// <param name="msgReq"></param>
        /// <returns></returns>
        public async Task<WxGetKfMsgListResp> GetKfMsgList(WxGetKfMsgListReq msgReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/msgrecord/getmsglist");
            req.CustomBody = JsonConvert.SerializeObject(msgReq);

            return await RestCommonOffcialAsync<WxGetKfMsgListResp>(req);
        }

        #endregion
    }
}
