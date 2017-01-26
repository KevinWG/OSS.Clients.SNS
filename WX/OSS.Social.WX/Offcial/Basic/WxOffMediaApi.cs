#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述: 公号素材管理部分
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-16
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using Newtonsoft.Json;
using OS.Common.ComModels;
using OS.Http;
using OS.Http.Models;
using OSS.Social.WX.Offcial.Basic.Mos;

namespace OSS.Social.WX.Offcial.Basic
{
     public partial class WxOffBasicApi
    {

        #region  临时素材

        /// <summary>
        /// 上传素材接口【临时素材】
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public WxMediaTempUploadResp UploadTempMedia(WxMediaTempUploadReq request )
         {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/upload?type=", request.type.ToString());
            req.FileParameterList.Add(new FileParameter("media", request.file_stream,request.file_name,request.content_type));

            return RestCommonOffcial<WxMediaTempUploadResp>(req);
         }

         /// <summary>
         ///  获取素材【临时素材】 文件流
         /// </summary>
         /// <param name="mediaId"></param>
         /// <returns></returns>
         public ResultMo<byte[]> DownloadTempMedia(string mediaId)
         {
             var accessToken = GetOffcialAccessToken();
             if (!accessToken.IsSuccess)
                 return accessToken.ConvertToResultOnly<byte[]>();

             var req = new OsHttpRequest();
             req.HttpMothed = HttpMothed.GET;
             req.AddressUrl = string.Concat(m_ApiUrl,
                 $"/cgi-bin/media/get?access_token={accessToken.access_token}&media_id={mediaId}");

            return RestCommon(req, resp =>
             {
                 if (!resp.ContentType.Contains("application/json"))
                     return new ResultMo<byte[]>(resp.RawBytes);
                 var res = JsonConvert.DeserializeObject<WxBaseResp>(resp.Content);

                 return res.ConvertToResultOnly<byte[]>();
             });
         }


         /// <summary>
         ///  获取视频素材【临时素材】下载地址
         /// </summary>
         /// <param name="mediaId"></param>
         /// <returns></returns>
         public WxMediaTempVideoUrlResp GetTempMediaVedioUrl(string mediaId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/get?media_id=", mediaId);

            return RestCommonOffcial<WxMediaTempVideoUrlResp>(req);
        }

        #endregion

        #region  文章素材    属于永久素材

        /// <summary>
        /// 添加永久微信图文素材（文章组合）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public WxMediaResp AddArticleGroup(IList<WxArticleInfo> list)
         {
             var req = new OsHttpRequest();
             req.HttpMothed = HttpMothed.POST;
             req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_news");
             req.CustomBody = JsonConvert.SerializeObject(new {articles = list});

             return RestCommonOffcial<WxMediaResp>(req);
         }

        /// <summary>
        ///  上传图片并获取地址
        ///      没有mediaId【图文】【微店】
        /// </summary>
        /// <param name="imgReq"></param>
        /// <returns></returns>
         public WxArticleUploadImgResp UploadFreeImage(WxMediaFileReq imgReq)
         {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadimg");
            req.FileParameterList.Add(new FileParameter("media",imgReq.file_stream,imgReq.file_name,imgReq.content_type));

            return RestCommonOffcial<WxArticleUploadImgResp>(req);
         }

         /// <summary>
         ///   获取图文素材（文章组合）
         /// </summary>
         /// <param name="mediaId">素材</param>
         /// <returns></returns>
         public WxGetArticleGroupResp GetArticleGroup(string mediaId)
         {
            var req=new OsHttpRequest();

            req.HttpMothed =HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_material");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\"}}";

             return RestCommonOffcial<WxGetArticleGroupResp>(req);
         }
        
        /// <summary>
        ///   修改图文素材列表中的文章接口
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="index"></param>
        /// <param name="article"></param>
        /// <returns></returns>
         public WxBaseResp UpdateArticle(string mediaId,int index,WxArticleInfo article)
         {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/update_news");
            req.CustomBody =JsonConvert.SerializeObject(new { media_id =mediaId,index=index, articles = article });

            return RestCommonOffcial<WxGetArticleGroupResp>(req);
        }

         #endregion
        
        #region    非文章类的其他永久素材

        /// <summary>
        ///  上传永久素材接口
        /// </summary>
        /// <param name="mediaReq"></param>
        /// <returns></returns>
         public WxMediaUploadResp UploadMedia(WxMediaUploadReq mediaReq)
         {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_material?type=", mediaReq.type.ToString());
            req.FileParameterList.Add(new FileParameter("media",mediaReq.file_stream,mediaReq.file_name,mediaReq.content_type));

            if (mediaReq.type == MediaType.video)
                req.Parameters.Add(new Parameter("description", $"{{\"title\":\"{mediaReq.title}\", \"introduction\":\"{mediaReq.introduction}\"}}", ParameterType.Form));

            return RestCommonOffcial<WxMediaUploadResp>(req);
         }


        /// <summary>
        ///  获取视频【永久】素材下载地址
        /// </summary>
        /// <param name="mediaId">素材</param>
        /// <returns></returns>
        public WxMediaVedioUrlResp GetMediaVedioUrl(string mediaId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_material");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\"}}";

            return RestCommonOffcial<WxMediaVedioUrlResp>(req);
        }


        /// <summary>
        ///  下载【永久】素材文件
        ///    视频  和  图文 请通过另外两个接口获取
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public ResultMo<byte[]> DownloadMedia(string mediaId)
        {
            var accessToken = GetOffcialAccessToken();
            if (!accessToken.IsSuccess)
                return accessToken.ConvertToResultOnly<byte[]>();

            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl,
                $"/cgi-bin/material/get_material?access_token=", accessToken.access_token);

            return RestCommon(req, resp =>
            {
                if (!resp.ContentType.Contains("application/json"))
                    return new ResultMo<byte[]>(resp.RawBytes);

                var resJson = JsonConvert.DeserializeObject<WxBaseResp>(resp.Content);
                return resJson.ConvertToResultOnly<byte[]>();
            });
        }

        /// <summary>
        ///  删除【永久】素材
        /// </summary>
        /// <param name="mediaId">素材Id</param>
        /// <returns></returns>
        public WxBaseResp DeleteMedia(string mediaId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/del_material");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\"}}";

            return RestCommonOffcial<WxBaseResp>(req);
        }

        #endregion


        /// <summary>
        ///   获取素材总数
        /// </summary>
        /// <returns></returns>
         public WxMediaCountResp GetMediaCount()
         {
            var req=new OsHttpRequest();
            req.HttpMothed=HttpMothed.GET;
             req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_materialcount");

             return RestCommonOffcial<WxMediaCountResp>(req);
         }


        /// <summary>
        ///   获取素材列表
        /// </summary>
        /// <returns></returns>
        public WxGetMediaListResp GetMediaList(WxGetMediaListReq request)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/batchget_material");
            req.CustomBody = JsonConvert.SerializeObject(request);

            return RestCommonOffcial<WxGetMediaListResp>(req);
        }

    }
}
