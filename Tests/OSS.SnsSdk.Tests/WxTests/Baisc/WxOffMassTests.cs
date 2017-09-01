using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.SnsSdk.Official.Wx.Assist;
using OSS.SnsSdk.Official.Wx.Basic;
using OSS.SnsSdk.Official.Wx.SysTools.Mos;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// WxOffBasicUserTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxOffMassTests: WxBaseTests
    {
        private static WxOffMassApi m_Api = new WxOffMassApi(m_Config);

        /// <summary>
        /// 发送模板测试
        /// </summary>
        [TestMethod]
        public void SendTemplateTest()
        {
            var res =
                m_Api.SendTemplateAsync("o7gE1s6mygEKgopVWp7BBtEAqT-w", "4E7QKo8GhQ0pNHDAfE3Z-w7vEWULDT3ZflBJUMYpd7s",
                    "http://www.osscoder.com",
                    new
                    {
                        first = new {value = "用户你好：", color = "#173177"},
                        trade_time = new {value = DateTime.Now.ToShortDateString()},
                        money = new {value = "100.00￥"},
                        remark = new {value = "请点击查看详情"},
                    }).WaitResult();
            Assert.IsTrue(res.IsSuccess() );
        }

        private static WxOffAssistApi m_AssistApi = new WxOffAssistApi(m_Config);

        [TestMethod]
        public void GetJsTicketTest()
        {
            var res =
                m_AssistApi.GetJsTicketFromCacheAsync(WxJsTicketType.jsapi).Result;
            Assert.IsTrue(res.IsSuccess());
        }

        
    }
}
