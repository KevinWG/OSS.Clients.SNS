using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Social.WX.Offcial;
using OS.Social.WX.Offcial.Basic;
using OS.Social.WX.Offcial.Basic.Mos;
using OS.Social.WX.Sns;

namespace OS.Social.Wx.Tests
{
    /// <summary>
    /// WxUserOffcialApiTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxUserOffcialApiTests:WxBaseOffcialApiTests
    {
        private static WxOffBasicApi  m_Api=new WxOffBasicApi(m_Config);

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



        [TestMethod]
        public void GetOpenIdListTest()
        {
            var res = m_Api.GetOpenIdList();
            Assert.IsTrue(res.IsSuccess);
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            var res = m_Api.GetUserInfo(new WxOffcialUserInfoReq() {openid = "o7gE1s7610fM84Qapv4eBla5Yqcc" });
            Assert.IsTrue(res.IsSuccess);
        }
        [TestMethod]
        public void GetUserInfoListTest()
        {
            var res = m_Api.GetUserInfoList(new List<WxOffcialUserInfoReq>() { new WxOffcialUserInfoReq() { openid = "o7gE1s7610fM84Qapv4eBla5Yqcc" } });
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void Test()
        {
            WxOauthApi a = new WxOauthApi();
        }
        //GetUserTagsByOpenId

    }


  
}
