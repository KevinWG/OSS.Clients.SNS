#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 基础功能模块 枚举实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-1
*       
*****************************************************************************/

#endregion

namespace OSS.SnsSdk.Official.Wx.Basic.Mos
{
    /// <summary>
    /// 素材类型
    /// </summary>
    public enum WxMediaType
    {
        /// <summary>
        /// 图片（image）: 2M，支持PNG\JPEG\JPG\GIF格式
        /// </summary>
        image,

        /// <summary>
        ///  语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,

        /// <summary>
        ///   视频（video）：10MB，支持MP4格式
        /// </summary>
        video,

        /// <summary>
        ///  缩略图（thumb）：64KB，支持JPG格式
        /// </summary>
        thumb
    }

    /// <summary>
    ///  群发消息时的素材类型
    /// </summary>
    public enum WxMassMsgType
    {
        /// <summary>
        ///  图文素材
        /// </summary>
        mpnews,

        /// <summary>
        ///   文本内容
        /// </summary>
        text,

        /// <summary>
        /// 图片（image）: 2M，支持PNG\JPEG\JPG\GIF格式
        /// </summary>
        image,

        /// <summary>
        ///  语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,

        /// <summary>
        ///   视频（video）：10MB，支持MP4格式
        /// </summary>
        mpvideo,

        /// <summary>
        ///  微信卡券
        /// </summary>
        wxcard

    }

}
