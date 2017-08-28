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
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <summary>
    ///   用户自定义实现的消息处理Handler
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    public abstract class WxMsgCustomMsgHandler<TRecMsg> : WxMsgCustomMsgHandler
        where TRecMsg : BaseRecMsg,new ()
    {
        public abstract BaseReplyMsg Excute(TRecMsg msg);

        /// <inheritdoc />
        public override BaseRecMsg CreateNewInstance()
        {
            return new TRecMsg();
        }

        protected internal override BaseReplyMsg Excute(BaseRecMsg msg)
        {
           return Excute(msg as TRecMsg);
        }
    }

    #region 通过字典全局自定义消息实现

    /// <summary>
    ///  自定义消息处理Handler基类
    /// </summary>
    public class WxMsgCustomMsgHandler
    {
        protected internal virtual BaseReplyMsg Excute(BaseRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        ///  创建的消息类型实例
        ///     后续的处理方法中会给对应属性赋值
        /// </summary>
        /// <returns></returns>
        public virtual BaseRecMsg CreateNewInstance()
        {
            return null;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///   自定义消息类型处理Handler
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    internal class WxCustomMsgDirHandler<TRecMsg> : WxMsgCustomMsgHandler
        where TRecMsg : BaseRecMsg, new()
    {
        protected internal override BaseReplyMsg Excute(BaseRecMsg msg)
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
