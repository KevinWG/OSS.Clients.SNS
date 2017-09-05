#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 微信开放平台相关授权接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Agent.Mos;

namespace OSS.SnsSdk.Official.Wx.Agent
{

    /// <summary>
    ///  第三方代理平台授权接口
    /// </summary>
    public class WxAgentAuthApi:WxAgentBaseApi
    {
        /// <summary>
        ///  获取预授权码pre_auth_code
        /// </summary>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <returns></returns>
        public async Task<WxGetPreAuthCodeResp> GetPreAuthCode(string verifyTicket)
        {
            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_create_preauthcode",
                HttpMothed = HttpMothed.GET,
                CustomBody = $"{{\"component_appid\":\"{ApiConfig.AppId}\"}}"
            };

            return await RestCommonAgentAsync<WxGetPreAuthCodeResp>(req, verifyTicket);
        }





    }
}
