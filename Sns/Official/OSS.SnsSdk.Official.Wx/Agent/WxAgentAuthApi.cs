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
        public WxAgentAuthApi(AppConfig config) : base(config)
        {
        }


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
                HttpMothed = HttpMothed.POST,
                CustomBody = $"{{\"component_appid\":\"{ApiConfig.AppId}\"}}"
            };

            return await RestCommonAgentAsync<WxGetPreAuthCodeResp>(req, verifyTicket);
        }

        /// <summary>
        /// 获取平台下当前授权账号的AccessToken响应实体
        /// </summary>
        /// <param name="authorizationCode">授权code,会在授权成功时返回给第三方平台</param>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <returns></returns>
        public async Task<WxGetGrantedAccessTokenResp> GetGrantorAccessToken(string authorizationCode, string verifyTicket)
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

            return await RestCommonAgentAsync<WxGetGrantedAccessTokenResp>(req, verifyTicket);
        }

        /// <summary>
        /// 获取平台下当前授权账号的AccessToken响应实体
        /// </summary>
        /// <param name="grantorRefreshToken">授权者的刷新Token</param>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <param name="grantorAppId">授权者的Appid</param>
        /// <returns></returns>
        public async Task<WxRefreshGrantedAccessTokenResp> RefreshGrantorAccessToken(string grantorAppId,string grantorRefreshToken, string verifyTicket)
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

            return await RestCommonAgentAsync<WxRefreshGrantedAccessTokenResp>(req, verifyTicket);
        }


        /// <summary>
        ///  获取公号的授权（账号+权限）信息
        /// </summary>
        /// <param name="grantorAppId"></param>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <returns></returns>
        public async Task<WxGetGrantorInfoResp> GetGrantorInfo(string grantorAppId, string verifyTicket)
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

            return await RestCommonAgentAsync<WxGetGrantorInfoResp>(req, verifyTicket);
        }


        /// <summary>
        ///  获取授权方的选项设置信息
        /// location_report(地理位置上报选项)	0-无上报 1-进入会话时上报 2-每5s上报
        /// voice_recognize（语音识别开关选项）	0-关闭语音识别 1-开启语音识别
        /// customer_service（多客服开关选项）	0-关闭多客服 1-开启多客服
        /// </summary>
        /// <param name="grantorAppId">授权公众号或小程序的appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <returns></returns>
        public async Task<WxGetGrantorOptionResp> GetGrantorInfo(string grantorAppId,string optionName, string verifyTicket)
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

            return await RestCommonAgentAsync<WxGetGrantorOptionResp>(req, verifyTicket);
        }

        /// <summary>
        ///  设置授权方的选项信息
        /// location_report(地理位置上报选项)	0-无上报 1-进入会话时上报 2-每5s上报
        /// voice_recognize（语音识别开关选项）	0-关闭语音识别 1-开启语音识别
        /// customer_service（多客服开关选项）	0-关闭多客服 1-开启多客服
        /// </summary>
        /// <param name="grantorAppId">授权公众号或小程序的appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="verifyTicket">微信后台推送的ticket，此ticket会定时推送</param>
        /// <param name="optionValue">设置的选项值</param>
        /// <returns></returns>
        public async Task<WxBaseResp> GetGrantorInfo(string grantorAppId, string optionName,string optionValue ,string verifyTicket)
        {
            var strContent = new StringBuilder();
            strContent.Append("{\"component_appid\":\"").Append(ApiConfig.AppId).Append("\",");
            strContent.Append("\"authorizer_appid\":\"").Append(grantorAppId).Append("\",");
            strContent.Append("\"option_name\":\"").Append(optionName).Append("\"}");
            strContent.Append("\"option_value\":\"").Append(optionValue).Append("\"}");

            var req = new OsHttpRequest
            {
                AddressUrl = $"{m_ApiUrl}/cgi-bin/component/api_set_authorizer_option",
                HttpMothed = HttpMothed.POST,
                CustomBody = strContent.ToString()
            };

            return await RestCommonAgentAsync<WxBaseResp>(req, verifyTicket);
        }
    }
}
