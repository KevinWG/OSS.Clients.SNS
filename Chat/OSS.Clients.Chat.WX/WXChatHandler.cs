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

using OSS.Clients.Chat.WX.Mos;

namespace OSS.Clients.Chat.WX
{
    /// <inheritdoc />
    /// <summary>
    /// 消息处理类
    ///  </summary>
    public class WXChatHandler: WXChatBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WXChatHandler() : this(null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mConfig"></param>
        public WXChatHandler(WXChatConfig mConfig):base(mConfig)
        {
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        private WXChatInternalProcessor<WXTextRecMsg> textPro;
        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessTextMsg(WXTextRecMsg msg)
        {
            return null;
        }


        private WXChatInternalProcessor<WXImageRecMsg> imagePro;
        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessImageMsg(WXImageRecMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXVoiceRecMsg> voicePro;
        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessVoiceMsg(WXVoiceRecMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXVideoRecMsg> videoPro;
        /// <summary>
        /// 处理视频消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessVideoMsg(WXVideoRecMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXVideoRecMsg> shortVideoPro;
        /// <summary>
        /// 处理小视频消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessShortVideoMsg(WXVideoRecMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXLocationRecMsg> locationPro;
        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessLocationMsg(WXLocationRecMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXLinkRecMsg> linkPro;
        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessLinkMsg(WXLinkRecMsg msg)
        {
            return null;
        }


        #endregion

        #region 事件列表  动作事件消息

        private WXChatInternalProcessor<WXSubScanRecEventMsg> subEventPro;
        /// <summary>
        /// 处理关注事件
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessSubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }
        
        private WXChatInternalProcessor<WXSubScanRecEventMsg> unsubEventPro;
        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        protected virtual WXNoneReplyMsg ProcessUnsubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXSubScanRecEventMsg> scanEventPro;
        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessScanEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXLocationRecEventMsg> locationEventPro;
        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual WXNoneReplyMsg ProcessLocationEventMsg(WXLocationRecEventMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXClickRecEventMsg> clickEventPro;
        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessClickEventMsg(WXClickRecEventMsg msg)
        {
            return null;
        }

        private WXChatInternalProcessor<WXViewRecEventMsg> viewEventPro;
        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessViewEventMsg(WXViewRecEventMsg msg)
        {
            return null;
        }

        #endregion

        #endregion
        
        internal override BaseWXChatProcessor GetInternalMsgProcessor(string msgType, string eventName)
        {
            switch (msgType.ToLower())
            {
                case "event":
                    return GetBasicEventMsgProcessor(eventName);
                case "text":
                    return textPro?? (textPro = new WXChatInternalProcessor<WXTextRecMsg> { ProcessFunc = ProcessTextMsg });
                case "image":
                    return imagePro??(imagePro = new WXChatInternalProcessor<WXImageRecMsg> {  ProcessFunc = ProcessImageMsg });
                case "voice":
                    return voicePro??(voicePro = new WXChatInternalProcessor<WXVoiceRecMsg> {  ProcessFunc = ProcessVoiceMsg });
                case "video":
                    return videoPro??(videoPro = new WXChatInternalProcessor<WXVideoRecMsg> { ProcessFunc = ProcessVideoMsg });
                case "shortvideo":
                    return shortVideoPro?? (shortVideoPro = new WXChatInternalProcessor<WXVideoRecMsg> {ProcessFunc = ProcessShortVideoMsg });
                case "location":
                    return locationPro??(locationPro = new WXChatInternalProcessor<WXLocationRecMsg> {  ProcessFunc = ProcessLocationMsg });
                case "link":
                    return linkPro??(linkPro = new WXChatInternalProcessor<WXLinkRecMsg> {ProcessFunc = ProcessLinkMsg });
            }
            return null;
        }

        private BaseWXChatProcessor GetBasicEventMsgProcessor(string eventName)
        {
            switch (eventName.ToLower())
            {
                case "subscribe":
                    return subEventPro??(subEventPro = new WXChatInternalProcessor<WXSubScanRecEventMsg> {  ProcessFunc = ProcessSubscribeEventMsg });
                case "unsubscribe":
                    return unsubEventPro??(unsubEventPro = new WXChatInternalProcessor<WXSubScanRecEventMsg> {  ProcessFunc = ProcessUnsubscribeEventMsg });
                case "scan":
                    return scanEventPro??(scanEventPro = new WXChatInternalProcessor<WXSubScanRecEventMsg> {  ProcessFunc = ProcessScanEventMsg });
                case "location":
                    return locationEventPro??(locationEventPro = new WXChatInternalProcessor<WXLocationRecEventMsg> {  ProcessFunc = ProcessLocationEventMsg });
                case "click":
                    return clickEventPro??(clickEventPro = new WXChatInternalProcessor<WXClickRecEventMsg> {  ProcessFunc = ProcessClickEventMsg });
                case "view":
                    return viewEventPro??(viewEventPro = new WXChatInternalProcessor<WXViewRecEventMsg> {  ProcessFunc = ProcessViewEventMsg });
            }
            return null;
        }
    }
}
