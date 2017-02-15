using System.Web.Mvc;
using OSS.Social.WX;
using OSS.Social.WX.Sns;
using OSS.Social.WX.SysUtils.Mos;

namespace OSS.Social.Samples.Controllers
{
    public class wxOauthController : Controller
    {
        private static WxAppCoinfig m_Config = new WxAppCoinfig()
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
            var tokecRes = m_AuthApi.GetAuthAccessTokenAsync(code);
            if (tokecRes.IsSuccess)
            {
                var userInfoRes = m_AuthApi.GetWxAuthUserInfoAsync(tokecRes.access_token, tokecRes.openid);
                return Content("你已成功获取用户信息!");
            }
            return Content("获取用户授权信息失败!");
        }
        
    }
}