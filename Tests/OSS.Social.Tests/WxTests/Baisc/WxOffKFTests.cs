using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extention;
using OSS.Social.WX.Offcial.Basic;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// 客服功能测试
    /// </summary>
    [TestClass]
    public class WxOffKfTests:WxBaseTests
    {
        private static WxOffKfApi m_Api = new WxOffKfApi(m_Config);
        /// <summary>
        /// 添加客服账号测试
        /// </summary>
        [TestMethod]
        public void AddKFAccountAsyncTest()
        {
            var res = m_Api.AddKfAccountAsync("kevin@osscoder.com","kevin").WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }
    }
}
