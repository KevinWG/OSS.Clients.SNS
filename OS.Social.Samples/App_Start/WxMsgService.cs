using OS.Social.WX.Msg;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.Samples
{
    public class WxMsgService : WxMsgHandler
    {
        public WxMsgService()
        {
            TextHandler += WxMsgService_TextHandler;
        }

        private BaseReplyContext WxMsgService_TextHandler(TextMsg arg)
        {
            return new TextReplyMsg()
            {
                 Content = "欢迎使用开源产品，记得贡献"
            };
        }
    }
}