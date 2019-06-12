#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSS - Oauth 公用枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-18
*       
*****************************************************************************/

#endregion

namespace OSS.SnsSdk.Oauth.Wx.Mos
{
    /// <summary>
    ///  授权客户端类型
    /// </summary>
    public enum AuthClientType
    {
        /// <summary>
        /// PC网页版
        /// </summary>
        Web = 1,

        /// <summary>
        /// 官方应用内浏览器页面授权【微信公号，支付宝生活号】
        /// </summary>
        InnerWeb = 2,

        /// <summary>
        /// 应用内静默授权
        /// </summary>
        InnerSilence = 4
    }
}
