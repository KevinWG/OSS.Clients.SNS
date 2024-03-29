﻿#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Common.Resp;

namespace OSS.Clients.Platform.Wechat
{
    /// <summary>
    ///  【服务商代理平台】 component_access_token 统一管理接口
    /// </summary>
    public interface IComponentAccessTokenProvider
    {
        /// <summary>
        ///  自定义获取 component_access_token 实现方法
        /// </summary>
        Task<StrResp> GetComponentAccessToken(WechatBaseReq req);
    }
}