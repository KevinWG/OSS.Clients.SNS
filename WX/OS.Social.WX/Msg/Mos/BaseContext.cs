using System;
using System.Collections.Generic;
using System.Text;
using OS.Common.ComModels;
using OS.Common.ComModels.Enums;
using OS.Common.Extention;

namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseContext
    {
        /// <summary>
        /// 接收方帐号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public long CreateTime { get; internal set; }
    }

    /// <summary>
    /// 普通消息
    /// </summary>
    public class BaseRecContext : BaseContext
    {
        private Dictionary<string, string> _propertyDirs;

        public MsgType MsgType { get; internal set; }

        protected virtual void FormatProperties()
        {
            ToUserName = GetValue("ToUserName");
            FromUserName = GetValue("FromUserName");
            CreateTime = Convert.ToInt64(GetValue("CreateTime"));
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetValue(string key)
        {
            string value;
            _propertyDirs.TryGetValue(key, out value);
            return value ?? string.Empty;
        }

        /// <summary>
        /// 从字典中获取属性信息
        /// </summary>
        /// <param name="dirs"></param>
        /// <returns></returns>
        public Dictionary<string, string> FromDirs(Dictionary<string, string> dirs)
        {
            _propertyDirs = dirs;
            FormatProperties();
            return _propertyDirs;
        }
    }

    /// <summary>
    /// 事件推送
    /// </summary>
    public class BaseRecEventContext : BaseRecContext
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType EventType { get; internal set; }
    }

    /// <summary>
    /// 被动回复
    /// </summary>
    public class BaseReplyContext : BaseContext
    {
        public ReplyMsgType MsgType { get; set; }


        private List<Tuple<string, object>> _propertyList;

        protected virtual void FormatXml()
        {
            SetXmlValue("ToUserName", ToUserName);
            SetXmlValue("FromUserName", FromUserName);
            SetXmlValue("CreateTime", CreateTime);
        }

        /// <summary>
        /// 设置属性信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected void SetXmlValue(string key, object value)
        {
            _propertyList.Add(Tuple.Create(key, value));
        }

        /// <summary>
        /// 转化为XML
        /// </summary>
        /// <param name="config">配置信息，处理消息是否加密</param>
        /// <returns></returns>
        public string ToXml(WxMsgServerConfig config)
        {
            if (MsgType == ReplyMsgType.None)
            {
                return string.Empty;
            }
            _propertyList = new List<Tuple<string, object>>();
            FormatXml();
            StringBuilder xml = new StringBuilder("<xml>");
            xml.Append(ProduceXml(_propertyList));
            xml.Append("</xml>");

            if (config.SecurityType != WxSecurityType.None)
            {
                var res = EncryptMsg(xml.ToString(),config);
                return res.IsSuccess ? res.Data : string.Empty;
            }
            return xml.ToString(); 
        }

        private string ProduceXml(List<Tuple<string, object>> list)
        {
            StringBuilder xml = new StringBuilder();

            foreach (Tuple<string, object> item in list)
            {
                //字段值不能为null，会影响后续流程
                if (string.IsNullOrEmpty(item.Item2?.ToString()))
                    continue;

                if (item.Item2 is int
                    || item.Item2 is Int64
                    || item.Item2 is double
                    || item.Item2 is float)
                {
                    xml.Append("<").Append(item.Item1).Append(">")
                        .Append(item.Item2)
                        .Append("</").Append(item.Item1).Append(">");
                }
                else if (item.Item2.GetType().IsGenericType)
                {
                    xml.Append("<").Append(item.Item1).Append(">")
                        .Append(ProduceXml((List<Tuple<string, object>>)item.Item2))
                        .Append("</").Append(item.Item1).Append(">");
                }
                else
                {
                    xml.Append("<").Append(item.Item1).Append(">")
                        .Append("<![CDATA[")
                        .Append(item.Item2)
                        .Append("]]>")
                        .Append("</").Append(item.Item1).Append(">");
                }
            }
            return xml.ToString();
        }


        /// <summary>
        ///  加密模式下，返回的消息体加密
        /// </summary>
        /// <param name="sReplyMsg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public ResultMo<string> EncryptMsg(string sReplyMsg, WxMsgServerConfig config)
        {
            string raw = "";
            try
            {
                raw = Cryptography.AesEncrypt(sReplyMsg, config.EncodingAesKey, config.AppId);
            }
            catch (Exception)
            {
                return new ResultMo<string>(ResultTypes.InnerError, "加密响应消息体出错！");
            }
            var date = DateTime.Now;

            var sTimeStamp =    date.ToUtcSeconds().ToString();
            var sNonce =    date.ToString("yyyyMMddHHssff");


            string msgSigature = WxMsgCrypt.GenerateSignature(config.Token, sTimeStamp, sNonce, raw);
            if (string.IsNullOrEmpty(msgSigature))
            {
                return new ResultMo<string>(ResultTypes.InnerError, "生成签名信息出错！");
            }
            StringBuilder sEncryptMsg = new StringBuilder();
            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";
            sEncryptMsg.Append("<xml>").Append(EncryptLabelHead).Append(raw).Append(EncryptLabelTail);
            sEncryptMsg.Append(MsgSigLabelHead).Append(msgSigature).Append(MsgSigLabelTail);
            sEncryptMsg.Append(TimeStampLabelHead).Append(sTimeStamp).Append(TimeStampLabelTail);
            sEncryptMsg.Append(NonceLabelHead).Append(sNonce).Append(NonceLabelTail);
            sEncryptMsg.Append("</xml>");
            return new ResultMo<string>(sEncryptMsg.ToString());
        }


    }

    public class MsgContext
    {
        /// <summary>
        /// 当前请求消息内容
        /// </summary>
        public string ContextXml { get; internal set; }

        /// <summary>
        /// 接收内容
        /// </summary>
        public BaseRecContext RecContext { get; set; }

        /// <summary>
        /// 被动回复内容
        /// </summary>
        public BaseReplyContext ReplyContext { get; internal set; }

    }
}