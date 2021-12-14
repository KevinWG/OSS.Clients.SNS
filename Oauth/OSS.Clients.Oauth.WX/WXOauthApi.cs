#region Copyright (C) 2016 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：Oauth2.0  授权基类
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.Resp;
using OSS.Common.Extension;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Oauth.WX
{
    /// <summary>
    /// oauth 授权接口
    /// </summary>
    public class WXOauthApi:WXOauthBaseApi
    {
        /// <inheritdoc />
        public WXOauthApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
        {
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="redirectUri">授权后重定向的回调链接地址</param>
        /// <param name="state"> 需要回传的值 </param>
        /// <param name="type">授权客户端类型，如果是pc，则生成的是微信页面二维码授权页</param>
        /// <returns>授权的地址【如果 appConfig.OperateMode==AppOperateMode.ByAgent 回调除了 code 和 state 参数，还会有appid 】</returns>
        public async Task<StrResp> GetAuthorizeUrl(string redirectUri,string state, AuthClientType type)
        {
            if (redirectUri.Contains("://"))
                redirectUri = redirectUri.UrlEncode();

            var authUrl=new StringBuilder("https://open.weixin.qq.com/connect/");

            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new StrResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            if (type == AuthClientType.Web)
            {
                authUrl.Append("qrconnect?appid=").Append(appConfig.AppId)
                    .Append("&redirect_uri=").Append(redirectUri)
                    .Append("&response_type=code&scope=snsapi_login");
            }
            else
            {
                var scope = type == AuthClientType.InnerSilence ? "snsapi_base" : "snsapi_userinfo";

                authUrl.Append("oauth2/authorize?appid=").Append(appConfig.AppId)
                    .Append("&redirect_uri=").Append(redirectUri)
                    .Append("&response_type=code&scope=").Append(scope);
            }
            authUrl.Append("&state=").Append(state);

            if (appConfig.OperateMode==AppOperateMode.ByAgent)
            {
                authUrl.Append("&component_appid=").Append(appConfig.ByAppId);
            }

            authUrl.Append("#wechat_redirect");
            return new StrResp(authUrl.ToString());
        }

        /// <summary>
        /// 获取授权access_token   (每个用户都是单独唯一)
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <returns></returns>
        public async Task<WXGetOauthAccessTokenResp> GetOauthAccessTokenAsync(string code)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetOauthAccessTokenResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var reqUrl=new StringBuilder(m_ApiUrl);
            if (appConfig.OperateMode == AppOperateMode.ByAgent)
            {
                var comAccessToken = WXOauthConfigProvider.AgentAccessTokenFunc?.Invoke(appConfig);
                if (string.IsNullOrEmpty(comAccessToken))
                {
                    throw new ArgumentNullException("AgentAccessToken", "AgentAccessToken未发现，请检查 WXOauthConfigProvider 下 AgentAccessTokenFunc 委托是否为空或者返回值不正确！");
                }
                reqUrl.Append("/sns/oauth2/component/access_token?appid=").Append(appConfig.AppId)
                    .Append("&code=").Append(code)
                    .Append("&grant_type=authorization_code")
                    .Append("&component_appid=").Append(appConfig.ByAppId)
                    .Append("&component_access_token=").Append(comAccessToken);
            }
            else
            {
                reqUrl.Append("/sns/oauth2/access_token?appid=").Append(appConfig.AppId)
                    .Append("&secret=").Append(appConfig.AppSecret)
                    .Append("&code=").Append(code)
                    .Append("&grant_type=authorization_code");
            }

            var req = new OssHttpRequest
            {
                AddressUrl = reqUrl.ToString(),
                HttpMethod = HttpMethod.Get
            };
            
            return await RestCommonJson<WXGetOauthAccessTokenResp>(req);
        }

        /// <summary>
        ///   刷新当前用户授权Token
        /// </summary>
        /// <param name="refreshToken">授权接口刷新调用凭证</param>
        /// <returns></returns>
        public async Task<WXGetOauthAccessTokenResp> RefreshOauthAccessTokenAsync(string refreshToken)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetOauthAccessTokenResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;

            var reqUrl = new StringBuilder(m_ApiUrl);
            if (appConfig.OperateMode == AppOperateMode.ByAgent)
            {
                var comAccessToken = WXOauthConfigProvider.AgentAccessTokenFunc?.Invoke(appConfig);
                if (string.IsNullOrEmpty(comAccessToken))
                {
                    throw new ArgumentNullException("AgentAccessToken", "AgentAccessToken未发现，请检查 WXOauthConfigProvider 下 AgentAccessTokenFunc 委托是否为空或者返回值不正确！");
                }
                reqUrl.Append("/sns/oauth2/component/refresh_token?appid=").Append(appConfig.AppId)
                    .Append("&grant_type=refresh_token")
                    .Append("&component_appid=").Append(appConfig.ByAppId) 
                    .Append("&component_access_token=").Append(comAccessToken);
            }
            else
            {
                reqUrl.Append("/sns/oauth2/refresh_token?appid=").Append(appConfig.AppId)
                          .Append("&grant_type=refresh_token");
            }
            reqUrl.Append("&refresh_token=").Append(refreshToken);


            var request = new OssHttpRequest
            {
                AddressUrl = reqUrl.ToString(),
                HttpMethod = HttpMethod.Get
            };
            
            return await RestCommonJson<WXGetOauthAccessTokenResp>(request);
        }
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public async Task<WXGetOauthUserResp> GetWXOauthUserInfoAsync(string accessToken, string openId)
        {
            var request = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/sns/userinfo?access_token={accessToken}&openid={openId}",
                HttpMethod = HttpMethod.Get
            };

            return await RestCommonJson<WXGetOauthUserResp>(request);
        }



        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public async Task<WXBaseResp> CheckOauthAccessTokenAsync(string accessToken, string openId)
        {
            string url = $"{m_ApiUrl}/sns/auth?access_token={accessToken}&openid={openId}";

            var request = new OssHttpRequest
            {
                AddressUrl = url,
                HttpMethod = HttpMethod.Get
            };

            return await RestCommonJson<WXBaseResp>(request);
        }

    }
}
