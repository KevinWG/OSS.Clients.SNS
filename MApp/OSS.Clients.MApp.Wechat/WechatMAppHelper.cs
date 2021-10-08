using System;
using System.Net.Http;
using OSS.Common.BasicMos;

namespace OSS.Clients.MApp.Wechat
{
    public static class WechatMAppHelper
    {
        public static string ApiHost = "https://api.weixin.qq.com";

        /// <summary>
        ///   http请求的HttpClient实例创建
        /// </summary>
        public static Func<HttpClient> HttpClientProvider { get; set; }

        /// <summary>
        ///  默认配置信息
        /// </summary>
        public static AppSecret DefaultConfig { get; set; }
    }
}
