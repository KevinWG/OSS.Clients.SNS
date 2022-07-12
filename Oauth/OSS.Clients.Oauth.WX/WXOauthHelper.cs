using System;
using System.Net.Http;
using OSS.Common;

namespace OSS.Clients.Oauth.WX
{
    /// <summary>
    ///  Oauth相关配置信息
    /// </summary>
    public static class WXOauthHelper
    {
        /// <summary>
        /// 访问配置信息提供者
        /// </summary>
        public static IAccessSecretProvider ConfigProvider { get; set; }
        
        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static IAccessSecret DefaultConfig { get; set; }

        /// <summary>
        ///    http 请求客户端 配置
        /// </summary>
        public static Func<HttpClient> ClientFactory { get; set; }

        /// <summary>
        /// 当 OperateMode = ByAgent 时，
        ///   调用此委托 获取第三方代理平台的 AccessToken 
        ///   可以调用 Official下的 WXAgentAuthApi（WXAgentBaseApi） 中接口
        /// 参数为当前appConfig
        /// </summary>
        public static Func<IAccessSecret, string> AgentAccessTokenFunc { get; set; }
    }


}
