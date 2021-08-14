using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSS.Clients.Chat.WX;
using OSS.Clients.Chat.WX.Mos;
using OSS.Clients.SNS.Samples.Controllers.Codes;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Log;

namespace OSS.Clients.SNS.Samples.Controllers
{
    public class WXChatController : Controller
    {
        private static readonly WXChatConfig config = new WXChatConfig()
        {
            AppId = "wx835dddda838bb558",
            SecurityType = WXSecurityType.Safe,
            Token = "2DMEMYU9Zrv8C4ddddzvTghlUf2Z60s3",
            EncodingAesKey = "b2kcgLAsxwMjWmi6tCixPTZQ1MbY76VLXgZKHgfLOSf",
        };

        private static readonly WXCustomMsgHandler _msgService = new WXCustomMsgHandler();



        static WXChatController()
        {
            WXChatConfigProvider.DefaultConfig = config;
        }


        #region   微信消息接口模块

        /// <summary>
        ///   在微信端配置的地址（包含微信第一次Get验证
        /// </summary>
        /// <param name="appid"> 如果是平台提供者，此参数为 授权的公众号AppId</param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public async Task<ContentResult> Msg(string appid, string signature, string msg_signature, string timestamp, string nonce, string echostr)
        {
            // 直接传入Stream也是可以的
            // 这里为了记录传入加密前的日志，所以先获取再传入
            // var res = _msgService.Process(Request.Body, signature, timestamp, nonce, echostr);

            string contentXml;
            using (var reader = new StreamReader(Request.Body))
            {
                contentXml = reader.ReadToEnd();
            }

            var res = await _msgService.Process(contentXml, signature, msg_signature, timestamp, nonce, echostr);
            if (res.IsSuccess())
                return Content(res.data);

            LogHelper.Info($" 当前请求处理失败，原因：{res.msg}");
            return Content("success");
        }

        #endregion

    }



}