using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.ComModels;
using OSS.Common.Extention;
using OSS.SnsSdk.Official.Wx.Basic;
using OSS.SnsSdk.Official.Wx.Basic.Mos;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// 客服功能测试
    /// </summary>
    [TestClass]
    public class WxOffKfTests:WxBaseTests
    {
        private static WxOffKfApi m_Api = new WxOffKfApi(m_Config);
        /// <summary>
        /// 添加客服账号测试
        /// </summary>
        [TestMethod]
        public void AddKFAccountAsyncTest()
        {
            var res = m_Api.AddKfAccountAsync("kevin@kevin","kevin").WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }

        /// <summary>
        /// 获取客服列表测试
        /// </summary>
        [TestMethod]
        public void GetKFAccountListAsyncTest()
        {
            var res = m_Api.GetKFAccountListAsync().WaitResult();
            Assert.IsTrue(res.IsSuccess());
            //{"kf_list":[{"kf_account":"kevin@kevin","kf_headimgurl":"","kf_id":2001,"kf_nick":"kevin"}]}
        }


        /// <summary>
        /// 邀请测试
        /// </summary>
        [TestMethod]
        public void InviteKfWorkerTest()
        {
            var res = m_Api.InviteKfWorker("kevin@kevin","kevin_-_wang").WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }



        /// <summary>
        ///  获取客服会话测试
        /// </summary>
        [TestMethod]
        public void GetKfSessionsByAccountTest()
        {
            var res = m_Api.GetKfSessionsByAccount("kevin@kevin").WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }


        /// <summary>
        ///  获取客服会话测试
        /// </summary>
        [TestMethod]
        public void GetKfWaitUserListTest()
        {
            var res = m_Api.GetKfWaitUserList().WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }


        /// <summary>
        ///  直接发起消息测试
        /// </summary>
        [TestMethod]
        public void SenKfMsgAsyncTest()
        {
            var res = m_Api.SenKfTextMsgAsync("oHoSOt2w5uuxWKeVQTwoZQDuZ-nM","只是测试").WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }
        

        /// <summary>
        ///  获取消息测试
        /// </summary>
        [TestMethod]
        public void GetKfMsgListTest()
        {
            var res = m_Api.GetKfMsgList(new WxGetKfMsgListReq()
            {
                endtime = DateTime.Now.ToUtcSeconds(),
                starttime = DateTime.Now.AddHours(-1).ToUtcSeconds()
                ,number = 100
            }).WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }



        /// <summary>
        ///  直接发起消息测试
        /// </summary>
        [TestMethod]
        public void SenKfMediaMsgAsyncTest()
        {
           //图片
             var res = m_Api.SenKfMediaMsgAsync("oHoSOt2w5uuxWKeVQTwoZQDuZ-nM","image", "w6q00gTWx6n6fsgBiM-VoKS32Uq-vNWhx5EpM85YyeG8IOk1FdPlJNo8bE7PFE6j", "kevin@kevin").WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }


        /// <summary>
        ///  直接发起消息测试
        /// </summary>
        [TestMethod]
        public void SenKfVideoMsgSyncTest()
        {
            // 视频
            var res = m_Api.SenKfVideoMsgSync("oHoSOt2w5uuxWKeVQTwoZQDuZ-nM", "zXOYSQS_A3op3R9ZW0EYKwbjgQ544KTICzLWYAUgpfU",
                    string.Empty, "视频标题", "视频描述", "kevin@kevin").WaitResult();
            Assert.IsTrue(res.IsSuccess());
        }
        
    }
}
