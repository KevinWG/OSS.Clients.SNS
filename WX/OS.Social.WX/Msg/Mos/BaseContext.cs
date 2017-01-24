using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OS.Common.Extention;

namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    ///  基础消息实体
    /// </summary>
    public abstract class BaseMsg
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
        /// 消息类型
        /// </summary>
        public string MsgType { get; internal set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public long CreateTime { get; internal set; }
    }

    /// <summary>
    /// 基础接收消息实体
    /// </summary>
    public class BaseRecMsg : BaseMsg
    {
        private IDictionary<string, string> m_PropertyDirs;
        
        /// <summary>
        ///  把消息的
        /// </summary>
        /// <param name="contentDirs"></param>
        public void SetMsgDirs(IDictionary<string, string> contentDirs)
        {
            m_PropertyDirs = contentDirs;

            MsgType = this["MsgType"];
            ToUserName = this["ToUserName"];
            FromUserName = this["FromUserName"];
            CreateTime = this["CreateTime"].ToInt64();
            MsgId = this["MsgId"].ToInt64();

            FormatPropertiesFromMsg();
        }
        

        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected virtual void FormatPropertiesFromMsg()
        {
        }
        
        /// <summary>
        /// 自定义索引，获取指定字段的值
        /// </summary>
        /// <param name="key"></param>
        public string this[string key]
        {
            get
            {
                string value;
                m_PropertyDirs.TryGetValue(key, out value);
                return value ?? string.Empty;
            }
        }
        
        /// <summary>
        /// 消息实体
        /// </summary>
        public string RecMsgXml { get;internal set; }

        /// <summary>
        ///   消息id
        /// </summary>
        public long MsgId { get; set; }

    }

    /// <summary>
    /// 基础事件接收消息实体
    /// </summary>
    public class BaseRecEventMsg : BaseRecMsg
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; internal set; }


        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();
            Event = this["Event"];
        }
    }





    /// <summary>
    /// 被动回复
    /// </summary>
    public class BaseReplyMsg : BaseMsg
    {
        
        private List<Tuple<string, object>> _propertyList;

        /// <summary>
        /// 
        /// </summary>
        protected virtual void FormatXml()
        {
           
        }

        /// <summary>
        /// 设置属性信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected void SetReplyXmlValue(string key, object value)
        {
            _propertyList.Add(Tuple.Create(key, value));
        }

        /// <summary>
        /// 转化为XML
        /// </summary>
        /// <returns></returns>
        public virtual string ToReplyXml()
        {
            _propertyList = new List<Tuple<string, object>>();

            SetReplyXmlValue("ToUserName", ToUserName);
            SetReplyXmlValue("FromUserName", FromUserName);
            SetReplyXmlValue("MsgType", MsgType);
            SetReplyXmlValue("CreateTime", CreateTime);

            FormatXml();

            StringBuilder xml = new StringBuilder("<xml>");
            xml.Append(ProduceXml(_propertyList));
            xml.Append("</xml>");
            
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
        
       
    }

    /// <summary>
    /// 当前请求的上下文
    /// </summary>
    public class MsgContext
    {
        /// <summary>
        /// 当前请求消息内容
        /// </summary>
        public string ContextXml { get; internal set; }

        /// <summary>
        /// 接收内容
        /// </summary>
        public BaseRecMsg RecMsg { get; set; }

        /// <summary>
        /// 被动回复内容
        /// </summary>
        public BaseReplyMsg ReplyMsg { get; internal set; }

    }
}