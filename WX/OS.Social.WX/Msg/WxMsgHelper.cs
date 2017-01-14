#region Copyright (C) 2016 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：微信传送消息解析帮助类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Xml;
using OS.Social.WX.Msg.Mos;

namespace OS.Social.WX.Msg
{
    /// <summary>
    /// 微信传送消息解析帮助类
    /// </summary>
    internal class WxMsgHelper
    {
        /// <summary>
        /// 获取事件实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dirs"></param>
        /// <param name="eventType">事件类型</param>
        /// <returns></returns>
        public static T GetEventMsg<T>(IDictionary<string, string> dirs, EventType eventType) where T : BaseRecEventMsg, new()
        {
            var msg = GetMsg<T>(dirs, MsgType.Event);
            msg.EventType = eventType;
            msg.SetMsgDirs(dirs);
            return msg;
        }

        /// <summary>
        /// 获取消息实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dirs"></param>
        /// <param name="msgType">消息类型</param>
        /// <returns></returns>
        public static T GetMsg<T>(IDictionary<string, string> dirs, MsgType msgType) where T : BaseRecMsg, new()
        {
            T t = new T();
            t.MsgType = msgType;
            t.SetMsgDirs(dirs);
            return t;
        }
        /// <summary>
        /// 把xml文本转化成字典对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ChangXmlToDir(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var dirs = new Dictionary<string, string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;
            XmlNodeList nodes = xmlNode.ChildNodes;

            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                dirs[xe.Name] = xe.InnerText; 
            }
            return dirs;
        }
    }
}