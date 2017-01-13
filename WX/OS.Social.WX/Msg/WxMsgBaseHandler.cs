#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄基类，主要声明相关事件
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-13
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Common.Encrypt;
using OS.Common.Extention;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.WX.Msg
{
    /// <summary>
    /// 消息处理基类
    ///  </summary>
    public abstract class WxMsgBaseHandler
    {
        protected readonly WxMsgServerConfig m_Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mConfig"></param>
        protected WxMsgBaseHandler(WxMsgServerConfig mConfig)
        {
            m_Config = mConfig;
        }
        
        #region   事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected event Func<TextMsg, BaseReplyContext> TextHandler;

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected event Func<ImageMsg, BaseReplyContext> ImageHandler;

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected event Func<VoiceMsg, BaseReplyContext> VoiceHandler;

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected event Func<VideoMsg, BaseReplyContext> VideoHandler;

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected event Func<LocationMsg, BaseReplyContext> LocationHandler;

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected event Func<LinkMsg, BaseReplyContext> LinkHandler;

        #endregion

        #region 事件列表  动作事件消息

        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected event Func<SubscribeEvent, BaseReplyContext> SubscribeEventHandler;

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected event Func<SubscribeEvent, BaseReplyContext> ScanEventHandler;

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected event Func<LocationEvent, NoReplyMsg> LocationEventHandler;

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected event Func<ClickEvent, BaseReplyContext> ClickEventHandler;

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected event Func<ViewEvent, BaseReplyContext> ViewEventHandler;

        /// <summary>
        /// 客服事件推送 
        /// </summary>
        protected event Func<KFEvent, BaseReplyContext> KefuEventHandler;

        #endregion

        /// <summary>
        /// 执行事件对应委托方法，如果对应的方法存在则执行
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected static BaseReplyContext ExecuteHandler<TRes>(TRes res, Func<TRes, BaseReplyContext> func) where TRes : BaseRecContext
        {
            var baseRep = func?.Invoke(res) ?? new NoReplyMsg();
            baseRep.ToUserName = res.FromUserName;
            baseRep.FromUserName = res.ToUserName;
            baseRep.CreateTime = DateTime.Now.ToLocalSeconds();
            return baseRep;
        }
        
        #endregion
        
        #region   核心处理方法
        
        /// <summary>
        /// 核心执行方法   ==  执行方法
        /// </summary>
        /// <param name="context"></param>
        protected void ProcessCore(MsgContext context)
        {
            var dirs = WxMsgHelper.ChangXmlToDir(context.ContextXml);
            var msgType = WxMsgHelper.GetMsgType(dirs);
            switch (msgType)
            {
                case MsgType.Event:
                    var eventType = WxMsgHelper.GetEventType(dirs);
                    ProcessingCoreEvent(eventType, dirs, context);
                    break;
                case MsgType.Text:
                    var textRecMsg = WxMsgHelper.GetMsg<TextMsg>(dirs, MsgType.Text);
                    context.ReplyContext = ExecuteHandler(textRecMsg, TextHandler);
                    context.RecContext = textRecMsg;
                    break;
                case MsgType.Video:
                case MsgType.Shortvideo:
                    var vedioRecMsg = WxMsgHelper.GetMsg<VideoMsg>(dirs, msgType);
                    context.ReplyContext = ExecuteHandler(vedioRecMsg, VideoHandler);
                    context.RecContext = vedioRecMsg;
                    break;
                case MsgType.Image:
                    var imageRecMsg = WxMsgHelper.GetMsg<ImageMsg>(dirs, MsgType.Image);
                    context.ReplyContext = ExecuteHandler(imageRecMsg, ImageHandler);
                    context.RecContext = imageRecMsg;
                    break;
                case MsgType.Link:
                    var linkRecMsg = WxMsgHelper.GetMsg<LinkMsg>(dirs, MsgType.Link);
                    context.ReplyContext = ExecuteHandler(linkRecMsg, LinkHandler);
                    context.RecContext = linkRecMsg;
                    break;
                case MsgType.Voice:
                    var voiceRecMsg = WxMsgHelper.GetMsg<VoiceMsg>(dirs, MsgType.Voice);
                    context.ReplyContext = ExecuteHandler(voiceRecMsg, VoiceHandler);
                    context.RecContext = voiceRecMsg;
                    break;
                case MsgType.Location:
                    var locationRecMsg = WxMsgHelper.GetMsg<LocationMsg>(dirs, MsgType.Location);
                    context.ReplyContext = ExecuteHandler(locationRecMsg, LocationHandler);
                    context.RecContext = locationRecMsg;
                    break;
            
            }
        }

        /// <summary>
        ///  核心执行方法  ===   其中的事件部分
        /// </summary>
        /// <param name="msgEventType"></param>
        /// <param name="dirValues"></param>
        /// <param name="context"></param>
        private void ProcessingCoreEvent(EventType msgEventType, Dictionary<string, string> dirValues,
            MsgContext context)
        {
            switch (msgEventType)
            {
                case EventType.Subscribe:
                case EventType.UnSubscribe:
                    var subRecMsg = WxMsgHelper.GetEventMsg<SubscribeEvent>(dirValues, msgEventType);
                    context.ReplyContext = ExecuteHandler(subRecMsg, SubscribeEventHandler);
                    context.RecContext = subRecMsg;
                    break;
                case EventType.Click:
                    var clickRecMsg = WxMsgHelper.GetEventMsg<ClickEvent>(dirValues, EventType.Click);
                    context.ReplyContext = ExecuteHandler(clickRecMsg, ClickEventHandler);
                    context.RecContext = clickRecMsg;
                    break;
                case EventType.Location:
                    var locationRecMsg = WxMsgHelper.GetEventMsg<LocationEvent>(dirValues, EventType.Location);
                    context.ReplyContext = ExecuteHandler(locationRecMsg, LocationEventHandler);
                    context.RecContext = locationRecMsg;
                    break;
                case EventType.Scan:
                    var scanRecMsg = WxMsgHelper.GetEventMsg<SubscribeEvent>(dirValues, EventType.Scan);
                    context.ReplyContext = ExecuteHandler(scanRecMsg, ScanEventHandler);
                    context.RecContext = scanRecMsg;
                    break;
                case EventType.View:
                    var viewRrecMsg = WxMsgHelper.GetEventMsg<ViewEvent>(dirValues, EventType.View);
                    context.ReplyContext = ExecuteHandler(viewRrecMsg, ViewEventHandler);
                    context.RecContext = viewRrecMsg;
                    break;
                case EventType.Kefu:
                    var kfRrecMsg = WxMsgHelper.GetEventMsg<KFEvent>(dirValues, EventType.Kefu);
                    context.ReplyContext = ExecuteHandler(kfRrecMsg, KefuEventHandler);
                    context.RecContext = kfRrecMsg;
                    break;
            }
        }

        #endregion
        
        /// <summary>
        ///  执行结束方法
        /// </summary>
        /// <param name="msgContext"></param>
        protected virtual void ProcessEnd(MsgContext msgContext)
        {

        }


        #region 处理消息加解密部分

        /// <summary>
        /// 核心执行方法    ==    验证签名和消息体信息解密处理部分
        /// </summary>
        /// <param name="contentXml">消息内容</param>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>消息体对应的字典</returns>
        private ResultMo<MsgContext> CheckAndDecryptMsg(string contentXml, string signature,
            string timestamp, string nonce)
        {
            var msgContext = new MsgContext();

            var resCheck = WxMsgCrypt.CheckSignature(m_Config.Token, signature, timestamp, nonce);
            if (resCheck.IsSuccess)
            {
                if (m_Config.SecurityType != WxSecurityType.None)
                {
                    var dirs = WxMsgHelper.ChangXmlToDir(contentXml);
                    if (dirs == null || !dirs.ContainsKey("Encrypt"))
                        return new ResultMo<MsgContext>(ResultTypes.ObjectNull, "加密消息为空");

                    msgContext.ContextXml = Cryptography.WxAesDecrypt(dirs["Encrypt"], m_Config.EncodingAesKey);
                    return new ResultMo<MsgContext>(msgContext);
                }
                msgContext.ContextXml = contentXml;
                return new ResultMo<MsgContext>(msgContext);
            }
            return resCheck.ConvertToResultOnly<MsgContext>();
        }

        /// <summary>
        ///  核心执行方法    == 加密模式下，返回的消息体加密
        /// </summary>
        /// <param name="sReplyMsg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        protected ResultMo<string> EncryptMsg(string sReplyMsg, WxMsgServerConfig config)
        {
            string raw = "";
            try
            {
                raw = Cryptography.AesEncrypt(sReplyMsg, config.EncodingAesKey, config.AppId);
            }
            catch (Exception)
            {
                return new ResultMo<string>(ResultTypes.InnerError, "加密响应消息体出错！");
            }
            var date = DateTime.Now;

            var sTimeStamp = date.ToUtcSeconds().ToString();
            var sNonce = date.ToString("yyyyMMddHHssff");


            string msgSigature = WxMsgCrypt.GenerateSignature(config.Token, sTimeStamp, sNonce, raw);
            if (string.IsNullOrEmpty(msgSigature))
            {
                return new ResultMo<string>(ResultTypes.InnerError, "生成签名信息出错！");
            }
            StringBuilder sEncryptMsg = new StringBuilder();
            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";
            sEncryptMsg.Append("<xml>").Append(EncryptLabelHead).Append(raw).Append(EncryptLabelTail);
            sEncryptMsg.Append(MsgSigLabelHead).Append(msgSigature).Append(MsgSigLabelTail);
            sEncryptMsg.Append(TimeStampLabelHead).Append(sTimeStamp).Append(TimeStampLabelTail);
            sEncryptMsg.Append(NonceLabelHead).Append(sNonce).Append(NonceLabelTail);
            sEncryptMsg.Append("</xml>");
            return new ResultMo<string>(sEncryptMsg.ToString());
        }

        #endregion


    }


    internal static class WxMsgCrypt
    {
        #region   私有辅助方法

        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        internal static ResultMo CheckSignature(string token, string signature,
            string timestamp, string nonce)
        {
            if (signature == GenerateSignature(token, timestamp, nonce))
            {
                return new ResultMo();
            }
            return new ResultMo(ResultTypes.UnAuthorize, "签名验证失败！");
        }


        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="strEncryptMsg"></param>
        /// <returns></returns>
        internal static string GenerateSignature(string token,
            string timestamp, string nonce, string strEncryptMsg = "")
        {
            List<string> strList = new List<string>() { token, timestamp, nonce, strEncryptMsg };
            strList.Sort();

            string waitEncropyStr = string.Join(string.Empty, strList);
            return Sha1.Encrypt(waitEncropyStr, Encoding.ASCII);
        }


        #endregion
    }
}
