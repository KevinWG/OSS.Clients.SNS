#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：OSS - 公号接收消息相关实体
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion
using OSS.Common.Extention;

namespace OSS.SnsSdk.Msg.Wx.Mos
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class WxTextRecMsg : WxBaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Content = this["Content"];
        }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class WxImageRecMsg : WxBaseRecMsg
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

    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class WxVoiceRecMsg : WxBaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Format = this["Format"];
            Recognition = this["Recognition"];
            MediaId = this["MediaId"];
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

    }

    /// <summary>
    /// 视频/小视频消息
    /// </summary>
    public class WxVideoRecMsg : WxBaseRecMsg
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

    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class WxLocationRecMsg : WxBaseRecMsg
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
       

    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class WxLinkRecMsg : WxBaseRecMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            Title = this["Title"];
            Description = this["Description"];
            Url = this["Url"];
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

    }

}
