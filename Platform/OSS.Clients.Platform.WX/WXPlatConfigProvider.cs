using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using OSS.Common.ComModels;

namespace OSS.Clients.Platform.WX
{

    /// <summary>
    ///  应用配置相关信息
    /// </summary>
    public static class WXPlatConfigProvider
    {
        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static AppConfig DefaultConfig { get; set; }

        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = "oss_sns";

        public static Func<HttpClient> ClientFactory { get; set; }


        /// <summary>
        /// 当 OperateMode = ByAgent 时，
        ///   调用此委托 获取通过第三方代理的 公众号AccessToken 【注：是被代理的公众号token】
        ///   接口位于： WXAgentAuthApi 中， 如果过期请自行调用RefreshToken方法，此方法需要返回可用的AccessToken
        /// 参数为当前ApiConfig
        /// </summary>
        public static Func<AppConfig, string> AccessTokenFromAgentFunc { get; set; }

        /// <summary>
        /// 获取第三方Agent的VerifyTicket（由微信推送过来）调用的委托
        /// 参数为当前ApiConfig
        /// </summary>
        public static Func<AppConfig, string> AgentVerifyTicketGetFunc { get; set; }

    }
}
