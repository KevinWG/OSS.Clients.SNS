using OSS.Tools.Http;
using System.Net.Http;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using System.Net.Http.Headers;
using System.Linq;

namespace OSS.Clients.MApp.Wechat
{
    /// <summary>
    ///  请求基类
    /// </summary>
    public abstract class WechatBaseReq : OssHttpRequest
    {
        /// <summary>
        /// 接口请求
        /// </summary>
        /// <param name="method">请求方法</param>
        protected WechatBaseReq(HttpMethod method)
        {
            http_method = method;
        }

        #region 秘钥配置信息

        private IAppSecret _appConfig;
        /// <summary>
        ///  秘钥配置信息
        /// </summary>
        public IAppSecret app_config
        {
            get { return _appConfig ?? WechatMAppHelper.DefaultConfig; }
            internal set { _appConfig = value; }
        }

        #endregion

        /// <summary>
        /// 获取请求地址
        /// </summary>
        /// <returns></returns>
        public abstract string GetApiPath();


        protected override void OnSending(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.Add("Accept", "application/json");
            if (http_method != HttpMethod.Get && httpRequestMessage.Content != null)
            {
                if (file_paras == null || !file_paras.Any())
                {
                    httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
                        { CharSet = "UTF-8" };
                }
            }
        }
    }


    public abstract class WechatMAppBaseReq<TResp> : WechatBaseReq
        where TResp : WechatMAppBaseResp
    {
        public WechatMAppBaseReq(HttpMethod method) : base(method)
        {
        }
    }
    
    public class WechatMAppBaseResp : Resp
    {
        private int _errCode;

        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode
        {
            get { return _errCode; }
            set { ret = _errCode = value; }
        }

        private string _errMsg;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg
        {
            get { return _errMsg; }
            set { msg = _errMsg = value; }
        }
    }
}
