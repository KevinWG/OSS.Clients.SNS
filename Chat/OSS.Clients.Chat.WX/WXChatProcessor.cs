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
    ///  消息处理基类
    /// </summary>
    public abstract class BaseWXChatProcessor
    {
        internal abstract WXBaseRecMsg CreateRecMsg();

        internal abstract WXBaseReplyMsg InternalExecute(WXBaseRecMsg msg);
    }

    /// <summary>
    /// 具体消息处理类
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    public abstract class WXChatProcessor<TRecMsg> : BaseWXChatProcessor
        where TRecMsg : WXBaseRecMsg, new()
    {
        protected abstract WXBaseReplyMsg Execute(TRecMsg msg);

        internal override WXBaseRecMsg CreateRecMsg()
        {
            return new TRecMsg();
        }

        internal override WXBaseReplyMsg InternalExecute(WXBaseRecMsg msg)
        {
            return Execute(msg as TRecMsg);
        }
    }


    /// <inheritdoc />
    /// <summary>
    ///   内部自定义消息类型处理Processor
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    internal class WXChatInternalProcessor<TRecMsg> : BaseWXChatProcessor
        where TRecMsg : WXBaseRecMsg, new()
    {
        private Func<TRecMsg, WXBaseReplyMsg> _processFunc;

        /// <summary>
        /// 处理方法实现
        /// </summary>
        internal Func<TRecMsg, WXBaseReplyMsg> ProcessFunc
        {
            get => _processFunc;
            set
            {
                _processFunc = value;
            }
        }

        internal override WXBaseRecMsg CreateRecMsg()
        {
            return new TRecMsg();
        }

        internal override WXBaseReplyMsg InternalExecute(WXBaseRecMsg msg)
        {
            return ProcessFunc?.Invoke(msg as TRecMsg);
        }
    }

}
