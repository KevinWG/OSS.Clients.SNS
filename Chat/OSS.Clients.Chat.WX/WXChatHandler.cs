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

        private  WXChatProcessor<WXTextRecMsg> textPro;
        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessTextMsg(WXTextRecMsg msg)
        {
            return null;
        }


        private  WXChatProcessor<WXImageRecMsg> imagePro;
        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessImageMsg(WXImageRecMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXVoiceRecMsg> voicePro;
        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessVoiceMsg(WXVoiceRecMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXVideoRecMsg> videoPro;
        /// <summary>
        /// 处理视频消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessVideoMsg(WXVideoRecMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXVideoRecMsg> shortVideoPro;
        /// <summary>
        /// 处理小视频消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessShortVideoMsg(WXVideoRecMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXLocationRecMsg> locationPro;
        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessLocationMsg(WXLocationRecMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXLinkRecMsg> linkPro;
        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessLinkMsg(WXLinkRecMsg msg)
        {
            return null;
        }


        #endregion

        #region 事件列表  动作事件消息

        private  WXChatProcessor<WXSubScanRecEventMsg> subEventPro;
        /// <summary>
        /// 处理关注事件
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessSubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }
        
        private  WXChatProcessor<WXSubScanRecEventMsg> unsubEventPro;
        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        protected virtual WXNoneReplyMsg ProcessUnsubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXSubScanRecEventMsg> scanEventPro;
        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessScanEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXLocationRecEventMsg> locationEventPro;
        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual WXNoneReplyMsg ProcessLocationEventMsg(WXLocationRecEventMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXClickRecEventMsg> clickEventPro;
        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessClickEventMsg(WXClickRecEventMsg msg)
        {
            return null;
        }

        private  WXChatProcessor<WXViewRecEventMsg> viewEventPro;
        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected virtual WXBaseReplyMsg ProcessViewEventMsg(WXViewRecEventMsg msg)
        {
            return null;
        }

        #endregion

        #endregion
        
        internal override WXChatProcessor GetBasicMsgProcessor(string msgType, string eventName)
        {
            switch (msgType.ToLower())
            {
                case "event":
                    return GetBasicEventMsgProcessor(eventName);
                case "text":
                    return textPro?? (textPro = new WXChatProcessor<WXTextRecMsg> { RecInsCreater = () => new WXTextRecMsg(), ProcessFunc = ProcessTextMsg });
                case "image":
                    return imagePro??(imagePro = new WXChatProcessor<WXImageRecMsg> { RecInsCreater = () => new WXImageRecMsg(), ProcessFunc = ProcessImageMsg });
                case "voice":
                    return voicePro??(voicePro = new WXChatProcessor<WXVoiceRecMsg> { RecInsCreater = () => new WXVoiceRecMsg(), ProcessFunc = ProcessVoiceMsg });
                case "video":
                    return videoPro??(videoPro = new WXChatProcessor<WXVideoRecMsg> { RecInsCreater = () => new WXVideoRecMsg(), ProcessFunc = ProcessVideoMsg });
                case "shortvideo":
                    return shortVideoPro?? (shortVideoPro = new WXChatProcessor<WXVideoRecMsg> { RecInsCreater = () => new WXVideoRecMsg(), ProcessFunc = ProcessShortVideoMsg });
                case "location":
                    return locationPro??(locationPro = new WXChatProcessor<WXLocationRecMsg> { RecInsCreater = () => new WXLocationRecMsg(), ProcessFunc = ProcessLocationMsg });
                case "link":
                    return linkPro??(linkPro = new WXChatProcessor<WXLinkRecMsg> { RecInsCreater = () => new WXLinkRecMsg(), ProcessFunc = ProcessLinkMsg });
            }
            return null;
        }

        private WXChatProcessor GetBasicEventMsgProcessor(string eventName)
        {
            switch (eventName.ToLower())
            {
                case "subscribe":
                    return subEventPro??(subEventPro = new WXChatProcessor<WXSubScanRecEventMsg> { RecInsCreater = () => new WXSubScanRecEventMsg(), ProcessFunc = ProcessSubscribeEventMsg });
                case "unsubscribe":
                    return unsubEventPro??(unsubEventPro = new WXChatProcessor<WXSubScanRecEventMsg> { RecInsCreater = () => new WXSubScanRecEventMsg(), ProcessFunc = ProcessUnsubscribeEventMsg });
                case "scan":
                    return scanEventPro??(scanEventPro = new WXChatProcessor<WXSubScanRecEventMsg> { RecInsCreater = () => new WXSubScanRecEventMsg(), ProcessFunc = ProcessScanEventMsg });
                case "location":
                    return locationEventPro??(locationEventPro = new WXChatProcessor<WXLocationRecEventMsg> { RecInsCreater = () => new WXLocationRecEventMsg(), ProcessFunc = ProcessLocationEventMsg });
                case "click":
                    return clickEventPro??(clickEventPro = new WXChatProcessor<WXClickRecEventMsg> { RecInsCreater = () => new WXClickRecEventMsg(), ProcessFunc = ProcessClickEventMsg });
                case "view":
                    return viewEventPro??(viewEventPro = new WXChatProcessor<WXViewRecEventMsg> { RecInsCreater = () => new WXViewRecEventMsg(), ProcessFunc = ProcessViewEventMsg });
            }
            return null;
        }
    }
}
