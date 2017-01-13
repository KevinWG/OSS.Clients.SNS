using System;
using System.Collections.Generic;

namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 无回复信息
    /// </summary>
    public class NoReplyMsg : BaseReplyContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NoReplyMsg()
        {
            MsgType = ReplyMsgType.None;
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
    public class TextReplyMsg : BaseReplyContext
    {
        public TextReplyMsg()
        {
            MsgType = ReplyMsgType.Text;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "text");
            SetReplyXmlValue("Content", Content);
        }
    }

    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class ImageReplyMsg : BaseReplyContext
    {
        public ImageReplyMsg()
        {
            MsgType = ReplyMsgType.Image;
        }

        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "image");

            var image = new List<Tuple<string, object>>();
            image.Add(Tuple.Create("MediaId", (object)MediaId));

            SetReplyXmlValue("Image", image);
        }
    }

    /// <summary>
    /// 回复语音消息
    /// </summary>
    public class VoiceReplyMsg : BaseReplyContext
    {
        public VoiceReplyMsg()
        {
            MsgType = ReplyMsgType.Voice;
        }

        /// <summary>
        /// 通过素材管理接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "voice");

            var voice = new List<Tuple<string, object>>();
            voice.Add(Tuple.Create("MediaId", (object)MediaId));

            SetReplyXmlValue("Voice", voice);
        }
    }

    /// <summary>
    /// 回复视频消息
    /// </summary>
    public class VideoReplyMsg : BaseReplyContext
    {
        public VideoReplyMsg()
        {
            MsgType = ReplyMsgType.Video;
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

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "video");

            var video = new List<Tuple<string, object>>();
            video.Add(Tuple.Create("MediaId", (object)MediaId));
            video.Add(Tuple.Create("Title", (object)Title));
            video.Add(Tuple.Create("Description", (object)Description));

            SetReplyXmlValue("Video", video);
        }
    }

    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class MusicReplyMsg : BaseReplyContext
    {
        public MusicReplyMsg()
        {
            MsgType = ReplyMsgType.Music;
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

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "music");

            var music = new List<Tuple<string, object>>();
            music.Add(Tuple.Create("Title", (object)Title));
            music.Add(Tuple.Create("Description", (object)Description));
            music.Add(Tuple.Create("MusicURL", (object)MusicURL));
            music.Add(Tuple.Create("HQMusicUrl", (object)HQMusicUrl));
            music.Add(Tuple.Create("ThumbMediaId", (object)ThumbMediaId));

            SetReplyXmlValue("Music", music);
        }
    }

    /// <summary>
    /// 回复图文消息
    /// </summary>
    public class NewsReplyMsg : BaseReplyContext
    {
        public NewsReplyMsg()
        {
            MsgType = ReplyMsgType.News;
            Items = new List<ArticleItem>();
        }

        /// <summary>
        /// 图文数量
        /// </summary>
        public int ArticleCount { get; internal set; }

        /// <summary>
        /// 图文列表
        /// </summary>
        public List<ArticleItem> Items { get; set; }

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "news");
            SetReplyXmlValue("ArticleCount", Items.Count);

            var items = new List<Tuple<string, object>>();
            foreach (var item in Items)
            {
                var itemDetails = new List<Tuple<string, object>>();
                itemDetails.Add(Tuple.Create("Title", (object)item.Title));
                itemDetails.Add(Tuple.Create("Description", (object)item.Description));
                itemDetails.Add(Tuple.Create("PicUrl", (object)item.PicUrl));
                itemDetails.Add(Tuple.Create("Url", (object)item.Url));

                items.Add(Tuple.Create("item", (object)itemDetails));
            }
            SetReplyXmlValue("Articles", items);
        }

        public class ArticleItem
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

    /// <summary>
    /// 转客服
    /// </summary>
    public class KeFuMsg : BaseReplyContext
    {
        public KeFuMsg()
        {
            MsgType = ReplyMsgType.KeFu;
        }

        protected override void FormatXml()
        {
            base.FormatXml();
            SetReplyXmlValue("MsgType", "transfer_customer_service");
        }
    }
}