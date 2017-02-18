using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Modules.CacheModule;
using OSS.Social.WX.Offcial.Store;
using OSS.Social.WX.Offcial.Store.Mos;

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

            req.address = "英特国际公寓";
            req.avg_price = 80;
            req.branch_name = "西坝河店";
            req.business_name = "绝味鸭脖";

            req.categories = new List<string>() { "美食" };
            req.city = "北京";
            req.district = "朝阳区";


            var res = m_Api.AddStoreAsync(req);
        }


    }
}
