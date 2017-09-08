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

using System.Text;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Extention;
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
        ///  构造函数
        /// </summary>
        /// <param name="config">第三方代理的配置信息</param>
        public WxAgentAuthApi(AppConfig config=null) : base(config)
        {
        }
        
        /// <summary>
        ///  获取预授权码pre_auth_code
        /// </summary>
        /// <returns></returns>
        private async Task<WxGetPreAuthCodeResp> GetPreAuthCode()
        {
            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_create_preauthcode",
                HttpMothed = HttpMothed.POST,
                CustomBody = $"{{\"component_appid\":\"{ApiConfig.AppId}\"}}"
            };

            return await RestCommonAgentAsync<WxGetPreAuthCodeResp>(req);
        }

        /// <summary>
        /// 获取公众号/小程序授权地址
        /// </summary>
        /// <param name="redirectUrl">回调地址</param>
        /// <returns></returns>
        public async Task<ResultMo<string>> GetPreAuthUrl(string redirectUrl)
        {
            var preAuthCodeRes = await GetPreAuthCode();

            if (!preAuthCodeRes.IsSuccess())
                return preAuthCodeRes.ConvertToResultOnly<string>();

            if (redirectUrl.Contains("://"))
                redirectUrl = redirectUrl.UrlEncode();
            
            var authUrl=
                $"https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid={ApiConfig.AppId}&pre_auth_code={preAuthCodeRes.pre_auth_code}&redirect_uri={redirectUrl}";
            return new ResultMo<string>(authUrl);
        }

        /// <summary>
        /// 获取平台下当前授权账号的AccessToken响应实体
        /// </summary>
        /// <param name="authorizationCode">授权code,会在授权成功时返回给第三方平台</param>
        /// <returns></returns>
        public async Task<WxGetGrantedAccessTokenResp> GetGrantorAccessToken(string authorizationCode)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"authorization_code\":\"").Append(authorizationCode).Append("\" }");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_query_auth",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxGetGrantedAccessTokenResp>(req);
        }

        /// <summary>
        /// 获取平台下当前授权账号的AccessToken响应实体
        /// </summary>
        /// <param name="grantorRefreshToken">授权者的刷新Token</param>
        /// <param name="grantorAppId">授权者的Appid</param>
        /// <returns></returns>
        public async Task<WxRefreshGrantedAccessTokenResp> RefreshGrantorAccessToken(string grantorAppId,string grantorRefreshToken)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"authorizer_refresh_token\":\"").Append(grantorRefreshToken).Append("\" }");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_authorizer_token",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxRefreshGrantedAccessTokenResp>(req);
        }


        /// <summary>
        ///  获取公号的授权（账号+权限）信息
        /// </summary>
        /// <param name="grantorAppId"></param>
        /// <returns></returns>
        public async Task<WxGetGrantorInfoResp> GetGrantorInfo(string grantorAppId)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\"}");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_get_authorizer_info",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxGetGrantorInfoResp>(req);
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
        public async Task<WxGetGrantorOptionResp> GetGrantorOption(string grantorAppId,string optionName)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"option_name\":\"").Append(optionName).Append("\"}");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_get_authorizer_option",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxGetGrantorOptionResp>(req);
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
        public async Task<WxBaseResp> SetGrantorOption(string grantorAppId, string optionName,string optionValue )
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"option_name\":\"").Append(optionName).Append("\",");
            strContent.Append("\"option_value\":\"").Append(optionValue).Append("\"}");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_set_authorizer_option",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxBaseResp>(req);
        }

        /// <summary>
        ///  获取授权者列表
        /// </summary>
        /// <param name="grantorAppId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<WxGetGrantorListResp> GetGrantorList(string grantorAppId, int offset, int count)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"offset\":\"").Append(offset).Append("\",");
            strContent.Append("\"count\":\"").Append(count).Append("\"}");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_get_authorizer_list",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxGetGrantorListResp>(req);
        }

    }
}
