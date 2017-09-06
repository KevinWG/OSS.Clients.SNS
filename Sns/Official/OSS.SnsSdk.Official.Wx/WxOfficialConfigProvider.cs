using System;
using OSS.Common.ComModels;
using OSS.Common.Plugs;
using OSS.SnsSdk.Official.Wx.Basic.Mos;

namespace OSS.SnsSdk.Official.Wx
{
    /// <summary>
    ///  公众号配置相关信息
    /// </summary>
    public static class WxOfficialConfigProvider
    {
        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static AppConfig DefaultConfig { get; set; }

        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = ModuleNames.SocialCenter;
        
        /// <summary>
        /// 当 OperateMode = ByAgent 时， 通过授权的公众号的 AccessToken 获取调用此方法
        /// </summary>
        public static Func<AppConfig, WxOffAccessTokenResp> AccessTokenFromAgentMethod { get; set; }
    }
}
