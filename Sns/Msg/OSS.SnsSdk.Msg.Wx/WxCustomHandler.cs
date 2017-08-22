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
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <summary>
    ///  用户自定义消息处理句柄
    /// </summary>
    public static class WxCustomHandlerProvider
    {
        private static readonly ConcurrentDictionary<string, WxMsgCustomBaseHandler> m_HandlerDirs =
            new ConcurrentDictionary<string, WxMsgCustomBaseHandler>();

        public static void Register<TRecMsg>(string name, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
        }

        /// <summary>
        /// 注册消息处理委托
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="recMsgType">消息实体类型</param>
        /// <param name="func"></param>
        public static ResultMo RegisterMsgHandler<TRecMsg>(string msgType, Type recMsgType,
            Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var key = msgType.ToLower();
            if (m_HandlerDirs.ContainsKey(key))
                return new ResultMo(ResultTypes.ObjectExsit, "已存在相同的消息处理类型！");

            var handler = new WxCustomBaseHandler<TRecMsg> {Handler = func};
            return m_HandlerDirs.TryAdd(key, handler)
                ? new ResultMo()
                : new ResultMo(ResultTypes.ObjectExsit, "注册消息处理句柄失败！");
        }

        /// <summary>
        /// 注册事件消息处理委托
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="recMsgEventType">事件消息实体类型</param>
        /// <param name="func"></param>
        public static ResultMo RegisterEventMsgHandler<TRecMsg>(string eventName, Type recMsgEventType,
            Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var key = string.Concat("event_", eventName);

            return RegisterMsgHandler(key, recMsgEventType, func);
        }


        /// <summary>
        ///  获取自定义的句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static WxMsgCustomBaseHandler GetHandler(string name)
        {
            m_HandlerDirs.TryGetValue(name, out WxMsgCustomBaseHandler handler);
            return handler ?? new WxMsgCustomBaseHandler();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class WxMsgCustomBaseHandler
    {
        public virtual BaseReplyMsg Excute(BaseRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        public virtual BaseRecMsg CreateInstance()
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    internal class WxCustomBaseHandler<TRecMsg> : WxMsgCustomBaseHandler
        where TRecMsg : BaseRecMsg, new()
    {
        public override BaseReplyMsg Excute(BaseRecMsg msg)
        {
            return Handler(msg as TRecMsg);
        }

        public override BaseRecMsg CreateInstance()
        {
            return new TRecMsg();
        }

        internal Func<TRecMsg, BaseReplyMsg> Handler { get; set; }
    }
    
}
