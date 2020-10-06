#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：基础实体类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016   忘记哪一天
*       
*****************************************************************************/

#endregion

using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.Platform.WX.Base.Mos
{

    /// <summary>
    /// 接口返回基础实例
    /// </summary>
    public class WXBaseResp : Resp
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
                    ret = m_errcode;
                }
            }
        }

        /// <summary>
        ///   错误信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
