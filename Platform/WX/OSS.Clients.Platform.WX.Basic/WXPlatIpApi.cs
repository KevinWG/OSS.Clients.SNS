#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 微信IP地址接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-2
*       
*****************************************************************************/

#endregion

using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Mos;
using System.Net.Http;
using System.Threading.Tasks;
using OSS.Common.BasicImpls;

namespace OSS.Clients.Platform.WX.Basic
{
    /// <summary>
    /// 微信服务器Ip信息API
    /// </summary>
    public class WXPlatIpApi : WXPlatBaseApi
    {
        /// <inheritdoc />
        public WXPlatIpApi(IMetaProvider<AppConfig> configProvider=null):base(configProvider){ 
        }

        /// <summary>
        /// 获取微信服务器列表
        /// </summary>
        /// <returns></returns>
        public async Task<WXIpListResp> GetWXIpListAsync()
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/getcallbackip")
            };

            return await RestCommonPlatAsync<WXIpListResp>(req);
        }
    }
}
