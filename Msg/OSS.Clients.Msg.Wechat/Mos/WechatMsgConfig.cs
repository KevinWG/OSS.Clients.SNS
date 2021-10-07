#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：OSS - 消息相关实体基类
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

namespace OSS.Clients.Msg.Wechat
{
    /// <summary>
    /// 微信消息配置信息
    /// </summary>
    public class WechatMsgConfig
    {
        /// <summary>
        /// 当前应用Id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 令牌配置
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 消息加解密密钥
        /// 消息加密方式为加密方式时使用
        /// </summary>
        public string EncodingAesKey { get; set; }

        /// <summary>
        /// 安全模式
        /// </summary>
        public WechatSecurityType SecurityType { get; set; }
        
    }

    /// <summary>
    /// 微信安全模式
    /// </summary>
    public enum WechatSecurityType
    {
        /// <summary>
        /// 明文
        /// </summary>
        None,

        ///// <summary>
        ///// 兼容模式
        ///// </summary>
        //Compitable,

        /// <summary>
        /// 安全模式
        /// </summary>
        Safe
    }
}