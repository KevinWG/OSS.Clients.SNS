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
using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;
using OSS.Common.BasicMos;

namespace OSS.Clients.Platform.Wechat
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
