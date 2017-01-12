using OS.Common.ComModels;

namespace OS.Social.WX
{
    /// <summary>
    ///  授权客户端类型
    /// </summary>
    public enum AuthClientType
    {
        /// <summary>
        /// PC网页版
        /// </summary>
        PC=1,

        /// <summary>
        /// 微信公众号
        /// </summary>
        WxOffcial=2
    }

    /// <summary>
    /// 接口返回基础实例
    /// </summary>
    public class WxBaseResp : ResultMo
    {
        private int m_errcode = 0;

        /// <summary>
        ///  错误代码
        /// </summary>
        public int errcode
        {
            get { return m_errcode; }
            set
            {
                m_errcode = value;
                if (m_errcode > 0)
                {
                    Ret = m_errcode;
                }
            }
        }

        /// <summary>
        ///   错误信息
        /// </summary>
        public string errmsg { get; set; }
    }

    /// <summary>
    ///  泛型的返回实体
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class WxBaseResp<Type> : WxBaseResp
    {


        /// <summary>
        /// 数据实体
        /// </summary>
        public Type Data { get; set; }
    }

    /// <summary>
    /// 微信公众平台配置
    /// </summary>
    public class WxAppCoinfig
    {
        /// <summary>
        ///   应用来源
        /// 如果填写,授权回调时在state中会有赋值
        /// </summary>
        public string AppSource { get; set; }

        /// <summary>
        /// 公众账号AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 公众账号AppSecret
        /// </summary>
        public string AppSecret { get; set; }
    }
}
