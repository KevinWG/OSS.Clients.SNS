#region Copyright (C) 2016 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：公号的功能接口基类，获取AccessToken ，获取微信服务器Ip列表
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2016   忘记哪一天
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using OS.Common.ComModels;
using OS.Common.Modules;
using OS.Common.Modules.CacheModule;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Mos;

namespace OS.Social.WX.Offcial
{
    /// <summary>
    /// 微信公号接口基类
    /// </summary>
    public class WxBaseOffcialApi:WxBaseApi
    {
        private readonly string m_OffcialAccessTokenKey;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxBaseOffcialApi(WxAppCoinfig config) : base(config)
        {
            m_OffcialAccessTokenKey = string.Concat("wx_offical_access_token_", config.AppId);
        }
       
        /// <summary>
        /// 获取微信服务器列表
        /// </summary>
        /// <returns></returns>
        public WxBaseResp<List<string>> GetWxIpList()
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/getcallbackip");

            return RestCommonOffcial(req, resp =>
            {
                JObject obj=JObject.Parse(resp.Content);
                var ipList = obj["ip_list"].Values<string>().ToList();
                 return new WxBaseResp<List<string>>() {Data = ipList };
            });
        }

        #region  基础方法

        /// <summary>
        ///   获取公众号的AccessToken
        /// </summary>
        /// <returns></returns>
        public WxOffcialAccessTokenResp GetOffcialAccessToken()
        {
            var tokenResp = CacheUtil.Get<WxOffcialAccessTokenResp>(m_OffcialAccessTokenKey, ModuleNames.SnsCenter);

            if (tokenResp == null || tokenResp.expires_date < DateTime.Now)
            {
                OsHttpRequest req = new OsHttpRequest();

                req.AddressUrl = $"{m_ApiUrl}/cgi-bin/token?grant_type=client_credential&appid={m_Config.AppId}&secret={m_Config.AppSecret}";
                req.HttpMothed = HttpMothed.GET;

                tokenResp = RestCommon<WxOffcialAccessTokenResp>(req);

                if (!tokenResp.IsSuccess)
                    return tokenResp;

                tokenResp.expires_date = DateTime.Now.AddSeconds(tokenResp.expires_in - 600);

                CacheUtil.AddOrUpdate(m_OffcialAccessTokenKey, tokenResp, TimeSpan.FromSeconds(tokenResp.expires_in), null, ModuleNames.SnsCenter);
            }

            return tokenResp;
        }

        /// <summary>
        ///   公众号主要Rest请求接口封装
        ///      主要是预处理accesstoken赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="funcFormat"></param>
        /// <returns></returns>
        protected T RestCommonOffcial<T>(OsHttpRequest req, Func<OsHttpResponse, T> funcFormat = null)
            where T : WxBaseResp, new()
        {
            var tokenRes = GetOffcialAccessToken();
            if (!tokenRes.IsSuccess)
                return tokenRes.ConvertToResult<T>();

            req.AddressUrl = string.Concat(req.AddressUrl, req.AddressUrl.IndexOf('?') > 0 ? "&" : "?", "access_token=",
                tokenRes.access_token);

            req.Parameters.Add(new Parameter("content-type", "application/json", ParameterType.Header));

            return RestCommon<T>(req, funcFormat);
        }

        #endregion

    }
}
