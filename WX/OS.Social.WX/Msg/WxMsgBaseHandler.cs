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
        /// 处理未知类型消息
        /// </summary>
        protected event Func<BaseRecMsg, BaseReplyMsg> NoneHandler;

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected event Func<TextRecMsg, BaseReplyMsg> TextHandler;

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected event Func<ImageRecMsg, BaseReplyMsg> ImageHandler;

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected event Func<VoiceRecMsg, BaseReplyMsg> VoiceHandler;

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected event Func<VideoRecMsg, BaseReplyMsg> VideoHandler;

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected event Func<LocationRecMsg, BaseReplyMsg> LocationHandler;

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected event Func<LinkRecMsg, BaseReplyMsg> LinkHandler;

        #endregion

        #region 事件列表  动作事件消息
        /// <summary>
        ///  处理未知事件消息
        /// </summary>
        protected event Func<BaseRecEventMsg, BaseReplyMsg> NoneEventHandler;

        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected event Func<SubscribeRecEventMsg, BaseReplyMsg> SubscribeEventHandler;

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected event Func<SubscribeRecEventMsg, BaseReplyMsg> ScanEventHandler;

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected event Func<LocationRecEventMsg, NoneReplyMsg> LocationEventHandler;

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected event Func<ClickRecEventMsg, BaseReplyMsg> ClickEventHandler;

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected event Func<ViewRecEventMsg, BaseReplyMsg> ViewEventHandler;

        /// <summary>
        /// 客服事件推送 
        /// </summary>
        protected event Func<KFRecEventMsg, BaseReplyMsg> KefuEventHandler;

        #endregion

        /// <summary>
        /// 执行事件对应委托方法，如果对应的方法存在则执行
        /// </summary>
        /// <typeparam name="TRecMsg"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected static BaseReplyMsg ExecuteHandler<TRecMsg>(TRecMsg res, Func<TRecMsg, BaseReplyMsg> func) where TRecMsg : BaseRecMsg,new ()
        {
            var baseRep = func?.Invoke(res) ?? new NoneReplyMsg();
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
        /// <param name="recMsgXml">传入消息的xml</param>
        protected ResultMo<MsgContext> ProcessCore(string recMsgXml)
        {
            var dirs = WxMsgHelper.ChangXmlToDir(recMsgXml);

            string msgType;
            if (dirs.TryGetValue("MsgType", out msgType))
            {
                MsgContext context = null;
                switch (msgType.ToLower())
                {
                    case "event":  //   如果是事件直接执行事件处理程序
                        return ProcessingEventCore(recMsgXml, dirs);
                    case "text":
                        context= ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Text, TextHandler);
                        break;
                    case "image":
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Image, ImageHandler);
                        break;
                    case "voice":
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Voice, VoiceHandler);
                        break;
                    case "video":
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Video, VideoHandler);
                        break;
                    case "shortvideo":
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Shortvideo, VideoHandler);
                        break;
                    case "location":
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Location, LocationHandler);
                        break;
                    case "link":
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.Link, LinkHandler);
                        break;
                    default:
                        context = ProcessMsgCoreExe(recMsgXml, dirs, MsgType.None, NoneHandler);
                        break;
                }

                return new ResultMo<MsgContext>(context);
            }

            return new ResultMo<MsgContext>(ResultTypes.ObjectNull,"不正确的消息数据格式！");
      
        }

        /// <summary>
        ///  根据具体的消息类型执行相关的消息委托方法
        /// </summary>
        /// <typeparam name="TRecMsg"></typeparam>
        /// <param name="recMsgXml"></param>
        /// <param name="recMsgDirs"></param>
        /// <param name="msgType"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private MsgContext ProcessMsgCoreExe<TRecMsg>(string recMsgXml, IDictionary<string, string> recMsgDirs, MsgType msgType, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg,new ()
        {
            var msgContext = new MsgContext();

            var recMsg= WxMsgHelper.GetMsg<TRecMsg>(recMsgDirs, msgType);
            recMsg.RecMsgXml = recMsgXml;

            msgContext.ReplyMsg = ExecuteHandler(recMsg, func);
            msgContext.RecMsg = recMsg;

            return msgContext;
        }


        /// <summary>
        ///  核心执行方法  ===   其中的事件部分
        /// </summary>
        /// <param name="recEventMsg"></param>
        /// <param name="recEventDirs"></param>
        private ResultMo<MsgContext> ProcessingEventCore(string recEventMsg, Dictionary<string, string> recEventDirs)
        {
            string msgEventType;
            if (recEventDirs.TryGetValue("Event", out msgEventType))
            {
                MsgContext context = null;
                switch (msgEventType.ToLower())
                {
                    case "subscribe":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.Subscribe,
                            SubscribeEventHandler);
                        break;
                    case "unsubscribe":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.UnSubscribe,
                            SubscribeEventHandler);
                        break;
                    case "scan":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.Scan,
                            ScanEventHandler);
                        break;
                    case "location":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.Location,
                            LocationEventHandler);
                        break;
                    case "click":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.Click,
                            ClickEventHandler);
                        break;
                    case "view":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.View,
                            ViewEventHandler);
                        break;
                    case "kf_create_session":
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.Kefu,
                            KefuEventHandler);
                        break;
                    default:
                        context = ProcessEventCoreExe(recEventMsg, recEventDirs, EventType.None,
                           NoneEventHandler);
                        break;
                }
                return new ResultMo<MsgContext>(context);
            }
            return new ResultMo<MsgContext>(ResultTypes.ObjectNull, "不正确的事件消息数据格式！");

        }

        /// <summary>
        ///  根据具体的事件消息类型执行相关的事件消息委托方法
        /// </summary>
        /// <typeparam name="TRecEventMsg"></typeparam>
        /// <param name="recEventMsgXml"></param>
        /// <param name="recEventMsgDirs"></param>
        /// <param name="eventType"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private MsgContext ProcessEventCoreExe<TRecEventMsg>(string recEventMsgXml, IDictionary<string, string> recEventMsgDirs, EventType eventType, Func<TRecEventMsg, BaseReplyMsg> func)
            where TRecEventMsg : BaseRecEventMsg, new()
        {
            var msgContext = new MsgContext();

            var recEventMsg = WxMsgHelper.GetEventMsg<TRecEventMsg>(recEventMsgDirs, eventType);
            recEventMsg.RecMsgXml = recEventMsgXml;

            msgContext.ReplyMsg = ExecuteHandler(recEventMsg, func);
            msgContext.RecMsg = recEventMsg;

            return msgContext;
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
        /// <param name="recXml">消息内容</param>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>验证结果及相应的消息内容体 （如果加密模式，返回的是解密后的明文）</returns>
        protected ResultMo<string> CheckAndDecryptMsg(string recXml, string signature,
            string timestamp, string nonce)
        {
            var resCheck = WxMsgCrypt.CheckSignature(m_Config.Token, signature, timestamp, nonce);
            if (resCheck.IsSuccess)
            {
                if (m_Config.SecurityType != WxSecurityType.None)
                {
                    var dirs = WxMsgHelper.ChangXmlToDir(recXml);
                    if (dirs == null || !dirs.ContainsKey("Encrypt"))
                        return new ResultMo<string>(ResultTypes.ObjectNull, "加密消息为空");

                    var recMsgXml= Cryptography.WxAesDecrypt(dirs["Encrypt"], m_Config.EncodingAesKey);
                    return new ResultMo<string>(recMsgXml);
                }
                return new ResultMo<string>(recXml);
            }
            return resCheck.ConvertToResultOnly<string>();
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
