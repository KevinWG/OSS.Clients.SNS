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
           
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessTextMsg(WxTextRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessImageMsg(WxImageRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessVoiceMsg(WxVoiceRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessVideoMsg(WxVideoRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessLocationMsg(WxLocationRecMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessLinkMsg(WxLinkRecMsg msg)
        {
            return null;
        }


        #endregion
        
        #region 事件列表  动作事件消息

        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessSubscribeEventMsg(WxSubscribeRecEventMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessScanEventMsg(WxSubscribeRecEventMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual WxNoneReplyMsg ProcessLocationEventMsg(WxLocationRecEventMsg msg)
        {
            return null;
        }

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessClickEventMsg(WxClickRecEventMsg msg)
        {
            return null;
        }

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
            WxMsgProcessor processor = null;
            switch (msgType.ToLower())
            {
                case "event":
                    processor = GetBasicEventMsgProcessor(eventName);
                    break;
                case "text":
                    processor = new WxMsgProcessor<WxTextRecMsg>()
                        {RecInsCreater = () => new WxTextRecMsg(), ProcessFunc = ProcessTextMsg};
                    break;
                case "image":
                    processor = new WxMsgProcessor<WxImageRecMsg>()
                        {RecInsCreater = () => new WxImageRecMsg(), ProcessFunc = ProcessImageMsg};
                    break;
                case "voice":
                    processor = new WxMsgProcessor<WxVoiceRecMsg>()
                        {RecInsCreater = () => new WxVoiceRecMsg(), ProcessFunc = ProcessVoiceMsg};
                    break;
                case "video":
                    processor = new WxMsgProcessor<WxVideoRecMsg>()
                        {RecInsCreater = () => new WxVideoRecMsg(), ProcessFunc = ProcessVideoMsg};
                    break;
                case "shortvideo":
                    processor = new WxMsgProcessor<WxVideoRecMsg>()
                        {RecInsCreater = () => new WxVideoRecMsg(), ProcessFunc = ProcessVideoMsg};
                    break;
                case "location":
                    processor = new WxMsgProcessor<WxLocationRecMsg>()
                        {RecInsCreater = () => new WxLocationRecMsg(), ProcessFunc = ProcessLocationMsg};
                    break;
                case "link":
                    processor = new WxMsgProcessor<WxLinkRecMsg>()
                        {RecInsCreater = () => new WxLinkRecMsg(), ProcessFunc = ProcessLinkMsg};
                    break;
            }
            return processor;
        }

        private WxMsgProcessor GetBasicEventMsgProcessor(string eventName)
        {
            WxMsgProcessor processor = null;
            switch (eventName)
            {
                case "subscribe":
                    processor = new WxMsgProcessor<WxSubscribeRecEventMsg>()
                        { RecInsCreater = () => new WxSubscribeRecEventMsg(), ProcessFunc = ProcessSubscribeEventMsg };
                    break;
                case "unsubscribe":
                    processor = new WxMsgProcessor<WxSubscribeRecEventMsg>()
                        { RecInsCreater = () => new WxSubscribeRecEventMsg(), ProcessFunc = ProcessSubscribeEventMsg };
                      break;
                case "scan":
                    processor = new WxMsgProcessor<WxSubscribeRecEventMsg>()
                        { RecInsCreater = () => new WxSubscribeRecEventMsg(), ProcessFunc = ProcessScanEventMsg };
                    break;
                case "location":
                    processor = new WxMsgProcessor<WxLocationRecEventMsg>()
                        { RecInsCreater = () => new WxLocationRecEventMsg(), ProcessFunc = ProcessLocationEventMsg };
                    break;
                case "click":
                    processor = new WxMsgProcessor<WxClickRecEventMsg>()
                        { RecInsCreater = () => new WxClickRecEventMsg(), ProcessFunc = ProcessClickEventMsg };
                    break;
                case "view":
                    processor = new WxMsgProcessor<WxViewRecEventMsg>()
                        { RecInsCreater = () => new WxViewRecEventMsg(), ProcessFunc = ProcessViewEventMsg };
                    break;
            }
            return processor;
        }
    }
}
