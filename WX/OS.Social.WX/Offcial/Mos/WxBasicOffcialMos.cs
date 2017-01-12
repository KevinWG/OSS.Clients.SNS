using System;
using System.Collections.Generic;

namespace OS.Social.WX.Offcial.Mos
{
    /// <summary>
    ///   公众号功能接口accesstoken信息
    /// </summary>
    public class WxOffcialAccessTokenResp:WxBaseResp
    {
        /// <summary>
        ///   token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 	凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 过期时间，接口获取数据后根据expires_in 计算的值( 扣除十分钟，作为中间的缓冲值)
        /// </summary>
        public DateTime expires_date { get; set; }


    }


    /// <summary>
    /// 获取微信服务器ip列表响应实体
    /// </summary>
    public class WxIpListResp : WxBaseResp
    {
        /// <summary>
        ///   ip列表
        /// </summary>
        public List<string> ip_list { get; set; }
    }

}
