using Microsoft.AspNetCore.Mvc;

namespace OSS.SocialSDK.Samples.Controllers
{
    public class HomeController : Controller
    {
        //private static AppConfig m_Config = new AppConfig()
        //{
        //    AppSource = "11",
        //    AppId = "wxaa9e6cb3f03afa97",
        //    AppSecret = "0fc0c6f735a90fda1df5fc840e010144"
        //};
        
        //private static WxOffBasicApi api = new WxOffBasicApi(m_Config);
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
