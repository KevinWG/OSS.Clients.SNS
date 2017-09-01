using Microsoft.AspNetCore.Mvc;
using OSS.Common.ComModels;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SnsSdk.Samples.Controllers.Codes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSS.SnsSdk.Samples.Controllers
{


    /// <summary>
    /// 主要是微信开放平台接口
    /// </summary>
    public class wxController : Controller
    {
        private static readonly WxMsgConfig config = new WxMsgConfig()
        {
            AppId = "wxe93108c5bf320bc9",
            SecurityType = WxSecurityType.None,
            Token = "2DkmMYU9Zrv8C4jam7zvTghlUf2Z60s3",
            EncodingAesKey = "2DkmMYU9Zrv8C4jam7zvTghlUf2Z60s3ghlUf2Z60s3",
        };


        private static WxPlatformMsgHandler pHandler = new WxPlatformMsgHandler(config);

        // GET: /<controller>/
        public IActionResult pauth(string signature, string timestamp, string nonce, string echostr)
        {
            var res = pHandler.Process(Request.Body, signature, timestamp, nonce, echostr);
            return Content(res.IsSuccess() ? res.data : "success");
        }

        
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IActionResult> msg(string app)
        //{

        //}
    }





}
