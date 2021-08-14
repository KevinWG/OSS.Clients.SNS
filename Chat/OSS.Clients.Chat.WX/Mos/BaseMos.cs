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

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OSS.Common.Extension;

namespace OSS.Clients.Chat.WX.Mos
{
    /// <summary>
    ///  基础消息实体
    /// </summary>
    public abstract class WXBaseMsg
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
        public string MsgType { get;protected set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public long CreateTime { get; internal set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// 基础接收消息实体
    /// </summary>
    public class WXBaseRecMsg : WXBaseMsg
    {
        private IDictionary<string, string> m_PropertyDirs;
        
        /// <summary>
        ///  把消息的
        /// </summary>
        /// <param name="contentDirs"></param>
        internal virtual void LoadMsgDirs(IDictionary<string, string> contentDirs)
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
                m_PropertyDirs.TryGetValue(key, out var value);
                return value ?? string.Empty;
            }
        }

        /// <summary>
        /// 消息实体
        /// </summary>
        public XmlDocument RecMsgXml { get; internal set; }

        /// <summary>
        ///   消息id
        /// </summary>
        public long MsgId { get; set; }

    }

    /// <summary>
    /// 基础事件接收消息实体
    /// </summary>
    public class WXBaseRecEventMsg : WXBaseRecMsg
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; internal set; }
        
        internal override void LoadMsgDirs(IDictionary<string, string> contentDirs)
        {
            base.LoadMsgDirs(contentDirs);
            Event = this["Event"];
        }
    }





    /// <summary>
    /// 被动回复
    /// </summary>
    public class WXBaseReplyMsg : WXBaseMsg
    {
        
        private IDictionary<string, object> _propertyList;
        
        protected virtual void SetValueToXml()
        {
        }
        
        /// <summary>
        /// 自定义索引，获取指定字段的值
        /// </summary>
        /// <param name="key"></param>
        public object this[string key]
        {
            set
            {
                if (value != null)
                {
                    _propertyList[key]= value;
                }
            }
        }

        /// <summary>
        /// 转化为XML
        /// </summary>
        /// <returns></returns>
        public virtual string ToReplyXml()
        {
            _propertyList = new Dictionary<string, object>();

            this["ToUserName"] = ToUserName;
            this["FromUserName"] = FromUserName;
            this["MsgType"] = MsgType;
            this["CreateTime"] = CreateTime;

            SetValueToXml();

            var xml = new StringBuilder("<xml>");
            ProduceXml(xml, _propertyList);
            xml.Append("</xml>");

            return xml.ToString();
        }

        private static void ProduceXml(StringBuilder xml, IDictionary<string, object> list, string keyName = null)
        {
            foreach (var item in list)
            {
                //  值为空的直接忽略
                if (item.Value == null)
                    continue;

                var valueType = item.Value.GetType();
                var key = keyName ?? item.Key;

                if (valueType.IsValueType)
                {
                    xml.Append("<").Append(key).Append(">")
                        .Append(item.Value)
                        .Append("</").Append(key).Append(">");
                }
                else if (valueType.IsGenericType)
                {
                    if (item.Value is IDictionary<string, object> dirVal)
                    {
                        xml.Append("<").Append(key).Append(">");
                        ProduceXml(xml, dirVal);
                        xml.Append("</").Append(key).Append(">");
                    }
                    else if (item.Value is Tuple<string, IDictionary<string, object>> tupleVal)
                    {
                        xml.Append("<").Append(key).Append(">");
                        ProduceXml(xml, tupleVal.Item2, tupleVal.Item1);
                        xml.Append("</").Append(key).Append(">");
                    }
                }
                else
                {
                    xml.Append("<").Append(key).Append(">")
                        .Append("<![CDATA[").Append(item.Value)
                        .Append("]]>").Append("</")
                        .Append(key).Append(">");
                }
            }
        }
    }

    /// <summary>
    /// 当前请求的上下文
    /// </summary>
    public class WXChatContext
    {
        /// <summary>
        /// 接收内容
        /// </summary>
        public WXBaseRecMsg RecMsg { get; set; }

        /// <summary>
        /// 被动回复内容
        /// </summary>
        public WXBaseReplyMsg ReplyMsg { get; internal set; }

    }
}