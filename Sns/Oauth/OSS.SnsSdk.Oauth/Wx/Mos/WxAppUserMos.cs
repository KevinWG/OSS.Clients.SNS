#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：小程序接口 —— 用户模块接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

namespace OSS.SnsSdk.Oauth.Wx.Mos
{
    public class WxGetSessionCodeResp : WxBaseResp
    {
        /// <summary>   
        ///    用户唯一标识
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///    会话密钥
        /// </summary>  
        public string session_key { get; set; }

    }
}
