using System;
using System.Net.Http;
using OSS.Common.BasicMos;

namespace OSS.Clients.Platform.WX.Base
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

        ///// <summary>
        /////   缓存模块名称
        /////     可通过 OSS.Tools.Cache 类库 定义缓存处理
        ///// </summary>
        //public static string CacheSourceName { get; set; } = "default";

        /// <summary>
        ///   http请求的HttpClient实例创建
        /// </summary>
        public static Func<HttpClient> ClientFactory { get; set; }

        /// <summary>
        ///  AccessToken的统一管理接口
        /// </summary>
        public static IAccessTokenHub AccessTokenHub { get; set; }

        /// <summary>
        ///  JsTicket统一管理接口
        /// </summary>
        public static IJsTicketHub JsTicketHub { get; set; }

        /// <summary>
        ///   【代理平台】AccessToken统一管理接口
        /// </summary>
        public static IAgentAccessTokenHub AgentAccessTokenHub { get; set; }
    }
}
