using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extention;
using OSS.SnsSdk.Official.Wx.Store;
using OSS.SnsSdk.Official.Wx.Store.Mos;

namespace OSS.Social.Tests.WxTests.Store
{
    /// <summary>
    /// WxOffStoreTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxOffStoreTests : WxBaseTests
    {
        public WxOffStoreTests()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }


        private static WxOffStoreApi m_Api = new WxOffStoreApi(m_Config);

        [TestMethod]
        public void AddStoreTest()
        {
            var req = new WxStoreBasicSmallMo();

            req.city = "北京";
            req.district = "朝阳区";
            req.address = "英特国际公寓A座";
            req.branch_name = "西坝河分部";
            req.business_name = "OSSCoder作坊";

            req.avg_price = 80;
            req.categories = new List<string>() { "公司企业,企业/工厂" };

            req.offset_type = 1;
            req.latitude = 39.967420F;
            req.longitude = 116.437680F;
            req.province = "北京市";
            req.telephone = "18610933383";

            req.introduction = "OSSCoder分部接待单位";
            req.open_time = "8:00-18:00";
            req.sid = "osss2";

            var res = m_Api.AddStoreAsync(req).WaitResult();
            Assert.IsTrue(res.IsSuccess);
            //{"errcode":0,"errmsg":"ok","poi_id":"467122530"}
        }

        [TestMethod]
        public void GetStoreCategoryAsyncTest()
        {
            var res = m_Api.GetStoreCategoryAsync().WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }

        [TestMethod]
        public void GetStoreAsyncTest()
        {
            var res = m_Api.GetStoreAsync(467122530).WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }

    
        [TestMethod]
        public void GetStoreListAsyncTest()
        {
            var res = m_Api.GetStoreListAsync().WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void DeleteStoreAsyncTest()
        {
            var res = m_Api.DeleteStoreAsync(467122530).WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }
    }
}
