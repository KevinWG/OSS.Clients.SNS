#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄，被动消息处理类
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.WX.Msg
{
    /// <summary>
    ///  消息对话事件句柄，被动消息处理
    /// </summary>
    public class WxMsgHandler:WxMsgBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxMsgHandler(WxMsgServerConfig config):base(config)
        {
        }

      
        
        #region 消息处理入口，出口（分为开始，处理，结束部分）

        /// <summary>
        ///  服务器验证
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public ResultMo ProcessServerCheck(string token, string signature, string timestamp,
            string nonce)
        {
            return WxMsgCrypt.CheckSignature(token, signature, timestamp, nonce);
        }
        
        #region   开始方法


        #endregion

        /// <summary>
        /// 核心执行方法
        /// </summary>
        /// <param name="contentXml">内容信息</param>
        /// <param name="signature">签名信息</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public ResultMo<string> Processing(string contentXml, string signature, string timestamp, string nonce)
        {
            var result = CheckAndDecryptMsg(contentXml, signature, timestamp, nonce);

       
            if (!result.IsSuccess)
                return result.ConvertToResultOnly<string>();

            var contextRes = ProcessCore(result.Data);
            if (!contextRes.IsSuccess)
                return contextRes.ConvertToResultOnly<string>();
            
            if (contextRes.Data.ReplyMsg == null)
                contextRes.Data.ReplyMsg = new NoReplyMsg();
            
            ProcessEnd(contextRes.Data);

            var resultString = contextRes.Data.ReplyMsg.ToReplyXml();
            if (m_Config.SecurityType != WxSecurityType.None && contextRes.Data.ReplyMsg.MsgType != ReplyMsgType.None)
            {
                return EncryptMsg(resultString, m_Config);
            }
            return new ResultMo<string>(resultString);
        }
  
        #endregion

    }




}
