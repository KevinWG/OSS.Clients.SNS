using System.Net.Http;
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

        #region 应用秘钥配置信息

        /// <summary>
        ///  公众号/小程序 应用秘钥配置信息
        /// </summary>
        public IAppSecret app_config { get; internal set; }

        #endregion


        protected abstract string GetApiUrl();

        protected override void PrepareSend()
        {
            
        }

        protected override void OnSending(HttpRequestMessage httpRequestMessage)
        {
            base.OnSending(httpRequestMessage);
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
            get { return msg;}
            set { msg = value; }
        }
    }
}
