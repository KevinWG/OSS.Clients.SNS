using System.Web.Mvc;
using OS.Social.WX;
using OS.Social.WX.Offcial;
using OS.Social.WX.Sns;

namespace OS.Social.Samples.Controllers
{
    public class WxOauthController : Controller
    {
        private static WxAppCoinfig m_Config = new WxAppCoinfig()
        {
            AppSource = "11",
            AppId = "你的appId",
            AppSecret = "你的secretkey"
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
            var tokecRes = m_AuthApi.GetAuthAccessToken(code);
            if (tokecRes.IsSuccess)
            {
                var userInfoRes = m_AuthApi.GetWxAuthUserInfo(tokecRes.AccessToken, tokecRes.OpenId);

                return Content("你已成功获取用户信息!");
            }
            return Content("获取用户授权信息失败!");
        }
        
    }
}