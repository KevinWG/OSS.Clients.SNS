#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：微信公众号基础接口 = 消息模块实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-1
*       
*****************************************************************************/

#endregion
namespace OSS.Social.WX.Offcial.Basic.Mos
{
    /// <summary>
    /// 根据tag群发接口响应实体
    /// </summary>
    public class WxSendGroupMsgResp:WxBaseResp
    {
        /// <summary>   
        ///    消息发送任务的ID
        /// </summary>  
        public string msg_id { get; set; }

        /// <summary>   
        ///    消息的数据ID，该字段只有在群发图文消息时，才会出现。可以用于在图文分析数据接口中，获取到对应的图文消息的数据，是图文分析数据接口中的msgid字段中的前半部分，详见图文分析数据接口中的msgid字段的介绍。
        /// </summary>  
        public string msg_data_id { get; set; }
    }
}
