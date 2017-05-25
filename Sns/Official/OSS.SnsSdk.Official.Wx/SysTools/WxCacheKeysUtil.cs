#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：微信的Social模块 - 缓存辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-18
*       
*****************************************************************************/

#endregion
namespace OSS.SnsSdk.Official.Wx.SysTools
{
    public static class WxCacheKeysUtil
    {
        /// <summary>
        ///  公众号的AccessToken 缓存 key   {0}=appid
        /// </summary>
        public const string OffcialAccessTokenKey = "social_wx_offcial_access_token_{0}";

        /// <summary>
        ///  公众号jsticket  缓存key
        /// {0}=appid   {1}=ticket type (jsapi,wx_card)
        /// </summary>
        public const string OffcialJsTicketKey = "social_wx_offcial_js_ticket_{0}_{1}";
    }
}
