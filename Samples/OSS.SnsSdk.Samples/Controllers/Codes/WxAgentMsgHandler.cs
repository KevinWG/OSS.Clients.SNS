using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Clients.Chat.WX;
using OSS.Clients.Chat.WX.Mos;
using OSS.Tools.DirConfig;
using OSS.Tools.Log;

namespace OSS.Clients.SNS.Samples.Controllers.Codes
{
    public class WXAgentHandler : WXChatBaseHandler
    {
        /// <inheritdoc />
        public WXAgentHandler()
        {
        }
    
        protected override BaseBaseProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgInfo.ContainsKey("ComponentVerifyTicket"))
            {
                return new WxAgentProcessor();
            }
            return null;
        }

        protected override Task ExecuteEnd(WXChatContext msgContext)
        {
            LogHelper.Info(msgContext.RecMsg.RecMsgXml.InnerXml, "PlatformMsg");
            return Task.CompletedTask;
        }
    }

    public class WxAgentProcessor : WXChatBaseProcessor<VerifComponentTicketRecMsg>
    {
        protected override Task<WXBaseReplyMsg> Execute(VerifComponentTicketRecMsg msg)
        {
            DirConfigHelper.SetDirConfig($"component_verify_ticket",
                new TicketMo { ticket = msg.ComponentVerifyTicket });

            return Task.FromResult<WXBaseReplyMsg>(WXNoneReplyMsg.None);
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
