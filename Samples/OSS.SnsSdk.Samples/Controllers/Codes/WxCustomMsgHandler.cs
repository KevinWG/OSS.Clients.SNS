using System.Collections.Generic;
using OSS.Clients.Chat.WX;
using OSS.Clients.Chat.WX.Mos;
using OSS.Tools.Log;

namespace OSS.Clients.Samples.Controllers.Codes
{
    public class WXCustomMsgHandler: WXChatHandler
    {
        public WXCustomMsgHandler(WXChatConfig mConfig = null) : base(mConfig)
        {

        }

        protected override WXBaseReplyMsg ProcessTextMsg(WXTextRecMsg msg)
        {
            return WXNoneReplyMsg.None;
        }


        protected override WXChatProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgType == "test_msg")
            {
                return new WXChatProcessor<WXTestRecMsg>()
                {
                    RecInsCreater = () => new WXTestRecMsg(),
                    ProcessFunc = (msg) => new WXTextReplyMsg {Content = "test" + msg.Test}
                };
            }
            return null;
        }


        protected override void Executing(WXChatContext context)
        {
            LogHelper.Info($"当前消息正文：{context.RecMsg.RecMsgXml.InnerXml}", "Executing");
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
