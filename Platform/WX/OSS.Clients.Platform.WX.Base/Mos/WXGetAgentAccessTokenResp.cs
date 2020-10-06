using System;
using System.Collections.Generic;
using System.Text;

namespace OSS.Clients.Platform.WX.Base.Mos
{
    /// <summary>
    /// 获取第三方代理平台的AccessToken响应实体
    /// </summary>
    public class WXGetAgentAccessTokenResp : WXBaseResp
    {
        /// <summary>   
        ///   第三方平台access_token
        /// </summary>  
        public string component_access_token { get; set; }

        /// <summary>   
        ///   有效期,两个小时
        /// </summary>  
        public int expires_in { get; set; }

        /// <summary>
        /// 【UTC】过期时间戳，接口获取数据后根据expires_in 计算的值( 扣除十分钟，作为中间的缓冲值)
        /// </summary>
        public long expires_date { get; set; }
    }
}
