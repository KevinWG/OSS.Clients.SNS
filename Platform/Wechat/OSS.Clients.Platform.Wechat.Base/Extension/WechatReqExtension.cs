#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OSS.Clients.Platform.Wechat
{
    /// <summary>
    ///  请求扩展方法
    /// </summary>
    public static class WechatReqExtension
    {
        /// <summary>
        ///  设置当前请求对应的秘钥配置信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="appConfig"></param>
        /// <returns></returns>
        public static TReq SetContextConfig<TReq>(this TReq req, IAppSecret appConfig)
            where TReq : WechatBaseReq
        {
            req.app_config = appConfig;
            return req;
        }

        #region 附带token请求

        ///// <summary>
        ///// 发送接口请求
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="funcFormat"></param>
        ///// <returns></returns>
        //public static async Task<TResp> SendAsync<TResp>(this WechatBaseComponentTokenReq<TResp> req)
        //    where TResp : WechatBaseResp, new()
        //{
        //    if (req.app_config == null)
        //        throw new NotImplementedException("微信接口请求配置信息为空，请设置!");

        //    var apiPath = req.GetApiPath();

        //    var accessTokenRes =
        //        await WechatPlatformHelper.ComponentAccessTokenProvider.GetComponentAccessToken(req.app_config);
        //    if (!accessTokenRes.IsSuccess())
        //        return new TResp().WithResp(accessTokenRes);

        //    var accessToken = accessTokenRes.data;
        //    if (string.IsNullOrEmpty(accessToken))
        //        return new TResp().WithResp(RespTypes.OperateFailed, "未能获取有效ComponentAccessToken！");

        //    req.address_url = string.Concat(WechatPlatformHelper.ApiHost, apiPath,
        //        (apiPath.IndexOf('?') > 0 ? "&" : "?"), "component_access_token=", accessToken);

        //    return await SendAsync(req, JsonFormat<TResp>);
        //}


        /// <summary>
        /// 发送接口请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Task<TResp> ExecuteAsync<TResp>(this WechatBaseTokenReq<TResp> req)
            where TResp : WechatBaseResp, new()
        {
            return ExecuteAsync(req, JsonFormat<TResp>);
        }

        /// <summary>
        /// 发送接口请求
        /// </summary>
        /// <param name="req"></param>
        /// <param name="funcFormat"></param>
        /// <returns></returns>
        public static async Task<TResp> ExecuteAsync<TResp>(this WechatBaseTokenReq<TResp> req, Func<HttpResponseMessage, Task<TResp>> funcFormat)
            where TResp : WechatBaseResp, new()
        {
            if (req.app_config == null)
            {
                throw new NotImplementedException("微信接口请求配置信息为空，请设置!");
            }

            var apiPath = req.GetApiPath();

            var accessTokenRes = await WechatPlatformHelper.AccessTokenProvider.GetAccessToken(req.app_config);
            if (!accessTokenRes.IsSuccess())
                return new TResp().WithResp(accessTokenRes);

            var accessToken = accessTokenRes.data;
            if (string.IsNullOrEmpty(accessToken))
                return new TResp().WithResp(RespTypes.OperateFailed, "未能获取有效AccessToken！");

            req.address_url = string.Concat(WechatPlatformHelper.ApiHost, apiPath,
                (apiPath.IndexOf('?') > 0 ? "&" : "?"), "access_token=", accessToken);

            return await SendAsync(req, funcFormat);
        }

        #endregion

        #region 不带token请求

        /// <summary>
        /// 发送接口请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Task<TResp> ExecuteAsync<TResp>(this WechatBaseReq<TResp> req)
            where TResp : WechatBaseResp, new()
        {
            return SendAsync(req, JsonFormat<TResp>); 
        }

        #endregion

        #region 辅助方法
        
        /// <summary>
        /// 发送接口请求
        /// </summary>
        /// <param name="req"></param>
        /// <param name="funcFormat"></param>
        /// <returns></returns>
        private static async Task<TResp> SendAsync<TResp>(WechatBaseReq<TResp> req,
            Func<HttpResponseMessage, Task<TResp>> funcFormat)
            where TResp : WechatBaseResp, new()
        {
            if (funcFormat == null)
                throw new ArgumentNullException(nameof(funcFormat), "接口响应格式化方法不能为空!");

            if (req.app_config == null)
            {
                throw new NotImplementedException("微信接口请求配置信息为空，请设置!");
            }

            req.address_url = string.Concat(WechatPlatformHelper.ApiHost, req.GetApiPath());

            var client = WechatPlatformHelper.HttpClientProvider?.Invoke();
            var resp   = await (client == null ? ((OssHttpRequest) req).SendAsync() : client.SendAsync(req));

            return await funcFormat(resp);
        }

        // Json 格式化处理
        private static async Task<T> JsonFormat<T>(HttpResponseMessage resp)
            where T : WechatBaseResp, new()
        {
            var content = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                return new T()
                {
                    ret = -(int) resp.StatusCode,
                    msg = string.Concat(resp.ReasonPhrase, "(", content, ")")
                };

            return string.IsNullOrEmpty(content)
                ? new T().WithResp(SysRespTypes.NetworkError, $"微信接口返回空信息({resp.ReasonPhrase})")
                : JsonConvert.DeserializeObject<T>(content);
        }

        #endregion
    }
}
