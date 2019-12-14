#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：自定义消息处理实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using OSS.Clients.Chat.WX.Mos;

namespace OSS.Clients.Chat.WX
{
    /// <summary>
    ///  自定义消息处理Handler基类
    /// 【用户自定义请返回：WXChatCustomMsgHandler&lt;TRecMsg&gt;或其子类】
    /// </summary>
    public abstract class WXChatProcessor
    {
        internal bool CanExecute { get; set; }

        protected internal abstract WXBaseReplyMsg Execute(WXBaseRecMsg msg);
        
        protected internal abstract WXBaseRecMsg CreateNewInstance();
    }

    /// <inheritdoc />
    /// <summary>
    ///   内部自定义消息类型处理Processor
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    public class WXChatProcessor<TRecMsg> : WXChatProcessor
        where TRecMsg : WXBaseRecMsg, new()
    {
        private Func<TRecMsg, WXBaseReplyMsg> _processFunc;

        /// <summary>
        /// 处理方法实现
        /// </summary>
        public Func<TRecMsg, WXBaseReplyMsg> ProcessFunc
        {
            get => _processFunc;
            set
            {
                CanExecute = true;
                _processFunc = value;
            }
        }
        /// <summary>
        ///  对应的接受消息创建实例方法
        ///    如果不设置，会通过反射创建
        /// </summary>
        public Func<TRecMsg> RecInsCreater { get; set; }
        
        protected internal override WXBaseReplyMsg Execute(WXBaseRecMsg msg)
        {
            return ProcessFunc?.Invoke(msg as TRecMsg);
        }
        
        protected internal override WXBaseRecMsg CreateNewInstance()
        {
            return RecInsCreater?.Invoke() ?? new TRecMsg();
        }
    }
    
}
