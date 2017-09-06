#region Copyright (C) 2017 OSS系列开源项目

/***************************************************************************
*　　	文件功能描述：微信第三方平台代理操作授权
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-9-3
*       
*****************************************************************************/

#endregion

using OSS.Common.ComModels;

namespace OSS.SnsSdk.Oauth.Wx
{
    /// <summary>
    /// 微信第三方平台代理操作授权
    /// </summary>
    public class WxAgentAuthApi:WxOauthBaseApi
    {
        /// <inheritdoc />
        public WxAgentAuthApi(AppConfig config) : base(config)
        {
        }
        
    }
}
