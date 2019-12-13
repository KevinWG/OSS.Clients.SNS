#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口基类，获取AccessToken ，获取微信服务器Ip列表
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016   忘记哪一天
*       
*****************************************************************************/

#endregion

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Common.Plugs;
using OSS.Common.Plugs.CachePlug;
using OSS.Common.Resp;
using OSS.Http.Extention;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Basic.Mos;
using OSS.SnsSdk.Official.Wx.Helpers;
using OSS.Tools.Cache;

namespace OSS.SnsSdk.Official.Wx
{
    /// <summary>
    ///  基类
    /// </summary>
    public class WxBaseApi : BaseApiConfigProvider<AppConfig>
    {
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxBaseApi(AppConfig config) : base(config)
        {
            //ModuleName = WxOfficialConfigProvider.ModuleName;
        }
        
        #endregion

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <param name="client">自定义HttpClient</param>
        /// <returns>实体类型</returns>
        public async Task<T> RestCommonJson<T>(OsHttpRequest request, HttpClient client = null)
            where T : WxBaseResp, new()
        {
            var t = await request.RestCommonJson<T>(client);

            if (!t.IsSuccess())
                t.msg = t.errmsg;

            return t;
        }

        /// <inheritdoc />
        protected override AppConfig GetDefaultConfig()
        {
            return WxOfficialConfigProvider.DefaultConfig;
        }
    }

    /// <summary>
    /// 微信公号接口基类
    /// </summary>
    public class WxOffBaseApi : WxBaseApi
    {
        #region 构造函数
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxOffBaseApi(AppConfig config) : base(config)
        {
        }

        #endregion

        #region  基础方法

        /// <summary>
        /// 获取微信服务器列表
        /// </summary>
        /// <returns></returns>
        public async Task<WxIpListResp> GetWxIpListAsync()
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/getcallbackip")
            };
            
            return await RestCommonOffcialAsync<WxIpListResp>(req);
        }

        /// <summary>
        ///   获取公众号的AccessToken
        ///     【首先从缓存中获取，如果没有再从远程获取】
        /// </summary>
        /// <returns></returns>
        public virtual async Task<WxOffAccessTokenResp> GetAccessTokenFromCacheAsync()
        {
            if (ApiConfig.OperateMode == AppOperateMode.ByAgent)
            {
                var atoken = WxOfficialConfigProvider.AccessTokenFromAgentFunc?.Invoke(ApiConfig);
                if (string.IsNullOrEmpty(atoken))
                {
                    throw new ArgumentNullException("access_token",
                        "access_token值未发现，请检查 WxOfficialConfigProvider 下 AccessTokenFromAgentFunc 委托是否为空或者返回值不正确！");
                }
                return new WxOffAccessTokenResp() {access_token = atoken};
            }

            var m_OffcialAccessTokenKey = string.Format(WxCacheKeysHelper.OffcialAccessTokenKey, ApiConfig.AppId);
            var tokenResp = CacheHelper.Get<WxOffAccessTokenResp>(m_OffcialAccessTokenKey, WxOfficialConfigProvider.ModuleName);

            if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
                return tokenResp;

            tokenResp = await GetAccessTokenFromWxAsync();

            if (!tokenResp.IsSuccess())
                return tokenResp;

            tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

            CacheHelper.Set(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in-600),
                WxOfficialConfigProvider.ModuleName);

            return tokenResp;
        }

        /// <summary>
        /// 从微信服务器获取AccessToken，请注意访问速率控制，正常情况请访问： 【GetAccessTokenFromCacheAsync】
        /// </summary>
        /// <returns></returns>
        public async Task<WxOffAccessTokenResp> GetAccessTokenFromWxAsync()
        {
            var req = new OsHttpRequest
            {
                AddressUrl =
                    $"{m_ApiUrl}/cgi-bin/token?grant_type=client_credential&appid={ApiConfig.AppId}&secret={ApiConfig.AppSecret}",
                HttpMethod = HttpMethod.Get
            };
            return await RestCommonJson<WxOffAccessTokenResp>(req);
        }

        /// <summary>
        ///   公众号主要Rest请求接口封装
        ///      主要是预处理accesstoken赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="client">自定义 HttpClient </param>
        /// <returns></returns>
        protected async Task<T> RestCommonOffcialAsync<T>(OsHttpRequest req,
            HttpClient client = null)
            where T : WxBaseResp, new()
        {
            var tokenRes = await GetAccessTokenFromCacheAsync();
            if (!tokenRes.IsSuccess())
                return new T().WithResp(tokenRes);// tokenRes.ConvertToResultInherit<T>();

            req.RequestSet = r =>
            {
                r.Headers.Add("Accept", "application/json");

                if (r.Content != null)
                {
                    r.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };
                }
            };
            req.AddressUrl = string.Concat(req.AddressUrl, req.AddressUrl.IndexOf('?') > 0 ? "&" : "?", "access_token=",
                tokenRes.access_token);

            return await RestCommonJson<T>(req, client);
        }
        
        /// <summary>
        ///   下载文件方法
        /// </summary>
        protected static async Task<WxFileResp> DownLoadFileAsync(HttpResponseMessage resp)
        {
            if (!resp.IsSuccessStatusCode)
                return new WxFileResp() {ret = (int) RespTypes.ObjectStateError, msg = "当前请求失败！"};

            var contentStr = resp.Content.Headers.ContentType.MediaType;
            if (!contentStr.Contains("application/json"))
                return new WxFileResp()
                {
                    content_type = contentStr,
                    file = await resp.Content.ReadAsByteArrayAsync()
                };
            return JsonConvert.DeserializeObject<WxFileResp>(await resp.Content.ReadAsStringAsync());
        }

        #endregion
    }


    /// <summary>
    ///  应用配置相关信息
    /// </summary>
    public static class WxOfficialConfigProvider
    {
        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static AppConfig DefaultConfig { get; set; }

        ///// <summary>
        /////  设置上下文配置信息
        ///// </summary>
        ///// <param name="config"></param>
        //public static void SetContextConfig(AppConfig config)
        //{
        //    WxBaseApi.SetContextConfig(config);
        //}
        
        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = "oss_sns";

        /// <summary>
        /// 当 OperateMode = ByAgent 时，
        ///   调用此委托 获取通过第三方代理的 公众号AccessToken 【注：是被代理的公众号token】
        ///   接口位于： WxAgentAuthApi 中， 如果过期请自行调用RefreshToken方法，此方法需要返回可用的AccessToken
        /// 
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
