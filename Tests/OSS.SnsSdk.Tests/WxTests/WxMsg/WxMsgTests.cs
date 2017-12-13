using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.Social.Tests.WxTests.WxMsg
{
    /// <summary>
    /// WxOffStoreTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxMsgTests : WxBaseTests
    {
        public WxMsgTests()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }


        [TestMethod]
        public void GetStoreCategoryAsyncTest()
        {
            var replyMsg = new WxNewsReplyMsg
            {
                Items = new List<WxArticleItem>
                {
                    new WxArticleItem() {Title = "test", Description = "noone"},
                    new WxArticleItem() {Title = "test1", Description = "more"}
                }
            };
            var str=replyMsg.ToReplyXml();
            Assert.IsTrue(!string.IsNullOrEmpty(str));
        }

    }
}
