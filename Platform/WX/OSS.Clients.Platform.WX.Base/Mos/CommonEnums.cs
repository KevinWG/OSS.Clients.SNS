﻿#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信的公用枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-18
*       
*****************************************************************************/

#endregion

namespace OSS.Clients.Platform.WX.Base.Mos
{
    /// <summary>
    /// 微信性别枚举
    /// </summary>
    public enum WXSex
    {
        /// <summary>
        ///   男
        /// </summary>
         MALE = 1,

        /// <summary>
        /// 女
        /// </summary>
         FEMALE = 2
    }

    /// <summary>
    ///  系统类型
    /// </summary>
    public enum WXClientPlatform
    {
        /// <summary>
        /// 
        /// </summary>
        IOS = 1,

        /// <summary>
        /// 
        /// </summary>
        Android = 2,

        /// <summary>
        /// 
        /// </summary>
        Others = 3
    }

    /// <summary>
    /// 二维码类型枚举
    /// </summary>
    public enum WXQrCodeType
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


    /// <summary>
    ///  jsticket 类型
    /// </summary>
    public enum WXJsTicketType
    {
        /// <summary>
        /// 卡券接口
        /// </summary>
       wx_card = 10,

        /// <summary>
        /// 正常js接口
        /// </summary>
         jsapi = 20
    }

}
