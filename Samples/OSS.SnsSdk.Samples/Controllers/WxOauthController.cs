using Microsoft.AspNetCore.Mvc;
using OSS.Common.ComModels;
using OSS.SnsSdk.Oauth;
using OSS.SnsSdk.Oauth.Wx;
using OSS.SnsSdk.Oauth.Wx.Mos;

namespace OSS.SnsSdk.Samples.Controllers
{
    public class wxOauthController : Controller
    {
        private static AppConfig m_Config = new AppConfig()
        {
            AppSource = "11",
            AppId = "wxaa9e6cb3f03afa97",
            AppSecret = "0fc0c6f735a90fda1df5fc840e010144"
        };

        private static WxOauthApi m_AuthApi = new WxOauthApi(m_Config);
        // GET: WxOauth
        public ActionResult auth()
        {
            //记得更换成自己的项目域名
            var res = m_AuthApi.GetAuthorizeUrl("http://www.social.com/wxoauth/callback", AuthClientType.WxOffcial);
            return Redirect(res);
        }

        public ActionResult callback(string code, string state)
        {
            //var tokecRes = m_AuthApi.GetAuthAccessTokenAsync(code).WaitResult();
            //if (tokecRes.IsSuccess)
            //{
            //    var userInfoRes = m_AuthApi.GetWxAuthUserInfoAsync(tokecRes.access_token, tokecRes.openid);
            //    return Content("你已成功获取用户信息!");
            //}
            return Content("获取用户授权信息失败!");
        }
        
    }
}