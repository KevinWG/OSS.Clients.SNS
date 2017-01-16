#region Copyright (C) 2016 Kevin (OS系列开源项目)

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
using OS.Common.ComModels;
using OS.Common.Modules;
using OS.Common.Modules.CacheModule;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Mos;

namespace OS.Social.WX.Offcial
{
    /// <summary>
    ///   用户管理，消息管理
    /// </summary>
    public partial class WxOffcialApi : WxOffcialBaseApi
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxOffcialApi(WxAppCoinfig config) : base(config)
        {
        }
        static WxOffcialApi()
        {
            #region  增加用户管理特殊 错误码(https://mp.weixin.qq.com/wiki)

            m_DicErrMsg.Add(45157, "标签名非法，请注意不能和其他标签重名");
            m_DicErrMsg.Add(45158, "标签名长度超过30个字节");
            m_DicErrMsg.Add(45056, "创建的标签数过多，请注意不能超过100个");

            //  基类中已经包含
            //m_DicErrMsg.Add(40003, "传入非法的openid");
            //m_DicErrMsg.Add(45159, "非法的tag_id");
            //m_DicErrMsg.Add(40032, "每次传入的openid列表个数不能超过50个");

            m_DicErrMsg.Add(45159, "非法的标签");
            m_DicErrMsg.Add(45059, "有粉丝身上的标签数已经超过限制");
            m_DicErrMsg.Add(49003, "传入的openid不属于此AppID");
            #endregion
        }


    }


    /// <summary>
    /// 微信公号接口基类
    /// </summary>
    public class WxOffcialBaseApi:WxBaseApi
    {
        private readonly string m_OffcialAccessTokenKey;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxOffcialBaseApi(WxAppCoinfig config) : base(config)
        {
            m_OffcialAccessTokenKey = string.Concat("wx_offical_access_token_", config.AppId);
        }
       
        /// <summary>
        /// 获取微信服务器列表
        /// </summary>
        /// <returns></returns>
        public WxIpListResp GetWxIpList()
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/getcallbackip");

            return RestCommonOffcial<WxIpListResp>(req);
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
