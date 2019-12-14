#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信的门店枚举
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-20
*       
*****************************************************************************/

#endregion

using OSS.Common.Extention;

namespace OSS.Clients.Platform.WX.Store.Mos
{
    /// <summary>
    ///   门店的可用状态
    /// </summary>
    public enum WXStoreAvailableStatus
    {
        /// <summary>
        ///  失败
        /// </summary>
        [OSDescript("失败")]
        error = 1,

        /// <summary>
        ///  审核中
        /// </summary>
        [OSDescript("审核中")]
        checking = 2,

        /// <summary>
        ///  通过
        /// </summary>
        [OSDescript("通过")]
        pass = 3,
        
        /// <summary>
        ///  驳回
        /// </summary>
        [OSDescript("驳回")]
        failed = 1
    }

    /// <summary>
    ///   门店的可用状态
    /// </summary>
    public enum WXStoreUpdateStatus
    {
        /// <summary>
        ///  无
        /// </summary>
        [OSDescript("可以更新")]
        done = 0,

        /// <summary>
        ///   正在更新
        /// </summary>
        [OSDescript("更新中")]
        doing = 1
    }
    


}
