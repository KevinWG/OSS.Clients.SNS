using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extention;
using OSS.Social.WX.Offcial;
using OSS.Social.WX.Offcial.Basic;
using OSS.Social.WX.Offcial.Basic.Mos;

namespace OSS.Social.Tests.WxTests.Baisc
{
    /// <summary>
    /// WxUserOffcialApiTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxOffBasicApiTests : WxBaseTests
    {
        private static WxOffUserApi  m_Api = new WxOffUserApi(m_Config);

        [TestMethod]
        public void GetAccessTokenTest()
        {
            var res = m_Api.GetAccessTokenAsync().WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }



        [TestMethod]
        public void TagTests()
        {
            var res = m_Api.AddTagAsync("我就是试一试！").WaitResult();
            Assert.IsTrue(res.IsSuccess || res.Ret == 45157);
        }

        [TestMethod]
        public void UpdateTagTest()
        {
            var res = m_Api.UpdateTagAsync(2, "我就是试一试！").WaitResult();
            Assert.IsTrue(res.IsSuccess || res.Ret == 45058);
        }

        [TestMethod]
        public void GetTagListTest()
        {
            var res = m_Api.GetTagListAsync().WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }
        //  todo  test


        [TestMethod]
        public void GetOpenIdListByTagTest()
        {
            var res = m_Api.GetOpenIdListByTagAsync(2).WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void SetOrCancleUsersTagTest()
        {
            var res = m_Api.SetOrCancleUsersTagAsync(new List<string>() { "" }, 2, 0).WaitResult();
            Assert.IsTrue(res.IsSuccess);

        }
        

        [TestMethod]
        public void GetOpenIdListTest()
        {
            var res = m_Api.GetOpenIdListAsync().WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }

        [TestMethod]
        public  void GetUserInfoTest()
        {
            var resTask = m_Api.GetUserInfoAsync(new WxOffcialUserInfoReq() { openid = "o7gE1s7610fM84Qapv4eBla5Yqcc" }).WaitResult();
            Assert.IsTrue(resTask.IsSuccess);
        }
        [TestMethod]
        public void GetUserInfoListTest()
        {
            var res = m_Api.GetUserInfoListAsync(new List<WxOffcialUserInfoReq>() { new WxOffcialUserInfoReq() { openid = "o7gE1s7610fM84Qapv4eBla5Yqcc" } }).WaitResult();

            Assert.IsTrue(res.IsSuccess);
        }


        private static WxOffMediaApi m_MediaApi = new WxOffMediaApi(m_Config);

        [TestMethod]
        public void UploadTempMediaTest()
        {
            var imageFile = new FileStream("E:\\1.jpg", FileMode.Open, FileAccess.Read);

            var req = new WxMediaTempUploadReq();
            req.type = WxMediaType.image;
            req.content_type = "image/jpeg";

            req.file_name = "1.jpg";
            req.file_stream = imageFile;

            var res = m_MediaApi.UploadTempMediaAsync(req).WaitResult();
            Assert.IsTrue(res.IsSuccess);
            //{ "type":"image","media_id":"w6q00gTWx6n6fsgBiM-VoKS32Uq-vNWhx5EpM85YyeG8IOk1FdPlJNo8bE7PFE6j","created_at":1487601780}
        }

        [TestMethod]
        public void GetTempMediaTest()
        {
            var res = m_MediaApi.DownloadTempMediaAsync("MrKJ-9MZ2EDTrVBM1D-dBcskHx6XcHlbx7JSi9J9MSpGvDMapQ9lYmg_p8R1ydDq").WaitResult();//MrKJ-9MZ2EDTrVBM1D-dBcskHx6XcHlbx7JSi9J9MSpGvDMapQ9lYmg_p8R1ydDq
            Assert.IsTrue(res.IsSuccess);
        }



        [TestMethod]
        public void UploadImgeMediaTest()
        {
            var imageFile = new FileStream("E:\\1.jpg", FileMode.Open, FileAccess.Read);

            var req = new WxFileReq();
            req.content_type = "image/jpeg";

            req.file_name = "1.jpg";
            req.file_stream = imageFile;

            var res = m_MediaApi.UploadFreeImageAsync(req).WaitResult();
            Assert.IsTrue(res.IsSuccess);
            //http://mmbiz.qpic.cn/mmbiz_jpg/N3louEAebXzhBzgsstFNBicyF1j1ZFIGgV55uQHPXLGDwIIDkvxrcnhEVGsEphEicICPLQ7Fh5kubPJg59u0rtFA/0
        }



        [TestMethod]
        public void UploadMediaTest()
        {
            var imageFile = new FileStream("E:\\1.jpg", FileMode.Open, FileAccess.Read);

            var req = new WxMediaUploadReq();
            req.type = WxMediaType.image;
            req.content_type = "image/jpeg";

            req.file_name = "1.jpg";
            req.file_stream = imageFile;

            var res = m_MediaApi.UploadMediaAsync(req).WaitResult();
            Assert.IsTrue(res.IsSuccess);

            // 1xOBXsBtRgetSsO8INAcQ1x8rkSc5MGMXuFfWxkGRDg
            // http://mmbiz.qpic.cn/mmbiz_jpg/N3louEAebXzhBzgsstFNBicyF1j1ZFIGgOaIEfWE2ra8KrwHvT5xuPlloMONKoj4rp5E5rFmfI8ZEz0qbSC4GFw/0?wx_fmt=jpeg
        }


        [TestMethod]
        public void UploadVedioMediaTest()
        {
            var imageFile = new FileStream("E:\\11.mp4", FileMode.Open, FileAccess.Read);

            var req = new WxMediaUploadReq();
            req.type = WxMediaType.video;
            req.content_type = "video/mpeg4";

            req.file_name = "11.mp4";
            req.file_stream = imageFile;

            req.introduction = "只是试一试好不好玩！";
            req.title = "只是个视频";

            var res = m_MediaApi.UploadMediaAsync(req).WaitResult();
            Assert.IsTrue(res.IsSuccess);

            //{ "media_id":"zXOYSQS_A3op3R9ZW0EYKwbjgQ544KTICzLWYAUgpfU"}
        }

        [TestMethod]
        public void GetVedioMediaUrlTest()
        {
            var res = m_MediaApi.GetMediaVedioUrlAsync("1xOBXsBtRgetSsO8INAcQxiKCT1JD-5toVEOzrnJ2r0").WaitResult();
            Assert.IsTrue(res.IsSuccess);
        }
    }
}
