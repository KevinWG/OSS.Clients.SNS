using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using OSS.Common.Plugs.DirConfigPlug;
using OSS.Social.Samples.Controllers.Codes;
using OSS.Social.WX.Msg.Mos;

namespace OSS.Social.Samples.Controllers
{
    public class WxMsgController : Controller
    {
        private static readonly WxMsgServerConfig config = DirConfigUtil.GetDirConfig<WxMsgServerConfig>("my_wxmsg_config");
        
        static WxMsgController()
        {
            if (config==null)
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
                var res = _msgService.Process( requestXml, signature, timestamp, nonce,echostr);
                if (res.IsSuccess)
                    return Content(res.Data);
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