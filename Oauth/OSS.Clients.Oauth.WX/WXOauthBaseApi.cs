#region Copyright (C) 2016 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：微信接口SDK基类
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.ComModels;
using OSS.Tools.Http.Extention;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Oauth.WX
{
    /// <summary>
    /// 微信接口SDK基类
    /// </summary>
    public class WXOauthBaseApi: BaseApiConfigProvider<AppConfig>
    {
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WXOauthBaseApi(AppConfig config):base(config)
        {
        }

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <param name="client">自定义 HttpClient </param>
        /// <returns>实体类型</returns>
        public async Task<T> RestCommonJson<T>(OssHttpRequest request, HttpClient client = null)
            where T : WXBaseResp, new()
        {
            var response = await request.RestSend(client);
            using (response)
            {
                if (!response.IsSuccessStatusCode)
                    return new T()
                    {
                        ret = -(int) response.StatusCode,
                        msg = response.ReasonPhrase
                    };
                var contentStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contentStr);
            }
        }

        /// <inheritdoc />
        protected override AppConfig GetDefaultConfig()
        {
            return WXOauthConfigProvider.DefaultConfig;
        }
    }

    /// <summary>
    ///  Oauth相关配置信息
    /// </summary>
    public static class WXOauthConfigProvider
    {
        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = "oss_sns_oauth";

        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static AppConfig DefaultConfig { get; set; }

        /// <summary>
        /// 当 OperateMode = ByAgent 时，
        ///   调用此委托 获取第三方代理平台的 AccessToken 
        ///   可以调用 Official下的 WXAgentAuthApi（WXAgentBaseApi） 中接口
        /// 参数为当前ApiConfig
        /// </summary>
        public static Func<AppConfig, string> AgentAccessTokenFunc { get; set; }

        ///// <summary>
        /////  设置上下文配置信息
        ///// </summary>
        ///// <param name="config"></param>
        //public static void SetContextConfig(AppConfig config)
        //{
        //    WXOauthBaseApi.SetContextConfig(config);
        //}

    }



}
