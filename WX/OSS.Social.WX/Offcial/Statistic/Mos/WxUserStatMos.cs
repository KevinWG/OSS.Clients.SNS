#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 用户统计接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace OSS.Social.WX.Offcial.Statistic.Mos
{
    /// <summary>
    ///   用户统计请求实体
    /// </summary>
    public class WxStatReq
    {
        /// <summary>   
        ///   必填    获取数据的起始日期，begin_date和end_date的差值需小于 接口最大时间跨度，否则会报错
        /// </summary>  
        public DateTime begin_date { get; set; }

        /// <summary>   
        ///   必填    获取数据的结束日期，end_date允许设置的最大值为昨日
        /// </summary>  
        public DateTime end_date { get; set; }
    }
    /// <summary>
    /// 微信统计响应实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class WxStatResp<TType> : WxBaseResp
        where TType : class, new()
    {
        /// <summary>
        /// 统计列表
        /// </summary>
        public List<TType> list { get; set; }
    }

    /// <summary>
    ///  用户
    /// </summary>
    public class WxUserStatResp : WxBaseResp
    {
        /// <summary>
        ///   统计信息列表
        /// </summary>
        public List<WxUserStatMo> list { get; set; }
    }


    /// <summary>
    /// 用户统计数据实体
    /// </summary>
    public class WxUserStatMo
    {
        /// <summary>   
        ///   数据的日期
        /// </summary>  
        public DateTime ref_date { get; set; }

        /// <summary>   
        ///   用户的渠道，数值代表的含义如下：
        /// 0 代表其他合计
        /// 1 代表公众号搜索
        /// 17 代表名片分享
        /// 30 代表扫描二维码
        /// 43 代表图文页右上角菜单
        /// 51 代表支付后关注（在支付完成页）
        /// 57 代表图文页内公众号名称
        /// 75 代表公众号文章广告
        /// 78 代表朋友圈广告
        /// </summary>  
        public int user_source { get; set; }

        /// <summary>   
        ///   新增的用户数量
        /// </summary>  
        public int new_user { get; set; }

        /// <summary>   
        ///   取消关注的用户数量，new_user减去cancel_user即为净增用户数量
        /// </summary>  
        public int cancel_user { get; set; }

        /// <summary>   
        ///   总用户量
        /// </summary>  
        public int cumulate_user { get; set; }
    }
}
