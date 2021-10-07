using System;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using System.Threading.Tasks;
using OSS.Tools.Cache;

namespace OSS.Clients.Platform.WX.Base.Interface.Impls
{
    internal class InterAccessTokenProvider : IAccessTokenProvider
    {
        private const string _AccessCacheKey = "OSS_Wechat_AccessToken_";

        public async Task<StrResp> GetAccessToken(IAppSecret config)
        {
            var key            = string.Concat(_AccessCacheKey, config.app_id);
            var accessTokenRes = await CacheHelper.GetAsync<WechatAccessTokenResp>(key);

            if (accessTokenRes != null)
                return new StrResp(accessTokenRes.access_token);

            accessTokenRes = await new WechatAccessTokenReq(config).SendAsync();
            if (!accessTokenRes.IsSuccess())
                return new StrResp().WithResp(accessTokenRes);

            await CacheHelper.SetAbsoluteAsync(key, accessTokenRes,
                TimeSpan.FromSeconds(accessTokenRes.expires_in - 60 * 5), "OSS.Clients.Platform.WX");// 按照返回的过期时间提前5分钟过期

            return new StrResp(accessTokenRes.access_token);

        }
    }
}
