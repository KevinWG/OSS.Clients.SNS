using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Samples.Controllers.Codes
{
    public class WxMsgService: WxMsgHandler
    {
        public WxMsgService(WxMsgConfig mConfig = null) : base(mConfig)
        {

        }

        protected override WxMsgProcessor GetCustomProcessor(string msgType, string eventName)
        {
            if (msgType == "test_msg")
            {
                return new WxMsgProcessor<WxTestRecMsg>()
                {
                    RecMsgInsCreater = () => new WxTestRecMsg(),
                    ProcessFunc = (msg) => new WxTextReplyMsg() {Content = "test" + msg.Test}
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
