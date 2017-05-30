using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.SnsSdk.Official.Wx.Basic;
using OSS.SnsSdk.Official.Wx.Basic.Mos;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// WxOffMenuApiTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxOffMenuApiTests:WxBaseTests
    {
        private static WxOffMenuApi m_Api =new WxOffMenuApi(m_Config);

        /// <summary>
        /// 添加客服账号测试
        /// </summary>
        [TestMethod]
        public void AddKFAccountAsyncTest()
        {
            var menu1=new WxMenuButtonMo();
            menu1.name = "OSSCoder官方源码";
            menu1.url = "https://github.com/KevinWG";
            menu1.type = WxButtonType.view.ToString();

            var list = new List<WxMenuButtonMo>(){menu1};
  
            var res = m_Api.AddOrUpdateMenuAsync(list).WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }
    }
}
