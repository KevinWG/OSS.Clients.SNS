using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SocialSDK.WX.Msg.Mos;

namespace OSS.SnsSdk.Samples.Controllers.Codes
{
    public class WxMsgService: WxMsgHandler
    {
        public WxMsgService(WxMsgServerConfig mConfig) : base(mConfig)
        {
            TextHandler += WxBasicMsgService_TextHandler;
        }

        private static BaseReplyMsg WxBasicMsgService_TextHandler(TextRecMsg arg)
        {
            return arg.Content == "oss"
                ? new TextReplyMsg() {Content = "欢迎关注.Net开源世界！"}
                : null;
        }

        protected override void ProcessEnd(MsgContext msgContext)
        {
            // 消息处理结束时必经方法，可以在这里进行一些全局性的操作
        }
    }
}
