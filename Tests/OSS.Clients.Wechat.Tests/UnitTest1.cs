using OSS.Clients.MApp.Wechat;
using OSS.Clients.Platform.Wechat;
using OSS.Common;
using OSS.Common.Resp;

namespace OSS.Clients.Wechat.Tests
{
    [TestClass]
    public class UnitTest1
    {

        //static UnitTest1()
        //{
        //    WechatPlatformHelper.DefaultConfig =
        //        new AccessSecret("", "");
        //}



        [TestMethod]
        public async Task TestMethod1()
        {
            var resp = await new WechatGetUserPhoneNumReq("ssssss").SendAsync();
            Assert.IsTrue(resp.IsSuccess());
        }
    }
}