using System;
using System.IO;
using System.Web.Mvc;
using OS.Common.Modules.LogModule;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.Samples.Controllers
{
    public class wxMsgController : Controller
    {
        private static readonly WxMsgServerConfig config = new WxMsgServerConfig()
        {
            Token = "你的token",
            EncodingAesKey = "你的加密key",
            SecurityType = WxSecurityType.Safe,//  在微信段设置的安全模式
            AppId = "你的appid"   //  
        };


        #region   微信消息接口模块

        private static readonly WxMsgService msgService = new WxMsgService(config);


        ///// <summary>
        /////   验证使用
        ///// </summary>
        ///// <param name="signature"></param>
        ///// <param name="timestamp"></param>
        ///// <param name="nonce"></param> 
        ///// <param name="echostr"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ContentResult msg(string signature, string timestamp, string nonce, string echostr)
        //{
        //    LogUtil.Info($"signature:{signature},timestamp:{timestamp},nonce:{nonce},echostr:{echostr}");
        //    var res = msgService.ProcessServerCheck(config.Token, signature, timestamp, nonce);

        //    if (res.IsSuccess)
        //    {
        //        return Content(echostr);
        //    }
        //    return Content(string.Empty);
        //}

        /// <summary>
        /// 正常消息使用
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public ContentResult msg(string signature, string timestamp, string nonce, string echostr)
        {
            LogUtil.Info($"signature:{signature}, timestamp:{timestamp}  , nonce:{nonce} , echostr:{echostr} ");
            string requestXml;
           
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                requestXml = reader.ReadToEnd();
                LogUtil.Info($"内容 requestXml:{requestXml}");
            }
            try
            {
                var res = msgService.Processing( requestXml, signature, timestamp, nonce,echostr);
                if (res.IsSuccess)
                {
                    LogUtil.Info(res.Data);
                    return Content(res.Data);
                }
                LogUtil.Error(res.Message);
            }
            catch (Exception ex)
            {
            }
            
            return Content("success");

        }

        #endregion

    }
}