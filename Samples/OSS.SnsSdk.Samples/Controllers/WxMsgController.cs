using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using OSS.Common.ComModels;
using OSS.SnsSdk.Msg.Wx;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SocialSDK.Samples.Controllers.Codes;
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
        // DirConfigUtil.GetDirConfig<WxMsgServerConfig>("my_wxmsg_config");

        static WxMsgController()
        {
            WxCustomMsgHandlerProvider.RegisterMsgHandler<TextRecMsg>("test_msg", recMsg =>
            {
                return new NoneReplyMsg();
            });


            if (config == null)
            {
                throw new ArgumentException("请给config 直接赋值，或者通过 DirConfigUtil.SetDirConfig(\"my_wxmsg_config\", config) 放入配置管理当中");
            }
            _msgService = new WxBasicMsgService(config);
        }

        #region   微信消息接口模块

        private static readonly WxBasicMsgService _msgService;

        public ContentResult Msg(string signature, string timestamp, string nonce, string echostr)
        {

            string requestXml;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                requestXml = reader.ReadToEnd();
            }
            try
            {
                var res = _msgService.Process(requestXml, signature, timestamp, nonce, echostr);
                if (res.IsSuccess())
                    return Content(res.data);
            }
            catch (Exception ex)
            {
            }
            return Content("success");
        }

        #endregion


        public ActionResult Index()
        {
            return View();
        }

    }




    
}