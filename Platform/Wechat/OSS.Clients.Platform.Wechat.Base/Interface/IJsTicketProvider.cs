#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Common;
using OSS.Common.Resp;

namespace OSS.Clients.Platform.Wechat
{
    /// <summary>
    ///  JSTicket统一管理接口
    /// </summary>
    public interface IJsTicketProvider
    {
        /// <summary>
        ///   JSTicket统一管理接口
        /// </summary>
        Task<StrResp> GetJsTicket(WechatJsTicketType type, IAccessSecret appConfig);
    }
}