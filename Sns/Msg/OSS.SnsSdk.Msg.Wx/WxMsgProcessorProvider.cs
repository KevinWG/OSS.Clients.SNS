
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
using OSS.Common.Plugs;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <summary>
    ///  用户自定义消息处理句柄
    /// </summary>
    public static class WxMsgProcessorProvider
    {
        private static readonly ConcurrentDictionary<string, WxMsgProcessor> processorDirs =
            new ConcurrentDictionary<string, WxMsgProcessor>();

        /// <summary>
        /// 注册消息处理委托
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="func"></param>
        public static ResultMo RegisteProcessor<TRecMsg>(string msgType, Func<TRecMsg, WxBaseReplyMsg> func)
            where TRecMsg : WxBaseRecMsg, new()
        {
            var key = msgType.ToLower();
            if (processorDirs.ContainsKey(key))
                return new ResultMo(ResultTypes.ObjectExsit, "已存在相同的消息处理类型！");

            var handler = new WxMsgProcessor<TRecMsg> { ProcessFunc = func };
            return processorDirs.TryAdd(key, handler)
                ? new ResultMo()
                : new ResultMo(ResultTypes.ObjectExsit, "注册消息处理句柄失败！");
        }

        /// <summary>
        /// 注册事件消息处理委托
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="func"></param>
        public static ResultMo RegisteEventProcessor<TRecMsg>(string eventName,Func<TRecMsg, WxBaseReplyMsg> func)
            where TRecMsg : WxBaseRecEventMsg, new()
        {
            var key = string.Concat("event_", eventName);

            return RegisteProcessor(key, func);
        }

        /// <summary>
        ///  获取自定义的句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static WxMsgProcessor GetProcessor(string name)
        {
            processorDirs.TryGetValue(name, out var processor);
            return processor;
        }
    }


    /// <summary>
    ///  消息模块配置相关
    /// </summary>
    public static class WxMsgConfigProvider
    {

        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static WxMsgConfig DefaultConfig { get; set; }

        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = ModuleNames.SocialCenter;
    }

}
