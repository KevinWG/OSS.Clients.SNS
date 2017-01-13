using System;

namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMsg : BaseRecContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            Content = GetValue("Content");
            MsgId = Convert.ToInt64(GetValue("MsgId"));
        }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMsg : BaseRecContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            PicUrl = GetValue("PicUrl");
            MediaId = GetValue("MediaId");
            MsgId = Convert.ToInt64(GetValue("MsgId"));
        }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMsg : BaseRecContext
    {

        protected override void FormatProperties()
        {
            base.FormatProperties();

            Format = GetValue("Format");
            Recognition = GetValue("Recognition");
            MediaId = GetValue("MediaId");
            MsgId = Convert.ToInt64(GetValue("MsgId"));
        }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string Recognition { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 视频/小视频消息
    /// </summary>
    public class VideoMsg : BaseRecContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            ThumbMediaId = GetValue("ThumbMediaId");
            MediaId = GetValue("MediaId");
            MsgId = Convert.ToInt64(GetValue("MsgId"));
        }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class LocationMsg : BaseRecContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            Latitude = Convert.ToDouble(GetValue("Location_X"));
            Longitude = Convert.ToDouble(GetValue("Location_Y"));
            Scale = Convert.ToInt32(GetValue("Scale"));
            Label = GetValue("Label");
            MsgId = Convert.ToInt64(GetValue("MsgId"));
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class LinkMsg : BaseRecContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            Title = GetValue("Title");
            Description = GetValue("Description");
            Url = GetValue("Url");
            MsgId = Convert.ToInt64( GetValue("MsgId"));
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
    }

}
