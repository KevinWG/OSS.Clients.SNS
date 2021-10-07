﻿using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http;

namespace OSS.Clients.Platform.WX
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
        /// <param name="apiRoute">接口路由地址</param>
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
            get { return _appConfig ?? WechatPlatformHelper.DefaultAppSecret;}
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
                if (file_paras==null||!file_paras.Any())
                {
                    httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
                        { CharSet = "UTF-8" };
                }
            }
        }
    }

    public abstract class WechatBaseReq<TRes> : WechatBaseReq
        where TRes : WechatBaseResp, new()
    {
        protected WechatBaseReq(HttpMethod method) : base(method)
        {
        }
    }

    /// <summary>
    ///  附带AccessToken的请求
    /// </summary>
    /// <typeparam name="TRes"></typeparam>
    public abstract class WechatBaseTokenReq<TRes> : WechatBaseReq<TRes>
        where TRes : WechatBaseResp, new()
    {
        protected WechatBaseTokenReq(HttpMethod method) : base(method)
        {
        }
    }

    ///// <summary>
    /////  附带 ComponentAccessToken 的请求
    ///// </summary>
    ///// <typeparam name="TRes"></typeparam>
    //public abstract class WechatBaseComponentTokenReq<TRes> : WechatBaseReq<TRes>
    //    where TRes : WechatBaseResp, new()
    //{
    //    protected WechatBaseComponentTokenReq(HttpMethod method) : base(method)
    //    {
    //    }
    //}



    /// <summary>
    /// 接口返回基础实例
    /// </summary>
    public class WechatBaseResp : Resp
    {
        private int m_errcode = 0;

        /// <summary>
        ///  错误代码
        /// </summary>
        public int errcode
        {
            get => m_errcode;
            set
            {
                m_errcode = value;
                if (m_errcode != 0)
                {
                    ret = -m_errcode;
                }
            }
        }

        /// <summary>
        ///   错误信息
        /// </summary>
        public string errmsg
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}
