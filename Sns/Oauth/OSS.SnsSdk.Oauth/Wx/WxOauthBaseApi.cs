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
using OSS.Common.ComModels;
using OSS.Common.Plugs;
using OSS.Http.Extention;
using OSS.Http.Mos;
using OSS.SnsSdk.Oauth.Wx.Mos;

namespace OSS.SnsSdk.Oauth.Wx
{
    /// <summary>
    /// 微信接口SDK基类
    /// </summary>
    public class WxOauthBaseApi: BaseConfigProvider<AppConfig, WxOauthBaseApi>
    {
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxOauthBaseApi(AppConfig config):base(config)
        {
            ModuleName = WxOauthConfigProvider.ModuleName;
        }

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <param name="client">自定义 HttpClient </param>
        /// <returns>实体类型</returns>
        public async Task<T> RestCommonJson<T>(OsHttpRequest request, HttpClient client = null)
             where T : WxBaseResp, new()
        {
            var t = await request.RestCommonJson<T>(client);

            if (!t.IsSuccess())
                t.msg =t.errmsg ;

            return t;
        }

        /// <inheritdoc />
        protected override AppConfig GetDefaultConfig()
        {
            return WxOauthConfigProvider.DefaultConfig;
        }
    }

    /// <summary>
    ///  Oauth相关配置信息
    /// </summary>
    public static class WxOauthConfigProvider
    {
        /// <summary>
        ///   当前模块名称
        /// </summary>
        public static string ModuleName { get; set; } = ModuleNames.SocialCenter;

        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static AppConfig DefaultConfig { get; set; }
    }



}
