
#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  接口调用统计实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using OSS.SnsSdk.Official.Wx;

namespace OSS.SocialSDK.WX.Offcial.Statistic.Mos
{
    /// <summary>
    ///  接口调用统计实体
    /// </summary>
    public class WxInterfaceStatMo
    {
        /// <summary>   
        ///   数据的日期
        /// </summary>  
        public string ref_date { get; set; }

        /// <summary>   
        ///   数据的小时
        /// </summary>  
        public string ref_hour { get; set; }

        /// <summary>   
        ///   通过服务器配置地址获得消息后，被动回复用户消息的次数
        /// </summary>  
        public string callback_count { get; set; }

        /// <summary>   
        ///   上述动作的失败次数
        /// </summary>  
        public string fail_count { get; set; }

        /// <summary>   
        ///   总耗时，除以callback_count即为平均耗时
        /// </summary>  
        public string total_time_cost { get; set; }

        /// <summary>   
        ///   最大耗时
        /// </summary>  
        public string max_time_cost { get; set; }
    }


    /// <summary>
    /// 接口统计信息响应实体
    /// </summary>
    public class WxInterfaceStatResp:WxBaseResp
    {
        /// <summary>
        /// 统计数据列表
        /// </summary>
        public List<WxInterfaceStatMo> list { get; set; }
    }
}
