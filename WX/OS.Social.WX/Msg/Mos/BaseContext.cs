using System;
using System.Collections.Generic;
using System.Text;
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
    public class BaseNormalContext : BaseContext
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
    public class BaseEventContext : BaseNormalContext
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
            CreateTime = DateTime.Now.ToLocalSeconds();

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
        /// <returns></returns>
        public string ToXml()
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

    public class MsgContext
    {

        /// <summary>
        /// 当前请求消息内容
        /// </summary>
        public string ContextXml { get; internal set; }

        /// <summary>
        /// 接收内容
        /// </summary>
        public BaseNormalContext NormalContext { get; set; }

        /// <summary>
        /// 被动回复内容
        /// </summary>
        public BaseReplyContext ReplyContext { get; internal set; }

    }
}