using Microsoft.AspNetCore.Mvc;
using OSS.Clients.Oauth.WX;
using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.BasicMos;
using OSS.Common.ComModels;
using OSS.Common.Resp;

namespace OSS.Clients.SNS.Samples.Controllers
{
    public class wxOauthController : Controller
    {
        private static AppConfig m_Config = new AppConfig()
        {
            AppId = "wxaa9e6cb3f03afa97",
            AppSecret = "0fc0c6f735a90fda1df5fc840e010144"
        };

        private static WXOauthApi m_AuthApi = new WXOauthApi(m_Config);
        // GET: WXOauth
        public ActionResult auth( int type)
        {
            //记得更换成自己的项目域名
            var res = m_AuthApi.GetAuthorizeUrl("http://www.social.com/wxoauth/callback","1", (AuthClientType)type);
            return Redirect(res);
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