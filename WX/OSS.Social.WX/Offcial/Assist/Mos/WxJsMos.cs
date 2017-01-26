#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述: 公号的功能辅助接口 —— js相关接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-24
*       
*****************************************************************************/

#endregion

using System;
using Newtonsoft.Json;

namespace OSS.Social.WX.Offcial.Assist.Mos
{
    public class WxGetJsTicketResp : WxBaseResp
    {
        /// <summary>   
        ///   签名所需凭证
        /// </summary>  
        public string ticket { get; set; }

        /// <summary>   
        ///   有效时间
        /// </summary>  
        public int expires_in { get; set; }

        /// <summary>
        ///   过期时间
        /// </summary>
        [JsonIgnore]
        public DateTime expires_time { get; set; }
    }


}
