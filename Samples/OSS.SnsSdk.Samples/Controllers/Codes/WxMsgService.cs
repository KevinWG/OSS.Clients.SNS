using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SocialSDK.WX.Msg.Mos;

namespace OSS.SnsSdk.Samples.Controllers.Codes
{
    public class WxMsgService: WxMsgHandler
    {
        public WxMsgService(WxMsgServerConfig mConfig = null) : base(mConfig)
        {

        }


        protected override WxMsgProcessor GetCustomMsgHandler(string msgType, string eventName = null)
        {
            return base.GetCustomMsgHandler(msgType, eventName);
        }


        protected override BaseReplyMsg ProcessTextHandler(TextRecMsg msg)
        {
            return msg.Content == "oss"
                ? new TextReplyMsg() { Content = "欢迎关注.Net开源世界！" }
                : null;
        }

        protected override void ExecuteEnd(MsgContext msgContext)
        {
            // 消息处理结束时必经方法，可以在这里进行一些全局性的操作
        }






    }
}
