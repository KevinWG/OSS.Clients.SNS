﻿using System;
using System.Net.Http;
using OSS.Clients.Platform.WX.Base.Interface.Impls;
using OSS.Common.BasicMos;

namespace OSS.Clients.Platform.WX
{
    public static class WechatPlatformHelper
    {
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        public static string ApiHost { get; set; } = "https://api.weixin.qq.com";

        /// <summary>
        ///  默认应用配置信息
        /// </summary>
        public static IAppSecret DefaultAppSecret { get; set; }

        /// <summary>
        ///   http请求的HttpClient实例创建
        /// </summary>
        public static Func<HttpClient> HttpClientProvider { get; set; }






        /// <summary>
        ///  自定义获取 access_token 接口实现
        ///     默认实现通过 OSS.Tools.Cache 缓存，并在过期前自动更新
        /// </summary>
        public static IAccessTokenProvider AccessTokenProvider { get; set; } = new InterAccessTokenProvider();


        /// <summary>
        ///  自定义获取 js_ticket 接口实现
        ///  默认实现通过 OSS.Tools.Cache 缓存，并在过期前自动更新
        /// </summary>
        public static IJsTicketProvider JsTicketProvider { get; set; } = new InterJsTicketProvider();

        ///// <summary>
        /////   自定义获取 component_access_token 接口实现
        /////  默认实现通过 OSS.Tools.Cache 缓存，并在过期前自动更新
        ///// </summary>
        //public static IComponentAccessTokenProvider ComponentAccessTokenProvider { get; set; }

    }

}
