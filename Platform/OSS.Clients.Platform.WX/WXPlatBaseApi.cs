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
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.Extention;
using OSS.Clients.Platform.WX.Helpers;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Cache;
using OSS.Tools.Http.Extention;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX
{
    /// <summary>
    ///  基类
    /// </summary>
    public class WXBaseApi : BaseApiConfigProvider<AppConfig>
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
        public WXBaseApi(AppConfig config) : base(config)
        {
            //SourceName = WXPlatConfigProvider.SourceName;
        }
        
        #endregion

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <returns>实体类型</returns>
        public async Task<T> RestCommonJson<T>(OssHttpRequest request)
            where T : WXBaseResp, new()
        {
            var resp = await request.RestSend(WXPlatConfigProvider.ClientFactory?.Invoke());
            if (!resp.IsSuccessStatusCode)
                return new T()
                {
                    ret = -(int) resp.StatusCode,
                    msg = resp.ReasonPhrase
                };

            var contentStr = await resp.Content.ReadAsStringAsync();
            var t= JsonConvert.DeserializeObject<T>(contentStr);

            if (!t.IsSuccess())
                t.msg = t.errmsg;

            return t;
        }

        /// <inheritdoc />
        protected override AppConfig GetDefaultConfig()
        {
            return WXPlatConfigProvider.DefaultConfig;
        }
    }

    /// <summary>
    /// 微信公号接口基类
    /// </summary>
    public class WXPlatBaseApi : WXBaseApi
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WXPlatBaseApi(AppConfig config) : base(config)
        {
        }

        #endregion

        #region  基础方法

        /// <summary>
        /// 获取微信服务器列表
        /// </summary>
        /// <returns></returns>
        public async Task<WXIpListResp> GetWXIpListAsync()
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/getcallbackip")
            };

            return await RestCommonOffcialAsync<WXIpListResp>(req);
        }

        /// <summary>
        ///   获取公众号的AccessToken
        ///     【首先从缓存中获取，如果没有再从远程获取】
        /// </summary>
        /// <returns></returns>
        public virtual async Task<WXPlatAccessTokenResp> GetAccessTokenFromCacheAsync()
        {
            if (ApiConfig.OperateMode == AppOperateMode.ByAgent)
            {
                var atoken = WXPlatConfigProvider.AccessTokenFromAgentFunc?.Invoke(ApiConfig);
                if (string.IsNullOrEmpty(atoken))
                {
                    throw new ArgumentNullException("access_token",
                        "access_token值未发现，请检查 WXPlatConfigProvider 下 AccessTokenFromAgentFunc 委托是否为空或者返回值不正确！");
                }

                return new WXPlatAccessTokenResp() {access_token = atoken};
            }

            var m_OffcialAccessTokenKey = string.Format(WXCacheKeysHelper.OffcialAccessTokenKey, ApiConfig.AppId);
            var tokenResp = await CacheHelper.GetAsync<WXPlatAccessTokenResp>(m_OffcialAccessTokenKey, WXPlatConfigProvider.CacheSourceName);

            if (tokenResp != null && tokenResp.expires_date >= DateTime.Now.ToUtcSeconds())
                return tokenResp;

            tokenResp = await GetAccessTokenFromWXAsync();

            if (!tokenResp.IsSuccess())
                return tokenResp;

            tokenResp.expires_date = DateTime.Now.ToUtcSeconds() + tokenResp.expires_in - 600;

            await CacheHelper.SetAbsoluteAsync(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in - 600), WXPlatConfigProvider.CacheSourceName);

            return tokenResp;
        }

        /// <summary>
        /// 从微信服务器获取AccessToken，请注意访问速率控制，正常情况请访问： 【GetAccessTokenFromCacheAsync】
        /// </summary>
        /// <returns></returns>
        public async Task<WXPlatAccessTokenResp> GetAccessTokenFromWXAsync()
        {
            var req = new OssHttpRequest
            {
                AddressUrl =
                    $"{m_ApiUrl}/cgi-bin/token?grant_type=client_credential&appid={ApiConfig.AppId}&secret={ApiConfig.AppSecret}",
                HttpMethod = HttpMethod.Get
            };
            return await RestCommonJson<WXPlatAccessTokenResp>(req);
        }

        /// <summary>
        ///   公众号主要Rest请求接口封装
        ///      主要是预处理accesstoken赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        protected async Task<T> RestCommonOffcialAsync<T>(OssHttpRequest req)
            where T : WXBaseResp, new()
        {
            var tokenRes = await GetAccessTokenFromCacheAsync();
            if (!tokenRes.IsSuccess())
                return new T().WithResp(tokenRes); // tokenRes.ConvertToResultInherit<T>();

            req.RequestSet = r =>
            {
                r.Headers.Add("Accept", "application/json");

                if (r.Content != null)
                {
                    r.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json") {CharSet = "UTF-8"};
                }
            };
            req.AddressUrl = string.Concat(req.AddressUrl, req.AddressUrl.IndexOf('?') > 0 ? "&" : "?", "access_token=",
                tokenRes.access_token);

            return await RestCommonJson<T>(req);
        }

        /// <summary>
        ///   下载文件方法
        /// </summary>
        protected static async Task<WXFileResp> DownLoadFileAsync(OssHttpRequest req)
        {
            var resp = await req.RestSend(WXPlatConfigProvider.ClientFactory?.Invoke());
            if (!resp.IsSuccessStatusCode)
                return new WXFileResp() {ret = (int) RespTypes.ObjectStateError, msg = "当前请求失败！"};

            var contentStr = resp.Content.Headers.ContentType.MediaType;
            using (resp)
            {
                if (!contentStr.Contains("application/json"))
                    return new WXFileResp()
                    {
                        content_type = contentStr,
                        file         = await resp.Content.ReadAsByteArrayAsync()
                    };
                return JsonConvert.DeserializeObject<WXFileResp>(await resp.Content.ReadAsStringAsync());
            }
        }

        #endregion
    }


}
