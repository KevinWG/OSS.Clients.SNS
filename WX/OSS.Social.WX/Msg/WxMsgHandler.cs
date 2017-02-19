#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄，被动消息处理类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.Social.WX.Msg.Mos;

namespace OSS.Social.WX.Msg
{
    /// <summary>
    ///  消息对话事件句柄，被动消息处理
    /// </summary>
    public class WxMsgHandler:WxMsgBasicHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxMsgHandler(WxMsgServerConfig config):base(config)
        {
        }


        private static readonly ConcurrentDictionary<string,Tuple<Type, Func<BaseRecMsg, BaseReplyMsg>> > m_MsgHandlerDir 
            = new ConcurrentDictionary<string, Tuple<Type, Func<BaseRecMsg, BaseReplyMsg>>>();

        /// <summary>
        /// 注册消息处理委托
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="recMsgType">消息实体类型</param>
        /// <param name="handler">消息处理委托</param>
        protected static ResultMo RegisterMsgHandler(string msgType,Type recMsgType, Func<BaseRecMsg, BaseReplyMsg> handler)
        {
            string key = msgType.ToLower();
            if (!m_MsgHandlerDir.ContainsKey(key))
            {
                var isTrue= m_MsgHandlerDir.TryAdd(key, Tuple.Create(recMsgType, handler));
                if (isTrue)
                {
                    return new ResultMo();
                }
            }
            return new ResultMo(ResultTypes.ObjectExsit,"已存在相同的消息处理类型！");
        }

        /// <summary>
        /// 注册事件消息处理委托
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="recMsgEventType">事件消息实体类型</param>
        /// <param name="handler">事件处理委托</param>
        protected static ResultMo RegisterEventMsgHandler(string eventName, Type recMsgEventType, Func<BaseRecMsg, BaseReplyMsg> handler)
        {
            string key =string.Concat("event_", eventName);

            return RegisterMsgHandler(key, recMsgEventType, handler);
        }

        /// <summary>
        /// 执行高级消息事件类型
        /// </summary>
        /// <param name="recMsgXml">接收到的消息内容体</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="msgDirs">消息内容体字典</param>
        /// <returns></returns>
        protected override MsgContext ProcessExecute_AdvancedMsg(string recMsgXml, string msgType, Dictionary<string, string> msgDirs)
        {
            string key = msgType == "event" ? string.Concat("event_", msgDirs["Event"].ToLower()) : msgType;
            if (!m_MsgHandlerDir.ContainsKey(key))
                return null;  //  交由后续默认事件处理

            var tupleItem = m_MsgHandlerDir[key];

            //    反射生成消息实体实例
            var msg = Activator.CreateInstance(tupleItem.Item1) as BaseRecMsg;
            if (msg != null)
            {
                msg.SetMsgDirs(msgDirs);
                msg.RecMsgXml = recMsgXml;
            }

            var replyMsg = ExecuteHandler(msg, tupleItem.Item2);
            return new MsgContext() {RecMsg = msg,ReplyMsg = replyMsg};
        }


    }




}
