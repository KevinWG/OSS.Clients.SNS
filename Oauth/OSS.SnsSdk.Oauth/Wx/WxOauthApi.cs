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
using System.Text;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Http.Mos;
using OSS.SnsSdk.Oauth.Wx.Mos;

namespace OSS.SnsSdk.Oauth.Wx
{
    /// <summary>
    /// oauth 授权接口
    /// </summary>
    public class WxOauthApi:WxOauthBaseApi
    {
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOauthApi(AppConfig config=null) : base(config)
        {
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="redirectUri">授权后重定向的回调链接地址</param>
        /// <param name="state"> 需要回传的值 </param>
        /// <param name="type">授权客户端类型，如果是pc，则生成的是微信页面二维码授权页</param>
        /// <returns>授权的地址【如果 ApiConfig.OperateMode==AppOperateMode.ByAgent 回调除了 code 和 state 参数，还会有appid 】</returns>
        public string GetAuthorizeUrl(string redirectUri,string state, AuthClientType type)
        {
            if (redirectUri.Contains("://"))
                redirectUri = redirectUri.UrlEncode();

            var authUrl=new StringBuilder("https://open.weixin.qq.com/connect/");
       

            if (type == AuthClientType.Web)
            {
                authUrl.Append("qrconnect?appid=").Append(ApiConfig.AppId)
                    .Append("&redirect_uri=").Append(redirectUri)
                    .Append("&response_type=code&scope=snsapi_login");
            }
            else
            {
                var scope = type == AuthClientType.InnerSilence ? "snsapi_base" : "snsapi_userinfo";

                authUrl.Append("oauth2/authorize?appid=").Append(ApiConfig.AppId)
                    .Append("&redirect_uri=").Append(redirectUri)
                    .Append("&response_type=code&scope=").Append(scope);
            }
            authUrl.Append("&state=").Append(state);

            if (ApiConfig.OperateMode==AppOperateMode.ByAgent)
            {
                authUrl.Append("&component_appid=").Append(ApiConfig.ByAppId);
            }

            authUrl.Append("#wechat_redirect");
            return authUrl.ToString();
        }

        /// <summary>
        /// 获取授权access_token   (每个用户都是单独唯一)
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <returns></returns>
        public async Task<WxGetOauthAccessTokenResp> GetOauthAccessTokenAsync(string code)
        {
            var reqUrl=new StringBuilder(m_ApiUrl);
            if (ApiConfig.OperateMode == AppOperateMode.ByAgent)
            {
                var comAccessToken = WxOauthConfigProvider.AgentAccessTokenFunc?.Invoke(ApiConfig);
                if (string.IsNullOrEmpty(comAccessToken))
                {
                    throw new ArgumentNullException("AgentAccessToken", "AgentAccessToken未发现，请检查 WxOauthConfigProvider 下 AgentAccessTokenFunc 委托是否为空或者返回值不正确！");
                }
                reqUrl.Append("/sns/oauth2/component/access_token?appid=").Append(ApiConfig.AppId)
                    .Append("&code=").Append(code)
                    .Append("&grant_type=authorization_code")
                    .Append("&component_appid=").Append(ApiConfig.ByAppId)
                    .Append("&component_access_token=").Append(comAccessToken);
            }
            else
            {
                reqUrl.Append("/sns/oauth2/access_token?appid=").Append(ApiConfig.AppId)
                    .Append("&secret=").Append(ApiConfig.AppSecret)
                    .Append("&code=").Append(code)
                    .Append("&grant_type=authorization_code");
            }

            var req = new OsHttpRequest
            {
                AddressUrl = reqUrl.ToString(),
                HttpMothed = HttpMothed.GET
            };
            
            return await RestCommonJson<WxGetOauthAccessTokenResp>(req);
        }

        /// <summary>
        ///   刷新当前用户授权Token
        /// </summary>
        /// <param name="refreshToken">授权接口刷新调用凭证</param>
        /// <returns></returns>
        public async Task<WxGetOauthAccessTokenResp> RefreshOauthAccessTokenAsync(string refreshToken)
        {
            var reqUrl = new StringBuilder(m_ApiUrl);
            if (ApiConfig.OperateMode == AppOperateMode.ByAgent)
            {
                var comAccessToken = WxOauthConfigProvider.AgentAccessTokenFunc?.Invoke(ApiConfig);
                if (string.IsNullOrEmpty(comAccessToken))
                {
                    throw new ArgumentNullException("AgentAccessToken", "AgentAccessToken未发现，请检查 WxOauthConfigProvider 下 AgentAccessTokenFunc 委托是否为空或者返回值不正确！");
                }
                reqUrl.Append("/sns/oauth2/component/refresh_token?appid=").Append(ApiConfig.AppId)
                    .Append("&grant_type=refresh_token")
                    .Append("&component_appid=").Append(ApiConfig.ByAppId) 
                    .Append("&component_access_token=").Append(comAccessToken);
            }
            else
            {
                reqUrl.Append("/sns/oauth2/refresh_token?appid=").Append(ApiConfig.AppId)
                          .Append("&grant_type=refresh_token");
            }
            reqUrl.Append("&refresh_token=").Append(refreshToken);


            var request = new OsHttpRequest
            {
                AddressUrl = reqUrl.ToString(),
                HttpMothed = HttpMothed.GET
            };
            
            return await RestCommonJson<WxGetOauthAccessTokenResp>(request);
        }
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public async Task<WxGetOauthUserResp> GetWxOauthUserInfoAsync(string accessToken, string openId)
        {
            var request = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/sns/userinfo?access_token={accessToken}&openid={openId}",
                HttpMothed = HttpMothed.GET
            };

            return await RestCommonJson<WxGetOauthUserResp>(request);
        }



        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public async Task<WxBaseResp> CheckOauthAccessTokenAsync(string accessToken, string openId)
        {
            string url = $"{m_ApiUrl}/sns/auth?access_token={accessToken}&openid={openId}";

            var request = new OsHttpRequest
            {
                AddressUrl = url,
                HttpMothed = HttpMothed.GET
            };

            return await RestCommonJson<WxBaseResp>(request);
        }

    }
}
