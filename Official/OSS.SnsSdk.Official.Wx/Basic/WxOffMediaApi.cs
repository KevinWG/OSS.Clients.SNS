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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Common.Resp;
using OSS.Http.Extention;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Basic.Mos;



namespace OSS.SnsSdk.Official.Wx.Basic
{
    /// <summary>
    ///  素材管理接口
    /// </summary>
    public  class WxOffMediaApi:WxOffBaseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOffMediaApi(AppConfig config=null):base(config)
        {
        }

        #region  临时素材

        /// <summary>
        /// 上传素材接口【临时素材】
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxMediaTempUploadResp> UploadTempMediaAsync(WxMediaTempUploadReq request)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/upload?type=", request.type.ToString())
            };

            req.FileParameters.Add(new FileParameter("media", request.file_stream, request.file_name,
                request.content_type));

            return await RestCommonOffcialAsync<WxMediaTempUploadResp>(req);
        }

        /// <summary>
        ///  获取素材【临时素材】 文件流
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WxFileResp> DownloadTempMediaAsync(string mediaId)
        {
            var accessToken = await GetAccessTokenFromCacheAsync();
            if (!accessToken.IsSuccess())
                return new WxFileResp().WithResp(accessToken);// accessToken.ConvertToResultInherit<WxFileResp>();

            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl,
                    $"/cgi-bin/media/get?access_token={accessToken.access_token}&media_id={mediaId}")
            };

            return await req.RestCommon(DownLoadFileAsync);
        }


        /// <summary>
        ///  获取视频素材【临时素材】下载地址
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WxMediaTempVideoUrlResp> GetTempMediaVedioUrlAsync(string mediaId)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/get?media_id=", mediaId)
            };

            return await RestCommonOffcialAsync<WxMediaTempVideoUrlResp>(req);
        }

        #endregion

        #region  文章素材    属于永久素材

        /// <summary>
        /// 添加永久微信图文素材（文章组合）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<WxMediaResp> AddArticleGroupAsync(IList<WxArticleInfo> list)
        {
            var req = new OsHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_news");
            req.CustomBody = JsonConvert.SerializeObject(new {articles = list});

            return await RestCommonOffcialAsync<WxMediaResp>(req);
        }

        /// <summary>
        ///  上传图片并获取地址
        ///      没有mediaId【图文】【微店】
        /// </summary>
        /// <param name="imgReq"></param>
        /// <returns></returns>
        public async Task<WxArticleUploadImgResp> UploadFreeImageAsync(WxFileReq imgReq)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadimg");
            req.FileParameters.Add(new FileParameter("media", imgReq.file_stream, imgReq.file_name,
                imgReq.content_type));

            return await RestCommonOffcialAsync<WxArticleUploadImgResp>(req);
        }

        /// <summary>
        ///   获取图文素材（文章组合）
        /// </summary>
        /// <param name="mediaId">素材</param>
        /// <returns></returns>
        public async Task<WxGetArticleGroupResp> GetArticleGroupAsync(string mediaId)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_material");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\"}}";

            return await RestCommonOffcialAsync<WxGetArticleGroupResp>(req);
        }

        /// <summary>
        ///   修改图文素材列表中的文章接口
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="index"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<WxBaseResp> UpdateArticleAsync(string mediaId, int index, WxArticleInfo article)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/update_news");
            req.CustomBody = JsonConvert.SerializeObject(new {media_id = mediaId, index = index, articles = article});

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        #endregion

        #region    非文章类的其他永久素材

        /// <summary>
        ///  上传永久素材接口
        /// </summary>
        /// <param name="mediaReq"></param>
        /// <returns></returns>
        public async Task<WxMediaUploadResp> UploadMediaAsync(WxMediaUploadReq mediaReq)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_material?type=", mediaReq.type.ToString())
            };

            req.FileParameters.Add(new FileParameter("media", mediaReq.file_stream, mediaReq.file_name,
                mediaReq.content_type));

            if (mediaReq.type == WxMediaType.video)
                req.FormParameters.Add(new FormParameter("description",
                    $"{{\"title\":\"{mediaReq.title}\", \"introduction\":\"{mediaReq.introduction}\"}}"));

            return await RestCommonOffcialAsync<WxMediaUploadResp>(req);
        }


        /// <summary>
        ///  获取视频【永久】素材下载地址
        /// </summary>
        /// <param name="mediaId">素材</param>
        /// <returns></returns>
        public async Task<WxMediaVedioUrlResp> GetMediaVedioUrlAsync(string mediaId)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_material"),
                CustomBody = $"{{\"media_id\":\"{mediaId}\"}}"
            };


            return await RestCommonOffcialAsync<WxMediaVedioUrlResp>(req);
        }


        /// <summary>
        ///  下载【永久】素材文件
        ///    视频  和  图文 请通过另外两个接口获取
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WxFileResp> DownloadMediaAsync(string mediaId)
        {
            var accessToken = await GetAccessTokenFromCacheAsync();
            if (!accessToken.IsSuccess())
                return new WxFileResp().WithResp(accessToken);// accessToken.ConvertToResultInherit<WxFileResp>();

            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl,
                    $"/cgi-bin/material/get_material?access_token=", accessToken.access_token)
            };

            return await req.RestCommon(DownLoadFileAsync);
        }

        /// <summary>
        ///  删除【永久】素材
        /// </summary>
        /// <param name="mediaId">素材Id</param>
        /// <returns></returns>
        public async Task<WxBaseResp> DeleteMediaAsync(string mediaId)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/del_material"),
                CustomBody = $"{{\"media_id\":\"{mediaId}\"}}"
            };
            
            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        #endregion


        /// <summary>
        ///   获取素材总数
        /// </summary>
        /// <returns></returns>
        public async Task<WxMediaCountResp> GetMediaCountAsync()
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_materialcount")
            };

            return await RestCommonOffcialAsync<WxMediaCountResp>(req);
        }


        /// <summary>
        ///   获取素材列表
        /// </summary>
        /// <returns></returns>
        public async Task<WxGetMediaListResp> GetMediaListAsync(WxGetMediaListReq request)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/batchget_material"),
                CustomBody = JsonConvert.SerializeObject(request)
            };

            return await RestCommonOffcialAsync<WxGetMediaListResp>(req);
        }

    }
}
