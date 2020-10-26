#region Copyright (C) 2016  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：自定义消息处理实现辅助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-3
*       
*****************************************************************************/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using OSS.Clients.Chat.WX.Mos;
using OSS.Common.BasicMos.Resp;
using OSS.Common.Extention;

namespace OSS.Clients.Chat.WX.Helper
{
    internal static class WXChatHelper
    {
        #region   消息内容加解密辅助方法

        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <returns></returns>
        internal static Resp CheckSignature(string token, string msgSignature,
            string timestamp, string nonce,string strEncryptMsg)
        {
            return msgSignature == GenerateSignature(token, timestamp, nonce, strEncryptMsg)
                ? new Resp() 
                : new Resp(RespTypes.SignError, "微信签名验证失败！");
        }

        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <returns></returns>
        internal static string GenerateSignature(string token,
            string timestamp, string nonce,string strEncrptyMsg)
        {
            var AL = new ArrayList {token, timestamp, nonce, strEncrptyMsg};
            AL.Sort(new DictionarySort());

            var raw = new StringBuilder();
            foreach (var t in AL)
            {
                raw.Append(t);
            }

            SHA1 sha = new SHA1CryptoServiceProvider();
            var  enc = new ASCIIEncoding();

            var dataToHash = enc.GetBytes(raw.ToString());
            var dataHashed = sha.ComputeHash(dataToHash);

            return BitConverter.ToString(dataHashed).Replace("-", "").ToLower();
        }

        /// <summary>
        ///  加密消息体
        /// </summary>
        /// <param name="sReplyMsg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static StrResp EncryptMsg(string sReplyMsg, WXChatConfig config)
        {
            string encryptMsg;
            try
            {
                encryptMsg = Cryptography.AESEncrypt(sReplyMsg, config.EncodingAesKey, config.AppId);
            }
            catch (Exception)
            {
                return new StrResp().WithResp(RespTypes.InnerError, "加密响应消息体出错！");
            }

            var date = DateTime.Now;

            var sTimeStamp = date.ToUtcSeconds().ToString();
            var sNonce     = date.ToString("yyyyMMddHHssff");

            var msgSigature = GenerateSignature(config.Token, sTimeStamp, sNonce, encryptMsg);
            if (string.IsNullOrEmpty(msgSigature))
            {
                return new StrResp().WithResp(RespTypes.InnerError, "生成签名信息出错！");
            }

            var sEncryptMsg = new StringBuilder();

            const string encryptLabelHead = "<Encrypt><![CDATA[";
            const string encryptLabelTail = "]]></Encrypt>";

            const string msgSigLabelHead = "<MsgSignature><![CDATA[";
            const string msgSigLabelTail = "]]></MsgSignature>";

            const string timeStampLabelHead = "<TimeStamp><![CDATA[";
            const string timeStampLabelTail = "]]></TimeStamp>";

            const string nonceLabelHead = "<Nonce><![CDATA[";
            const string nonceLabelTail = "]]></Nonce>";

            sEncryptMsg.Append("<xml>")
            .Append(encryptLabelHead).Append(encryptMsg).Append(encryptLabelTail)
            .Append(msgSigLabelHead).Append(msgSigature).Append(msgSigLabelTail)

            .Append(timeStampLabelHead).Append(sTimeStamp).Append(timeStampLabelTail)
            .Append(nonceLabelHead).Append(sNonce).Append(nonceLabelTail)
            .Append("</xml>");

            return new StrResp(sEncryptMsg.ToString());
        }


        #endregion

        #region  消息内容辅助类
        
        /// <summary>
        /// 把xml文本转化成字典对象
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, string> ChangXmlToDir(string xml,out XmlDocument xmlDoc)
        {
            xmlDoc = GetXmlDocment(xml);
            var xmlNode = xmlDoc.FirstChild;
            var nodes = xmlNode.ChildNodes;

            var dirs = new Dictionary<string, string>(nodes.Count);
            foreach (XmlNode xn in nodes)
            {
                var xe = (XmlElement)xn;
                dirs[xe.Name] = xe.InnerText;
            }
            return dirs;
        }

        internal static XmlDocument GetXmlDocment(string xml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlDoc.XmlResolver = null;
            return xmlDoc;
        }


        #endregion
    }


    /// <inheritdoc />
    internal class DictionarySort : System.Collections.IComparer
    {
        /// <inheritdoc />
        public int Compare(object oLeft, object oRight)
        {
            string sLeft        = oLeft as string;
            string sRight       = oRight as string;
            int    iLeftLength  = sLeft.Length;
            int    iRightLength = sRight.Length;
            int    index        = 0;
            while (index < iLeftLength && index < iRightLength)
            {
                if (sLeft[index] < sRight[index])
                    return -1;
                else if (sLeft[index] > sRight[index])
                    return 1;
                else
                    index++;
            }
            return iLeftLength - iRightLength;

        }
    }
}