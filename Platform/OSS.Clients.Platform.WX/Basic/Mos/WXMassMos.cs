#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号基础接口 = 消息模块实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-1
*       
*****************************************************************************/

#endregion


using System.Collections.Generic;
using OSS.Clients.Platform.WX;

namespace OSS.Clients.Platform.WX.Basic.Mos
{
    /// <summary>
    /// 根据tag群发接口响应实体
    /// </summary>
    public class WXSendMassMsgResp:WXBaseResp
    {
        /// <summary>   
        ///    消息发送任务的ID
        /// </summary>  
        public long msg_id { get; set; }

        /// <summary>   
        ///    消息的数据ID，该字段只有在群发图文消息时，才会出现。可以用于在图文分析数据接口中，获取到对应的图文消息的数据，是图文分析数据接口中的msgid字段中的前半部分，详见图文分析数据接口中的msgid字段的介绍。
        /// </summary>  
        public long msg_data_id { get; set; }
    }

    /// <summary>
    ///  群发信息状态查询
    /// </summary>
    public class WXMassMsgStateResp : WXBaseResp
    {
        /// <summary>   
        ///    群发消息后返回的消息id
        /// </summary>  
        public string msg_id { get; set; }

        /// <summary>   
        ///    消息发送后的状态，SEND_SUCCESS表示发送成功
        /// </summary>  
        public string msg_status { get; set; }
    }

    #region 模板行业相关实体
    /// <summary>
    ///  获取行业设置信息
    /// </summary>
    public class WXGetTemplateIndustryResp : WXBaseResp
    {
        /// <summary>
        /// 帐号设置的主营行业
        /// </summary>
        public WXTemplateIndustryItem primary_industry { get; set; }
        /// <summary>
        /// 帐号设置的副营行业
        /// </summary>
        public WXTemplateIndustryItem secondary_industry { get; set; }
    }


    /// <summary>
    ///  行业设置信息
    /// </summary>
    public class WXTemplateIndustryItem
    {
        /// <summary>
        ///  
        /// </summary>
        public string first_class { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string second_class { get; set; }
    }
    #endregion

    /// <summary>
    ///  添加微信模板响应实体
    /// </summary>
    public class WXAddTemplateResp : WXBaseResp
    {
        /// <summary>
        ///  模板Id
        /// </summary>
        public string template_id { get; set; }
    }

    /// <summary>
    ///  获取模板列表
    /// </summary>
    public class WXGetTemplateListResp : WXBaseResp
    {
        /// <summary>
        /// 模板列表
        /// </summary>
        public List<WXTemplateMo> template_list { get; set; }
    }

    /// <summary>
    ///  模板信息
    /// </summary>
    public class WXTemplateMo
    {
        /// <summary>   
        ///     必填  模板ID
        /// </summary>  
        public string template_id { get; set; }

        /// <summary>   
        ///     必填  模板标题
        /// </summary>  
        public string title { get; set; }

        /// <summary>   
        ///     必填  模板所属行业的一级行业
        /// </summary>  
        public string primary_industry { get; set; }

        /// <summary>   
        ///     必填  模板所属行业的二级行业
        /// </summary>  
        public string deputy_industry { get; set; }

        /// <summary>   
        ///     必填  模板内容
        /// </summary>  
        public string content { get; set; }

        /// <summary>   
        ///     必填  模板示例
        /// </summary>  
        public string example { get; set; }
    }
}
