using System.Collections.Generic;
using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Samples.Controllers.Codes
{
    public class WxCustomMsgHandler: WxMsgHandler
    {
        public WxCustomMsgHandler(WxMsgConfig mConfig = null) : base(mConfig)
        {

        }

        protected override WxMsgProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgType == "test_msg")
            {
                return new WxMsgProcessor<WxTestRecMsg>()
                {
                    RecInsCreater = () => new WxTestRecMsg(),
                    ProcessFunc = (msg) => new WxTextReplyMsg {Content = "test" + msg.Test}
                };
            }
            return null;
        }
    }

    public class WxTestRecMsg : WxBaseRecMsg
    {
        public string Test { get; set; }

        protected override void FormatPropertiesFromMsg()
        {
            Test = this["Test"];
            base.FormatPropertiesFromMsg();
        }
    }

}
