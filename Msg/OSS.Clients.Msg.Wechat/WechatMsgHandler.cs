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
using OSS.Clients.Msg.Wechat.Helper;
using OSS.Common;

namespace OSS.Clients.Msg.Wechat
{
     /// <summary>
     /// 消息处理的默认类
     /// </summary>
    public class WechatMsgHandler: WechatBaseMsgHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WechatMsgHandler() 
        {
        }


        #region   基础消息的事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessTextMsg(WechatTextRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessImageMsg(WechatImageRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessVoiceMsg(WechatVoiceRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理视频消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessVideoMsg(WechatVideoRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理小视频消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessShortVideoMsg(WechatVideoRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessLocationMsg(WechatLocationRecMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessLinkMsg(WechatLinkRecMsg msg)
        {
            return InterUtil.NullResult;
        }


        #endregion

        #region 事件列表  动作事件消息

        /// <summary>
        /// 处理关注事件
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessSubscribeEventMsg(WechatSubScanRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessUnsubscribeEventMsg(WechatSubScanRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessScanEventMsg(WechatSubScanRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessLocationEventMsg(WechatLocationRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessClickEventMsg(WechatClickRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送
        /// </summary>
        protected virtual Task<WechatBaseReplyMsg> ProcessViewEventMsg(WechatViewRecEventMsg msg)
        {
            return InterUtil.NullResult;
        }

        #endregion

        #endregion
        
        internal override InternalBaseProcessor GetInternalMsgProcessor(string msgType, string eventName)
        {
            switch (msgType.ToLower())
            {
                case "event":
                    return GetBasicEventMsgProcessor(eventName);
                case "text":
                    return SingleInstance<InternalWechatChatProcessor<WechatTextRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatTextRecMsg> {ProcessFunc = ProcessTextMsg});
                case "image":
                    return SingleInstance<InternalWechatChatProcessor<WechatImageRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatImageRecMsg> { ProcessFunc = ProcessImageMsg });
                case "voice":
                    return SingleInstance<InternalWechatChatProcessor<WechatVoiceRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatVoiceRecMsg> { ProcessFunc = ProcessVoiceMsg });
                case "video":
                    return SingleInstance<InternalWechatChatProcessor<WechatVideoRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatVideoRecMsg> { ProcessFunc = ProcessVideoMsg });
                case "shortvideo":
                    return SingleInstance<InternalWechatChatProcessor<WechatVideoRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatVideoRecMsg> { ProcessFunc = ProcessShortVideoMsg });
                case "location":
                    return SingleInstance<InternalWechatChatProcessor<WechatLocationRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatLocationRecMsg> { ProcessFunc = ProcessLocationMsg });
                case "link":
                    return SingleInstance<InternalWechatChatProcessor<WechatLinkRecMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatLinkRecMsg> { ProcessFunc = ProcessLinkMsg });
            }
            return null;
        }

        private InternalBaseProcessor GetBasicEventMsgProcessor(string eventName)
        {
            switch (eventName.ToLower())
            {
                case "subscribe":
                    return SingleInstance<InternalWechatChatProcessor<WechatSubScanRecEventMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatSubScanRecEventMsg> { ProcessFunc = ProcessSubscribeEventMsg });
                case "unsubscribe":
                    return SingleInstance<InternalWechatChatProcessor<WechatSubScanRecEventMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatSubScanRecEventMsg> { ProcessFunc = ProcessUnsubscribeEventMsg });
                case "scan":
                    return SingleInstance<InternalWechatChatProcessor<WechatSubScanRecEventMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatSubScanRecEventMsg> { ProcessFunc = ProcessScanEventMsg });
                case "location":
                    return SingleInstance<InternalWechatChatProcessor<WechatLocationRecEventMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatLocationRecEventMsg> { ProcessFunc = ProcessLocationEventMsg });
                case "click":
                    return SingleInstance<InternalWechatChatProcessor<WechatClickRecEventMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatClickRecEventMsg> { ProcessFunc = ProcessClickEventMsg });
                case "view":
                    return SingleInstance<InternalWechatChatProcessor<WechatViewRecEventMsg>>.GetInstance(() =>
                        new InternalWechatChatProcessor<WechatViewRecEventMsg> { ProcessFunc = ProcessViewEventMsg });
            }
            return null;
        }
    }
}
