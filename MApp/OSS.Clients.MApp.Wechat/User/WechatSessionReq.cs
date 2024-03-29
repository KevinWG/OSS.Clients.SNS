﻿using System.Net.Http;
using OSS.Clients.Platform.Wechat;

namespace OSS.Clients.MApp.Wechat
{
    /// <summary>
    ///  建立回话请求
    /// </summary>
    public class WechatSessionReq : WechatBaseReq<WechatSessionResp>
    {
        /// <summary>
        ///  建立回话请求
        /// </summary>
        public WechatSessionReq(string jsCode) : base(HttpMethod.Get)
        {
            _code = jsCode;
        }
        private readonly string _code;


        /// <inheritdoc />
        public override string GetApiPath()
        {
            return
                $"/sns/jscode2session?appid={access_config.access_key}&secret={access_config.access_secret}&js_code={_code}&grant_type=authorization_code";
        }
    }

    public class WechatSessionResp:WechatBaseResp
    {
        /// <summary>
        ///string 用户唯一标识
        /// </summary>  
        public string openid { get; set; }

        /// <summary>
        ///string 会话密钥
        /// </summary>  
        public string session_key { get; set; }


        /// <summary>
        ///string 用户在开放平台的唯一标识符，若当前小程序已绑定到微信开放平台帐号下会返回，详见UnionID机制说明。
        /// </summary>  
        public string unionid { get; set; }
    }
}
