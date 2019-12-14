using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Clients.Chat.WX.Mos;
using OSS.Social.Tests.WXTests;

namespace OSS.Social.Tests.WxTests.WxMsg
{
    /// <summary>
    /// WXPlatStoreTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WXChatTests : WXBaseTests
    {
        public WXChatTests()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }


        [TestMethod]
        public void GetStoreCategoryAsyncTest()
        {
            var replyMsg = new WXNewsReplyMsg
            {
                Items = new List<WXArticleItem>
                {
                    new WXArticleItem() {Title = "test", Description = "noone"},
                    new WXArticleItem() {Title = "test1", Description = "more"}
                }
            };
            var str=replyMsg.ToReplyXml();
            Assert.IsTrue(!string.IsNullOrEmpty(str));
        }

    }
}
