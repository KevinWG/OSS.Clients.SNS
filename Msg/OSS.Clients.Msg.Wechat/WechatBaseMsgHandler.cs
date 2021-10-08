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
using System.Threading.Tasks;
using System.Xml;
using OSS.Clients.Msg.Wechat.Helper;
using OSS.Common;
using OSS.Common.BasicMos.Resp;
using OSS.Common.Extension;

namespace OSS.Clients.Msg.Wechat
{
    /// <summary>
    ///   消息处理的基类
    /// </summary>
    public class WechatBaseMsgHandler //: BaseMetaImpl<WechatChatConfig>
    {
        /// <summary>
        /// 对话消息处理基类
        /// </summary>
        protected WechatBaseMsgHandler()
        {
        }

        #region 消息处理入口，出口（分为开始，处理，结束部分）

        /// <summary>
        ///  服务器接入验证
        /// </summary>
        /// <returns></returns>
        public static StrResp CheckServerValid(WechatMsgConfig appConfig, WechatRequestPara reqBody)
        {
            var checkSignRes =
                WechatChatHelper.CheckSignature(appConfig.Token, reqBody.signature, reqBody.timestamp, reqBody.nonce, string.Empty);

            var resultRes = new StrResp().WithResp(checkSignRes); // checkSignRes.ConvertToResult<string>();
            resultRes.data = resultRes.IsSuccess() ? reqBody.echostr : string.Empty;

            return resultRes;
        }


        /// <summary>
        /// 消息处理入口
        /// </summary>
        /// <param name="reqBody">请求参数信息</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public async Task<StrResp> Process(WechatRequestPara reqBody)
        {
            if (string.IsNullOrEmpty(reqBody.signature) || string.IsNullOrEmpty(reqBody.timestamp) || string.IsNullOrEmpty(reqBody.nonce))
                return new StrResp().WithResp(RespTypes.ParaError, "消息相关参数错误！");

            var appConfig = GetConfig(reqBody.app_id);

            // 一.  检查是否是微信服务端首次地址配置验证
            if (!string.IsNullOrEmpty(reqBody.echostr))
                return CheckServerValid(appConfig, reqBody);

            if (string.IsNullOrEmpty(reqBody.body))
                return new StrResp().WithResp(RespTypes.ParaError, "消息相关参数错误！");

            var checkRes = Prepare(appConfig, reqBody);
            if (!checkRes.IsSuccess())
                return new StrResp().WithResp(checkRes);

            var contextRes = await Processing(checkRes.data);
            if (!contextRes.IsSuccess())
                return new StrResp().WithResp(contextRes);

            var resultString = contextRes.data.ReplyMsg.ToReplyXml();
            if (appConfig.SecurityType != WechatSecurityType.None &&
                !string.IsNullOrEmpty(contextRes.data.ReplyMsg.MsgType))
            {
                return WechatChatHelper.EncryptMsg(resultString, appConfig);
            }

            return new StrResp(resultString);
        }


        /// <summary>
        /// 核心执行方法
        /// </summary>
        /// <param name="recMsgXml">传入消息的xml</param>
        /// <returns></returns>
        protected virtual async Task<Resp<WechatChatContext>> Processing(string recMsgXml)
        {
            var recMsgDirs = WechatChatHelper.ChangXmlToDir(recMsgXml, out var xmlDoc);
            recMsgDirs.TryGetValue("MsgType", out var msgType);
            string eventName = null;

            if (msgType == "event")
            {
                if (!recMsgDirs.TryGetValue("Event", out eventName))
                    return new Resp<WechatChatContext>().WithResp(RespTypes.ParaError, "事件消息数据中未发现 事件类型（Event）字段！");
            }

            var processor = GetInternalMsgProcessor(msgType, eventName)
                ?? GetCustomProcessor(msgType, eventName, recMsgDirs);

            var context = await (processor != null
                ? ExecuteProcessor(xmlDoc, recMsgDirs, processor)
                : ExecuteProcessor(xmlDoc, recMsgDirs, SingleInstance<InternalWechatChatProcessor>.Instance));

            await ProcessEnd(context);

            return new Resp<WechatChatContext>(context);
        }

