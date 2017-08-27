#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息模块基类  -   主要处理配置信息相关
*　　	
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-8-27
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Xml;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Common.Plugs;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SocialSDK.WX.Msg.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    public abstract class WxBaseMsg:BaseConfigProvider<WxMsgServerConfig>
    {
        protected WxBaseMsg(WxMsgServerConfig config=null):base(config)
        {
            ModuleName = ModuleNames.SocialCenter;
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected event Func<TextRecMsg, BaseReplyMsg> TextHandler;

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected event Func<ImageRecMsg, BaseReplyMsg> ImageHandler;

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected event Func<VoiceRecMsg, BaseReplyMsg> VoiceHandler;

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected event Func<VideoRecMsg, BaseReplyMsg> VideoHandler;

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected event Func<LocationRecMsg, BaseReplyMsg> LocationHandler;

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected event Func<LinkRecMsg, BaseReplyMsg> LinkHandler;

        #endregion


        #region 事件列表  动作事件消息


        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected event Func<SubscribeRecEventMsg, BaseReplyMsg> SubscribeEventHandler;

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected event Func<SubscribeRecEventMsg, BaseReplyMsg> ScanEventHandler;

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected event Func<LocationRecEventMsg, NoneReplyMsg> LocationEventHandler;

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected event Func<ClickRecEventMsg, BaseReplyMsg> ClickEventHandler;

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected event Func<ViewRecEventMsg, BaseReplyMsg> ViewEventHandler;

        #endregion

        #endregion
        
        /// <summary>
        ///  执行基础消息类型
        /// </summary>
        /// <param name="rMsg"></param>
        /// <param name="msgType"></param>
        /// <param name="rDirs"></param>
        /// <returns>返回基础消息处理结果</returns>
        protected MsgContext ProcessExecute_BasicMsg(XmlDocument rMsg, string msgType,
            Dictionary<string, string> rDirs)
        {
            MsgContext context = null;
            switch (msgType.ToLower())
            {
                case "event":
                    context = ProcessExecute_BasicEventMsg(rMsg, rDirs);
                    break;
                case "text":
                    context = ExecuteHandler(rMsg, rDirs, new TextRecMsg(), TextHandler);
                    break;
                case "image":
                    context = ExecuteHandler(rMsg, rDirs, new ImageRecMsg(), ImageHandler);
                    break;
                case "voice":
                    context = ExecuteHandler(rMsg, rDirs, new VoiceRecMsg(), VoiceHandler);
                    break;
                case "video":
                    context = ExecuteHandler(rMsg, rDirs, new VideoRecMsg(), VideoHandler);
                    break;
                case "shortvideo":
                    context = ExecuteHandler(rMsg, rDirs, new VideoRecMsg(), VideoHandler);
                    break;
                case "location":
                    context = ExecuteHandler(rMsg, rDirs, new LocationRecMsg(), LocationHandler);
                    break;
                case "link":
                    context = ExecuteHandler(rMsg, rDirs, new LinkRecMsg(), LinkHandler);
                    break;
            }
            return context;
        }


        /// <summary>
        ///  执行基础事件消息类型
        /// </summary>
        /// <param name="reMsg"></param>
        /// <param name="reDirs"></param>
        /// <returns>返回基础事件消息处理结果</returns>
        private MsgContext ProcessExecute_BasicEventMsg(XmlDocument reMsg, Dictionary<string, string> reDirs)
        {
            var eventType = reDirs["Event"].ToLower();
            MsgContext context = null;
            switch (eventType)
            {
                case "subscribe":
                    context = ExecuteHandler(reMsg, reDirs, new SubscribeRecEventMsg(), SubscribeEventHandler);
                    break;
                case "unsubscribe":
                    context = ExecuteHandler(reMsg, reDirs, new SubscribeRecEventMsg(), SubscribeEventHandler);
                    break;
                case "scan":
                    context = ExecuteHandler(reMsg, reDirs, new SubscribeRecEventMsg(), ScanEventHandler);
                    break;
                case "location":
                    context = ExecuteHandler(reMsg, reDirs, new LocationRecEventMsg(), LocationEventHandler);
                    break;
                case "click":
                    context = ExecuteHandler(reMsg, reDirs, new ClickRecEventMsg(), ClickEventHandler);
                    break;
                case "view":
                    context = ExecuteHandler(reMsg, reDirs, new ViewRecEventMsg(), ViewEventHandler);
                    break;
            }
            return context;
        }

        /// <summary>
        ///  根据具体的消息类型执行相关的消息委托方法(基础消息)
        /// </summary>
        /// <typeparam name="TRecMsg"></typeparam>
        /// <param name="recMsgXml"></param>
        /// <param name="recMsgDirs"></param>
        /// <param name="recMsg"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected static MsgContext ExecuteHandler<TRecMsg>(XmlDocument recMsgXml,
            IDictionary<string, string> recMsgDirs, TRecMsg recMsg, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var msgContext = new MsgContext();

            recMsg.SetMsgDirs(recMsgDirs);
            recMsg.RecMsgXml = recMsgXml;

            msgContext.ReplyMsg = ExecuteHandlerDelegate(recMsg, func);
            msgContext.RecMsg = recMsg;

            return msgContext;
        }


        /// <summary>
        /// 执行事件对应委托方法，如果对应的方法存在则执行
        /// </summary>
        /// <typeparam name="TRecMsg"></typeparam>
        /// <param name="res"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static BaseReplyMsg ExecuteHandlerDelegate<TRecMsg>(TRecMsg res, Func<TRecMsg, BaseReplyMsg> func)
            where TRecMsg : BaseRecMsg, new()
        {
            var baseRep = func?.Invoke(res) ?? new NoneReplyMsg();

            baseRep.ToUserName = res.FromUserName;
            baseRep.FromUserName = res.ToUserName;
            baseRep.CreateTime = DateTime.Now.ToLocalSeconds();

            return baseRep;
        }
        
        /// <summary>
        ///   执行处理未知消息
        /// </summary>
        /// <returns></returns>
        protected virtual BaseReplyMsg ProcessUnknowHandler(BaseRecMsg msg)
        {
            return new NoneReplyMsg();
        }

    }
}
