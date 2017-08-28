
#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：用户自定义消息处理方法提供者
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-8-1
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
    public static class WxCustomMsgHandlerProvider
    {
        private static readonly ConcurrentDictionary<string, WxMsgCustomMsgHandler> m_HandlerDirs =
            new ConcurrentDictionary<string, WxMsgCustomMsgHandler>();

        /// <summary>
        /// 注册消息处理委托
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="func"></param>
        public static ResultMo RegisterMsgHandler<TRecMsg>(string msgType, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var key = msgType.ToLower();
            if (m_HandlerDirs.ContainsKey(key))
                return new ResultMo(ResultTypes.ObjectExsit, "已存在相同的消息处理类型！");

            var handler = new WxCustomMsgDirHandler<TRecMsg> { Handler = func };
            return m_HandlerDirs.TryAdd(key, handler)
                ? new ResultMo()
                : new ResultMo(ResultTypes.ObjectExsit, "注册消息处理句柄失败！");
        }

        /// <summary>
        /// 注册事件消息处理委托
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="func"></param>
        public static ResultMo RegisterEventMsgHandler<TRecMsg>(string eventName,Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var key = string.Concat("event_", eventName);

            return RegisterMsgHandler(key, func);
        }

        /// <summary>
        ///  获取自定义的句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static WxMsgCustomMsgHandler GetHandler(string name)
        {
            m_HandlerDirs.TryGetValue(name, out WxMsgCustomMsgHandler handler);
            return handler ?? new WxMsgCustomMsgHandler();
        }
    }

}
