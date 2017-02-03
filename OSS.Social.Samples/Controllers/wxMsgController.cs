using System;
using System.IO;
using System.Web.Mvc;
using OSS.Common.Modules.LogModule;
using OSS.Social.WX.Msg.Mos;

namespace OSS.Social.Samples.Controllers
{
    public class wxMsgController : Controller
    {
        private static readonly WxMsgServerConfig config = new WxMsgServerConfig()
        {
            Token = "90ae131cfda74ae9906bcfc574e2a84a",
            EncodingAesKey = "你的加密key",
            SecurityType = WxSecurityType.None,//  在微信段设置的安全模式
            AppId = "wxc6544368e4c92a16"   //  
        };


        #region   微信消息接口模块

        private static readonly WxBasicMsgService msgService = new WxBasicMsgService(config);

        public ContentResult msg(string signature, string timestamp, string nonce, string echostr)
        {
            string requestXml;
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                requestXml = reader.ReadToEnd();
            }
            try
            {
                var res = msgService.Process( requestXml, signature, timestamp, nonce,echostr);
                if (res.IsSuccess)
                    return Content(res.Data);
            }
            catch (Exception ex)
            {
            }
            return Content("success");
        }

        #endregion

    }
}