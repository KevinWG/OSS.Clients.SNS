using System.Collections.Generic;
using OSS.Common.Plugs.DirConfigPlug;
using OSS.Common.Plugs.LogPlug;
using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Samples.Controllers.Codes
{
    public class WxPlatformMsgHandler:WxMsgBaseHandler
    {
        public WxPlatformMsgHandler(WxMsgConfig config):base(config)
        {
            
        }
        //var res = DirConfigUtil.SetDirConfig<TicketMo>($"{ApiConfig.AppId}_component_verify_ticket",
        //    new TicketMo { ticket = msg.ComponentVerifyTicket });


        protected override WxMsgProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgInfo.ContainsKey("ComponentVerifyTicket"))
            {
                return new WxMsgProcessor<VerifComponentTicketRecMsg>()
                {
                    RecInsCreater=() => new VerifComponentTicketRecMsg(),
                    ProcessFunc = msg => WxNoneReplyMsg.None
                };
            }
            return null;
        }

        protected override void ExecuteEnd(WxMsgContext msgContext)
        {
            LogUtil.Info(msgContext.RecMsg.RecMsgXml.InnerXml, "PlatformMsg");
        }


    }


    

    public class VerifComponentTicketRecMsg : WxBaseRecMsg
    {
        public string AppId { get; set; }

        public string InfoType { get; set; }
        public string ComponentVerifyTicket { get; set; }

        protected override void FormatPropertiesFromMsg()
        {
            AppId = this["AppId"];
            InfoType = this["InfoType"];
            ComponentVerifyTicket = this["ComponentVerifyTicket"];
            base.FormatPropertiesFromMsg();
        }
    }




    public class TicketMo
    {
        public string ticket { get; set; }
    }
}
