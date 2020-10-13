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
using OSS.Clients.Chat.WX.Mos;
using OSS.Common.BasicImpls;

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
        public WXChatHandler() : this(null)
        {
        }

        /// <inheritdoc />
        public WXChatHandler(IMetaProvider<WXChatConfig> configProvider = null) : base(configProvider)
        {
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        private InternalWXChatProcessor<WXTextRecMsg> textPro;
        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessTextMsg(WXTextRecMsg msg)
        {
            return null;
        }


        private InternalWXChatProcessor<WXImageRecMsg> imagePro;
        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessImageMsg(WXImageRecMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXVoiceRecMsg> voicePro;
        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessVoiceMsg(WXVoiceRecMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXVideoRecMsg> videoPro;
        /// <summary>
        /// 处理视频消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessVideoMsg(WXVideoRecMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXVideoRecMsg> shortVideoPro;
        /// <summary>
        /// 处理小视频消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessShortVideoMsg(WXVideoRecMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXLocationRecMsg> locationPro;
        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessLocationMsg(WXLocationRecMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXLinkRecMsg> linkPro;
        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessLinkMsg(WXLinkRecMsg msg)
        {
            return null;
        }


        #endregion

        #region 事件列表  动作事件消息

        private InternalWXChatProcessor<WXSubScanRecEventMsg> subEventPro;
        /// <summary>
        /// 处理关注事件
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessSubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }
        
        private InternalWXChatProcessor<WXSubScanRecEventMsg> unsubEventPro;
        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessUnsubscribeEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXSubScanRecEventMsg> scanEventPro;
        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessScanEventMsg(WXSubScanRecEventMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXLocationRecEventMsg> locationEventPro;
        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessLocationEventMsg(WXLocationRecEventMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXClickRecEventMsg> clickEventPro;
        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessClickEventMsg(WXClickRecEventMsg msg)
        {
            return null;
        }

        private InternalWXChatProcessor<WXViewRecEventMsg> viewEventPro;
        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送
        /// </summary>
        protected virtual Task<WXBaseReplyMsg> ProcessViewEventMsg(WXViewRecEventMsg msg)
        {
            return null;
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
                    return textPro?? (textPro = new InternalWXChatProcessor<WXTextRecMsg> { ProcessFunc = ProcessTextMsg });
                case "image":
                    return imagePro??(imagePro = new InternalWXChatProcessor<WXImageRecMsg> {  ProcessFunc = ProcessImageMsg });
                case "voice":
                    return voicePro??(voicePro = new InternalWXChatProcessor<WXVoiceRecMsg> {  ProcessFunc = ProcessVoiceMsg });
                case "video":
                    return videoPro??(videoPro = new InternalWXChatProcessor<WXVideoRecMsg> { ProcessFunc = ProcessVideoMsg });
                case "shortvideo":
                    return shortVideoPro?? (shortVideoPro = new InternalWXChatProcessor<WXVideoRecMsg> {ProcessFunc = ProcessShortVideoMsg });
                case "location":
                    return locationPro??(locationPro = new InternalWXChatProcessor<WXLocationRecMsg> {  ProcessFunc = ProcessLocationMsg });
                case "link":
                    return linkPro??(linkPro = new InternalWXChatProcessor<WXLinkRecMsg> {ProcessFunc = ProcessLinkMsg });
            }
            return null;
        }

        private BaseBaseProcessor GetBasicEventMsgProcessor(string eventName)
        {
            switch (eventName.ToLower())
            {
                case "subscribe":
                    return subEventPro??(subEventPro = new InternalWXChatProcessor<WXSubScanRecEventMsg> {  ProcessFunc = ProcessSubscribeEventMsg });
                case "unsubscribe":
                    return unsubEventPro??(unsubEventPro = new InternalWXChatProcessor<WXSubScanRecEventMsg> {  ProcessFunc = ProcessUnsubscribeEventMsg });
                case "scan":
                    return scanEventPro??(scanEventPro = new InternalWXChatProcessor<WXSubScanRecEventMsg> {  ProcessFunc = ProcessScanEventMsg });
                case "location":
                    return locationEventPro??(locationEventPro = new InternalWXChatProcessor<WXLocationRecEventMsg> {  ProcessFunc = ProcessLocationEventMsg });
                case "click":
                    return clickEventPro??(clickEventPro = new InternalWXChatProcessor<WXClickRecEventMsg> {  ProcessFunc = ProcessClickEventMsg });
                case "view":
                    return viewEventPro??(viewEventPro = new InternalWXChatProcessor<WXViewRecEventMsg> {  ProcessFunc = ProcessViewEventMsg });
            }
            return null;
        }
    }
}
