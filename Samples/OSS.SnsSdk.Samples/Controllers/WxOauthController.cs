using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSS.Clients.Oauth.WX;
using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.SNS.Samples.Controllers
{
    public class OauthConfigProvider : IMetaProvider<AppConfig>
    {
        private static AppConfig m_Config = new AppConfig()
        {
            AppId = "wxaa9e6cb3f03afa97",
            AppSecret = "0fc0c6f735a90fda1df5fc840e010144"
        };
        public Task<Resp<AppConfig>> GetMeta()
        {
            return Task.FromResult(new Resp<AppConfig>(m_Config));
        }
    }

    public class wxOauthController : Controller
    {
        private static WXOauthApi m_AuthApi = new WXOauthApi(new OauthConfigProvider());
        // GET: WXOauth
        public ActionResult auth( int type)
        {
            //记得更换成自己的项目域名
            var res = m_AuthApi.GetAuthorizeUrl("http://www.social.com/wxoauth/callback","1", (AuthClientType)type);
            res.Wait();

            return Redirect(res.Result.data);
        }

        public ActionResult callback(string code, string state)
        {
            var tokecRes = m_AuthApi.GetOauthAccessTokenAsync(code).Result;
            if (!tokecRes.IsSuccess())
                return Content("获取用户授权信息失败!");

            var userInfoRes = m_AuthApi.GetWXOauthUserInfoAsync(tokecRes.access_token, tokecRes.openid).Result;
            return Content($"你已成功获取用户:{userInfoRes.nickname} 信息!");
        }
        
    }
}