#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄，被动消息处理类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using OS.Common.ComModels;
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
        /// 核心执行方法
        /// </summary>
        /// <param name="contentXml">内容信息</param>
        /// <param name="signature">签名信息</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符创</param>
        /// <param name="echostr">验证服务器参数，如果存在则只进行签名验证，并将在结果Data中返回</param>
        /// <returns>消息结果，Data为响应微信数据，如果出错Message为错误信息</returns>
        public ResultMo<string> Processing(string contentXml, string signature, string timestamp, string nonce,string echostr)
        {
            // 一.  检查是否是服务器验证
            if (!string.IsNullOrEmpty(echostr))
            {
                return CheckServerValid(signature, timestamp, nonce, echostr);
            }

            // 二.  正常消息处理
            {
                var checkRes = CheckAndDecryptMsg(contentXml, signature, timestamp, nonce);
                if (!checkRes.IsSuccess)
                    return checkRes.ConvertToResultOnly<string>();

                var contextRes = ProcessCore(checkRes.Data);
                if (!contextRes.IsSuccess)
                    return contextRes.ConvertToResultOnly<string>();

                ProcessEnd(contextRes.Data);

                var resultString = contextRes.Data.ReplyMsg.ToReplyXml();
                if (m_Config.SecurityType != WxSecurityType.None &&
                    contextRes.Data.ReplyMsg.MsgType != ReplyMsgType.None)
                {
                    return EncryptMsg(resultString, m_Config);
                }
                return new ResultMo<string>(resultString);
            }
        }

       /// <summary>
       ///  服务器验证
       /// </summary>
       /// <param name="signature"></param>
       /// <param name="timestamp"></param>
       /// <param name="nonce"></param>
       /// <param name="echostr"></param>
       /// <returns></returns>
        public ResultMo<string> CheckServerValid(string signature, string timestamp, string nonce, string echostr)
        {
            var checkSignRes = WxMsgCrypt.CheckSignature(m_Config.Token, signature, timestamp, nonce);
            var resultRes = checkSignRes.ConvertToResultOnly<string>();
            resultRes.Data = resultRes.IsSuccess ? echostr : string.Empty;
            return resultRes;
        }

        #endregion

    }




}
