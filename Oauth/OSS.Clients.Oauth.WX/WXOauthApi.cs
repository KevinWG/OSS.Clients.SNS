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

using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.Resp;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OSS.Common.Extension;
using OSS.Tools.Http;

namespace OSS.Clients.Oauth.WX
{
    /// <summary>
    /// oauth 授权接口
    /// </summary>
    public class WXOauthApi:WXOauthBaseApi
    {
        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="redirectUri">授权后重定向的回调链接地址</param>
        /// <param name="state"> 需要回传的值 </param>
        /// <param name="type">授权客户端类型，如果是pc，则生成的是微信页面二维码授权页</param>
        /// <param name="proxiedSubAppId">服务商模式下 被代理的子应用Id</param>
        /// <returns>授权的地址【服务商模式下 回调除了 code 和 state 参数，还会有appid 】</returns>
        public async Task<StrResp> GetAuthorizeUrl(string redirectUri,string state, AuthClientType type, string proxiedSubAppId = "")
        {
            if (redirectUri.Contains("://"))
                redirectUri = redirectUri.SafeEscapeUriDataString();

            var authUrl=new StringBuilder("https://open.weixin.qq.com/connect/");

            var isAgentMode = !string.IsNullOrEmpty(proxiedSubAppId);

            var appConfig   = await GetAccessConfig();
            var appId       = isAgentMode ? proxiedSubAppId: appConfig.access_key  ;
      

            if (type == AuthClientType.Web)
            {
                authUrl.Append("qrconnect?appid=").Append(appId)
                    .Append("&redirect_uri=").Append(redirectUri)
                    .Append("&response_type=code&scope=snsapi_login");
            }
            else
            {
                var scope = type == AuthClientType.InnerSilence ? "snsapi_base" : "snsapi_userinfo";

                authUrl.Append("oauth2/authorize?appid=").Append(appId)
                    .Append("&redirect_uri=").Append(redirectUri)
                    .Append("&response_type=code&scope=").Append(scope);
            }
            authUrl.Append("&state=").Append(state);

            if (isAgentMode)
            {
                authUrl.Append("&component_appid=").Append(appConfig.access_key);
            }

            authUrl.Append("#wechat_redirect");
            return new StrResp(authUrl.ToString());
        }

        /// <summary>
        /// 获取授权access_token   (每个用户都是单独唯一)
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <param name="proxiedSubAppId">服务商模式下 被代理的子应用Id</param>
        /// <returns></returns>
        public async Task<WXGetOauthAccessTokenResp> GetOauthAccessTokenAsync(string code, string proxiedSubAppId = "")
        {
            var appConfig = await GetAccessConfig();
            var reqUrl=new StringBuilder(m_ApiUrl);

            if (!string.IsNullOrEmpty(proxiedSubAppId))
            {
                var comAccessToken = WXOauthHelper.AgentAccessTokenFunc?.Invoke(appConfig);
                if (string.IsNullOrEmpty(comAccessToken))
                {
                    throw new ArgumentNullException("AgentAccessToken", "AgentAccessToken未发现，请检查 WXOauthConfigProvider 下 AgentAccessTokenFunc 委托是否为空或者返回值不正确！");
                }
                reqUrl.Append("/sns/oauth2/component/access_token?appid=").Append(proxiedSubAppId)
                    .Append("&code=").Append(code)
                    .Append("&grant_type=authorization_code")
                    .Append("&component_appid=").Append(appConfig.access_key)
                    .Append("&component_access_token=").Append(comAccessToken);
            }
            else
            {
                reqUrl.Append("/sns/oauth2/access_token?appid=").Append(appConfig.access_key)
                    .Append("&secret=").Append(appConfig.access_secret)
                    .Append("&code=").Append(code)
                    .Append("&grant_type=authorization_code");
            }

            var req = new OssHttpRequest
            {
                address_url = reqUrl.ToString(),
                http_method = HttpMethod.Get
            };
            
            return await RestCommonJson<WXGetOauthAccessTokenResp>(req);
        }

        /// <summary>
        ///   刷新当前用户授权Token
        /// </summary>
        /// <param name="refreshToken">授权接口刷新调用凭证</param>
        /// <param name="proxiedSubAppId">服务商模式下 被代理的子应用Id</param>
        /// <returns></returns>
        public async Task<WXGetOauthAccessTokenResp> RefreshOauthAccessTokenAsync(string refreshToken,string proxiedSubAppId = "")
        {
            var appConfig = await GetAccessConfig();

            var reqUrl = new StringBuilder(m_ApiUrl);
            if (!string.IsNullOrEmpty(proxiedSubAppId))
            {
                var comAccessToken = WXOauthHelper.AgentAccessTokenFunc?.Invoke(appConfig);
                if (string.IsNullOrEmpty(comAccessToken))
                {
                    throw new ArgumentNullException("AgentAccessToken", "AgentAccessToken未发现，请检查 WXOauthConfigProvider 下 AgentAccessTokenFunc 委托是否为空或者返回值不正确！");
                }
                reqUrl.Append("/sns/oauth2/component/refresh_token?appid=").Append(proxiedSubAppId)
                    .Append("&grant_type=refresh_token")
                    .Append("&component_appid=").Append(appConfig.access_key) 
                    .Append("&component_access_token=").Append(comAccessToken);
            }
            else
            {
                reqUrl.Append("/sns/oauth2/refresh_token?appid=").Append(appConfig.access_key)
                          .Append("&grant_type=refresh_token");
            }
            reqUrl.Append("&refresh_token=").Append(refreshToken);


            var request = new OssHttpRequest
            {
                address_url =  reqUrl.ToString(),
                http_method = HttpMethod.Get
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
                address_url = $"{m_ApiUrl}/sns/userinfo?access_token={accessToken}&openid={openId}",
                http_method = HttpMethod.Get
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
                address_url = url,
                http_method = HttpMethod.Get
            };

            return await RestCommonJson<WXBaseResp>(request);
        }

    }
}
