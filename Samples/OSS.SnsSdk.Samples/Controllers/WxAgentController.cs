using Microsoft.AspNetCore.Mvc;
using OSS.Clients.Chat.WX.Mos;
using OSS.Clients.SNS.Samples.Controllers.Codes;
using OSS.Common.BasicMos.Resp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSS.Clients.SNS.Samples.Controllers
{
    /// <summary>
    /// 主要是微信开放平台接口
    /// </summary>
    public class WXController : Controller
    {
        private static readonly WXChatConfig config = new WXChatConfig()
        {
            AppId = "wxd93108c6bf360bv9",
            SecurityType = WXSecurityType.None,
            Token = "2DkmMYU9Zrv3C4jam7zvTghlUf2Z60s3",
            EncodingAesKey = "2DkmMYU9Zrv8C4jam7zvTghlUf2Z60s3ghlUf2Z60s3",
        };
        private static WXAgentController pHandler = new WXAgentController(config);

        // GET: /<controller>/
        public IActionResult pauth(string signature, string timestamp, string nonce, string echostr)
        {
            var res = pHandler.Process(Request.Body, signature, timestamp, nonce, echostr);
            return Content(res.IsSuccess() ? res.data : "success");
        }
    }
}
