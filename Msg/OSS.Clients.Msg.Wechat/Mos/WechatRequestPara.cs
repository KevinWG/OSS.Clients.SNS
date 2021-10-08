

namespace OSS.Clients.Msg.Wechat.Mos
{
    /// <summary>
    ///  接口请求的消息体
    /// </summary>
    public class WechatRequestPara
    {
        /// <summary>
        ///  消息主体
        /// </summary>
        public string body { get; set; }

        /// <summary>
        ///  签名信息，请注意和[msg_signature]区分
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 消息体签名
        /// </summary>
        public string msg_signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 随机字符创
        /// </summary>
        public string nonce { get; set; }

        /// <summary>
        /// 验证服务器参数，微信服务器首次验证接口时传递，需要作为响应信息原值返回
        /// </summary>
        public string echostr { get; set; }


        /// <summary>
        ///  应用Id（可忽略，多租户应用时方便传值，处理不同配置
        /// </summary>
        public string app_id { get; set; }
    }
}
