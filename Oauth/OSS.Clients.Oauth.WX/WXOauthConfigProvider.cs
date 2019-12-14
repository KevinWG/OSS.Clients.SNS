using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using OSS.Common.ComModels;

namespace OSS.Clients.Oauth.WX
{

    /// <summary>
    ///  Oauth相关配置信息
    /// </summary>
    public static class WXOauthConfigProvider
    {
        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = "oss_sns_oauth";

        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static AppConfig DefaultConfig { get; set; }

        public static Func<HttpClient> ClientFactory { get; set; }

        /// <summary>
        /// 当 OperateMode = ByAgent 时，
        ///   调用此委托 获取第三方代理平台的 AccessToken 
        ///   可以调用 Official下的 WXAgentAuthApi（WXAgentBaseApi） 中接口
        /// 参数为当前ApiConfig
        /// </summary>
        public static Func<AppConfig, string> AgentAccessTokenFunc { get; set; }

        ///// <summary>
        /////  设置上下文配置信息
        ///// </summary>
        ///// <param name="config"></param>
        //public static void SetContextConfig(AppConfig config)
        //{
        //    WXOauthBaseApi.SetContextConfig(config);
        //}

    }


}
