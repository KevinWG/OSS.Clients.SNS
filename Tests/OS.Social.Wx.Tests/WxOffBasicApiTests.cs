using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Social.WX.Offcial;
using OS.Social.WX.Offcial.Basic;
using OS.Social.WX.Offcial.Basic.Mos;
using OS.Social.WX.Sns;

namespace OS.Social.Wx.Tests
{
    /// <summary>
    /// WxUserOffcialApiTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxOffBasicApiTests:WxBaseOffApiTests
    {
        private static WxOffBasicApi  m_Api=new WxOffBasicApi(m_Config);

        [TestMethod]
        public void TagTests()
        {
            var res = m_Api.AddTag("我就是试一试！");
            Assert.IsTrue(res.IsSuccess||res.Ret==45157);
        }

        [TestMethod]
        public void UpdateTagTest()
        {
            var res = m_Api.UpdateTag(2, "我就是试一试！");
            Assert.IsTrue(res.IsSuccess || res.Ret == 45058);
        }

        [TestMethod]
        public void GetTagListTest()
        {
            var res = m_Api.GetTagList();
            Assert.IsTrue(res.IsSuccess);
        }
        //  todo  test


        [TestMethod]
        public void GetOpenIdListByTagTest()
        {
            var res = m_Api.GetOpenIdListByTag(2);
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void SetOrCancleUsersTagTest()
        {
            var res = m_Api.SetOrCancleUsersTag(new List<string>() {""}, 2,0);
            Assert.IsTrue(res.IsSuccess);

        }



        [TestMethod]
        public void GetOpenIdListTest()
        {
            var res = m_Api.GetOpenIdList();
            Assert.IsTrue(res.IsSuccess);
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            var res = m_Api.GetUserInfo(new WxOffcialUserInfoReq() {openid = "o7gE1s7610fM84Qapv4eBla5Yqcc" });
            Assert.IsTrue(res.IsSuccess);
        }
        [TestMethod]
        public void GetUserInfoListTest()
        {
            var res = m_Api.GetUserInfoList(new List<WxOffcialUserInfoReq>() { new WxOffcialUserInfoReq() { openid = "o7gE1s7610fM84Qapv4eBla5Yqcc" } });
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void UploadTempMediaTest()
        {
            var imageFile=new FileStream("E:\\1.jpg",FileMode.Open,FileAccess.Read);
            
            var req=new WxMediaTempUploadReq();
            req.type=MediaType.image;
            req.content_type = "image/jpeg";

            req.file_name = "1.jpg";
            req.file_stream = imageFile;

            var res = m_Api.UploadTempMedia(req);
            Assert.IsTrue(res.IsSuccess);
        }

        [TestMethod]
        public void GetTempMediaTest()
        {
            var res = m_Api.DownloadTempMedia("MrKJ-9MZ2EDTrVBM1D-dBcskHx6XcHlbx7JSi9J9MSpGvDMapQ9lYmg_p8R1ydDq");//MrKJ-9MZ2EDTrVBM1D-dBcskHx6XcHlbx7JSi9J9MSpGvDMapQ9lYmg_p8R1ydDq
            Assert.IsTrue(res.IsSuccess);
        }



        [TestMethod]
        public void UploadImgeMediaTest()
        {
            var imageFile = new FileStream("E:\\1.jpg", FileMode.Open, FileAccess.Read);

            var req = new WxMediaFileReq();
            req.content_type = "image/jpeg";

            req.file_name = "1.jpg";
            req.file_stream = imageFile;

            var res = m_Api.UploadFreeImage(req);
            Assert.IsTrue(res.IsSuccess);
            //http://mmbiz.qpic.cn/mmbiz_jpg/N3louEAebXzhBzgsstFNBicyF1j1ZFIGgV55uQHPXLGDwIIDkvxrcnhEVGsEphEicICPLQ7Fh5kubPJg59u0rtFA/0
        }



        [TestMethod]
        public void UploadMediaTest()
        {
            var imageFile = new FileStream("E:\\1.jpg", FileMode.Open, FileAccess.Read);

            var req = new WxMediaUploadReq();
            req.type = MediaType.image;
            req.content_type = "image/jpeg";

            req.file_name = "1.jpg";
            req.file_stream = imageFile;
            
            var res = m_Api.UploadMedia(req);
            Assert.IsTrue(res.IsSuccess);

           // 1xOBXsBtRgetSsO8INAcQ1x8rkSc5MGMXuFfWxkGRDg
           // http://mmbiz.qpic.cn/mmbiz_jpg/N3louEAebXzhBzgsstFNBicyF1j1ZFIGgOaIEfWE2ra8KrwHvT5xuPlloMONKoj4rp5E5rFmfI8ZEz0qbSC4GFw/0?wx_fmt=jpeg
        }


        [TestMethod]
        public void UploadVedioMediaTest()
        {
            var imageFile = new FileStream("E:\\11.mp4", FileMode.Open, FileAccess.Read);

            var req = new WxMediaUploadReq();
            req.type = MediaType.video;
            req.content_type = "video/mpeg4";

            req.file_name = "11.mp4";
            req.file_stream = imageFile;

            req.introduction = "只是试一试好不好玩！";
            req.title = "只是个视频";

            var res = m_Api.UploadMedia(req);
            Assert.IsTrue(res.IsSuccess);

            // 1xOBXsBtRgetSsO8INAcQxiKCT1JD-5toVEOzrnJ2r0
        }

        [TestMethod]
        public void GetVedioMediaUrlTest()
        {
           
            var res = m_Api.GetMediaVedioUrl("1xOBXsBtRgetSsO8INAcQxiKCT1JD-5toVEOzrnJ2r0");
            Assert.IsTrue(res.IsSuccess);
            
        }


    }



}
