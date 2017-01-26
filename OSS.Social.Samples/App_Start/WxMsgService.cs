using OSS.Social.WX.Msg;
using OSS.Social.WX.Msg.Mos;

namespace OSS.Social.Samples
{
    public class WxMsgService : WxMsgHandler
    {
        static WxMsgService()
        {
            RegisterEventMsgHandler("test_advance_event", typeof (LocationRecEventMsg), localMsg =>
            {
                var recMsg = localMsg as LocationRecEventMsg;
                if (recMsg == null)
                    return new NoneReplyMsg();

                return  new TextReplyMsg() {Content = $"这只是一个测试  经纬度：({recMsg.Latitude},{recMsg.Longitude})" };
            });
            //  http://localhost:17449/wxMsg/msg?signature=af2649978a19858a7f6faf8e7831830a7c3c4833&timestamp=11111111111&nonce=234532
            //  post 以下数据测试
            //  <xml>
            //< ToUserName >< ![CDATA[toUser]] ></ ToUserName >
            //< FromUserName >< ![CDATA[fromUser]] ></ FromUserName >
            //< CreateTime > 123456789 </ CreateTime >
            //< MsgType >< ![CDATA[event]]></MsgType>
            //<Event><![CDATA[test_advance_event]]></Event>
            //<Latitude>23.137466</Latitude>
            //<Longitude>113.352425</Longitude>
            //<Precision>119.385040</Precision>
            //</xml>
        }

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