#region MyRegion

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 获取平台授权token相关实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion
namespace OSS.SnsSdk.Official.Wx.Platform.Mos
{
    /// <summary>
    ///  获取平台的AccessToken请求实体
    /// </summary>
    public class WxGetPlatformAccessTokenReq
    {
        /// <summary>   
        ///   第三方平台appid
        /// </summary>  
        public string component_appid { get; set; }

        /// <summary>   
        ///   第三方平台appsecret
        /// </summary>  
        public string component_appsecret { get; set; }

        /// <summary>   
        ///   微信后台推送的ticket，此ticket会定时推送，具体请见本页的推送说明
        /// </summary>  
        public string component_verify_ticket { get; set; }
    }

    /// <summary>
    /// 获取平台的AccessToken响应实体
    /// </summary>
    public class WxGetPlatformAccessTokenResp: WxBaseResp
    {
        /// <summary>   
        ///   第三方平台access_token
        /// </summary>  
        public string component_access_token { get; set; }

        /// <summary>   
        ///   有效期,两个小时
        /// </summary>  
        public string expires_in { get; set; }

        /// <summary>
        /// 【UTC】过期时间，接口获取数据后根据expires_in 计算的值( 扣除十分钟，作为中间的缓冲值)
        /// </summary>
        public long expires_date { get; set; }
    }
}
