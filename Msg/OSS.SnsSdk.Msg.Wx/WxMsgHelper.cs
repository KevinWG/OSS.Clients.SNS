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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OSS.Common.ComModels;
using OSS.Common.Encrypt;
using OSS.Common.Extention;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    internal static class WxMsgHelper
    {
        #region   消息内容加解密辅助方法

        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        internal static ResultMo CheckSignature(string token, string signature,
            string timestamp, string nonce)
        {
            return signature == GenerateSignature(token, timestamp, nonce)
                ? new ResultMo() 
                : new ResultMo(ResultTypes.UnAuthorize, "签名验证失败！");
        }
        
        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="strEncryptMsg"></param>
        /// <returns></returns>
        internal static string GenerateSignature(string token,
            string timestamp, string nonce, string strEncryptMsg = "")
        {
            var strList = new List<string>() { token, timestamp, nonce, strEncryptMsg };
            strList.Sort();

            var waitEncropyStr = string.Join(string.Empty, strList);
            return Sha1.Encrypt(waitEncropyStr, Encoding.ASCII);
        }



        /// <summary>
        ///  加密消息体
        /// </summary>
        /// <param name="sReplyMsg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static ResultMo<string> EncryptMsg(string sReplyMsg, WxMsgConfig config)
        {
            string raw;
            try
            {
                raw = Cryptography.AesEncrypt(sReplyMsg, config.EncodingAesKey, config.AppId);
            }
            catch (Exception)
            {
                return new ResultMo<string>(ResultTypes.InnerError, "加密响应消息体出错！");
            }
            var date = DateTime.Now;

            var sTimeStamp = date.ToUtcSeconds().ToString();
            var sNonce = date.ToString("yyyyMMddHHssff");


            var msgSigature = GenerateSignature(config.Token, sTimeStamp, sNonce, raw);
            if (string.IsNullOrEmpty(msgSigature))
            {
                return new ResultMo<string>(ResultTypes.InnerError, "生成签名信息出错！");
            }

            var sEncryptMsg = new StringBuilder();

            const string encryptLabelHead = "<Encrypt><![CDATA[";
            const string encryptLabelTail = "]]></Encrypt>";
            const string msgSigLabelHead = "<MsgSignature><![CDATA[";
            const string timeStampLabelHead = "<TimeStamp><![CDATA[";

            const string timeStampLabelTail = "]]></TimeStamp>";
            const string nonceLabelHead = "<Nonce><![CDATA[";
            const string nonceLabelTail = "]]></Nonce>";

            sEncryptMsg.Append("<xml>").Append(encryptLabelHead).Append(raw).Append(encryptLabelTail);
            sEncryptMsg.Append(msgSigLabelHead).Append(msgSigature).Append("]]></MsgSignature>");
            sEncryptMsg.Append(timeStampLabelHead).Append(sTimeStamp).Append(timeStampLabelTail);
            sEncryptMsg.Append(nonceLabelHead).Append(sNonce).Append(nonceLabelTail);
            sEncryptMsg.Append("</xml>");

            return new ResultMo<string>(sEncryptMsg.ToString());
        }


        #endregion

        #region  消息内容辅助类
        
        /// <summary>
        /// 把xml文本转化成字典对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xmlDoc">返回格式化后的xml对象</param>
        /// <returns></returns>
        internal static Dictionary<string, string> ChangXmlToDir(string xml,out XmlDocument xmlDoc)
        {
            if (string.IsNullOrEmpty(xml))
            {
                xmlDoc = null;
                return null;
            }
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
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
        #endregion
    }
}