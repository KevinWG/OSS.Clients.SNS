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

        private  static WxBaseOffcialApi m_Api=new WxBaseOffcialApi(new WxAppCoinfig()
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




        public void SendTemplateTest()
        {
            //	4E7QKo8GhQ0pNHDAfE3Z-w7vEWULDT3ZflBJUMYpd7s     	{{first.DATA}} 交易时间：{{trade_time.DATA}} 金额：{{money.DATA}} {{remark.DATA}}


        }




    }
}
