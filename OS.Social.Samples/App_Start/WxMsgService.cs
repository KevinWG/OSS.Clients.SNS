using OS.Social.WX.Msg;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.Samples
{
    public class WxMsgService : WxMsgHandler
    {
        public WxMsgService(WxMsgServerConfig config) :base(config)
        {
            TextHandler += WxMsgService_TextHandler;
        }
        /// <summary>
        ///   文本消息处理事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private BaseReplyMsg WxMsgService_TextHandler(TextRecMsg arg)
        {
            return new TextReplyMsg()
            {
                 Content = "欢迎使用开源产品，记得贡献"
            };
        }
    }
}