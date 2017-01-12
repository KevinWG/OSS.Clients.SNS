using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OS.Social.WX;
using OS.Social.WX.Offcial;

namespace OS.Social.Wx.Tests
{
    [TestClass]
    public class WxOffcialApiTests
    {

        private  static WxOffcialApi m_Api=new WxOffcialApi(new WxAppCoinfig()
        {
            AppId = "wxaa9e6cb3f03afa97",
            AppSecret = "0fc0c6f735a90fda1df5fc840e010144"
        });

        [TestMethod]
        public void GetWxIpListTest()
        {
            var iplist = m_Api.GetWxIpList();
            Assert.IsTrue(iplist.IsSuccess && iplist.Data.Count > 0);
        }
    }
}
