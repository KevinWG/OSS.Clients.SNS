#region Copyright (C) 2016  Kevin  （OSS系列开源项目）

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
using System.IO;
using System.Xml;
using OSS.Clients.Chat.WX.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos.Resp;
using OSS.Common.Extention;

namespace OSS.Clients.Chat.WX
{
    /// <summary>
    ///   消息处理的基类
    /// </summary>
    public class WXChatBaseHandler:BaseApiConfigProvider<WXChatConfig>
    {
        /// <summary>
        /// 消息处理基类
        /// </summary>
        /// <param name="config"></param>
        protected WXChatBaseHandler(WXChatConfig config=null):base(config)
        {
        }
        
        #region 消息处理入口，出口（分为开始，处理，结束部分）

        /// <summary>
        ///  服务器接入验证     
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public StrResp CheckServerValid(string signature, string timestamp, string nonce, string echostr)
        {
            var checkSignRes = WXChatHelper.CheckSignature(ApiConfig.Token, signature, timestamp, nonce);

            var resultRes =new StrResp().WithResp(checkSignRes);// checkSignRes.ConvertToResult<string>();
            resultRes.data = resultRes.IsSuccess() ? echostr : string.Empty;

            return resultRes;
        }
        /// <summary>
        ///    消息处理入口
        /// </summary>
        /// <param name="reqStream">内容的数据流</param>
        /// <param name="signature">签名信息</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符创</param>
        /// <param name="echostr">验证服务器参数，如果存在则只进行签名验证，并将在结果data中返回</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public StrResp Process(Stream reqStream, string signature, string timestamp, string nonce,
            string echostr)
        {
            string contentXml;
            using (var reader = new StreamReader(reqStream))
            {
                contentXml = reader.ReadToEnd();
            }
            return Process(contentXml, signature, timestamp, nonce, echostr);
        }

