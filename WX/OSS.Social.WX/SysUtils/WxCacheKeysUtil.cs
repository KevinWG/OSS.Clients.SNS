namespace OSS.Social.WX.SysUtils
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
