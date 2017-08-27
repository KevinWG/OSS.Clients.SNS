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
    ///  自定义消息处理Handler基类
    /// </summary>
    internal class WxMsgCustomMsgBaseHandler
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

    /// <inheritdoc />
    /// <summary>
    ///   自定义消息类型处理Handler
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    internal class WxCustomMsgHandler<TRecMsg> : WxMsgCustomMsgBaseHandler
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
