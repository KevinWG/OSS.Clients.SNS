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
using System.Threading.Tasks;
using System.Xml;
using OSS.Clients.Chat.WX.Helper;
using OSS.Clients.Chat.WX.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos.Resp;
using OSS.Common.Extention;

namespace OSS.Clients.Chat.WX
{
    /// <summary>
    ///   消息处理的基类
    /// </summary>
    public class WXChatBaseHandler: BaseMetaImpl<WXChatConfig>
    {
        /// <inheritdoc />
        protected WXChatBaseHandler(IMetaProvider<WXChatConfig> configProvider=null):base(configProvider)
        {
        }
        
        #region 消息处理入口，出口（分为开始，处理，结束部分）

        /// <summary>
        ///  服务器接入验证     
        /// </summary>
        /// <returns></returns>
        public static StrResp CheckServerValid(WXChatConfig appConfig, string signature, string timestamp, string nonce, string echostr)
        {
            var checkSignRes = WXChatHelper.CheckSignature(appConfig.Token, signature, timestamp, nonce,string.Empty);

            var resultRes =new StrResp().WithResp(checkSignRes);// checkSignRes.ConvertToResult<string>();
            resultRes.data = resultRes.IsSuccess() ? echostr : string.Empty;

            return resultRes;
        }

        /// <summary>
        ///    消息处理入口
        /// </summary>
        /// <param name="reqStream">内容的数据流</param>
        /// <param name="signature">签名信息</param>
        /// <param name="msg_signature">消息体签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符创</param>
        /// <param name="echostr">验证服务器参数，如果存在则只进行签名验证，并将在结果data中返回</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public Task<StrResp> Process(Stream reqStream, string signature,string msg_signature, string timestamp, string nonce,
            string echostr)
        {
            string contentXml;
            using (var reader = new StreamReader(reqStream))
            {
                contentXml = reader.ReadToEnd();
            }
            return Process(contentXml, signature,msg_signature, timestamp, nonce, echostr);
        }

