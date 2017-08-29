#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄基类，主要声明相关事件
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-13
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.Common.Encrypt;
using OSS.Common.Extention;
using OSS.SnsSdk.Msg.Wx.Mos;
using OSS.SocialSDK.WX.Msg.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <inheritdoc />
    /// <summary>
    /// 消息处理类
    ///  </summary>
    public class WxMsgHandler: WxMsgBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mConfig"></param>
        public WxMsgHandler(WxMsgServerConfig mConfig=null):base(mConfig)
        {
           
        }
    }


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
        internal static ResultMo<string> EncryptMsg(string sReplyMsg, WxMsgServerConfig config)
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

            const string EncryptLabelHead = "<Encrypt><![CDATA[";
            const string EncryptLabelTail = "]]></Encrypt>";
            const string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            const string MsgSigLabelTail = "]]></MsgSignature>";
            const string TimeStampLabelHead = "<TimeStamp><![CDATA[";

            const string TimeStampLabelTail = "]]></TimeStamp>";
            const string NonceLabelHead = "<Nonce><![CDATA[";
            const string NonceLabelTail = "]]></Nonce>";

            sEncryptMsg.Append("<xml>").Append(EncryptLabelHead).Append(raw).Append(EncryptLabelTail);
            sEncryptMsg.Append(MsgSigLabelHead).Append(msgSigature).Append(MsgSigLabelTail);
            sEncryptMsg.Append(TimeStampLabelHead).Append(sTimeStamp).Append(TimeStampLabelTail);
            sEncryptMsg.Append(NonceLabelHead).Append(sNonce).Append(NonceLabelTail);
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
        internal static Dictionary<string, string> ChangXmlToDir(string xml,ref XmlDocument xmlDoc)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var dirs = new Dictionary<string, string>();

            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlNode = xmlDoc.FirstChild;
            var nodes = xmlNode.ChildNodes;

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
