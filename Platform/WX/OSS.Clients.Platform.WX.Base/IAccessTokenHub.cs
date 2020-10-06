#region Copyright (C) 2020 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口基类，获取AccessToken 接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2020-10-7
*       
*****************************************************************************/

#endregion
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;

namespace OSS.Clients.Platform.WX.Base
{
    /// <summary>
    ///  AccessToken统一管理接口
    /// </summary>
    public interface IAccessTokenHub
    {
        /// <summary>
        ///   获取AccessToken（从统一缓存或Token中心获取
        /// </summary>
        Task<Resp<string>> GetAccessToken(AppConfig config);
    }

    /// <summary>
    ///  【代理平台】 AccessToken统一管理接口
    /// </summary>
    public interface IAgentAccessTokenHub
    {
        /// <summary>
        /// 当自身是第三方【代理平台】时，对应的 OperateMode = ByAgent 时，获取被代理的应用AccessToken
        /// </summary>
        Task<Resp<string>> GetAccessTokenByAgentProxy(AppConfig config) ;

        /// <summary>
        /// 当自身是第三方【代理平台】时，获取第三方Agent的VerifyTicket（由微信通过消息接口推送给平台方）
        /// </summary>
        Task<Resp<string>> GetAgentVerifyTicket(AppConfig config);
    }

    /// <summary>
    ///  JSTicket统一管理接口
    /// </summary>
    public interface IJsTicketHub
    {
        /// <summary>
        ///   JSTicket统一管理接口
        /// </summary>
        Task<Resp<string>> GetJsTicket(AppConfig config, WXJsTicketType type);
    }
}
