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
        /// <summary>
        /// 消息处理基类
        /// </summary>
        /// <param name="config"></param>
        protected WxMsgBaseHandler(WxMsgServerConfig config=null):base(config)
        {
            ModuleName = ModuleNames.SocialCenter;
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
        /// 核心执行方法
        /// </summary>
        /// <param name="recMsgXml">传入消息的xml</param>
        protected virtual ResultMo<MsgContext> Execute(string recMsgXml)
        {
            XmlDocument xmlDoc = null;
            var recMsgDirs = WxMsgHelper.ChangXmlToDir(recMsgXml, ref xmlDoc);

            if (!recMsgDirs.ContainsKey("MsgType"))
                return new ResultMo<MsgContext>(ResultTypes.ParaError, "消息数据中未发现 消息类型（MsgType）字段！");

            var msgType = recMsgDirs["MsgType"].ToLower();
            var eventName = string.Empty;

            if (msgType == "event")
            {
                if (!recMsgDirs.TryGetValue("Event", out eventName))
                    return new ResultMo<MsgContext>(ResultTypes.ParaError, "事件消息数据中未发现 事件类型（Event）字段！");
            }

            var processor = GetMsgProcessor(msgType, eventName) 
                ?? GetBasicMsgProcessor(msgType, eventName);

            var context = processor != null
                ? ExecuteHandler(xmlDoc, recMsgDirs, processor.CreateNewInstance(), processor.Execute)
                : ExecuteHandler(xmlDoc, recMsgDirs, new BaseRecMsg(), ExecuteUnknowProcessor);

            return new ResultMo<MsgContext>(context);
        }


        /// <summary>
        ///   执行处理未知消息
        /// </summary>
        /// <returns></returns>
        protected virtual BaseReplyMsg ExecuteUnknowProcessor(BaseRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        ///  获取消息处理Processor
        ///   【返回对象需继承：WxMsgProcessor&lt;TRecMsg&gt;】
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        protected virtual WxMsgProcessor GetMsgProcessor(string msgType, string eventName)
        {
            return null;
        }

        internal virtual WxMsgProcessor GetBasicMsgProcessor(string msgType, string eventName)
        {
            var key = msgType == "event" ? string.Concat("event_", eventName ?? string.Empty) : msgType;
            return WxMsgProcessorProvider.GetProcessor(key);
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

            recMsg.LoadMsgDirs(recMsgDirs);
            recMsg.RecMsgXml = recMsgXml;

            msgContext.RecMsg = recMsg;


            var reply= func?.Invoke(recMsg) ?? new NoneReplyMsg();

            reply.ToUserName = recMsg.FromUserName;
            reply.FromUserName = recMsg.ToUserName;
            reply.CreateTime = DateTime.Now.ToLocalSeconds();

            msgContext.ReplyMsg = reply;

            return msgContext;
        }

    }
}
