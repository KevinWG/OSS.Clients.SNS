#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息模块基类  -   主要处理配置信息相关
*　　	
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-8-27
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Xml;
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.Common.Extention;
using OSS.Common.Plugs;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SocialSDK.WX.Msg.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <summary>
    ///   消息处理的基类
    /// </summary>
    public abstract class WxMsgBaseHandler:BaseConfigProvider<WxMsgServerConfig>
    {
        protected WxMsgBaseHandler(WxMsgServerConfig config=null):base(config)
        {
            ModuleName = ModuleNames.SocialCenter;
        }


        #region   基础消息的事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual BaseReplyMsg ProcessTextHandler(TextRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual BaseReplyMsg ProcessImageHandler(ImageRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual BaseReplyMsg ProcessVoiceHandler(VoiceRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected virtual  BaseReplyMsg ProcessVideoHandler(VideoRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual  BaseReplyMsg ProcessLocationHandler(LocationRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual BaseReplyMsg ProcessLinkHandler(LinkRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        ///   执行处理未知消息
        /// </summary>
        /// <returns></returns>
        protected virtual BaseReplyMsg ProcessUnknowHandler(BaseRecMsg msg)
        {
            return new NoneReplyMsg();
        }
        #endregion


        #region 事件列表  动作事件消息

        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected virtual  BaseReplyMsg ProcessSubscribeEventHandler(SubscribeRecEventMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual BaseReplyMsg ProcessScanEventHandler(SubscribeRecEventMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual NoneReplyMsg ProcessLocationEventHandler(LocationRecEventMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual  BaseReplyMsg ProcessClickEventHandler(ClickRecEventMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected virtual  BaseReplyMsg ProcessViewEventHandler(ViewRecEventMsg msg)
        {
            return new NoneReplyMsg();
        }

        #endregion

        #endregion

        /// <summary>
        ///  执行基础消息类型
        /// </summary>
        /// <param name="rMsg"></param>
        /// <param name="msgType"></param>
        /// <param name="rDirs"></param>
        /// <returns>返回基础消息处理结果</returns>
        protected MsgContext ProcessExecute_BasicMsg(XmlDocument rMsg, string msgType,
            Dictionary<string, string> rDirs)
        {
            MsgContext context = null;
            switch (msgType.ToLower())
            {
                case "event":
                    context = ProcessExecute_BasicEventMsg(rMsg, rDirs);
                    break;
                case "text":
                    context = ExecuteHandler(rMsg, rDirs, new TextRecMsg(), ProcessTextHandler);
                    break;
                case "image":
                    context = ExecuteHandler(rMsg, rDirs, new ImageRecMsg(), ProcessImageHandler);
                    break;
                case "voice":
                    context = ExecuteHandler(rMsg, rDirs, new VoiceRecMsg(), ProcessVoiceHandler);
                    break;
                case "video":
                    context = ExecuteHandler(rMsg, rDirs, new VideoRecMsg(), ProcessVideoHandler);
                    break;
                case "shortvideo":
                    context = ExecuteHandler(rMsg, rDirs, new VideoRecMsg(), ProcessVideoHandler);
                    break;
                case "location":
                    context = ExecuteHandler(rMsg, rDirs, new LocationRecMsg(), ProcessLocationHandler);
                    break;
                case "link":
                    context = ExecuteHandler(rMsg, rDirs, new LinkRecMsg(), ProcessLinkHandler);
                    break;
            }
            return context;
        }


        /// <summary>
        ///  执行基础事件消息类型
        /// </summary>
        /// <param name="reMsg"></param>
        /// <param name="reDirs"></param>
        /// <returns>返回基础事件消息处理结果</returns>
        private MsgContext ProcessExecute_BasicEventMsg(XmlDocument reMsg, Dictionary<string, string> reDirs)
        {
            var eventType = reDirs["Event"].ToLower();
            MsgContext context = null;
            switch (eventType)
            {
                case "subscribe":
                    context = ExecuteHandler(reMsg, reDirs, new SubscribeRecEventMsg(), ProcessSubscribeEventHandler);
                    break;
                case "unsubscribe":
                    context = ExecuteHandler(reMsg, reDirs, new SubscribeRecEventMsg(), ProcessSubscribeEventHandler);
                    break;
                case "scan":
                    context = ExecuteHandler(reMsg, reDirs, new SubscribeRecEventMsg(), ProcessScanEventHandler);
                    break;
                case "location":
                    context = ExecuteHandler(reMsg, reDirs, new LocationRecEventMsg(), ProcessLocationEventHandler);
                    break;
                case "click":
                    context = ExecuteHandler(reMsg, reDirs, new ClickRecEventMsg(), ProcessClickEventHandler);
                    break;
                case "view":
                    context = ExecuteHandler(reMsg, reDirs, new ViewRecEventMsg(), ProcessViewEventHandler);
                    break;
            }
            return context;
        }



        /// <summary>
        /// 执行高级消息事件类型
        /// </summary>
        /// <param name="recMsgXml">接收到的消息内容体</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="msgDirs">消息内容体字典</param>
        /// <returns></returns>
        private MsgContext ProcessExecute_CustomHandler(XmlDocument recMsgXml, string msgType,
            IDictionary<string, string> msgDirs)
        {
            var handler = GetMsgProcessor(msgType,
                msgDirs.TryGetValue("Event", out var eName) ? eName : string.Empty);

            if (handler == null)
            {
                var key = msgType == "event" ? string.Concat("event_", eName ?? string.Empty) : msgType;
                handler = WxMsgProcessorProvider.GetHandler(key);
            }

            if (handler == null)
                return null;  //  交由后续默认事件处理

            var recMsg = handler.CreateNewInstance();
            return ExecuteHandler(recMsgXml, msgDirs, recMsg, handler.Execute);
        }










        #region 消息处理入口，出口（分为开始，处理，结束部分）

        /// <summary>
        ///  服务器验证
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public ResultMo<string> CheckServerValid(string signature, string timestamp, string nonce, string echostr)
        {
            var checkSignRes = WxMsgHelper.CheckSignature(ApiConfig.Token, signature, timestamp, nonce);
            var resultRes = checkSignRes.ConvertToResultOnly<string>();
            resultRes.data = resultRes.IsSuccess() ? echostr : string.Empty;
            return resultRes;
        }

        /// <summary>
        /// 核心执行方法
        /// </summary>
        /// <param name="contentXml">内容信息</param>
        /// <param name="signature">签名信息</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符创</param>
        /// <param name="echostr">验证服务器参数，如果存在则只进行签名验证，并将在结果Data中返回</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public ResultMo<string> Process(string contentXml, string signature, string timestamp, string nonce,
            string echostr)
        {
            // 一.  检查是否是服务器验证
            if (!string.IsNullOrEmpty(echostr))
            {
                return CheckServerValid(signature, timestamp, nonce, echostr);
            }

            // 二.  正常消息处理
            {
                var checkRes = PrepareExecute(contentXml, signature, timestamp, nonce);
                if (!checkRes.IsSuccess())
                    return checkRes.ConvertToResultOnly<string>();

                var contextRes = Execute(checkRes.data);
                if (!contextRes.IsSuccess())
                    return contextRes.ConvertToResultOnly<string>();

                ExecuteEnd(contextRes.data);

                var resultString = contextRes.data.ReplyMsg.ToReplyXml();
                if (ApiConfig.SecurityType != WxSecurityType.None &&
                     !string.IsNullOrEmpty(contextRes.data.ReplyMsg.MsgType))
                {
                    return WxMsgHelper.EncryptMsg(resultString, ApiConfig);
                }
                return new ResultMo<string>(resultString);
            }
        }


        #endregion




        #region 消息处理 == start   验证消息参数以及加解密部分

        /// <summary>
        /// 核心执行方法    ==    验证签名和消息体信息解密处理部分
        /// </summary>
        /// <param name="recXml">消息内容</param>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>验证结果及相应的消息内容体 （如果加密模式，返回的是解密后的明文）</returns>
        private ResultMo<string> PrepareExecute(string recXml, string signature,
            string timestamp, string nonce)
        {
            if (string.IsNullOrEmpty(recXml))
                return new ResultMo<string>(ResultTypes.ObjectNull, "接收的消息体为空！");

            var resCheck = WxMsgHelper.CheckSignature(ApiConfig.Token, signature, timestamp, nonce);
            if (!resCheck.IsSuccess())
                return resCheck.ConvertToResultOnly<string>();

            if (ApiConfig.SecurityType == WxSecurityType.None)
                return new ResultMo<string>(recXml);

            XmlDocument xmlDoc = null;
            var dirs = WxMsgHelper.ChangXmlToDir(recXml, ref xmlDoc);

            if (dirs == null || !dirs.ContainsKey("Encrypt"))
                return new ResultMo<string>(ResultTypes.ObjectNull, "加密消息为空");

            var recMsgXml = Cryptography.WxAesDecrypt(dirs["Encrypt"], ApiConfig.EncodingAesKey);

            return new ResultMo<string>(recMsgXml);
        }

        #endregion

        #region   消息处理具体执行部分，高级部分可以覆盖

        /// <summary>
        /// 核心执行方法   ==  执行方法
        /// </summary>
        /// <param name="recMsgXml">传入消息的xml</param>
        protected virtual ResultMo<MsgContext> Execute(string recMsgXml)
        {
            XmlDocument xmlDoc = null;
            var recMsgDirs = WxMsgHelper.ChangXmlToDir(recMsgXml, ref xmlDoc);

            if (!recMsgDirs.ContainsKey("MsgType"))
                return new ResultMo<MsgContext>(ResultTypes.ParaError, "消息数据中未发现 消息类型（MsgType）字段！");

            var msgType = recMsgDirs["MsgType"].ToLower();
            if (msgType == "event")
            {
                if (!recMsgDirs.ContainsKey("Event"))
                    return new ResultMo<MsgContext>(ResultTypes.ParaError, "事件消息数据中未发现 事件类型（Event）字段！");
            }

            var context = ProcessExecute_BasicMsg(xmlDoc, msgType, recMsgDirs)
                          ?? ProcessExecute_CustomHandler(xmlDoc, msgType, recMsgDirs)
                          ?? ExecuteHandler(xmlDoc, recMsgDirs, new BaseRecMsg(), ProcessUnknowHandler);

            return new ResultMo<MsgContext>(context);
        }



        /// <summary>
        ///  获取用户自定义的消息处理Handler
        ///   【返回对象需继承：WxMsgProcessor&lt;TRecMsg&gt;】
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        protected virtual WxMsgProcessor GetMsgProcessor(string msgType, string eventName = null)
        {
            return null;
        }


        #endregion

        #region  消息处理 == end  当前消息处理结束触发

        /// <summary>
        ///  执行结束方法
        /// </summary>
        /// <param name="msgContext"></param>
        protected virtual void ExecuteEnd(MsgContext msgContext)
        {

        }

        #endregion



        /// <summary>
        ///  根据具体的消息类型执行相关的消息委托方法(基础消息)
        /// </summary>
        /// <typeparam name="TRecMsg"></typeparam>
        /// <param name="recMsgXml"></param>
        /// <param name="recMsgDirs"></param>
        /// <param name="recMsg"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static MsgContext ExecuteHandler<TRecMsg>(XmlDocument recMsgXml,
            IDictionary<string, string> recMsgDirs, TRecMsg recMsg, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            if (recMsg == null)
                recMsg = new TRecMsg();

            var msgContext = new MsgContext();

            recMsg.SetMsgDirs(recMsgDirs);
            recMsg.RecMsgXml = recMsgXml;

            msgContext.ReplyMsg = ExecuteHandlerDelegate(recMsg, func);
            msgContext.RecMsg = recMsg;

            return msgContext;
        }


        /// <summary>
        /// 执行事件对应委托方法，如果对应的方法存在则执行
        /// </summary>
        /// <typeparam name="TRecMsg"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static BaseReplyMsg ExecuteHandlerDelegate<TRecMsg>(TRecMsg res, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg
        {
            var baseRep = func?.Invoke(res) ?? new NoneReplyMsg();

            baseRep.ToUserName = res.FromUserName;
            baseRep.FromUserName = res.ToUserName;
            baseRep.CreateTime = DateTime.Now.ToLocalSeconds();

            return baseRep;
        }

    }
}
