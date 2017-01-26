#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：微信的公用枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-18
*       
*****************************************************************************/

#endregion

using System.ComponentModel;

namespace OSS.Social.WX.SysUtils.Mos
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
        [Description("男")]
        Male=1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female=2
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
        [Description("临时二维码")]
        QR_SCENE=10,


        [Description("永久二维码-值")]
        QR_LIMIT_SCENE=20,

        [Description("永久二维码-字符")]
        QR_LIMIT_STR_SCENE=21,

        [Description("卡券二维码")]
        QR_CARD=30,

        [Description("卡券二维码（多）")]
        QR_MULTIPLE_CARD=31

    }

    public enum WxJsTicketType
    {
        [Description("卡券接口")]
        wx_card = 30,

        [Description("正常js接口")]
        jsapi = 31
    }


}
