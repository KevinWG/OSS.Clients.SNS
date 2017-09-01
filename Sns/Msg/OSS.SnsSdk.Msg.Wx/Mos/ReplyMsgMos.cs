#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄基类，主要声明相关事件
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace OSS.SnsSdk.Msg.Wx.Mos
{
    /// <summary>
    /// 无回复信息
    /// </summary>
    public class WxNoneReplyMsg : WxBaseReplyMsg
    {
        /// <summary>
        ///  默认none对象
        /// </summary>
        public static WxNoneReplyMsg None => new WxNoneReplyMsg();

        public WxNoneReplyMsg()
        {
            MsgType = string.Empty;
        }
        /// <summary>
        ///  缺省情况下直接回复 success
        /// </summary>
        /// <returns></returns>
        public override string ToReplyXml() => "success";
    }
    /// <summary>
    /// 回复文本消息
    /// </summary>
    public class WxTextReplyMsg : WxBaseReplyMsg
    {
        public WxTextReplyMsg()
        {
            MsgType = "text";
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        protected override void SetValueToXml()
        {
            this["Content"]= Content;
        }
    }

    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class WxImageReplyMsg : WxBaseReplyMsg
    {
        /// <inheritdoc />
        public WxImageReplyMsg()
        {
            MsgType = "image";
        }

        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        /// <inheritdoc />
        protected override void SetValueToXml()
        {
            var image = new Dictionary<string, object> {{"MediaId", MediaId}};

            this["Image"]= image;
        }
    }

    /// <summary>
    /// 回复语音消息
    /// </summary>
    public class WxVoiceReplyMsg : WxBaseReplyMsg
    {
        public WxVoiceReplyMsg()
        {
            MsgType = "voice";
        }

        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        protected override void SetValueToXml()
        {
            var voice = new Dictionary<string, object> {{"MediaId", MediaId}};

            this["Voice"]= voice;
        }
    }

    /// <summary>
    /// 回复视频消息
    /// </summary>
    public class WxVideoReplyMsg : WxBaseReplyMsg
    {
        public WxVideoReplyMsg()
        {
            MsgType = "video";
        }

        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }

        protected override void SetValueToXml()
        {
            var video = new Dictionary<string, object>
            {
                {"MediaId", MediaId},
                {"Title", Title},
                {"Description", Description}
            };

            this["Video"]= video;
        }
    }

    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class WxMusicReplyMsg : WxBaseReplyMsg
    {
        public WxMusicReplyMsg()
        {
            MsgType = "music";
        }

        /// <summary>
        /// 缩略图的媒体id，通过素材管理接口上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 音乐标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 音乐描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicURL { get; set; }

        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl { get; set; }

        protected override void SetValueToXml()
        {
            var music = new Dictionary<string, object>()
            {
                {"Title", Title},
                {"Description", Description},
                {"MusicURL", MusicURL},
                {"HQMusicUrl", HQMusicUrl},
                {"ThumbMediaId", ThumbMediaId}
            };


            this["Music"]= music;
        }
    }

    /// <summary>
    /// 回复图文消息
    /// </summary>
    public class WxNewsReplyMsg : WxBaseReplyMsg
    {
        public WxNewsReplyMsg()
        {
            MsgType = "news";
            Items = new List<WxArticleItem>();
        }

        /// <summary>
        /// 图文数量
        /// </summary>
        public int ArticleCount { get; internal set; }

        /// <summary>
        /// 图文列表
        /// </summary>
        public List<WxArticleItem> Items { get; set; }

        /// <inheritdoc />
        protected override void SetValueToXml()
        {
            this["ArticleCount"]= Items.Count;

            var items = new Dictionary<string, object>();
            foreach (var item in Items)
            {
                var itemDetails = new Dictionary<string, object>
                {
                    {"Title", item.Title},
                    {"Description", item.Description},
                    {"PicUrl", item.PicUrl},
                    {"Url", item.Url}
                };
                items.Add("item", itemDetails);
            }
            this["Articles"]= items;
        }

        public class WxArticleItem
        {
            /// <summary>
            /// 图文消息标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 图文消息描述
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
            /// </summary>
            public string PicUrl { get; set; }
            /// <summary>
            /// 点击图文消息跳转链接
            /// </summary>
            public string Url { get; set; }
        }
    }
}