using System.Net.Http;
using OSS.Clients.Platform.Wechat;

namespace OSS.Clients.MApp.Wechat
{

    /// <summary>
    ///  建立回话请求
    /// </summary>
    public class WechatGetUserPhoneNumReq : WechatBaseTokenReq<WechatGetUserPhoneNumResp>
    {
        /// <summary>
        ///  建立回话请求
        /// </summary>
        public WechatGetUserPhoneNumReq(string code) : base(HttpMethod.Post)
        {
            _code = code;
        }

        private readonly string _code;

        /// <inheritdoc />
        protected override void PrepareSend()
        {
            custom_body = $"{{\"code\":\"{_code}\"}}";
        }

        /// <inheritdoc />
        public override string GetApiPath()
        {
            return "/wxa/business/getuserphonenumber";
        }
    }

    public class WechatGetUserPhoneNumResp : WechatBaseResp
    {
        /// <summary>
        ///  手机号信息
        /// </summary>
        public PhoneInfo phone_info { get; set; }
    }


    public class PhoneInfo
    {
        /// <summary>
        ///  手机号
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        ///  没有区号的手机号
        /// </summary>
        public string purePhoneNumber { get; set; }

        /// <summary>
        ///  区号
        /// </summary>
        public string countryCode { get; set; }
    }

}
