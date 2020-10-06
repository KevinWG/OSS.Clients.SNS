#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Basic
{
    /// <summary>
    /// 消息相关接口
    /// </summary>
    public class WXPlatMassApi:WXPlatBaseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WXBaseApi.DefaultConfig 属性赋值</param>
        public WXPlatMassApi(AppConfig config=null):base(config)
        {
        }

        #region  模板功能

        /// <summary>
        /// 设置所属行业
        /// 行业编码查询：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277
        /// </summary>
        /// <param name="industryId1">必选-公众号模板消息所属行业编号1</param>
        /// <param name="industryId2">必选-公众号模板消息所属行业编号2</param>
        /// <returns></returns>
        public async Task<WXBaseResp> SetTemplateIndustry(int industryId1, int industryId2)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/template/api_set_industry"),
                CustomBody = $"{{\"industry_id1\":\"{industryId1}\",\"industry_id2\":\"{industryId2}\"}}"
            };
            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        //WXGetTemplateIndustryResp

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetTemplateIndustryResp> GetTemplateIndustry()
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/template/get_industry")
            };
            return await RestCommonPlatAsync<WXGetTemplateIndustryResp>(req);
        }

        
        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="shortTemplateId">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <returns></returns>
        public async Task<WXAddTemplateResp> AddTemplate(string shortTemplateId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/template/api_add_template"),
                CustomBody = $"{{\"template_id_short\":\"{shortTemplateId}\"}}"
            };
            return await RestCommonPlatAsync<WXAddTemplateResp>(req);
        }


        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="templateId">公众帐号下模板消息ID</param>
        /// <returns></returns>
        public async Task<WXAddTemplateResp> DeleteTemplate(string templateId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/template/del_private_template"),
                CustomBody = $"{{\"template_id\":\"{templateId}\"}}"
            };
            return await RestCommonPlatAsync<WXAddTemplateResp>(req);
        }


        /// <summary>
        /// 获取公众号下模板列表
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetTemplateListResp> GetTemplateList()
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/template/get_all_private_template")
            };
            return await RestCommonPlatAsync<WXGetTemplateListResp>(req);
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">消息详情链接地址</param>
        /// <param name="data">消息数据， 格式可以为： new{first = new {value = "用户", color = "#173177"},{...}}</param>
        /// <returns></returns>
        public async Task<WXBaseResp> SendTemplateAsync(string openId, string templateId, string url, object data)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/template/send")
            };

            var param = new
            {
                touser = openId,
                template_id = templateId,
                url = url,
                data = data
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        #endregion

        #region 发送图文消息接口

        /// <summary>
        ///   上传群发消息中的图文消息素材【订阅号与服务号认证后均可用】
        ///   群发接口中的上传素材接口
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public async Task<WXMediaResp> UploadMassMsgArticlesAsync(List<WXArticleInfo> articles )
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadnews");
            req.CustomBody = JsonConvert.SerializeObject(new {articles = articles});

            return await RestCommonPlatAsync<WXMediaResp>(req);
        }



        /// <summary>
        ///   上传群发消息中的视频【订阅号与服务号认证后均可用】 
        ///   群发接口中的上传视频
        /// </summary>
        /// <param name="mediaId">media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="title">消息的标题</param>
        /// <param name="desp">消息的描述</param>
        /// <returns></returns>
        public async Task<WXMediaResp> UploadMassMsgVedioAsync(string mediaId,string title,string desp)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadvideo");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\",\"title\":\"{title}\",\"description\":\"{desp}\"}}";

            return await RestCommonPlatAsync<WXMediaResp>(req);
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
        public async Task<WXSendMassMsgResp> SendMassMsgByTagAsync(int tagId, bool isToAll,WXMassMsgType msgType, string data, int sendIgnoreReprint = 0)
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

            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/sendall");
            req.CustomBody = msgStr.ToString();

            return await RestCommonPlatAsync<WXSendMassMsgResp>(req);
        }

        private static void GenerateMsgBody(WXMassMsgType msgType, string data, StringBuilder msgStr)
        {
            #region  拼接内容mediaid, content , cardid
            msgStr.Append("\"").Append(msgType).Append("\":{");
            switch (msgType)
            {
                case WXMassMsgType.text:
                    msgStr.Append("\"content\":\"").Append(data).Append("\"");
                    break;
                case WXMassMsgType.wxcard:
                    msgStr.Append("\"card_id\":\"").Append(data).Append("\"");
                    break;
                default:
                    msgStr.Append("\"media_id\":\"").Append(data).Append("\"");
                    break;
            }
            msgStr.Append("},");
            msgStr.Append("\"msgtype\":\"").Append(msgType);

            if (msgType == WXMassMsgType.mpnews)
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
        public async Task<WXSendMassMsgResp> SendMassMsgByOpenIds(List<string> openIds , WXMassMsgType msgType, string data, int sendIgnoreReprint = 0)
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

            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/send");
            req.CustomBody = msgStr.ToString();

            return await RestCommonPlatAsync<WXSendMassMsgResp>(req);
        }
        #endregion

        /// <summary>
        ///  删除群发消息
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> DeleteMassMsgAsync(long msgId)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/delete");
            req.CustomBody = $"{{\"msg_id\":{msgId}}}";

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        /// <summary>
        ///  查询群发消息状态
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public async Task<WXMassMsgStateResp> GetMassMsgStateAsync(long msgId)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/get");
            req.CustomBody = $"{{\"msg_id\":{msgId}}}";

            return await RestCommonPlatAsync<WXMassMsgStateResp>(req);
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
        public async Task<WXSendMassMsgResp> PreviewMassMsgAsync(string wxName, string openId, WXMassMsgType msgType, string data, int sendIgnoreReprint = 0)
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

            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/message/mass/preview");
            req.CustomBody = msgStr.ToString();

            return await RestCommonPlatAsync<WXSendMassMsgResp>(req);
        }



    }
}
