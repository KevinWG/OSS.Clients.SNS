using System.IO;
using Microsoft.AspNetCore.Mvc;
using OSS.Common.ComModels;
using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SnsSdk.Samples.Controllers.Codes;
using OSS.SocialSDK.WX.Msg.Mos;

namespace OSS.SnsSdk.Samples.Controllers
{
    public class WxMsgController : Controller
    {
        private static readonly WxMsgServerConfig config = new WxMsgServerConfig()
        {
            AppId = "wx835d1fda838bb558",
            SecurityType = WxSecurityType.None,
            Token = "2DMEMYU9Zrv8C4jam7zvTghlUf2Z60s3",
            EncodingAesKey = string.Empty,
        };
        private static readonly WxMsgService _msgService;


        static WxMsgController()
        {
            _msgService = new WxMsgService(config);

            //  用户可以自定义消息处理委托，也可以通过 RegisterEventMsgHandler 自定义事件处理委托
            WxCustomMsgHandlerProvider.RegisterMsgHandler<TextRecMsg>("test_msg", recMsg =>
            {
                return new TextReplyMsg() {Content = " test_msg 类型消息返回 "};
            });
        }

        #region   微信消息接口模块


        /// <summary>
        ///   在微信端配置的地址（包含微信第一次Get验证
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public ContentResult Msg(string signature, string timestamp, string nonce, string echostr)
        {
            string requestXml;
            using (var reader = new StreamReader(Request.Body))
            {
                requestXml = reader.ReadToEnd();
            }

            var res = _msgService.Process(requestXml, signature, timestamp, nonce, echostr);
            return Content(res.IsSuccess() ? res.data : "success");
        }

        #endregion

    }




    
}