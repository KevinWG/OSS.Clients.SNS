using Microsoft.AspNetCore.Mvc;
using OSS.Common.ComModels;
using OSS.Common.Resp;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SnsSdk.Samples.Controllers.Codes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSS.SnsSdk.Samples.Controllers
{
    /// <summary>
    /// 主要是微信开放平台接口
    /// </summary>
    public class WxController : Controller
    {
        private static readonly WxMsgConfig config = new WxMsgConfig()
        {
            AppId = "wxd93108c6bf360bv9",
            SecurityType = WxSecurityType.None,
            Token = "2DkmMYU9Zrv3C4jam7zvTghlUf2Z60s3",
            EncodingAesKey = "2DkmMYU9Zrv8C4jam7zvTghlUf2Z60s3ghlUf2Z60s3",
        };
        private static WxAgentController pHandler = new WxAgentController(config);

        // GET: /<controller>/
        public IActionResult pauth(string signature, string timestamp, string nonce, string echostr)
        {
            var res = pHandler.Process(Request.Body, signature, timestamp, nonce, echostr);
            return Content(res.IsSuccess() ? res.data : "success");
        }
    }
}
