﻿namespace OSS.Clients.Chat.WX.Mos
{
    /// <summary>
    /// 微信消息配置信息
    /// </summary>
    public class WXChatConfig
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
        public WXSecurityType SecurityType { get; set; }
        
    }

    /// <summary>
    /// 微信安全模式
    /// </summary>
    public enum WXSecurityType
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