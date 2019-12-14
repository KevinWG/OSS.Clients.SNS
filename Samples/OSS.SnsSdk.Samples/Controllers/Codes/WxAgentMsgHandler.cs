using System.Collections.Generic;
using OSS.Clients.Chat.WX;
using OSS.Clients.Chat.WX.Mos;
using OSS.Tools.DirConfig;
using OSS.Tools.Log;

namespace OSS.Clients.SNS.Samples.Controllers.Codes
{
    public class WXAgentController : WXChatBaseHandler
    {
        public WXAgentController(WXChatConfig config):base(config)
        {
        }
    
        protected override WXChatProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgInfo.ContainsKey("ComponentVerifyTicket"))
            {
                return new WXChatProcessor<VerifComponentTicketRecMsg>()
                {
                    RecInsCreater=() => new VerifComponentTicketRecMsg(),
                    ProcessFunc = msg =>
                    {
                        var res = DirConfigHelper.SetDirConfig<TicketMo>($"{ApiConfig.AppId}_component_verify_ticket",
                            new TicketMo { ticket = msg.ComponentVerifyTicket });
                        return WXNoneReplyMsg.None;
                    }
                };
            }
            return null;
        }

        protected override void ExecuteEnd(WXChatContext msgContext)
        {
            LogHelper.Info(msgContext.RecMsg.RecMsgXml.InnerXml, "PlatformMsg");
        }
    }

    public class VerifComponentTicketRecMsg : WXBaseRecMsg
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
