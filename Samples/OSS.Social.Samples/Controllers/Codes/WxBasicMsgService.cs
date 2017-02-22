using OSS.Common.Modules.LogModule;
using OSS.Social.WX.Msg;
using OSS.Social.WX.Msg.Mos;

namespace OSS.Social.Samples.Controllers.Codes
{
    public class WxBasicMsgService: WxMsgBasicHandler
    {
        public WxBasicMsgService(WxMsgServerConfig mConfig) : base(mConfig)
        {
            TextHandler += WxBasicMsgService_TextHandler;
        }

        private BaseReplyMsg WxBasicMsgService_TextHandler(TextRecMsg arg)
        {
            if (arg.Content=="oss")
            {
                return new TextReplyMsg() { Content = "欢迎关注.Net开源世界！" };
            }
            return null;
        }

        protected override void ProcessEnd(MsgContext msgContext)
        {
            LogUtil.Info(msgContext.RecMsg.RecMsgXml?.OuterXml);
        }
    }
}
