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
    /// 【用户自定义请返回：WxMsgCustomMsgHandler&lt;TRecMsg&gt;或其子类】
    /// </summary>
    public class WxMsgProcessor
    {

        internal bool CanExecute { get; set; }

        /// <summary>
        ///  执行方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected internal virtual WxBaseReplyMsg Execute(WxBaseRecMsg msg)
        {
            return null;
        }

        /// <summary>
        ///  创建的消息类型实例
        /// </summary>
        /// <returns></returns>
        protected internal virtual WxBaseRecMsg CreateNewInstance()
        {
            return null;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///   内部自定义消息类型处理Processor
    /// </summary>
    /// <typeparam name="TRecMsg"></typeparam>
    public class WxMsgProcessor<TRecMsg> : WxMsgProcessor
        where TRecMsg : WxBaseRecMsg, new()
    {
        private Func<TRecMsg, WxBaseReplyMsg> _processFunc;

        /// <summary>
        /// 处理方法实现
        /// </summary>
        public Func<TRecMsg, WxBaseReplyMsg> ProcessFunc
        {
            get => _processFunc;
            set
            {
                CanExecute = true;
                _processFunc = value;
            }
        }

        protected internal override WxBaseReplyMsg Execute(WxBaseRecMsg msg)
        {
            return ProcessFunc?.Invoke(msg as TRecMsg);
        }

        /// <summary>
        ///  对应的接受消息创建实例方法
        ///    如果不设置，会通过反射创建
        /// </summary>
        public Func<TRecMsg> RecInsCreater { get; set; }
        
        protected internal override WxBaseRecMsg CreateNewInstance()
        {
            return RecInsCreater?.Invoke() ?? new TRecMsg();
        }
    }

}
