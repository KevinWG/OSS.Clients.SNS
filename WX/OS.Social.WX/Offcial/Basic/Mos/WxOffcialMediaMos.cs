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


namespace OS.Social.WX.Offcial.Basic.Mos
{
     class WxOffcialMediaMos
    {
    }


    /// <summary>
    /// 素材类型
    /// </summary>
    public enum MediaType
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
}
