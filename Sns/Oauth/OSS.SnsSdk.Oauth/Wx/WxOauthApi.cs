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

using System.Threading.Tasks;
using OSS.Common.ComModels;
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
        /// <param name="redirectUri">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="type">授权客户端类型，如果是pc，则生成的是微信页面二维码授权页</param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string redirectUri, AuthClientType type)
        {

            if (type == AuthClientType.PC)
            {
                return
                    $"https://open.weixin.qq.com/connect/qrconnect?appid={ApiConfig.AppId}&redirect_uri={redirectUri}&response_type=code&scope=snsapi_login&state={ApiConfig.AppSource}#wechat_redirect";

            }
            var scope = type == AuthClientType.WxSilence ? "snsapi_base" : "snsapi_userinfo";
            return
                $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={ApiConfig.AppId}&redirect_uri={redirectUri}&response_type=code&scope={scope}&state={ApiConfig.AppSource}#wechat_redirect";

        }

        /// <summary>
        /// 获取授权access_token   (每个用户都是单独唯一)
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <returns></returns>
        public async Task<WxGetOauthAccessTokenResp> GetOauthAccessTokenAsync(string code)
        {
            var req = new OsHttpRequest
            {
                AddressUrl =
                    $"{m_ApiUrl}/sns/oauth2/access_token?appid={ApiConfig.AppId}&secret={ApiConfig.AppSecret}&code={code}&grant_type=authorization_code",
                HttpMothed = HttpMothed.GET
            };


            return await RestCommonJson<WxGetOauthAccessTokenResp>(req);
        }

        /// <summary>
        ///   刷新当前用户授权Token
        /// </summary>
        /// <param name="accessToken">授权接口调用凭证</param>
        /// <returns></returns>
        public async Task<WxGetOauthAccessTokenResp> RefreshOauthAccessTokenAsync(string accessToken)
        {
            var request = new OsHttpRequest
            {
                AddressUrl =
                    $"{m_ApiUrl}/sns/oauth2/refresh_token?appid={ApiConfig.AppId}&grant_type=refresh_token&refresh_token={accessToken}",
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
