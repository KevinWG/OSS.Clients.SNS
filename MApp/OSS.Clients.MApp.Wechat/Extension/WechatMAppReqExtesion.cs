using OSS.Tools.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.MApp.Wechat
{
    public static class WechatMAppReqExtesion
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


        /// <summary>
        ///  发送接口请求（默认json格式化
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Task<TResp> SendAsync<TResp>(this WechatMAppBaseReq<TResp> req)
            where TResp : WechatMAppBaseResp, new()
        {
            return SendAsync(req, JsonFormator<TResp>);
        }


        #region 辅助方法

        /// <summary>
        ///  发送接口请求
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="req"></param>
        /// <param name="formator">HttpResponseMessage 响应格式化提供者</param>
        /// <returns></returns>
        private static async Task<TResp> SendAsync<TResp>(WechatMAppBaseReq<TResp> req,Func<HttpResponseMessage,Task<TResp>> formator)
            where TResp : WechatMAppBaseResp, new()
        {
            if (formator == null)
                throw new ArgumentNullException(nameof(formator), "接口响应格式化方法不能为空!");

            if (req.app_config == null)
                throw new NotImplementedException("微信接口请求配置信息为空，请设置!");
            
            req.address_url = string.Concat(WechatMAppHelper.ApiHost, req.GetApiPath());

            var client = WechatMAppHelper.HttpClientProvider?.Invoke();
            var resp   = await (client == null ? ((OssHttpRequest)req).SendAsync() : client.SendAsync(req));

            return await formator(resp);
        }

        // Json 格式化处理
        private static async Task<T> JsonFormator<T>(HttpResponseMessage resp)
            where T : WechatMAppBaseResp, new()
        {
            var content = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                return new T()
                {
                    ret = -(int)resp.StatusCode,
                    msg = string.Concat(resp.ReasonPhrase, "(", content, ")")
                };

            return string.IsNullOrEmpty(content)
                ? new T().WithResp(SysRespTypes.NetworkError, $"微信接口返回空信息({resp.ReasonPhrase})")
                : JsonConvert.DeserializeObject<T>(content);
        }

        #endregion


    }
}
