using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Social.WX.Offcial;

namespace OS.Social.Wx.Tests
{
    /// <summary>
    /// WxUserOffcialApiTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxUserOffcialApiTests:WxBaseOffcialApiTests
    {
        private static WxUserOffcialApi  m_Api=new WxUserOffcialApi(m_Config);

        [TestMethod]
        public void AddTagTest()
        {
            var res = m_Api.AddTag("我就是试一试！");
            Assert.IsTrue(res.IsSuccess||res.Ret==45157);
        }

        [TestMethod]
        public void UpdateTagTest()
        {
            var res = m_Api.UpdateTag(2, "我就是试一试！");
            Assert.IsTrue(res.IsSuccess || res.Ret == 45058);
        }

        [TestMethod]
        public void GetTagListTest()
        {
            var res = m_Api.GetTagList();
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void GetOpenIdListTest()
        {
            var res = m_Api.GetOpenIdListByTag(2);
            Assert.IsTrue(res.IsSuccess);
        }

    }
}
