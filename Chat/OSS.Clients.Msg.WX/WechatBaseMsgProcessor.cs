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
using System.Threading.Tasks;
using OSS.Clients.Msg.Wechat.Helper;

namespace OSS.Clients.Msg.Wechat
{
    /// <summary>
    ///  消息处理最底层基类
    /// </summary>
    public abstract class BaseBaseProcessor
    {
        internal abstract WechatBaseRecMsg CreateRecMsg();

        internal abstract Task<WechatBaseReplyMsg> InternalExecute(WechatBaseRecMsg msg);
    }

    /// <summary>
    /// 消息处理基类
    /// </summary>
    /// <typeparam name="TRecMsg">接收消息类型</typeparam>
    public abstract class WechatBaseMsgProcessor<TRecMsg> : BaseBaseProcessor
        where TRecMsg : WechatBaseRecMsg, new()
    {
        /// <summary>
        ///  消息执行方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected abstract Task<WechatBaseReplyMsg> Execute(TRecMsg msg);

        internal override WechatBaseRecMsg CreateRecMsg()
        {
            return new TRecMsg();
        }

        internal override async Task<WechatBaseReplyMsg> InternalExecute(WechatBaseRecMsg msg)
        {
            var  replyMsg= await Execute(msg as TRecMsg);
            return replyMsg;
        }
    }

    internal class InternalWechatChatProcessor : WechatBaseMsgProcessor<WechatBaseRecMsg>
    {
        protected override Task<WechatBaseReplyMsg> Execute(WechatBaseRecMsg msg)
        {
            // 统一交由后续拦截
            return InterUtil.NullResult;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///   内部自定义消息类型处理Processor
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    internal class InternalWechatChatProcessor<TRecMsg> : BaseBaseProcessor
        where TRecMsg : WechatBaseRecMsg, new()
    {
        /// <summary>
        /// 处理方法实现
        /// </summary>
        internal Func<TRecMsg, Task<WechatBaseReplyMsg>> ProcessFunc {private get; set; }

        internal override WechatBaseRecMsg CreateRecMsg()
        {
            return new TRecMsg();
        }

        internal override Task<WechatBaseReplyMsg> InternalExecute(WechatBaseRecMsg msg)
        {
            if (ProcessFunc!=null)
            {
                return ProcessFunc.Invoke(msg as TRecMsg);
            }
            return InterUtil.NullResult;
        }
    }

}
