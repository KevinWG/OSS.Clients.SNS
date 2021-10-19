#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

namespace OSS.Clients.Platform.Wechat
{
    /// <summary>
    /// 二维码类型枚举
    /// </summary>
    public enum WechatQrCodeType
    {
        /// <summary>
        /// 临时二维码
        /// </summary>
        QR_SCENE = 10,

        /// <summary>
        ///  临时字符串二维码
        /// </summary>
        QR_STR_SCENE = 12,

        /// <summary>
        /// 永久二维码-值
        /// </summary>

        QR_LIMIT_SCENE = 20,

        /// <summary>
        /// 永久二维码-字符
        /// </summary>
        QR_LIMIT_STR_SCENE = 21,

        /// <summary>
        /// 卡券二维码
        /// </summary>
        QR_CARD = 30,

        /// <summary>
        /// 卡券二维码（多）
        /// </summary>
        QR_MULTIPLE_CARD = 31

    }
}