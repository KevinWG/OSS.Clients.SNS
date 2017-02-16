using System;
using Microsoft.AspNetCore.Mvc;
using OSS.Social.WX;
using OSS.Social.WX.Offcial.Basic;

namespace OSS.Social.Samples.Controllers
{
    public class HomeController : Controller
    {
        private static WxAppCoinfig m_Config = new WxAppCoinfig()
        {
            AppSource = "11",
            AppId = "wxaa9e6cb3f03afa97",
            AppSecret = "0fc0c6f735a90fda1df5fc840e010144"
        };
        
        private static WxOffBasicApi api = new WxOffBasicApi(m_Config);
        public IActionResult Index()
        {
            //var token = api.GetAccessTokenAsync().WaitResult();
            //return Content($"accesstoken:{token.access_token}");

            //var path = AppContext.BaseDirectory;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
