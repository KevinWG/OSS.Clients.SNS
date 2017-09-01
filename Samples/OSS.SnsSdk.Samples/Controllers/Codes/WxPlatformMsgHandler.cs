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

        protected override WxMsgProcessor GetCustomProcessor(string msgType, string eventName, IDictionary<string, string> msgInfo)
        {
            if (msgInfo.ContainsKey("ComponentVerifyTicket"))
            {
                return new WxMsgProcessor<VerifComponentTicketRecMsg>()
                {
                    RecMsgInsCreater=() => new VerifComponentTicketRecMsg(),
                    ProcessFunc = msg =>
                    {
                        DirConfigUtil.SetDirConfig($"{ApiConfig.AppId}_component_verify_ticket",msg);

                        return WxNoneReplyMsg.None;
                    }
                };
            }
            return base.GetCustomProcessor(msgType, eventName, msgInfo);
        }

        protected override void ExecuteEnd(WxMsgContext msgContext)
        {
            LogUtil.Info(msgContext.RecMsg.RecMsgXml.InnerText,"PlatformMsg");
            base.ExecuteEnd(msgContext);
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
}
