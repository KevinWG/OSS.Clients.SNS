
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
    public static class WxMsgProcessorProvider
    {
        private static readonly ConcurrentDictionary<string, WxMsgProcessor> m_HandlerDirs =
            new ConcurrentDictionary<string, WxMsgProcessor>();

        /// <summary>
        /// 注册消息处理委托
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="func"></param>
        public static ResultMo RegisteProcessor<TRecMsg>(string msgType, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var key = msgType.ToLower();
            if (m_HandlerDirs.ContainsKey(key))
                return new ResultMo(ResultTypes.ObjectExsit, "已存在相同的消息处理类型！");

            var handler = new WxMsgRegProcessor<TRecMsg> { Handler = func };
            return m_HandlerDirs.TryAdd(key, handler)
                ? new ResultMo()
                : new ResultMo(ResultTypes.ObjectExsit, "注册消息处理句柄失败！");
        }

        /// <summary>
        /// 注册事件消息处理委托
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="func"></param>
        public static ResultMo RegisteEventProcessor<TRecMsg>(string eventName,Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecEventMsg, new()
        {
            var key = string.Concat("event_", eventName);

            return RegisteProcessor(key, func);
        }

        /// <summary>
        ///  获取自定义的句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static WxMsgProcessor GetHandler(string name)
        {
            m_HandlerDirs.TryGetValue(name, out WxMsgProcessor handler);
            return handler ?? new WxMsgProcessor();
        }
    }


    #region 通过字典全局自定义消息实现


    /// <inheritdoc />
    /// <summary>
    ///   自定义消息类型处理Handler
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    internal class WxMsgRegProcessor<TRecMsg> : WxMsgProcessor
        where TRecMsg : BaseRecMsg, new()
    {
        protected internal override BaseReplyMsg Execute(BaseRecMsg msg)
        {
            return Handler?.Invoke(msg as TRecMsg);
        }

        public override BaseRecMsg CreateNewInstance()
        {
            return new TRecMsg();
        }

        public Func<TRecMsg, BaseReplyMsg> Handler { get; set; }
    }

    #endregion

}
