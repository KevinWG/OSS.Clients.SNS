﻿#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 微信开放平台相关授权接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OSS.Clients.Platform.WX.Agent.Mos;
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Common.Extension;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Agent
{
    /// <summary>
    ///  第三方代理平台授权接口
    /// </summary>
    public class WXAgentAuthApi: WXPlatBaseApi
    {
        /// <inheritdoc />
        public WXAgentAuthApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
        {
        }

        /// <summary>
        ///  获取预授权码pre_auth_code
        /// </summary>
        /// <returns></returns>
        private async Task<WXGetPreAuthCodeResp> GetPreAuthCode()
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetPreAuthCodeResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_create_preauthcode",
                HttpMethod = HttpMethod.Post,
                CustomBody = $"{{\"component_appid\":\"{appConfig.AppId}\"}}"
            };

            return await RestCommonPlatAsync<WXGetPreAuthCodeResp>(req);
        }

        /// <summary>
        /// 获取公众号/小程序授权地址
        /// </summary>
        /// <param name="redirectUrl">回调地址</param>
        /// <returns></returns>
        public async Task<StrResp> GetPreAuthUrl(string redirectUrl)
        {
            var preAuthCodeRes = await GetPreAuthCode();

            if (!preAuthCodeRes.IsSuccess())
                return new StrResp().WithResp(preAuthCodeRes);// preAuthCodeRes.ConvertToResult<string>();

            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new StrResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            if (redirectUrl.Contains("://"))
                redirectUrl = redirectUrl.UrlEncode();

            var authUrl=
                $"https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid={appConfig.AppId}&pre_auth_code={preAuthCodeRes.pre_auth_code}&redirect_uri={redirectUrl}";
            return new StrResp(authUrl);
        }

        /// <summary>
        /// 获取平台下当前授权账号的AccessToken响应实体
        /// </summary>
        /// <param name="authorizationCode">授权code,会在授权成功时返回给第三方平台</param>
        /// <returns></returns>
        public async Task<WXGetGrantedAccessTokenResp> GetGrantorAccessToken(string authorizationCode)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetGrantedAccessTokenResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;

            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"authorization_code\":\"").Append(authorizationCode).Append("\" }");

            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_query_auth",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonPlatAsync<WXGetGrantedAccessTokenResp>(req);
        }

        /// <summary>
        /// 获取平台下当前授权账号的AccessToken响应实体
        /// </summary>
        /// <param name="grantorRefreshToken">授权者的刷新Token</param>
        /// <param name="grantorAppId">授权者的Appid</param>
        /// <returns></returns>
        public async Task<WXRefreshGrantedAccessTokenResp> RefreshGrantorAccessToken(string grantorAppId,string grantorRefreshToken)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXRefreshGrantedAccessTokenResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"authorizer_refresh_token\":\"").Append(grantorRefreshToken).Append("\" }");

            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_authorizer_token",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonPlatAsync<WXRefreshGrantedAccessTokenResp>(req);
        }


        /// <summary>
        ///  获取公号的授权（账号+权限）信息
        /// </summary>
        /// <param name="grantorAppId"></param>
        /// <returns></returns>
        public async Task<WXGetGrantorInfoResp> GetGrantorInfo(string grantorAppId)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetGrantorInfoResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\"}");

            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_get_authorizer_info",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonPlatAsync<WXGetGrantorInfoResp>(req);
        }


        /// <summary>
        ///  获取授权方的选项设置信息
        /// location_report(地理位置上报选项)	0-无上报 1-进入会话时上报 2-每5s上报
        /// voice_recognize（语音识别开关选项）	0-关闭语音识别 1-开启语音识别
        /// customer_service（多客服开关选项）	0-关闭多客服 1-开启多客服
        /// </summary>
        /// <param name="grantorAppId">授权公众号或小程序的appid</param>
        /// <param name="optionName">选项名称</param>
        /// <returns></returns>
        public async Task<WXGetGrantorOptionResp> GetGrantorOption(string grantorAppId,string optionName)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetGrantorOptionResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"option_name\":\"").Append(optionName).Append("\"}");

            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_get_authorizer_option",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonPlatAsync<WXGetGrantorOptionResp>(req);
        }

        /// <summary>
        ///  设置授权方的选项信息
        /// location_report(地理位置上报选项)	0-无上报 1-进入会话时上报 2-每5s上报
        /// voice_recognize（语音识别开关选项）	0-关闭语音识别 1-开启语音识别
        /// customer_service（多客服开关选项）	0-关闭多客服 1-开启多客服
        /// </summary>
        /// <param name="grantorAppId">授权公众号或小程序的appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="optionValue">设置的选项值</param>
        /// <returns></returns>
        public async Task<WXBaseResp> SetGrantorOption(string grantorAppId, string optionName,string optionValue )
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXBaseResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"option_name\":\"").Append(optionName).Append("\",");
            strContent.Append("\"option_value\":\"").Append(optionValue).Append("\"}");

            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_set_authorizer_option",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        /// <summary>
        ///  获取授权者列表
        /// </summary>
        /// <param name="grantorAppId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<WXGetGrantorListResp> GetGrantorList(string grantorAppId, int offset, int count)
        {
            var appConfigRes = await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new WXGetGrantorListResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(appConfig.AppId).Append("\",");
            strContent.Append("\"offset\":\"").Append(offset).Append("\",");
            strContent.Append("\"count\":\"").Append(count).Append("\"}");

            var req = new OssHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_get_authorizer_list",
                HttpMethod = HttpMethod.Post,
                CustomBody = strContent.ToString()
            };

            return await RestCommonPlatAsync<WXGetGrantorListResp>(req);
        }

    }
}
