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

using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <inheritdoc />
    /// <summary>
    /// 消息处理类
    ///  </summary>
    public class WxMsgHandler: WxMsgBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mConfig"></param>
        public WxMsgHandler(WxMsgConfig mConfig=null):base(mConfig)
        {
            textPro = new WxMsgProcessor<WxTextRecMsg> {RecInsCreater = () => new WxTextRecMsg(), ProcessFunc = ProcessTextMsg};
            imagePro= new WxMsgProcessor<WxImageRecMsg> { RecInsCreater = () => new WxImageRecMsg(), ProcessFunc = ProcessImageMsg };
            voicePro = new WxMsgProcessor<WxVoiceRecMsg> { RecInsCreater = () => new WxVoiceRecMsg(), ProcessFunc = ProcessVoiceMsg };
            videoPro = new WxMsgProcessor<WxVideoRecMsg> { RecInsCreater = () => new WxVideoRecMsg(), ProcessFunc = ProcessVideoMsg };
            shortVideoPro = new WxMsgProcessor<WxVideoRecMsg> { RecInsCreater = () => new WxVideoRecMsg(), ProcessFunc = ProcessShortVideoMsg };

            locationPro = new WxMsgProcessor<WxLocationRecMsg> { RecInsCreater = () => new WxLocationRecMsg(), ProcessFunc = ProcessLocationMsg };
            linkPro = new WxMsgProcessor<WxLinkRecMsg> { RecInsCreater = () => new WxLinkRecMsg(), ProcessFunc = ProcessLinkMsg };

            subEventPro = new WxMsgProcessor<WxSubScanRecEventMsg> { RecInsCreater = () => new WxSubScanRecEventMsg(), ProcessFunc = ProcessSubscribeEventMsg };
            unsubEventPro = new WxMsgProcessor<WxSubScanRecEventMsg> { RecInsCreater = () => new WxSubScanRecEventMsg(), ProcessFunc = ProcessUnsubscribeEventMsg };
            scanEventPro = new WxMsgProcessor<WxSubScanRecEventMsg> { RecInsCreater = () => new WxSubScanRecEventMsg(), ProcessFunc = ProcessScanEventMsg };
            locationEventPro = new WxMsgProcessor<WxLocationRecEventMsg> { RecInsCreater = () => new WxLocationRecEventMsg(), ProcessFunc = ProcessLocationEventMsg };

            clickEventPro = new WxMsgProcessor<WxClickRecEventMsg> { RecInsCreater = () => new WxClickRecEventMsg(), ProcessFunc = ProcessClickEventMsg };
            viewEventPro = new WxMsgProcessor<WxViewRecEventMsg> { RecInsCreater = () => new WxViewRecEventMsg(), ProcessFunc = ProcessViewEventMsg };
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        private static WxMsgProcessor<WxTextRecMsg> textPro;
        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessTextMsg(WxTextRecMsg msg)
        {
            return null;
        }


        private static WxMsgProcessor<WxImageRecMsg> imagePro;
        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessImageMsg(WxImageRecMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxVoiceRecMsg> voicePro;
        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessVoiceMsg(WxVoiceRecMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxVideoRecMsg> videoPro;
        /// <summary>
        /// 处理视频消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessVideoMsg(WxVideoRecMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxVideoRecMsg> shortVideoPro;
        /// <summary>
        /// 处理小视频消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessShortVideoMsg(WxVideoRecMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxLocationRecMsg> locationPro;
        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessLocationMsg(WxLocationRecMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxLinkRecMsg> linkPro;
        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessLinkMsg(WxLinkRecMsg msg)
        {
            return null;
        }


        #endregion

        #region 事件列表  动作事件消息

        private static WxMsgProcessor<WxSubScanRecEventMsg> subEventPro;
        /// <summary>
        /// 处理关注事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessSubscribeEventMsg(WxSubScanRecEventMsg msg)
        {
            return null;
        }
        
        private static WxMsgProcessor<WxSubScanRecEventMsg> unsubEventPro;
        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessUnsubscribeEventMsg(WxSubScanRecEventMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxSubScanRecEventMsg> scanEventPro;
        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessScanEventMsg(WxSubScanRecEventMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxLocationRecEventMsg> locationEventPro;
        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual WxNoneReplyMsg ProcessLocationEventMsg(WxLocationRecEventMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxClickRecEventMsg> clickEventPro;
        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessClickEventMsg(WxClickRecEventMsg msg)
        {
            return null;
        }

        private static WxMsgProcessor<WxViewRecEventMsg> viewEventPro;
        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessViewEventMsg(WxViewRecEventMsg msg)
        {
            return null;
        }

        #endregion

        #endregion
        
        internal override WxMsgProcessor GetBasicMsgProcessor(string msgType, string eventName)
        {
            switch (msgType.ToLower())
            {
                case "event":
                    return GetBasicEventMsgProcessor(eventName);
                case "text":
                    return textPro;
                case "image":
                    return imagePro;
                case "voice":
                    return voicePro;
                case "video":
                    return videoPro;
                case "shortvideo":
                    return shortVideoPro;
                case "location":
                    return locationPro;
                case "link":
                    return linkPro;
            }
            return null;
        }

        private WxMsgProcessor GetBasicEventMsgProcessor(string eventName)
        {
            switch (eventName)
            {
                case "subscribe":
                    return subEventPro;
                case "unsubscribe":
                    return unsubEventPro;
                case "scan":
                    return scanEventPro;
                case "location":
                    return locationEventPro;
                case "click":
                    return clickEventPro;
                case "view":
                    return viewEventPro;
            }
            return null;
        }
    }
}
