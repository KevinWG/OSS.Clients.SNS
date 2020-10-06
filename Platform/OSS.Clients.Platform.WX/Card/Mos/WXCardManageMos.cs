#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore
/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券添加修改通用实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/
#endregion

using System.Collections.Generic;
using OSS.Clients.Platform.WX.Base.Mos;

namespace OSS.Clients.Platform.WX.Card.Mos
{
    /// <summary>
    ///   添加卡券响应实体
    /// </summary>
    public class WXAddCardResp : WXBaseResp
    {
        /// <summary>
        ///   卡券Id
        /// </summary>
        public string card_id { get; set; }
    }

    #region  卡券列表

    public class WXGetUserCardCodeListResp : WXBaseResp
    {
        /// <summary>
        ///   卡券列表
        /// </summary>
        public List<WXCardCodeItemMo> card_list { get; set; }

        /// <summary>
        /// 是否有可用的朋友的券
        /// </summary>
        public bool has_share_card { get; set; }
    }


    public class WXCardCodeItemMo
    {
        /// <summary>
        ///   卡券的code码，单张卡券唯一标识
        /// </summary>
        public string code { get; set; }


        /// <summary>
        ///  卡券id
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// 获取卡券id列表
    /// </summary>
    public class WXGetCardIdListResp : WXBaseResp
    {
        /// <summary>
        /// 该商户名下卡券ID总数
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        ///    卡券id列表
        /// </summary>
        public List<string> card_id_list { get; set; }
    }

    #endregion

    #region  获取卡券信息相关实体

    /// <summary>
    ///   获取卡券详情响应实体
    /// </summary>
    public class WXGetCardDetailRsp : WXBaseResp
    {
        /// <summary>
        ///   返回的卡详情实体
        /// </summary>
        public WXCardPackageMo card { get; set; }
    }


    /// <summary>
    /// 卡券类型  和  卡券相关类型的包实体
    /// </summary>
    public class WXCardPackageMo
    {
        /// <summary>
        /// 卡券类型
        ///  typeof(WXCardType).ToEnumDirs  获取对应的枚举字典列表
        /// </summary>
        public string card_type { get; set; }

        /// <summary>
        /// 通用券
        /// </summary>
        public WXCouponCardMo general_coupon { get; set; }

        /// <summary>
        /// 团购券
        /// </summary>
        public WXGroupCardMo groupon { get; set; }

        /// <summary>
        /// 礼品券
        /// </summary>
        public WXGiftCardMo gift { get; set; }

        /// <summary>
        /// 代金券
        /// </summary>
        public WXCashCardMo cash { get; set; }

        /// <summary>
        /// 折扣券
        /// </summary>
        public WXDiscountCardMo discount { get; set; }


        /// <summary>
        /// 会员卡
        /// </summary>
        public WXMemberCardMo member_card { get; set; }

        /// <summary>
        /// 会议门票
        /// </summary>
        public WXMeetingCardMo meeting_ticket { get; set; }

        /// <summary>
        /// 门票
        /// </summary>
        public WXScenicCardMo scenic_ticket { get; set; }

        /// <summary>
        /// 电影票
        /// </summary>
        public WXMovieCardMo movie_ticket { get; set; }

        /// <summary>
        /// 飞机票
        /// </summary>
        public WXBoardCardMo boarding_pass { get; set; }

    }

    #endregion

    #region  修改卡券相关实体

    /// <summary>
    ///   修改卡券的请求基类
    /// </summary>
    public class WXUpdateCardBaseReq
    {
        /// <summary>
        ///   基础信息
        /// </summary>
        public WXCardBasicBaseMo base_info { get; set; }
    }


    /// <summary>
    ///   修改响应实体
    /// </summary>
    public class WXUpdateCardResp : WXBaseResp
    {
        /// <summary>
        /// 是否提交审核，false为修改后不会重新提审，true为修改字段后重新提审，该卡券的状态变为审核中
        /// </summary>
        public bool send_check { get; set; }
    }

    #endregion

   
}
