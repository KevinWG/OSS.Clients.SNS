using OS.Common.Extention;

namespace OSS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextRecMsg : BaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Content = this["Content"];
            MsgId = this["MsgId"].ToInt64();
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
    public class ImageRecMsg : BaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            PicUrl = this["PicUrl"];
            MediaId = this["MediaId"];
            MsgId = this["MsgId"].ToInt64();
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
    public class VoiceRecMsg : BaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Format = this["Format"];
            Recognition = this["Recognition"];
            MediaId = this["MediaId"];
            MsgId = this["MsgId"].ToInt64();
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
    public class VideoRecMsg : BaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            ThumbMediaId = this["ThumbMediaId"];
            MediaId = this["MediaId"];
            MsgId = this["MsgId"].ToInt64();
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
    public class LocationRecMsg : BaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Latitude = this["Location_X"].ToDouble();
            Longitude =this["Location_Y"].ToDouble();
            Scale = this["Scale"].ToInt32();
            Label = this["Label"];
            MsgId = this["MsgId"].ToInt64();
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
    public class LinkRecMsg : BaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Title = this["Title"];
            Description = this["Description"];
            Url = this["Url"];
            MsgId = this["MsgId"].ToInt64();
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
