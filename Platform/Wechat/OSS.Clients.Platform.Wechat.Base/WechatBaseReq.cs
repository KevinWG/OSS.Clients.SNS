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

using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http;

namespace OSS.Clients.Platform.Wechat
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
            get { return _appConfig ?? WechatPlatformHelper.DefaultAppSecret;}
            internal set { _appConfig = value; }
        }

        #endregion

        /// <summary>
        /// 获取请求地址
        /// </summary>
        /// <returns></returns>
        public abstract string GetApiPath();


        /// <inheritdoc />
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

    /// <summary>
    ///   微信请求基类
    /// </summary>
    /// <typeparam name="TRes"></typeparam>
    public abstract class WechatBaseReq<TRes> : WechatBaseReq
        where TRes : WechatBaseResp, new()
    {
        /// <inheritdoc />
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
        /// <inheritdoc />
        protected WechatBaseTokenReq(HttpMethod method) : base(method)
        {
        }
    }

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
