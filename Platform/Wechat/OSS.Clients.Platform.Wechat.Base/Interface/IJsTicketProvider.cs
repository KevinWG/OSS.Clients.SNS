using System.Threading.Tasks;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.Platform.WX
{
    /// <summary>
    ///  JSTicket统一管理接口
    /// </summary>
    public interface IJsTicketProvider
    {
        /// <summary>
        ///   JSTicket统一管理接口
        /// </summary>
        Task<StrResp> GetJsTicket(IAppSecret config, WechatJsTicketType type);
    }
}