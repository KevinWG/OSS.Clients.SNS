#region Copyright (C) 2016 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：基础实体类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016   忘记哪一天
*       
*****************************************************************************/

#endregion


using OS.Common.ComModels;

namespace OSS.Social.WX
{
 

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
                if (m_errcode != 0)
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