        /// <summary>
        /// 消息处理入口
        /// </summary>
        /// <param name="contentXml">内容信息</param>
        /// <param name="signature">签名信息，请注意不是[msg_signature]</param>
        /// <param name="msg_signature">消息体签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符创</param>
        /// <param name="echostr">验证服务器参数，如果存在则只进行签名验证，并将在结果data中返回</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public async Task<StrResp> Process(string contentXml, string signature, string msg_signature, string timestamp, string nonce,
            string echostr)
        {
            if (string.IsNullOrEmpty(contentXml)|| string.IsNullOrEmpty(signature)
                || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce))
                return new StrResp().WithResp(RespTypes.ParaError,"消息相关参数错误！");
            
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new StrResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;

            // 一.  检查是否是微信服务端首次地址配置验证
            if (!string.IsNullOrEmpty(echostr))
                return CheckServerValid(appConfig,signature, timestamp, nonce, echostr);
            
            var checkRes = Prepare(appConfig, contentXml, signature, msg_signature, timestamp, nonce);
            if (!checkRes.IsSuccess())
                return new StrResp().WithResp(checkRes); 

            var contextRes =await Processing(checkRes.data);
            if (!contextRes.IsSuccess())
                return new StrResp().WithResp(contextRes);

            var resultString = contextRes.data.ReplyMsg.ToReplyXml();
            if (appConfig.SecurityType != WXSecurityType.None &&
                !string.IsNullOrEmpty(contextRes.data.ReplyMsg.MsgType))
            {
                return WXChatHelper.EncryptMsg(resultString, appConfig);
            }
            return new StrResp(resultString);
        }


        /// <summary>
        /// 核心执行方法
        /// </summary>
        /// <param name="recMsgXml">传入消息的xml</param>
        /// <returns></returns>
        protected virtual async Task<Resp<WXChatContext>> Processing(string recMsgXml)
        {
            var recMsgDirs = WXChatHelper.ChangXmlToDir(recMsgXml, out var xmlDoc);
            recMsgDirs.TryGetValue("MsgType", out var msgType);
            string eventName = null;

            if (msgType == "event")
            {
                if (!recMsgDirs.TryGetValue("Event", out eventName))
                    return new Resp<WXChatContext>().WithResp(RespTypes.ParaError, "事件消息数据中未发现 事件类型（Event）字段！");
            }

            var processor = GetInternalMsgProcessor(msgType, eventName)
                ?? GetCustomProcessor(msgType, eventName, recMsgDirs);

            var context = await (processor != null
                ? ExecuteProcessor(xmlDoc, recMsgDirs, processor)
                : ExecuteProcessor(xmlDoc, recMsgDirs, SingleInstance<InternalWXChatProcessor>.Instance));

            await ExecuteEnd(context);

            return new Resp<WXChatContext>(context);
        }

        /// <summary>
        /// 核心执行 过程的  验签和解密
        /// </summary>
        /// <returns>验证结果及相应的消息内容体 （如果加密模式，返回的是解密后的明文）</returns>
        private static StrResp Prepare(WXChatConfig appConfig, string recXml, string signature, string msg_signature,
            string timestamp, string nonce)
        {
            var isEncryptMsg = appConfig.SecurityType == WXSecurityType.Safe;
            if (!isEncryptMsg)
            {
                var resCheck = WXChatHelper.CheckSignature(appConfig.Token, signature, timestamp, nonce, String.Empty);
                return !resCheck.IsSuccess() ? new StrResp().WithResp(resCheck) : new StrResp(recXml);
            }

            if (string.IsNullOrEmpty(msg_signature))
                return new StrResp().WithResp(RespTypes.ParaError, "msg_signature 消息体验证签名参数为空！");

            var xmlDoc = WXChatHelper.GetXmlDocment(recXml);
            var encryStr= xmlDoc?.FirstChild["Encrypt"]?.InnerText;

            if ( string.IsNullOrEmpty(encryStr))
                return new StrResp().WithResp(RespTypes.ObjectNull, "安全接口的加密字段为空！");

            var cryptMsgCheck = WXChatHelper.CheckSignature(appConfig.Token, msg_signature, timestamp, nonce, encryStr);
            if (!cryptMsgCheck.IsSuccess())
               return new StrResp().WithResp(cryptMsgCheck);

            var recMsgXml = Cryptography.AESDecrypt(encryStr, appConfig.EncodingAesKey);
            return new StrResp(recMsgXml);
        }

        /// <summary>
        ///  执行具体消息处理委托
        /// </summary>
        /// <returns></returns>
        private async Task<WXChatContext> ExecuteProcessor(XmlDocument recMsgXml,
            IDictionary<string, string> recMsgDirs, BaseBaseProcessor processor)
        {
            var recMsg = processor.CreateRecMsg();
            recMsg.LoadMsgDirs(recMsgDirs);
            recMsg.RecMsgXml = recMsgXml;

            var msgContext = new WXChatContext {RecMsg = recMsg, ReplyMsg = await processor.InternalExecute(recMsg)};

            if (msgContext.ReplyMsg == null)
                msgContext.ReplyMsg =(await ProcessUnknowMsg(recMsg)) ?? WXNoneReplyMsg.None;

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
        protected virtual Task<WXBaseReplyMsg> ProcessUnknowMsg(WXBaseRecMsg msg)
        {
            return Task.FromResult<WXBaseReplyMsg>(null);
        }

        /// <summary>
        ///  执行结束方法
        /// </summary>
        /// <param name="msgContext"></param>
        protected virtual Task ExecuteEnd(WXChatContext msgContext)
        {
            return Task.CompletedTask;
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
        protected virtual BaseBaseProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            return null;
        }

        internal virtual BaseBaseProcessor GetInternalMsgProcessor(string msgType, string eventName)
        {
            return null;
        }

        #endregion


        /// <inheritdoc />
        protected override WXChatConfig GetDefaultMeta()
        {
            return WXChatConfigProvider.DefaultConfig;
        }
    }
}