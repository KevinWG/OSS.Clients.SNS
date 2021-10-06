using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;
using OSS.Common.BasicMos;

namespace OSS.Clients.Platform.WX
{
    /// <summary>
    ///   access_token 统一管理接口
    /// </summary>
    public interface IAccessTokenProvider
    {
        /// <summary>
        ///   自定义获取 access_token 实现方法
        /// </summary>
        Task<StrResp> GetAccessToken(IAppSecret config);
    }
}
