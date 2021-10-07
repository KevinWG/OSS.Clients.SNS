namespace OSS.Clients.Platform.WX
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