        /// <summary>
        /// 核心执行 过程的  验签和解密
        /// </summary>
        /// <returns>验证结果及相应的消息内容体 （如果加密模式，返回的是解密后的明文）</returns>
        private static StrResp Prepare(WechatMsgConfig appConfig, WechatRequestPara reqBody)
        {
            var isEncryptMsg = appConfig.SecurityType == WechatSecurityType.Safe;
            if (!isEncryptMsg)
            {
                var resCheck =
                    WechatChatHelper.CheckSignature(appConfig.Token, reqBody.signature, reqBody.timestamp, reqBody.nonce, string.Empty);
                return !resCheck.IsSuccess() ? new StrResp().WithResp(resCheck) : new StrResp(reqBody.body);
            }

            if (string.IsNullOrEmpty(reqBody.msg_signature))
                return new StrResp().WithResp(RespTypes.ParaError, "msg_signature 消息体验证签名参数为空！");

            var xmlDoc   = WechatChatHelper.GetXmlDocment(reqBody.body);
            var encryStr = xmlDoc?.FirstChild["Encrypt"]?.InnerText;

            if (string.IsNullOrEmpty(encryStr))
                return new StrResp().WithResp(RespTypes.ObjectNull, "安全接口的加密字段为空！");

            var cryptMsgCheck =
                WechatChatHelper.CheckSignature(appConfig.Token, reqBody.msg_signature, reqBody.timestamp, reqBody.nonce, encryStr);
            if (!cryptMsgCheck.IsSuccess())
                return new StrResp().WithResp(cryptMsgCheck);

            var recMsgXml = Cryptography.AESDecrypt(encryStr, appConfig.EncodingAesKey);
            return new StrResp(recMsgXml);
        }

        /// <summary>
        ///  执行具体消息处理委托
        /// </summary>
        /// <returns></returns>
        private async Task<WechatChatContext> ExecuteProcessor(XmlDocument recMsgXml,
            IDictionary<string, string> recMsgDirs, BaseBaseProcessor processor)
        {
            var recMsg = processor.CreateRecMsg();

            recMsg.LoadMsgDirs(recMsgDirs);
            recMsg.RecMsgXml = recMsgXml;

            var msgContext = new WechatChatContext {RecMsg = recMsg};
            var pTask      = processor.InternalExecute(recMsg);
            if (pTask != null)
                msgContext.ReplyMsg = await pTask;

            if (msgContext.ReplyMsg == null)
                msgContext.ReplyMsg = (await ProcessUnknowMsg(recMsg)) ?? WechatNoneReplyMsg.None;

            msgContext.ReplyMsg.ToUserName   = recMsg.FromUserName;
            msgContext.ReplyMsg.FromUserName = recMsg.ToUserName;
            msgContext.ReplyMsg.CreateTime   = DateTime.Now.ToUtcSeconds();

            return msgContext;
        }

        #endregion

        #region 消息执行时生命周期的关键事件

        /// <summary>
        ///   执行处理未知消息
        /// </summary>
        /// <returns></returns>
        protected virtual Task<WechatBaseReplyMsg> ProcessUnknowMsg(WechatBaseRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        ///  执行结束方法
        /// </summary>
        /// <param name="msgContext"></param>
        protected virtual Task ProcessEnd(WechatChatContext msgContext)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region 获取 Processor

        /// <summary>
        ///  获取消息处理Processor
        ///   【返回对象需继承：WechatChatProcessor&lt;TRecMsg&gt;】
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="msgInfo">对应消息的键值对</param>
        /// <returns>WechatChatProcessor&lt;TRecMsg&gt;或其子类，如果没有定义对应的消息类型，返回Null即可</returns>
        protected virtual BaseBaseProcessor GetCustomProcessor(string msgType, string eventName,
            IDictionary<string, string> msgInfo)
        {
            return null;
        }

        internal virtual BaseBaseProcessor GetInternalMsgProcessor(string msgType, string eventName)
        {
            return null;
        }

        #endregion

        /// <inheritdoc />
        protected virtual WechatMsgConfig GetConfig(string appId)
        {
            var config = WechatMsgHelper.DefaultConfig;
            if (config == null)
            {
                throw new ArgumentNullException($"配置信息为空，请通过 {nameof(WechatMsgHelper.DefaultConfig)} 设置");
            }
            return config;
        }
    }
}