        /// <summary>
        /// 消息处理入口
        /// </summary>
        /// <param name="contentXml">内容信息</param>
        /// <param name="signature">签名信息</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符创</param>
        /// <param name="echostr">验证服务器参数，如果存在则只进行签名验证，并将在结果data中返回</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public StrResp Process(string contentXml, string signature, string timestamp, string nonce,
            string echostr)
        {
            // 一.  检查是否是服务器设置验证
            if (!string.IsNullOrEmpty(echostr))
            {
                return CheckServerValid(signature, timestamp, nonce, echostr);
            }

            // 二.  正常消息处理
            var checkRes = Prepare(contentXml, signature, timestamp, nonce);
            if (!checkRes.IsSuccess())
                return new StrResp().WithResp(checkRes); //checkRes.ConvertToResult<string>();

            var contextRes = Processing(checkRes.data);
            if (!contextRes.IsSuccess())
                return new StrResp().WithResp(contextRes);// contextRes.ConvertToResult<string>();

            var resultString = contextRes.data.ReplyMsg.ToReplyXml();
            if (ApiConfig.SecurityType != WXSecurityType.None &&
                !string.IsNullOrEmpty(contextRes.data.ReplyMsg.MsgType))
            {
                return WXChatHelper.EncryptMsg(resultString, ApiConfig);
            }
            return new StrResp(resultString);
        }


        /// <summary>
        /// 核心执行方法
        /// </summary>
        /// <param name="recMsgXml">传入消息的xml</param>
        /// <returns></returns>
        protected virtual Resp<WXChatContext> Processing(string recMsgXml)
        {
            var recMsgDirs = WXChatHelper.ChangXmlToDir(recMsgXml, out XmlDocument xmlDoc);

            recMsgDirs.TryGetValue("MsgType", out var msgType);// recMsgDirs["MsgType"].ToLower();
            string eventName = null;

            if (msgType == "event")
            {
                if (!recMsgDirs.TryGetValue("Event", out eventName))
                    return new Resp<WXChatContext>().WithResp(RespTypes.ParaError, "事件消息数据中未发现 事件类型（Event）字段！");
            }

            var processor = GetInternalMsgProcessor(msgType, eventName)
                ?? (GetCustomProcessor(msgType, eventName, recMsgDirs)
                    ?? GetRegProcessor(msgType, eventName));

            var context = processor != null
                ? ExecuteProcessor(xmlDoc, recMsgDirs, processor)
                : ExecuteProcessor(xmlDoc, recMsgDirs,new WXChatInternalProcessor<WXBaseRecMsg>());

            ExecuteEnd(context);

            return new Resp<WXChatContext>(context);
        }


        /// <summary>
        /// 核心执行 过程的  验签和解密
        /// </summary>
        /// <param name="recXml">消息内容</param>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>验证结果及相应的消息内容体 （如果加密模式，返回的是解密后的明文）</returns>
        private StrResp Prepare(string recXml, string signature,
            string timestamp, string nonce)
        {
            if (string.IsNullOrEmpty(recXml))
                return new StrResp().WithResp(RespTypes.ObjectNull, "接收的消息体为空！");

            var resCheck = WXChatHelper.CheckSignature(ApiConfig.Token, signature, timestamp, nonce);
            if (!resCheck.IsSuccess())
                return new StrResp().WithResp(resCheck);// resCheck.ConvertToResult<string>();

            if (ApiConfig.SecurityType == WXSecurityType.None)
                return new StrResp(recXml);

            var dirs = WXChatHelper.ChangXmlToDir(recXml, out XmlDocument xmlDoc);

            if (dirs == null || !dirs.TryGetValue("Encrypt", out var encryStr)
                || string.IsNullOrEmpty(encryStr))
                return new StrResp().WithResp(RespTypes.ObjectNull, "加密消息为空");

            var recMsgXml = Cryptography.WXAesDecrypt(encryStr, ApiConfig.EncodingAesKey);

            return new StrResp(recMsgXml);
        }

        /// <summary>
        ///  执行具体消息处理委托
        /// </summary>
        /// <returns></returns>
        private WXChatContext ExecuteProcessor(XmlDocument recMsgXml,
            IDictionary<string, string> recMsgDirs, BaseWXChatProcessor processor)
        {
            var recMsg = processor.CreateRecMsg();
            recMsg.LoadMsgDirs(recMsgDirs);
            recMsg.RecMsgXml = recMsgXml;

            var msgContext = new WXChatContext
            {
                RecMsg = recMsg, 
                ReplyMsg = processor?.InternalExecute(recMsg)
            };

            if (msgContext.ReplyMsg == null)
                msgContext.ReplyMsg = ProcessUnknowMsg(recMsg) ?? WXNoneReplyMsg.None;

            msgContext.ReplyMsg.ToUserName = recMsg.FromUserName;
            msgContext.ReplyMsg.FromUserName = recMsg.ToUserName;
            msgContext.ReplyMsg.CreateTime = DateTime.Now.ToLocalSeconds();

            return msgContext;
        }


        #endregion

        #region  消息执行时生命周期的关键事件

        /// <summary>
        ///   执行处理未知消息
        /// </summary>
        /// <returns></returns>
        protected virtual WXBaseReplyMsg ProcessUnknowMsg(WXBaseRecMsg msg)
        {
            return null;
        }

        /// <summary>
        ///  执行结束方法
        /// </summary>
        /// <param name="msgContext"></param>
        protected virtual void ExecuteEnd(WXChatContext msgContext)
        {
        }

        #endregion

        #region  获取 Processor
        /// <summary>
        ///  获取消息处理Processor
        ///   【返回对象需继承：WXChatProcessor&lt;TRecMsg&gt;】
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="msgInfo">对应消息的键值对</param>
        /// <returns>WXChatProcessor&lt;TRecMsg&gt;或其子类，如果没有定义对应的消息类型，返回Null即可</returns>
        protected virtual BaseWXChatProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            return null;
        }

        internal virtual BaseWXChatProcessor GetInternalMsgProcessor(string msgType, string eventName)
        {
            return null;
        }

        private static BaseWXChatProcessor GetRegProcessor(string msgType, string eventName)
        {
            var key = msgType == "event" ? string.Concat("event_", eventName ?? string.Empty) : msgType;
            return WXChatConfigProvider.GetProcessor(key);
        }
        #endregion




        /// <inheritdoc />
        protected override WXChatConfig GetDefaultConfig()
        {
            return WXChatConfigProvider.DefaultConfig;
        }
    }
}