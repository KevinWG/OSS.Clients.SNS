#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券统计接口相关实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-29
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using OSS.Clients.Platform.WX.Base.Mos;

namespace OSS.Clients.Platform.WX.Card.Mos
{
    /// <summary>
    /// 卡券统计基类
    /// </summary>
    public class WXCardStatBaseMo
    {
        /// <summary>   
        ///    日期信息
        /// </summary>  
        public DateTime ref_date { get; set; }

        /// <summary>   
        ///    浏览次数
        /// </summary>  
        public int view_cnt { get; set; }

        /// <summary>   
        ///    浏览人数
        /// </summary>  
        public int view_user { get; set; }

        /// <summary>   
        ///    领取次数
        /// </summary>  
        public int receive_cnt { get; set; }

        /// <summary>   
        ///    领取人数
        /// </summary>  
        public int receive_user { get; set; }

        /// <summary>   
        ///    使用次数
        /// </summary>  
        public int verify_cnt { get; set; }

        /// <summary>   
        ///    使用人数
        /// </summary>  
        public int verify_user { get; set; }
    }

    /// <summary>
    ///  卡券统计信息
    /// </summary>
    public class WXCardStatMo: WXCardStatBaseMo
    {
        /// <summary>   
        ///    转赠次数
        /// </summary>  
        public int given_cnt { get; set; }

        /// <summary>   
        ///    转赠人数
        /// </summary>  
        public int given_user { get; set; }
        
        /// <summary>   
        ///    过期次数
        /// </summary>  
        public int expire_cnt { get; set; }

        /// <summary>   
        ///    过期人数
        /// </summary>  
        public int expire_user { get; set; }
    }


    /// <summary>
    /// 拉取免费券（优惠券、团购券、折扣券、礼品券）在固定时间区间内的相关卡的统计数据。
    /// 统计信息关联到具体的卡
    /// </summary>
    public class WXCardItemStatMo : WXCardStatMo
    {
        /// <summary>   
        ///    卡券ID
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///    cardtype:0：折扣券，1：代金券，2：礼品券，3：优惠券，4：团购券（暂不支持拉取特殊票券类型数据，电影票、飞机票、会议门票、景区门票）
        /// </summary>  
        public int card_type { get; set; }

    }




    /// <summary>
    ///   会员卡统计信息
    /// </summary>
    public class WXMemberCardStatMo: WXCardStatBaseMo
    {
        /// <summary>   
        ///    激活人数
        /// </summary>  
        public int active_user { get; set; }

        /// <summary>   
        ///    有效会员总人数
        /// </summary>  
        public int total_user { get; set; }

        /// <summary>   
        ///    历史领取会员卡总人数
        /// </summary>  
        public int total_receive_user { get; set; }
    }

    /// <summary>
    ///   单个会员卡统计信息
    /// </summary>
    public class WXMemberCardDetailStatMo: WXMemberCardStatMo
    {
        /// <summary>   
        ///    子商户类型
        /// </summary>  
        public int merchanttype { get; set; }

        /// <summary>   
        ///    子商户ID
        /// </summary>  
        public int submerchantid { get; set; }
        
        /// <summary>   
        ///    新用户数
        /// </summary>  
        public int new_user { get; set; }

        /// <summary>   
        ///    应收金额（仅限使用快速买单的会员卡）
        /// </summary>  
        public int payOriginalFee { get; set; }

        /// <summary>   
        ///    实收金额（仅限使用快速买单的会员卡）
        /// </summary>  
        public int fee { get; set; }

    }


    /// <summary>
    /// 微信统计的响应实体
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class WXCardStatResp<TType> : WXBaseResp
        where TType:class 
    {
        /// <summary>
        /// 统计相关列表
        /// </summary>
        public List<TType> list { get; set; }
    }





}
