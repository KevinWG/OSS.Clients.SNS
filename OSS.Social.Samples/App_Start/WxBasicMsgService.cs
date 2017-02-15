using OSS.Common.ComModels.Enums;
using OSS.Common.Extention;
using OSS.Social.WX.Msg;
using OSS.Social.WX.Msg.Mos;

namespace OSS.Social.Samples
{
    public class WxBasicMsgService : WxBasicMsgHandler
    {
        public WxBasicMsgService(WxMsgServerConfig config) : base(config)
        {
            TextHandler += WxMsgService_TextHandler;
        }
        private BaseReplyMsg WxMsgService_TextHandler(TextRecMsg arg)
        {
            return new TextReplyMsg()
            {
                Content = "欢迎使用开源产品，记得贡献"
            };
        }
    }
}