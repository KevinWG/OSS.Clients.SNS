using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extention;
using OSS.Social.WX;
using OSS.Social.WX.Offcial.Basic;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// WxOffBasicUserTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxOffMassTests: WxBaseTests
    {
        private static WxOffBasicApi m_Api = new WxOffBasicApi(m_Config);

        /// <summary>
        /// 发送模板测试
        /// </summary>
        [TestMethod]
        public void SendTemplateTest()
        {
            var res =
                m_Api.SendTemplateAsync("o7gE1s6mygEKgopVWp-w", "4E7QKo8GhQ0pNHDAfE3Z-w7vEWULDT3ZflBJUMYpd7s",
                    "http://www.osscoder.com",
                    new
                    {
                        first = new {value = "用户你好：", color = "#173177"},
                        trade_time = new {value = DateTime.Now.ToShortDateString()},
                        money = new {value = "100.00￥"},
                        remark = new {value = "请点击查看详情"},
                    }).WaitResult();
            Assert.IsTrue(res.IsSuccess );
        }
    }
}
