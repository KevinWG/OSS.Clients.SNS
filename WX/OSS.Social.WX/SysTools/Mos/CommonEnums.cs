#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：微信的公用枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-18
*       
*****************************************************************************/

#endregion

using OSS.Common.Extention;

namespace OSS.Social.WX.SysTools.Mos
{
    /// <summary>
    ///  授权客户端类型
    /// </summary>
    public enum AuthClientType
    {
        /// <summary>
        /// PC网页版
        /// </summary>
        PC = 1,

        /// <summary>
        /// 微信公众号
        /// </summary>
        WxOffcial = 2
    }


    /// <summary>
    /// 微信性别枚举
    /// </summary>
    public enum WxSex
    {
        /// <summary>
        ///   男
        /// </summary>
        [OSDescript("男")]
        MALE=1,
        /// <summary>
        /// 女
        /// </summary>
        [OSDescript("女")]
        FEMALE=2
    }

    /// <summary>
    ///  系统类型
    /// </summary>
    public enum WxClientPlatform
    {
        /// <summary>
        /// 
        /// </summary>
        IOS=1,
        /// <summary>
        /// 
        /// </summary>
        Android=2,
        /// <summary>
        /// 
        /// </summary>
        Others=3
    }



    public enum WxQrCodeType
    {
        [OSDescript("临时二维码")]
        QR_SCENE=10,


        [OSDescript("永久二维码-值")]
        QR_LIMIT_SCENE=20,

        [OSDescript("永久二维码-字符")]
        QR_LIMIT_STR_SCENE=21,

        [OSDescript("卡券二维码")]
        QR_CARD=30,

        [OSDescript("卡券二维码（多）")]
        QR_MULTIPLE_CARD=31

    }

    public enum WxJsTicketType
    {
        /// <summary>
        /// 卡券接口
        /// </summary>
        [OSDescript("卡券接口")]
        wx_card = 10,

        /// <summary>
        /// 正常js接口
        /// </summary>
        [OSDescript("正常js接口")]
        jsapi = 20
    }


}
