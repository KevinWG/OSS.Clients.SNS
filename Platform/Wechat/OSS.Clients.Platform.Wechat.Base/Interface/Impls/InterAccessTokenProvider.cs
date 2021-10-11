#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;
using OSS.Common.BasicMos;
using OSS.Tools.Cache;

namespace OSS.Clients.Platform.Wechat.Base.Interface.Impls
{
    internal class InterAccessTokenProvider : IAccessTokenProvider
    {
        private const string _AccessCacheKey = "OSS_Wechat_AccessToken_";


        public async Task<StrResp> GetAccessToken(IAppSecret appConfig)
        {
            var key            = string.Concat(_AccessCacheKey, appConfig);
            var accessTokenRes = await CacheHelper.GetAsync<WechatAccessTokenResp>(key);

            if (accessTokenRes != null)
                return new StrResp(accessTokenRes.access_token);

            accessTokenRes = await new WechatAccessTokenReq(appConfig).ExecuteAsync();
            if (!accessTokenRes.IsSuccess())
                return new StrResp().WithResp(accessTokenRes);

            await CacheHelper.SetAbsoluteAsync(key, accessTokenRes,
                TimeSpan.FromSeconds(accessTokenRes.expires_in - 60 * 5), "OSS.Clients.Platform.Wechat");// 按照返回的过期时间提前5分钟过期

            return new StrResp(accessTokenRes.access_token);
        }
    }
}
