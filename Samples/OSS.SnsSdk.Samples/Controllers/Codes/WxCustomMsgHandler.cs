using System.Collections.Generic;
using OSS.Common.Plugs.LogPlug;
using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.Tools.Log;

namespace OSS.SnsSdk.Samples.Controllers.Codes
{
    public class WxCustomMsgHandler: WxMsgHandler
    {
        public WxCustomMsgHandler(WxMsgConfig mConfig = null) : base(mConfig)
        {

        }

        protected override WxBaseReplyMsg ProcessTextMsg(WxTextRecMsg msg)
        {
            return WxNoneReplyMsg.None;
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


        protected override void Executing(WxMsgContext context)
        {
            LogHelper.Info($"当前消息正文：{context.RecMsg.RecMsgXml.InnerXml}", "Executing");
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
