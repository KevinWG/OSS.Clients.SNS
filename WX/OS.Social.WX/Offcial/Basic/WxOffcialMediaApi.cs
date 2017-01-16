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
using OS.Social.WX.Offcial.Basic.Mos;

namespace OS.Social.WX.Offcial.Basic
{
     public partial class WxOffcialApi
    {
        /// <summary>
        /// 上传素材接口【临时素材】
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
         public WxMediaUploadResp UploadMedia(WxMediaUploadReq request )
         {
            var req=new OsHttpRequest();
            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/upload?type=", request.type.ToString());
            req.FileParameterList.Add(new FileParameter("media", request.file_stream,request.file_name,request.content_type));

            return RestCommonOffcial<WxMediaUploadResp>(req);
         }

         /// <summary>
         ///  获取素材【临时素材】下载地址，请及时将素材存储下来
         ///     此地址请不要对外公开，图片等包含AccessToken信息，同样2个小时候地址过期
         /// </summary>
         /// <param name="mediaId"></param>
         /// <param name="type">主要用来判断是否是视频类型，如果是需要发起请求</param>
         /// <returns></returns>
         public ResultMo<string> GetMediaUrlById(string mediaId, MediaType type)
         {
             var accessToken = GetOffcialAccessToken();
             if (!accessToken.IsSuccess)
                 return accessToken.ConvertToResultOnly<string>();

             string addressUrl = string.Concat(m_ApiUrl,
                 $"/cgi-bin/media/get?access_token={accessToken.access_token}&media_id={mediaId}");
             if (type == MediaType.video)
             {
                 var req = new OsHttpRequest();
                 req.HttpMothed = HttpMothed.GET;
                 req.AddressUrl = addressUrl;

                 var vedioRes = RestCommonOffcial<WxMediaVideoUrlResp>(req);
                 if (!vedioRes.IsSuccess)
                     return vedioRes.ConvertToResultOnly<string>();

                 return new ResultMo<string>(vedioRes.video_url);
             }
             return new ResultMo<string>(addressUrl);
         }



         /// <summary>
         /// 添加永久微信图文素材列表（文章）
         /// </summary>
         /// <param name="list"></param>
         /// <returns></returns>
         public WxMediaResp AddArticles(IList<WxArticleInfo> list)
         {
             var req = new OsHttpRequest();
             req.HttpMothed = HttpMothed.POST;
             req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/material/add_news");
             req.CustomBody = JsonConvert.SerializeObject(new {articles = list});

             return RestCommonOffcial<WxMediaResp>(req);
         }
    }
}
