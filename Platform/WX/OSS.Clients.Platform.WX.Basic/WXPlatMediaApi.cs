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
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Basic
{
    /// <summary>
    ///  素材管理接口
    /// </summary>
    public  class WXPlatMediaApi:WXPlatBaseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WXBaseApi.DefaultConfig 属性赋值</param>
        public WXPlatMediaApi(AppConfig config=null):base(config)
        {
        }

        #region  临时素材

        /// <summary>
        /// 上传素材接口【临时素材】
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WXMediaTempUploadResp> UploadTempMediaAsync(WXMediaTempUploadReq request)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/upload?type=", request.type.ToString())
            };

            req.FileParameters.Add(new FileParameter("media", request.file_stream, request.file_name,
                request.content_type));

            return await RestCommonPlatAsync<WXMediaTempUploadResp>(req);
        }

        /// <summary>
        ///  获取素材【临时素材】 文件流
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WXFileResp> DownloadTempMediaAsync(string mediaId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl,$"/cgi-bin/media/get?media_id={mediaId}")
            };

            return await DownLoadFileAsync(req);
        }


        /// <summary>
        ///  获取视频素材【临时素材】下载地址
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WXMediaTempVideoUrlResp> GetTempMediaVedioUrlAsync(string mediaId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/get?media_id=", mediaId)
            };

            return await RestCommonPlatAsync<WXMediaTempVideoUrlResp>(req);
        }

        #endregion

        #region  文章素材    属于永久素材

        /// <summary>
        /// 添加永久微信图文素材（文章组合）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<WXMediaResp> AddArticleGroupAsync(IList<WXArticleInfo> list)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_news");
            req.CustomBody = JsonConvert.SerializeObject(new {articles = list});

            return await RestCommonPlatAsync<WXMediaResp>(req);
        }

        /// <summary>
        ///  上传图片并获取地址
        ///      没有mediaId【图文】【微店】
        /// </summary>
        /// <param name="imgReq"></param>
        /// <returns></returns>
        public async Task<WXArticleUploadImgResp> UploadFreeImageAsync(WXFileReq imgReq)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/uploadimg");
            req.FileParameters.Add(new FileParameter("media", imgReq.file_stream, imgReq.file_name,
                imgReq.content_type));

            return await RestCommonPlatAsync<WXArticleUploadImgResp>(req);
        }

        /// <summary>
        ///   获取图文素材（文章组合）
        /// </summary>
        /// <param name="mediaId">素材</param>
        /// <returns></returns>
        public async Task<WXGetArticleGroupResp> GetArticleGroupAsync(string mediaId)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_material");
            req.CustomBody = $"{{\"media_id\":\"{mediaId}\"}}";

            return await RestCommonPlatAsync<WXGetArticleGroupResp>(req);
        }

        /// <summary>
        ///   修改图文素材列表中的文章接口
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="index"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> UpdateArticleAsync(string mediaId, int index, WXArticleInfo article)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/update_news");
            req.CustomBody = JsonConvert.SerializeObject(new {media_id = mediaId, index = index, articles = article});

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        #endregion

        #region    非文章类的其他永久素材

        /// <summary>
        ///  上传永久素材接口
        /// </summary>
        /// <param name="mediaReq"></param>
        /// <returns></returns>
        public async Task<WXMediaUploadResp> UploadMediaAsync(WXMediaUploadReq mediaReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_material?type=", mediaReq.type.ToString())
            };

            req.FileParameters.Add(new FileParameter("media", mediaReq.file_stream, mediaReq.file_name,
                mediaReq.content_type));

            if (mediaReq.type == WXMediaType.video)
                req.FormParameters.Add(new FormParameter("description",
                    $"{{\"title\":\"{mediaReq.title}\", \"introduction\":\"{mediaReq.introduction}\"}}"));

            return await RestCommonPlatAsync<WXMediaUploadResp>(req);
        }


        /// <summary>
        ///  获取视频【永久】素材下载地址
        /// </summary>
        /// <param name="mediaId">素材</param>
        /// <returns></returns>
        public async Task<WXMediaVedioUrlResp> GetMediaVedioUrlAsync(string mediaId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_material"),
                CustomBody = $"{{\"media_id\":\"{mediaId}\"}}"
            };


            return await RestCommonPlatAsync<WXMediaVedioUrlResp>(req);
        }


        /// <summary>
        ///  下载【永久】素材文件
        ///    视频  和  图文 请通过另外两个接口获取
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<WXFileResp> DownloadMediaAsync(string mediaId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl,"/cgi-bin/material/get_material")
            };

            return await DownLoadFileAsync(req);
        }

        /// <summary>
        ///  删除【永久】素材
        /// </summary>
        /// <param name="mediaId">素材Id</param>
        /// <returns></returns>
        public async Task<WXBaseResp> DeleteMediaAsync(string mediaId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/del_material"),
                CustomBody = $"{{\"media_id\":\"{mediaId}\"}}"
            };
            
            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        #endregion


        /// <summary>
        ///   获取素材总数
        /// </summary>
        /// <returns></returns>
        public async Task<WXMediaCountResp> GetMediaCountAsync()
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/get_materialcount")
            };

            return await RestCommonPlatAsync<WXMediaCountResp>(req);
        }


        /// <summary>
        ///   获取素材列表
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetMediaListResp> GetMediaListAsync(WXGetMediaListReq request)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/batchget_material"),
                CustomBody = JsonConvert.SerializeObject(request)
            };

            return await RestCommonPlatAsync<WXGetMediaListResp>(req);
        }

    }
}
