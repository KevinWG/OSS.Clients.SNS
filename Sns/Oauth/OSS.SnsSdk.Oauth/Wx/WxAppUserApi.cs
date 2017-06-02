#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：小程序接口 —— 用户模块接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Http.Mos;
using OSS.SnsSdk.Oauth.Wx.Mos;

namespace OSS.SnsSdk.Oauth.Wx
{
    /// <summary>
    ///  小程序用户相关接口
    /// </summary>
    public class WxAppUserApi : WxBaseApi
    {
        public WxAppUserApi(AppConfig config) : base(config)
        {
        }

        #region  登录接口

        /// <summary>
        ///   获取会话code接口
        /// </summary>
        /// <param name="jsCode"></param>
        /// <returns></returns>
        public async Task<WxGetSessionCodeResp> GetSessionCodeAsync(string jsCode)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, $"/sns/jscode2session?appid={ApiConfig.AppId}&secret={ApiConfig.AppSecret}&js_code={jsCode}&grant_type=authorization_code");

            return await RestCommonJson<WxGetSessionCodeResp>(req);
        }

        #endregion
    }
}
