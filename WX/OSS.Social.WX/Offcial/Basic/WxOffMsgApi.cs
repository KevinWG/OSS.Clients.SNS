#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 主动发送消息接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-1
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using OSS.Http;
using OSS.Http.Models;
using OSS.Social.WX.Offcial.Basic.Mos;

namespace OSS.Social.WX.Offcial.Basic
{
    public partial class WxOffBasicApi
    {
        #region  模板功能

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">消息详情链接地址</param>
        /// <param name="data">消息数据</param>
        /// <returns></returns>
        public WxBaseResp SendTemplate(string openId, string templateId, string url, object data)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/template/send");
            var param = new
            {
                touser = openId,
                template_id = templateId,
                url = url,
                data = data
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return RestCommonOffcial<WxBaseResp>(req);
        }

        #endregion


        #region 发送图文消息接口

        /// <summary>
        ///   上传群发消息中的图文消息素材【订阅号与服务号认证后均可用】
        ///   群发接口中的上传素材接口
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public WxMediaResp UploadMassMsgArticles(List<WxArticleInfo> articles )
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadnews");
            req.CustomBody = JsonConvert.SerializeObject(new {articles = articles});

            return RestCommonOffcial<WxMediaResp>(req);
        }



        /// <summary>
        ///   上传群发消息中的视频【订阅号与服务号认证后均可用】 
        ///   群发接口中的上传视频
        /// </summary>
        /// <param name="mediaId">media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="title">消息的标题</param>
        /// <param name="desp">消息的描述</param>
        /// <returns></returns>
        public WxMediaResp UploadMassMsgVedio(string mediaId,string title,string desp)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadvideo");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\",\"title\":\"{title}\",\"description\":\"{desp}\"}}";

            return RestCommonOffcial<WxMediaResp>(req);
        }

        /// <summary>
        /// 根据Tag群发消息接口
        /// </summary>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写tag_id</param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据tag_id发送给指定群组的用户</param>
        /// <param name="msgType">群发的消息类型</param>
        /// <param name="data">素材消息的media_id,  text类型时是content, wxcard 时是card_id </param>
        /// <param name="sendIgnoreReprint">当 send_ignore_reprint=1时，文章被判定为转载时，且原创文允许转载时，将继续进行群发操作。当 send_ignore_reprint =0时，文章被判定为转载时，将停止群发操作。send_ignore_reprint 默认为0</param>
        /// <returns></returns>
        public WxSendMassMsgResp SendMassMsgByTag(int tagId, bool isToAll,WxSendMsgType msgType, string data, int sendIgnoreReprint = 0)
        {
            var msgStr=new StringBuilder("{");

            #region  拼接filter
            msgStr.Append("\"filter\":{");
            msgStr.Append("\"is_to_all\":").Append(isToAll).Append(",");
            msgStr.Append("\"tag_id\":").Append(tagId);
            msgStr.Append("},");
            #endregion
            
            GenerateMsgBody(msgType, data, msgStr);
            
            msgStr.Append("}");

            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/sendall");
            req.CustomBody = msgStr.ToString();

            return RestCommonOffcial<WxSendMassMsgResp>(req);
        }

        private static void GenerateMsgBody(WxSendMsgType msgType, string data, StringBuilder msgStr)
        {
            #region  拼接内容mediaid, content , cardid
            msgStr.Append("\"").Append(msgType).Append("\":{");
            switch (msgType)
            {
                case WxSendMsgType.text:
                    msgStr.Append("\"content\":\"").Append(data).Append("\"");
                    break;
                case WxSendMsgType.wxcard:
                    msgStr.Append("\"card_id\":\"").Append(data).Append("\"");
                    break;
                default:
                    msgStr.Append("\"media_id\":\"").Append(data).Append("\"");
                    break;
            }
            msgStr.Append("},");
            msgStr.Append("\"msgtype\":\"").Append(msgType);

            if (msgType == WxSendMsgType.mpnews)
                msgStr.Append("\",").Append("\"send_ignore_reprint\":").Append(msgType);
            #endregion
        }

        /// <summary>
        /// 根据Tag群发消息接口
        /// </summary>
        /// <param name="openIds"></param>
        /// <param name="msgType">群发的消息类型</param>
        /// <param name="data">素材消息的media_id,  text类型时是content, wxcard 时是card_id </param>
        /// <param name="sendIgnoreReprint">当 send_ignore_reprint=1时，文章被判定为转载时，且原创文允许转载时，将继续进行群发操作。当 send_ignore_reprint =0时，文章被判定为转载时，将停止群发操作。send_ignore_reprint 默认为0</param>
        /// <returns></returns>
        public WxSendMassMsgResp SendMassMsgByOpenIds(List<string> openIds , WxSendMsgType msgType, string data, int sendIgnoreReprint = 0)
        {
            var msgStr = new StringBuilder("{");

            #region  拼接touser
            msgStr.Append("\"touser\":[");
            for (int i = 0; i < openIds.Count; i++)
            {
                if (i>0)
                    msgStr.Append(",");
                msgStr.Append("\"").Append(openIds[i]).Append("\"");
            }
            msgStr.Append("],");
            #endregion

            // 拼接内容mediaid, content , cardid
            GenerateMsgBody(msgType, data, msgStr);
            msgStr.Append("}");

            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/send");
            req.CustomBody = msgStr.ToString();

            return RestCommonOffcial<WxSendMassMsgResp>(req);
        }
        #endregion

        /// <summary>
        ///  删除群发消息
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public WxBaseResp DeleteMassMsg(long msgId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/delete");
            req.CustomBody = $"{{\"msg_id\":{msgId}}}";

            return RestCommonOffcial<WxBaseResp>(req);
        }

        /// <summary>
        ///  查询群发消息状态
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public WxMassMsgStateResp GetMassMsgState(long msgId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/get");
            req.CustomBody = $"{{\"msg_id\":{msgId}}}";

            return RestCommonOffcial<WxMassMsgStateResp>(req);
        }

        /// <summary>
        /// 根据Tag群发消息接口
        /// </summary>
        /// <param name="openId">openid，wxName和openId同时赋值时，以wxname优先</param>
        /// <param name="wxName">微信名称，wxName和openId同时赋值时，以wxname优先</param>
        /// <param name="msgType">群发的消息类型</param>
        /// <param name="data">素材消息的media_id,  text类型时是content, wxcard 时是card_id </param>
        /// <param name="sendIgnoreReprint">当 send_ignore_reprint=1时，文章被判定为转载时，且原创文允许转载时，将继续进行群发操作。当 send_ignore_reprint =0时，文章被判定为转载时，将停止群发操作。send_ignore_reprint 默认为0</param>
        /// <returns></returns>
        public WxSendMassMsgResp PreviewMassMsg(string wxName, string openId, WxSendMsgType msgType, string data, int sendIgnoreReprint = 0)
        {
            var msgStr = new StringBuilder("{");

            #region  拼接 发送用户信息
            if (!string.IsNullOrEmpty(wxName))
                msgStr.Append("\"towxname\":\"").Append(wxName).Append("\",");
            if (!string.IsNullOrEmpty(openId))
                msgStr.Append("\"touser\":\"").Append(openId).Append("\",");
            #endregion

            // 拼接内容mediaid, content , cardid
            GenerateMsgBody(msgType, data, msgStr);
            msgStr.Append("}");

            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/preview");
            req.CustomBody = msgStr.ToString();

            return RestCommonOffcial<WxSendMassMsgResp>(req);
        }



    }
}
