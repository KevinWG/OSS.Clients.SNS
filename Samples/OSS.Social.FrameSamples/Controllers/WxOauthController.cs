using OSS.Social.WX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OSS.Common.Extention;
using OSS.Http;
using OSS.Http.Mos;
using OSS.Social.WX.Offcial.Basic;
using OSS.Social.WX.Sns;
using OSS.Social.WX.SysTools.Mos;

namespace OSS.Social.FrameSamples.Controllers
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

        public async Task<ActionResult> callback(string code, string state)
        {
            var tokecRes = await m_AuthApi.GetAuthAccessTokenAsync(code);
            if (tokecRes.IsSuccess)
            {
                var userInfoRes =await m_AuthApi.GetWxAuthUserInfoAsync(tokecRes.access_token, tokecRes.openid);
                return Content("你已成功获取用户信息!");
            }
            return Content("获取用户授权信息失败!");
        }
        private static WxOffKfApi m_KfApi = new WxOffKfApi(m_Config);
        
        public async Task<ActionResult> getKfListTest()
        {
            var tokecRes = await m_KfApi.GetKFAccountListAsync();
            if (tokecRes.IsSuccess)
            {
                //var userInfoRes = m_AuthApi.GetWxAuthUserInfoAsync(tokecRes.access_token, tokecRes.openid).WaitResult(-1);
                //return Content("你已成功获取用户信息!");
            }
            return Content("获取用户授权信息失败!");

        }

        public ActionResult getTest()
        {
            var resp = GetAsync();
            return Content(resp);
        }



        private  string GetAsync()
        {
            var req = new OsHttpRequest();
            req.AddressUrl =
                "https://api.weixin.qq.com/sns/oauth2/access_token?appid=wxaa9e6cb3f03afa97&secret=0fc0c6f735a90fda1df5fc840e010144&code=ssss&grant_type=authorization_code";
            req.HttpMothed = HttpMothed.GET;
            var result = req.RestSend().WaitResult();
            return  result.Content.ReadAsStringAsync().WaitResult();
        }



        public async Task<ActionResult> GetAsyn()
        {
            var uri ="http://www.baidu.com";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                var content= (await response.Content.ReadAsStringAsync());
                return Content(content);
            }
        }
        public ActionResult Get()
        {
            var uri = "http://www.baidu.com";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(uri);
                response.Wait();

                var con = response.Result.Content.ReadAsStringAsync();
                con.Wait();

                return Content(con.Result);
            }
        }
        public ActionResult GetWait()
        {
            var con = GetContentAsyn();
            con.Wait();
            return Content(con.Result);
        }

        private async Task<string> GetContentAsyn()
        {
            var uri = "http://www.baidu.com";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                var con = await response.Content.ReadAsStringAsync();

                return con;
            }
        }
    }
}