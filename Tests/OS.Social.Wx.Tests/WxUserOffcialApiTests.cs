using System.Collections.Generic;
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
        private static WxOffcialUserApi  m_Api=new WxOffcialUserApi(m_Config);

        [TestMethod]
        public void TagTests()
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
        //  todo  test

        [TestMethod]
        public void GetOpenIdListTest()
        {
            var res = m_Api.GetOpenIdListByTag(2);
            Assert.IsTrue(res.IsSuccess);
        }



        [TestMethod]
        public void GetOpenIdListByTagTest()
        {
            var res = m_Api.GetOpenIdListByTag(2);
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void SetOrCancleUsersTagTest()
        {
            var res = m_Api.SetOrCancleUsersTag(new List<string>() {""}, 2,0);
            Assert.IsTrue(res.IsSuccess);

        }
        //GetUserTagsByOpenId

    }
}
