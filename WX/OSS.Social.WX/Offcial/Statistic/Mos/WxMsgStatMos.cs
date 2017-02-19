
#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  消息统计接口实体
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
    /// 微信统计消息基类
    /// </summary>
    public class WxMsgStatBaseMo
    {
        /// <summary>   
        ///   数据的日期，需在begin_date和end_date之间
        /// </summary>  
        public DateTime ref_date { get; set; }

        /// <summary>   
        ///   上行发送了（向公众号发送了）消息的用户数
        /// </summary>  
        public int msg_user { get; set; }
    }

    /// <summary>
    /// 消息统计实体
    /// </summary>
    public class WxMsgUpStatMo: WxMsgStatBaseMo
    {
        /// <summary>   
        ///   消息类型，代表含义如下：1代表文字2代表图片3代表语音4代表视频6代表第三方应用消息（链接消息）
        /// </summary>  
        public int msg_type { get; set; }
      

        /// <summary>   
        ///   上行发送了消息的消息总数
        /// </summary>  
        public int msg_count { get; set; }
    }

    /// <summary>
    /// 消息时分统计实体
    /// </summary>
    public class WxMsgUpStatHourMo: WxMsgUpStatMo
    {
        /// <summary>   
        ///   数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>  
        public int ref_hour { get; set; }
    }

    /// <summary>
    ///  消息分布统计实体
    /// </summary>
    public class WxMsgUpStatDistMo : WxMsgStatBaseMo
    {
        /// <summary>   
        ///   当日发送消息量分布的区间，0代表“0”，1代表“1-5”，2代表“6-10”，3代表“10次以上”
        /// </summary>  
        public int count_interval { get; set; }
    }


}
