using System;
using System.IO;
using System.Web.Mvc;
using OS.Common.ComModels.Enums;
using OS.Common.Modules.LogModule;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.Samples.Controllers
{
    public class wxController : Controller
    {
        private static readonly WxMsgServerConfig config = new WxMsgServerConfig()
        {
            Token = "你的token",
            EncodingAesKey = "你的加密key",
            SecurityType = WxSecurityType.Safe,
            AppId = "appid"

        };


        #region   微信消息接口模块

        private static readonly WxMsgService msgService = new WxMsgService();

        /// <summary>
        ///   验证使用
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param> 
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult msg(string signature, string timestamp, string nonce, string echostr)
        {
            LogUtil.Info($"signature:{signature},timestamp:{timestamp},nonce:{nonce},echostr:{echostr}");
            var res = msgService.ProcessServerCheck(config.Token, signature, timestamp, nonce);

            if (res.Ret == ResultTypes.Success)
            {
                return Content(echostr);
            }
            return Content(string.Empty);
        }

        /// <summary>
        /// 正常消息使用
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult msg(string signature, string timestamp, string nonce)
        {
            string requestXml;
            LogUtil.Info($"进入接口：signature:{signature}   ,timestamp:{timestamp}         ,nonce:{nonce}");
            
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                requestXml = reader.ReadToEnd();
                LogUtil.Info($"内容 requestXml:{requestXml}");
            }
            //return Content(String.Empty);
            try
            {
                var res = msgService.Processing(config, requestXml, signature, timestamp, nonce);
                if (res.Ret==ResultTypes.Success)
                {
                    return Content(res.Data);
                }
                LogUtil.Error(res.Message);
            }
            catch (Exception ex)
            {

            }
            
            return Content(new NoReplyMsg().ToXml(config));

        }

        #endregion

    }
}