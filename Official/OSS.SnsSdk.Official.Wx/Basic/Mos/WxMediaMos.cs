#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述: 公号素材管理实体Mo
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-16
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace OSS.SnsSdk.Official.Wx.Basic.Mos
{
    #region  正常素材文件上传的基础实体
    /// <summary>
    /// 素材上传请求基础实体
    ///    文章中的图片上传请求也使用这个实体
    /// </summary>
    public class WxFileReq
    {
        /// <summary>
        /// formdata中的文件名称  如：my_photo_20170116.jpg
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        ///  请求中的contentteype
        /// </summary>
        public string content_type { get; set; }

        /// <summary>
        ///   文件的流，调用方会自动调用Dispose方法
        ///   防止文件较大时byte 传值太大，调用时使用了buffer处理
        /// </summary>
        public Stream file_stream { get; set; }
    }
    /// <summary>
    /// 文件下载的响应实体
    /// </summary>
    public class WxFileResp:WxBaseResp
    {
        /// <summary>
        ///  请求中的contentteype
        /// </summary>
        public string content_type { get; set; }

        /// <summary>
        ///  文件的字节流
        /// </summary>
        public byte[] file { get; set; }
    }

    /// <summary>
    ///  微信素材添加基础响应实体
    /// </summary>
    public class WxMediaResp : WxBaseResp
    {
        /// <summary>   
        ///   媒体文件上传后，获取标识
        /// </summary>  
        public string media_id { get; set; }
    }

    #endregion

    #region  上传 【永久】 请求及响应参数

    /// <summary>
    /// 上传【永久】素材请求参数
    /// </summary>
    public class WxMediaUploadReq:WxFileReq
    {
        /// <summary>
        /// 素材类型
        /// </summary>
        public WxMediaType type { get; set; }
        
        /// <summary>
        /// 视频素材的标题    可空  【视频】类型素材需要 
        /// </summary>
        public string title { get; set; }


        /// <summary>
        /// 视频素材的描述    可空  【视频】类型素材需要 
        /// </summary>
        public string introduction { get; set; }
    }

    /// <summary>
    /// 上传【永久】素材响应接口
    /// </summary>
    public class WxMediaUploadResp : WxMediaResp
    {
        /// <summary>
        ///   新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 获取【永久】视频素材的地址
    /// </summary>
    public class WxMediaVedioUrlResp:WxBaseResp
    {
        /// <summary>
        ///  视频标题
        /// </summary>
        public string title { get; set; }


        /// <summary>
        /// 视频描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string down_url { get; set; }
    }

    #endregion

    #region  上传【临时】 请求及响应参数
    /// <summary>
    /// 上传【临时】素材请求参数
    /// </summary>
    public class WxMediaTempUploadReq : WxFileReq
    {
        /// <summary>
        /// 素材类型
        /// </summary>
        public WxMediaType type { get; set; }
    }
    
    /// <summary>
    /// 上传【临时】素材响应接口
    /// </summary>
    public class WxMediaTempUploadResp: WxMediaResp
    {
        /// <summary>   
        ///   媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）
        /// </summary>  
        public string type { get; set; }

        /// <summary>   
        ///   媒体文件上传时间戳
        /// </summary>  
        public string created_at { get; set; }
    }

    /// <summary>
    /// 获取【临时素材】视频类型 下载地址 响应实体
    /// </summary>
    public class WxMediaTempVideoUrlResp:WxBaseResp
    {
        /// <summary>
        /// 视频下载地址
        /// </summary>
        public string video_url { get; set; }
    }
    
    #endregion

    #region  文章素材，以及文章中图片上传响应实体
    /// <summary>
    ///  微信图文素材
    /// </summary>
    public class WxArticleInfo
    {
        /// <summary>   
        ///   必填    标题
        /// </summary>  
        public string title { get; set; }

        /// <summary>   
        ///   必填    图文消息的封面图片素材id（必须是永久mediaID）
        /// </summary>  
        public string thumb_media_id { get; set; }

        /// <summary>   
        ///   必填    作者
        /// </summary>  
        public string author { get; set; }

        /// <summary>   
        ///   必填    图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空
        /// </summary>  
        public string digest { get; set; }

        /// <summary>   
        ///   必填    是否显示封面，0为false，即不显示，1为true，即显示
        /// </summary>  
        public string show_cover_pic { get; set; }

        /// <summary>   
        ///   必填    图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS,涉及图片url必须来源"上传图文消息内的图片获取URL"接口获取。外部图片url将被过滤。
        /// </summary>  
        public string content { get; set; }

        /// <summary>   
        ///   必填    图文消息的原文地址，即点击“阅读原文”后的URL
        /// </summary>  
        public string content_source_url { get; set; }
    }

    /// <summary>
    /// 微信中文章图片上传响应实体
    /// </summary>
    public class WxArticleUploadImgResp:WxBaseResp
    { 
        /// <summary>
        ///   图片地址，直接在文章中使用
        /// </summary>
        public string url { get; set; }
    }


    /// <summary>
    ///   获取文章组合响应实体
    /// </summary>
    public class WxGetArticleGroupResp : WxBaseResp
    {
        /// <summary>
        ///   文章组合列表
        /// </summary>
        public List<WxArticleInfo> news_item { get; set; }
    }

    #endregion


    /// <summary>
    ///    获取素材总数响应实体
    /// </summary>
    public class WxMediaCountResp:WxBaseResp
    {
        /// <summary>   
        ///   语音总数量
        /// </summary>  
        public string voice_count { get; set; }

        /// <summary>   
        ///   视频总数量
        /// </summary>  
        public string video_count { get; set; }

        /// <summary>   
        ///   图片总数量
        /// </summary>  
        public string image_count { get; set; }

        /// <summary>   
        ///   图文总数量
        /// </summary>  
        public string news_count { get; set; }
    }

    /// <summary>
    ///   获取素材列表请求实体
    /// </summary>
    public class WxGetMediaListReq
    {
        /// <summary>   
        ///   必填    素材的类型，图片（image）、视频（video）、语音（voice）、图文（news）
        /// </summary>  
        [JsonConverter(typeof(StringEnumConverter))]
        public WxMediaType type { get; set; }

        /// <summary>   
        ///   必填    从全部素材的该偏移位置开始返回，0表示从第一个素材返回
        /// </summary>  
        public int offset { get; set; }

        /// <summary>   
        ///   必填    返回素材的数量，取值在1到20之间
        /// </summary>  
        public int count { get; set; }
    }


    /// <summary>
    /// 获取素材列表
    /// </summary>
    public class WxGetMediaListResp : WxBaseResp
    {
        /// <summary>   
        ///   该类型的素材的总数
        /// </summary>  
        public int total_count { get; set; }

        /// <summary>   
        ///   本次调用获取的素材的数量
        /// </summary>  
        public int item_count { get; set; }

        /// <summary>
        ///   素材列表
        /// </summary>
        public List<MediaListItem> item { get; set; }

    }

    /// <summary>
    ///    素材列表Item
    /// </summary>
    public class MediaListItem
    {
        /// <summary>
        ///   素材Id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>   
        ///   这篇图文消息素材的最后更新时间
        /// </summary>  
        public long update_time { get; set; }

        /// <summary>   
        ///   文件名称
        /// </summary>  
        public string name { get; set; }

        /// <summary>
        ///   地址
        /// </summary>
        public string url { get; set; }


        /// <summary>
        /// 图文内容
        /// </summary>
        public object content {
            set
            {
                object _content = value;
                news_lsit = _content != null ? ((JToken) _content)["news_item"].ToObject<List<WxArticleInfo>>():new List<WxArticleInfo>();
            }
        }

        /// <summary>
        ///  图文列表
        /// </summary>
        public List<WxArticleInfo> news_lsit { get;private set; }

    }


   

 
}
