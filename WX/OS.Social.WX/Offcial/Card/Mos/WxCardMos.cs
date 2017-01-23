#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  现金券实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

namespace OS.Social.WX.Offcial.Card.Mos
{
    #region  团购券

    /// <summary>
    ///  添加团购券请求实体
    /// </summary>
    public class WxAddGroupCardReq : WxAddCardBaseReq
    {
        public WxGroupCardMo groupon { get; set; }
    }

    /// <summary>
    ///   团购券信息
    /// </summary>
    public class WxGroupCardMo
    {
        /// <summary>
        ///   卡券基本信息
        /// </summary>
        public WxCardBasicMo base_info { get; set; }

        /// <summary>
        ///   卡券高级信息
        /// </summary>
        public WxCardAdvancedMo advanced_info { get; set; }

        /// <summary>
        ///  必填 团购券专用，团购详情。
        /// </summary>
        public string deal_detail { get; set; }
    }
    
    #endregion
    
    #region  现金券

    /// <summary>
    ///  添加现金券请求实体
    /// </summary>
    public class WxAddCashCardReq : WxAddCardBaseReq
    {
        public WxCashCardMo cash { get; set; }
    }

    /// <summary>
    ///   现金券信息
    /// </summary>
    public class WxCashCardMo
    {
        /// <summary>
        ///   卡券基本信息
        /// </summary>
        public WxCardBasicMo base_info { get; set; }

        /// <summary>
        ///   卡券高级信息
        /// </summary>
        public WxCardAdvancedMo advanced_info { get; set; }

        /// <summary>
        ///  必填 代金券专用，表示起用金额（单位为分）,如果无起用门槛则填0
        /// </summary>
        public int least_cost { get; set; }

        /// <summary>
        ///  必填 代金券专用，表示减免金额。（单位为分）
        /// </summary>
        public int reduce_cost { get; set; }
    }

    #endregion

    #region   折扣券

    /// <summary>
    ///  添加折扣券请求实体
    /// </summary>
    public class WxAddDiscountCardReq : WxAddCardBaseReq
    {
        public WxDiscountCardMo discount { get; set; }
    }

    /// <summary>
    ///   折扣券信息
    /// </summary>
    public class WxDiscountCardMo
    {
        /// <summary>
        ///   卡券基本信息
        /// </summary>
        public WxCardBasicMo base_info { get; set; }

        /// <summary>
        ///   卡券高级信息
        /// </summary>
        public WxCardAdvancedMo advanced_info { get; set; }

        /// <summary>
        ///  必填 折扣券专用，表示打折额度（百分比）。填30就是七折
        /// </summary>
        public int discount { get; set; }

    }

    #endregion

    #region   兑换券

    /// <summary>
    ///  添加兑换券请求实体
    /// </summary>
    public class WxAddGiftCardReq : WxAddCardBaseReq
    {
        public WxGiftCardMo gift { get; set; }
    }

    /// <summary>
    ///   兑换券信息
    /// </summary>
    public class WxGiftCardMo
    {
        /// <summary>
        ///   卡券基本信息
        /// </summary>
        public WxCardBasicMo base_info { get; set; }

        /// <summary>
        ///   卡券高级信息
        /// </summary>
        public WxCardAdvancedMo advanced_info { get; set; }

        /// <summary>
        ///  必填  string(3072) 兑换券专用，填写兑换内容的名称
        /// </summary>
        public string gift { get; set; }

    }

    #endregion

    #region   优惠券

    /// <summary>
    ///  添加优惠券请求实体
    /// </summary>
    public class WxAddCouponCardReq : WxAddCardBaseReq
    {
        public WxCouponCardMo general_coupon { get; set; }
    }

    /// <summary>
    ///   优惠券信息
    /// </summary>
    public class WxCouponCardMo
    {
        /// <summary>
        ///   卡券基本信息
        /// </summary>
        public WxCardBasicMo base_info { get; set; }

        /// <summary>
        ///   卡券高级信息
        /// </summary>
        public WxCardAdvancedMo advanced_info { get; set; }

        /// <summary>
        ///  必填  string(3072) 优惠券专用，填写优惠详情
        /// </summary>
        public string default_detail { get; set; }
    }

    #endregion
}
