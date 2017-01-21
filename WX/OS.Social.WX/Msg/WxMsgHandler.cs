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
using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.WX.Msg
{
    /// <summary>
    ///  消息对话事件句柄，被动消息处理
    /// </summary>
    public class WxMsgHandler:WxMsgBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxMsgHandler(WxMsgServerConfig config):base(config)
        {
        }

        
        static WxMsgHandler()
        {
          
        }


        private static readonly ConcurrentDictionary<string,Tuple<Type, Func<BaseRecMsg, BaseReplyMsg>> > m_MsgHandlerDir 
            = new ConcurrentDictionary<string, Tuple<Type, Func<BaseRecMsg, BaseReplyMsg>>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="recType"></param>
        /// <param name="handler"></param>
        protected  static ResultMo RegisterMsgHandler(string msgType,Type recType, Func<BaseRecMsg, BaseReplyMsg> handler)
        {
            string key = msgType.ToLower();
            if (!m_MsgHandlerDir.ContainsKey(key))
            {
                var isTrue= m_MsgHandlerDir.TryAdd(key, Tuple.Create(recType, handler));
                if (isTrue)
                {
                    return new ResultMo();
                }
            }
            return new ResultMo(ResultTypes.ObjectExsit,"已存在相同的消息处理类型！");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="recMsgType"></param>
        /// <param name="handler"></param>
        protected static ResultMo RegisterEventMsgHandler(string eventType, Type recMsgType, Func<BaseRecMsg, BaseReplyMsg> handler)
        {
            string key =string.Concat("event-", eventType);

            return RegisterMsgHandler(key, recMsgType, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recMsg"></param>
        /// <param name="msgType"></param>
        /// <param name="msgDirs"></param>
        /// <returns></returns>
        protected override MsgContext ProcessExecute_AdvancedMsg(string recMsg, string msgType, Dictionary<string, string> msgDirs)
        {
            string key = msgType == "event" ? string.Concat("event-", msgDirs["Event"].ToLower()) : msgType;
            if (!m_MsgHandlerDir.ContainsKey(key))
                return null;

            var tupleItem = m_MsgHandlerDir[key];
            //  处理msg
            var msg = Activator.CreateInstance(tupleItem.Item1) as BaseRecMsg;
            if (msg != null)
            {
                msg.SetMsgDirs(msgDirs);
                msg.RecMsgXml = recMsg;
            }
            var replyMsg = ExecuteHandler(msg, tupleItem.Item2);

            return new MsgContext() {RecMsg = msg,ReplyMsg = replyMsg};
        }


    }




}
