using System;
using System.Collections.Generic;
using System.Text;
using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Common.ComUtils;
using OS.Common.Encrypt;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.WX.Msg
{
    public class WxMsgHandler
    {
        private readonly WxMsgServerConfig _config;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WxMsgHandler(WxMsgServerConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("config", "请配置服务端令牌等配置信息");
            _config = config;
        }

        /// <summary>
        /// 验证服务器地址的有效性
        /// </summary>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns></returns>
        public ResultModel CheckSign(string signature, string timestamp, string nonce)
        {
            string token = _config.Token;

            List<string> strList = new List<string>() { token, timestamp, nonce };
            strList.Sort();

            string waitEncropyStr = string.Join(string.Empty, strList);
            if (signature == Sha1.Encrypt(waitEncropyStr, Encoding.ASCII))
            {
                return new ResultModel();
            }
            return new ResultModel(ResultTypes.NoRight,"加密验证失败！");
        }

        #region 普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected event Func<TextMsg, BaseReplyContext> TextHandler;

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected event Func<ImageMsg, BaseReplyContext> ImageHandler;

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected event Func<VoiceMsg, BaseReplyContext> VoiceHandler;

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected event Func<VideoMsg, BaseReplyContext> VideoHandler;

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected event Func<LocationMsg, BaseReplyContext> LocationHandler;

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected event Func<LinkMsg, BaseReplyContext> LinkHandler;

        #endregion

        #region 事件推送 
        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected event Func<SubscribeEvent, BaseReplyContext> SubscribeEventHandler;

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected event Func<SubscribeEvent, BaseReplyContext> ScanEventHandler;

        /// <summary>
        /// 处理上报地理位置事件
        /// </summary>
        protected event Func<LocationEvent, BaseReplyContext> LocationEventHandler;

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected event Func<ClickEvent, BaseReplyContext> ClickEventHandler;

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected event Func<ViewEvent, BaseReplyContext> ViewEventHandler;

        /// <summary>
        /// 客服事件推送 
        /// </summary>
        protected event Func<KFEvent, BaseReplyContext> KefuEventHandler;

        #endregion

        /// <summary>
        /// 消息处理结束事件
        /// </summary>
        protected event Action<MsgContext> ProcessingEndHandler;

        /// <summary>
        /// 执行事件对应委托方法，如果对应的方法存在则执行
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static BaseReplyContext ExecuteHandler<TRes>(TRes res, Func<TRes, BaseReplyContext> func)
        {
            return func != null ? func(res) : new NoReplyMsg();
        }

        public MsgContext Processing(string xml)
        {
            var context = new MsgContext();
            if (!string.IsNullOrEmpty(xml))
            {
                context.ContextXml = xml;
                var dirs = WxMsgHelper.ChangXmlToDir(xml);
                if (dirs != null)
                {
                    var msgType = WxMsgHelper.GetMsgType(dirs);
                    switch (msgType)
                    {
                        case MsgType.Event:
                            var eventType = WxMsgHelper.GetEventType(dirs);
                            ProcessingEvent(eventType, dirs, context);
                            break;
                        case MsgType.Text:
                            var textRecMsg = WxMsgHelper.GetMsg<TextMsg>(dirs, MsgType.Text);
                            context.ReplyContext = ExecuteHandler(textRecMsg, TextHandler);
                            context.NormalContext = textRecMsg;
                            break;
                        case MsgType.Video:
                        case MsgType.Shortvideo:
                            var vedioRecMsg = WxMsgHelper.GetMsg<VideoMsg>(dirs, msgType);
                            context.ReplyContext = ExecuteHandler(vedioRecMsg, VideoHandler);
                            context.NormalContext = vedioRecMsg;
                            break;
                        case MsgType.Image:
                            var imageRecMsg = WxMsgHelper.GetMsg<ImageMsg>(dirs, MsgType.Image);
                            context.ReplyContext = ExecuteHandler(imageRecMsg, ImageHandler);
                            context.NormalContext = imageRecMsg;
                            break;
                        case MsgType.Link:
                            var linkRecMsg = WxMsgHelper.GetMsg<LinkMsg>(dirs, MsgType.Link);
                            context.ReplyContext = ExecuteHandler(linkRecMsg, LinkHandler);
                            context.NormalContext = linkRecMsg;
                            break;
                        case MsgType.Voice:
                            var voiceRecMsg = WxMsgHelper.GetMsg<VoiceMsg>(dirs, MsgType.Voice);
                            context.ReplyContext = ExecuteHandler(voiceRecMsg, VoiceHandler);
                            context.NormalContext = voiceRecMsg;
                            break;
                        case MsgType.Location:
                            var locationRecMsg = WxMsgHelper.GetMsg<LocationMsg>(dirs, MsgType.Location);
                            context.ReplyContext = ExecuteHandler(locationRecMsg, LocationHandler);
                            context.NormalContext = locationRecMsg;
                            break;
                    }
                }
            }
            if (context.ReplyContext == null)
                context.ReplyContext = new NoReplyMsg();

            ProcessingEndHandler?.Invoke(context);
            return context;
        }

        private void ProcessingEvent(EventType msgEventType, Dictionary<string, string> dirValues, MsgContext context)
        {
            switch (msgEventType)
            {
                case EventType.Subscribe:
                case EventType.UnSubscribe:
                    var subRecMsg = WxMsgHelper.GetEventMsg<SubscribeEvent>(dirValues, msgEventType);
                    context.ReplyContext = ExecuteHandler(subRecMsg, SubscribeEventHandler);
                    context.NormalContext = subRecMsg;
                    break;
                case EventType.Click:
                    var clickRecMsg = WxMsgHelper.GetEventMsg<ClickEvent>(dirValues, EventType.Click);
                    context.ReplyContext = ExecuteHandler(clickRecMsg, ClickEventHandler);
                    context.NormalContext = clickRecMsg;
                    break;
                case EventType.Location:
                    var locationRecMsg = WxMsgHelper.GetEventMsg<LocationEvent>(dirValues, EventType.Location);
                    context.ReplyContext = ExecuteHandler(locationRecMsg, LocationEventHandler);
                    context.NormalContext = locationRecMsg;
                    break;
                case EventType.Scan:
                    var scanRecMsg = WxMsgHelper.GetEventMsg<SubscribeEvent>(dirValues, EventType.Scan);
                    context.ReplyContext = ExecuteHandler(scanRecMsg, ScanEventHandler);
                    context.NormalContext = scanRecMsg;
                    break;
                case EventType.View:
                    var viewRrecMsg = WxMsgHelper.GetEventMsg<ViewEvent>(dirValues, EventType.View);
                    context.ReplyContext = ExecuteHandler(viewRrecMsg, ViewEventHandler);
                    context.NormalContext = viewRrecMsg;
                    break;
                case EventType.Kefu:
                    var kfRrecMsg = WxMsgHelper.GetEventMsg<KFEvent>(dirValues, EventType.Kefu);
                    context.ReplyContext = ExecuteHandler(kfRrecMsg, KefuEventHandler);
                    context.NormalContext = kfRrecMsg;
                    break;
            }
        }

   
    }
}
