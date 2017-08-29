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

using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <summary>
    ///  自定义消息处理Handler基类
    /// 【用户自定义请继承：WxMsgCustomMsgHandler&lt;TRecMsg&gt;】
    /// </summary>
    public class WxMsgProcessor
    {
        /// <summary>
        ///  执行方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected internal virtual BaseReplyMsg Execute(BaseRecMsg msg)
        {
            return new NoneReplyMsg();
        }

        /// <summary>
        ///  创建的消息类型实例
        /// </summary>
        /// <returns></returns>
        public virtual BaseRecMsg CreateNewInstance()
        {
            return null;
        }
    }


    /// <inheritdoc />
    /// <summary>
    ///   用户自定义实现的消息处理Handler基类
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    public abstract class WxMsgProcessor<TRecMsg> : WxMsgProcessor
        where TRecMsg : BaseRecMsg,new ()
    {
        public abstract BaseReplyMsg Execute(TRecMsg msg);

        /// <inheritdoc />
        public override BaseRecMsg CreateNewInstance()
        {
            return new TRecMsg();
        }

        protected internal override BaseReplyMsg Execute(BaseRecMsg msg)
        {
           return Execute(msg as TRecMsg);
        }
    }


 
}
