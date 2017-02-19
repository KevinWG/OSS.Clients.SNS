using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extention;
using OSS.Social.WX;
using OSS.Social.WX.Offcial.Basic;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// 客服功能测试
    /// </summary>
    [TestClass]
    public class WxOffKfTests:WxBaseTests
    {
        private static WxOffBasicApi m_Api = new WxOffBasicApi(m_Config);
        /// <summary>
        /// 添加客服账号测试
        /// </summary>
        [TestMethod]
        public void AddKFAccountAsyncTest()
        {
            var res =
                m_Api.AddKFAccountAsync("kevin@osscoder.com","kevin").WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }
    }
}
