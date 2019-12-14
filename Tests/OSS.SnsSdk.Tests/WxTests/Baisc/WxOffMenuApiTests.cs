using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Clients.Platform.WX.Basic;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.Common.Resp;

namespace OSS.Social.Tests.WXTests.Baisc
{
    /// <summary>
    /// WXPlatMenuApiTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WXPlatMenuApiTests:WXBaseTests
    {
        private static WXPlatMenuApi m_Api =new WXPlatMenuApi(m_Config);

        /// <summary>
        /// 添加客服账号测试
        /// </summary>
        [TestMethod]
        public void AddKFAccountAsyncTest()
        {
            var menu1=new WXMenuButtonMo();
            menu1.name = "OSSCoder官方源码";
            menu1.url = "https://github.com/KevinWG";
            menu1.type = WXButtonType.view.ToString();

            var list = new List<WXMenuButtonMo>(){menu1};
  
            var res = m_Api.AddOrUpdateMenuAsync(list).WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }
    }
}
