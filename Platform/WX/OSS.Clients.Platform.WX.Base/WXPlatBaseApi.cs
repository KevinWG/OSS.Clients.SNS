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
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http.Extention;
using OSS.Tools.Http.Mos;
using OSS.Clients.Platform.WX.Base.Mos;

namespace OSS.Clients.Platform.WX.Base
{
     /// <summary>
    /// 微信公号接口基类
    /// </summary>
    public class WXPlatBaseApi : BaseMetaImpl<AppConfig>
    {
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";

        #region 构造函数

        /// <inheritdoc />
        public WXPlatBaseApi(IMetaProvider<AppConfig> configProvider) : base(configProvider)
        {
        }

        #endregion

        #region  基础方法

        /// <summary>
        ///   公众号主要Rest请求接口封装
        ///      主要是预处理accesstoken赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        protected async Task<T> RestCommonPlatAsync<T>(OssHttpRequest req)
            where T : WXBaseResp, new()
        {
            var configRes =await GetMeta();
            if (!configRes.IsSuccess())
               return new T().WithResp(configRes);

            var  appConfig = configRes.data;
            var tokenRes = await GetAccessToken(appConfig);
            if (!tokenRes.IsSuccess())
                return new T().WithResp(tokenRes); 

            req.RequestSet = r =>
            {
                r.Headers.Add("Accept", "application/json");
                if (r.Content != null)
                {
                    r.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };
                }
            };

            req.AddressUrl = string.Concat(req.AddressUrl,
                (req.AddressUrl.IndexOf('?') > 0 ? "&" : "?"),
                (appConfig.OperateMode == AppOperateMode.ByAgent ? "component_access_token=" : "access_token="),
                tokenRes.data);

            return await RestCommonJson<T>(req);
        }

        /// <summary>
        ///   下载文件方法
        /// </summary>
        protected async Task<WXFileResp> DownLoadFileAsync(OssHttpRequest req)
        {
            var configRes = await GetMeta();
            if (!configRes.IsSuccess())
                return new WXFileResp().WithResp(configRes);

            var appConfig = configRes.data;
            var tokenRes = await GetAccessToken(appConfig);
            if (!tokenRes.IsSuccess())
                return new WXFileResp().WithResp(tokenRes);

            req.AddressUrl = string.Concat(req.AddressUrl,
                (req.AddressUrl.IndexOf('?') > 0 ? "&" : "?"),
                (appConfig.OperateMode == AppOperateMode.ByAgent ? "component_access_token=" : "access_token="),
                tokenRes.data);

            var resp = await req.RestSend(WXPlatConfigProvider.ClientFactory?.Invoke());
            if (!resp.IsSuccessStatusCode)
                return new WXFileResp() { ret = (int)RespTypes.ObjectStateError, msg = "当前请求失败！" };

            var contentStr = resp.Content.Headers.ContentType.MediaType;
            using (resp)
            {
                if (!contentStr.Contains("application/json"))
                    return new WXFileResp()
                    {
                        content_type = contentStr,
                        file = await resp.Content.ReadAsByteArrayAsync()
                    };
                return JsonConvert.DeserializeObject<WXFileResp>(await resp.Content.ReadAsStringAsync());
            }
        }

        private static Task<StrResp> GetAccessToken(AppConfig config)
        {

            if (config.OperateMode == AppOperateMode.ByAgent)
            {
                if (WXPlatConfigProvider.AgentAccessTokenHub == null)
                    throw new NullReferenceException("WXPlatConfigProvider 下的 AccessTokenHub 属性需要配置，设置统一AccessToken获取方法。");

                return WXPlatConfigProvider.AgentAccessTokenHub.GetAccessTokenByAgentProxy(config);
            }
            if (WXPlatConfigProvider.AccessTokenHub == null)
                throw new NullReferenceException("WXPlatConfigProvider 下的 AccessTokenHub 属性需要配置，设置统一AccessToken获取方法。");

            return WXPlatConfigProvider.AccessTokenHub.GetAccessToken(config);
        }

        #endregion



        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <returns>实体类型</returns>
        protected static async Task<T> RestCommonJson<T>(OssHttpRequest request)
            where T : WXBaseResp, new()
        {
            var resp = await request.RestSend(WXPlatConfigProvider.ClientFactory?.Invoke());
            if (!resp.IsSuccessStatusCode)
                return new T()
                {
                    ret = -(int)resp.StatusCode,
                    msg = resp.ReasonPhrase
                };

            var contentStr = await resp.Content.ReadAsStringAsync();
            var t = JsonConvert.DeserializeObject<T>(contentStr);

            if (!t.IsSuccess())
                t.msg = t.errmsg;

            return t;
        }


        /// <inheritdoc />
        protected override AppConfig GetDefaultMeta()
        {
            return WXPlatConfigProvider.DefaultConfig;
        }
    }


}
