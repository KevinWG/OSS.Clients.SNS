using System.Collections.Generic;
using System.Threading.Tasks;
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

        protected override Task<WXBaseReplyMsg> ProcessTextMsg(WXTextRecMsg msg)
        {
            return Task.FromResult<WXBaseReplyMsg>(WXNoneReplyMsg.None);
        }


        protected override BaseBaseProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgType == "test_msg")
            {
                return new WXTestProcessor();
            }
            return null;
        }

    }

    public class WXTestProcessor:WXChatBaseProcessor<WXTestRecMsg>
    {
        protected override Task<WXBaseReplyMsg> Execute(WXTestRecMsg msg)
        {
            return Task.FromResult<WXBaseReplyMsg>(new WXTextReplyMsg { Content = "test" + msg.Test });
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
