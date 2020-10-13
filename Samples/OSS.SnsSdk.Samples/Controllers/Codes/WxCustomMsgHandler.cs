using System.Collections.Generic;
using OSS.Clients.Chat.WX;
using OSS.Clients.Chat.WX.Mos;
using OSS.Common.BasicImpls;

namespace OSS.Clients.SNS.Samples.Controllers.Codes
{
    public class WXCustomMsgHandler: WXChatHandler
    {
        public WXCustomMsgHandler(IMetaProvider<WXChatConfig> configProvider = null) : base(configProvider)
        {

        }

        protected override WXBaseReplyMsg ProcessTextMsg(WXTextRecMsg msg)
        {
            return WXNoneReplyMsg.None;
        }


        protected override BaseWXChatProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgType == "test_msg")
            {
                return new WXTestProcessor();
            }
            return null;
        }

    }

    public class WXTestProcessor:WXChatProcessor<WXTestRecMsg>
    {
        protected override WXBaseReplyMsg Execute(WXTestRecMsg msg)
        {
            return new WXTextReplyMsg {Content = "test" + msg.Test};
        }
    }

    public class WXTestRecMsg : WXBaseRecMsg
    {
        public string Test { get; set; }

        protected override void FormatPropertiesFromMsg()
        {
            Test = this["Test"];
            base.FormatPropertiesFromMsg();
        }
    }

}
