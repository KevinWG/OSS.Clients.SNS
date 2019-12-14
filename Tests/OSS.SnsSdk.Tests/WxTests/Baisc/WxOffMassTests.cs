using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Clients.Platform.WX.Assist;
using OSS.Clients.Platform.WX.Basic;
using OSS.Clients.Platform.WX.Helpers.Mos;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Common.Resp;

namespace OSS.Social.Tests.WXTests.Baisc
{
    /// <summary>
    /// WXPlatBasicUserTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WXPlatMassTests: WXBaseTests
    {
        private static WXPlatMassApi m_Api = new WXPlatMassApi(m_Config);

        /// <summary>
        /// 发送模板测试
        /// </summary>
        [TestMethod]
        public void SendTemplateTest()
        {
            var res =
                m_Api.SendTemplateAsync("o7gE1s6mygEKgopVWp7BBtEAqT-w", "4E7QKo8GhQ0pNHDAfE3Z-w7vEWULDT3ZflBJUMYpd7s",
                    "http://www.osscore.com",
                    new
                    {
                        first = new {value = "用户你好：", color = "#173177"},
                        trade_time = new {value = DateTime.Now.ToShortDateString()},
                        money = new {value = "100.00￥"},
                        remark = new {value = "请点击查看详情"},
                    }).WaitResult();
            Assert.IsTrue(res.IsSuccess() );
        }

        private static WXPlatAssistApi m_AssistApi = new WXPlatAssistApi(m_Config);

        [TestMethod]
        public void GetJsTicketTest()
        {
            var res =
                m_AssistApi.GetJsTicketFromCacheAsync(WXJsTicketType.jsapi).Result;
            Assert.IsTrue(res.IsSuccess());
        }

        [TestMethod]
        public void TempleteTest()
        {
            var res = m_Api.GetTemplateIndustry().Result;

            var resList = m_Api.GetTemplateList().Result;


        }
    }
}
