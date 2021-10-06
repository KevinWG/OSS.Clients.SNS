using System.Threading.Tasks;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.Platform.WX
{
    /// <summary>
    ///  【服务商代理平台】 component_access_token 统一管理接口
    /// </summary>
    public interface IComponentAccessTokenProvider
    {
        /// <summary>
        ///  自定义获取 component_access_token 实现方法
        /// </summary>
        Task<StrResp> GetAccessTokenByAgentProxy(IAppSecret config);
    }
}