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
            return new TextReplyMsg(){ Content = "您好，如果您需要地陪服务请关注公众号“口袋地陪”！"};
        }

        protected override void ProcessEnd(MsgContext msgContext)
        {
            LogUtil.Info(msgContext.RecMsg.RecMsgXml?.OuterXml);
        }
    }
}
