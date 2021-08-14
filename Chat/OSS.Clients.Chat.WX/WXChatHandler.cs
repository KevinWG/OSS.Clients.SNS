#region Copyright (C) 2017  Kevin  （OSS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄基类，主要声明相关事件
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-13
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Clients.Chat.WX.Helper;
using OSS.Clients.Chat.WX.Mos;
using OSS.Common;

namespace OSS.Clients.Chat.WX
{
     /// <summary>
     /// 消息处理的默认类
     /// </summary>
    public class WXChatHandler: WXChatBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WXChatHandler() 
        {
        }

        /// <summary>
        ///   对话消息处理基类
        /// </summary>
        /// <param name="config"></param>
        protected WXChatHandler(WXChatConfig config):base(config)
        {
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessTextMsg(WXTextRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessImageMsg(WXImageRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessVoiceMsg(WXVoiceRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理视频消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessVideoMsg(WXVideoRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理小视频消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessShortVideoMsg(WXVideoRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessLocationMsg(WXLocationRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessLinkMsg(WXLinkRecMsg msg)
        {
            return InterUtil.NullResult;
        }


        #endregion

        #region 事件列表  动作事件消息

        /// <summary>
        /// 处理关注事件
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessSubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessUnsubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessScanEventMsg(WXSubScanRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessLocationEventMsg(WXLocationRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessClickEventMsg(WXClickRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessViewEventMsg(WXViewRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        #endregion

        #endregion
        
        internal override BaseBaseProcessor GetInternalMsgProcessor(string msgType, string eventName)
        {
            switch (msgType.ToLower())
            {
                case "event":
                    return GetBasicEventMsgProcessor(eventName);
                case "text":
                    return SingleInstance<InternalWXChatProcessor<WXTextRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXTextRecMsg> {ProcessFunc = ProcessTextMsg});
                case "image":
                    return SingleInstance<InternalWXChatProcessor<WXImageRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXImageRecMsg> { ProcessFunc = ProcessImageMsg });
                case "voice":
                    return SingleInstance<InternalWXChatProcessor<WXVoiceRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXVoiceRecMsg> { ProcessFunc = ProcessVoiceMsg });
                case "video":
                    return SingleInstance<InternalWXChatProcessor<WXVideoRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXVideoRecMsg> { ProcessFunc = ProcessVideoMsg });
                case "shortvideo":
                    return SingleInstance<InternalWXChatProcessor<WXVideoRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXVideoRecMsg> { ProcessFunc = ProcessShortVideoMsg });
                case "location":
                    return SingleInstance<InternalWXChatProcessor<WXLocationRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXLocationRecMsg> { ProcessFunc = ProcessLocationMsg });
                case "link":
                    return SingleInstance<InternalWXChatProcessor<WXLinkRecMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXLinkRecMsg> { ProcessFunc = ProcessLinkMsg });
            }
            return null;
        }

        private BaseBaseProcessor GetBasicEventMsgProcessor(string eventName)
        {
            switch (eventName.ToLower())
            {
                case "subscribe":
                    return SingleInstance<InternalWXChatProcessor<WXSubScanRecEventMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXSubScanRecEventMsg> { ProcessFunc = ProcessSubscribeEventMsg });
                case "unsubscribe":
                    return SingleInstance<InternalWXChatProcessor<WXSubScanRecEventMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXSubScanRecEventMsg> { ProcessFunc = ProcessUnsubscribeEventMsg });
                case "scan":
                    return SingleInstance<InternalWXChatProcessor<WXSubScanRecEventMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXSubScanRecEventMsg> { ProcessFunc = ProcessScanEventMsg });
                case "location":
                    return SingleInstance<InternalWXChatProcessor<WXLocationRecEventMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXLocationRecEventMsg> { ProcessFunc = ProcessLocationEventMsg });
                case "click":
                    return SingleInstance<InternalWXChatProcessor<WXClickRecEventMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXClickRecEventMsg> { ProcessFunc = ProcessClickEventMsg });
                case "view":
                    return SingleInstance<InternalWXChatProcessor<WXViewRecEventMsg>>.GetInstance(() =>
                        new InternalWXChatProcessor<WXViewRecEventMsg> { ProcessFunc = ProcessViewEventMsg });
            }
            return null;
        }
    }
}
