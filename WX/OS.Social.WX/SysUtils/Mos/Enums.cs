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

namespace OS.Social.WX.SysUtils.Mos
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



    /// <summary>
    ///   门店的可用状态
    /// </summary>
    public enum StoreAvailableStatus
    {
        /// <summary>
        ///  失败
        /// </summary>
        [Description("失败")]
        error =1,

        /// <summary>
        ///  审核中
        /// </summary>
        [Description("审核中")]
        checking = 2,

        /// <summary>
        ///  通过
        /// </summary>
        [Description("通过")]
        pass = 3,


        /// <summary>
        ///  驳回
        /// </summary>
        [Description("驳回")]
        failed = 1
        
    }

    /// <summary>
    ///   门店的可用状态
    /// </summary>
    public enum StoreUpdateStatus
    {
        /// <summary>
        ///  无
        /// </summary>
        [Description("可以更新")]
        done = 0,

        /// <summary>
        ///   正在更新
        /// </summary>
        [Description("更新中")]
        doing =1
       
    }

}
