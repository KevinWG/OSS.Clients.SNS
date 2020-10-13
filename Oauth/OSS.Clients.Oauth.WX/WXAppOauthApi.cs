#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：小程序接口 —— 用户模块接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Threading.Tasks;
using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Oauth.WX
{
    /// <summary>
    ///  小程序用户相关接口
    /// </summary>
    public class WXAppOauthApi : WXOauthBaseApi
    {
        /// <inheritdoc />
        public WXAppOauthApi(IMetaProvider<AppConfig> configProvider=null):base(configProvider)
        {
        }

        #region  登录接口

        /// <summary>
        ///   获取会话code接口
        /// </summary>
        /// <param name="jsCode"></param>
        /// <returns></returns>
        public async Task<WXGetSessionCodeResp> GetSessionCodeAsync(string jsCode)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetSessionCodeResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl,
                    $"/sns/jscode2session?appid={appConfig.AppId}&secret={appConfig.AppSecret}&js_code={jsCode}&grant_type=authorization_code")
            };
            return await RestCommonJson<WXGetSessionCodeResp>(req);
        }

        #endregion
    }
}
