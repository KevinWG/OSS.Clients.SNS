#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：微信的门店枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-20
*       
*****************************************************************************/

#endregion

using System.ComponentModel;

namespace OS.Social.WX.Offcial.Store.Mos
{
    /// <summary>
    ///   门店的可用状态
    /// </summary>
    public enum WxStoreAvailableStatus
    {
        /// <summary>
        ///  失败
        /// </summary>
        [Description("失败")]
        error = 1,

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
    public enum WxStoreUpdateStatus
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
        doing = 1
    }
    


}
