﻿
#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：用户自定义消息处理方法提供者
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-8-1
*       
*****************************************************************************/

#endregion

using OSS.Clients.Chat.WX.Mos;

namespace OSS.Clients.Chat.WX
{
    /// <summary>
    ///  用户自定义消息处理句柄
    /// </summary>
    public static class WXChatConfigProvider
    {
        /// <summary>
        /// 默认的配置AppKey信息
        /// </summary>
        public static WXChatConfig DefaultConfig { get; set; }
    }
}
