#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公众号功能接口accesstoken信息实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016  
*       
*****************************************************************************/

#endregion

using OSS.Clients.Platform.WX.Base.Mos;
using System.Collections.Generic;

namespace OSS.Clients.Platform.WX.Basic.Mos
{


    /// <summary>
    /// 获取微信服务器ip列表响应实体
    /// </summary>
    public class WXIpListResp : WXBaseResp
    {
        /// <summary>
        ///   ip列表
        /// </summary>
        public List<string> ip_list { get; set; }
    }

}
