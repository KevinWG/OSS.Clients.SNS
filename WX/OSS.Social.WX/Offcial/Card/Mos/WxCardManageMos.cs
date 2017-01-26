#region Copyright (C) 2017 Kevin (OS系列开源项目)
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
using System.ComponentModel;
using Newtonsoft.Json;

namespace OSS.Social.WX.Offcial.Card.Mos
{
    /// <summary>
    ///   添加卡券响应实体
    /// </summary>
    public class WxAddCardResp : WxBaseResp
    {
        /// <summary>
        ///   卡券Id
        /// </summary>
        public string card_id { get; set; }
    }

    #region  卡券列表

    public class WxGetUserCardCodeListResp : WxBaseResp
    {
        /// <summary>
        ///   卡券列表
        /// </summary>
        public List<WxCardCodeItemMo> card_list { get; set; }

        /// <summary>
        /// 是否有可用的朋友的券
        /// </summary>
        public bool has_share_card { get; set; }
    }


    public class WxCardCodeItemMo
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
    public class WxGetCardIdListResp : WxBaseResp
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
    public class WxGetCardDetailRsp : WxBaseResp
    {
        /// <summary>
        ///   返回的卡详情实体
        /// </summary>
        public WxCardPackageMo card { get; set; }
    }


    /// <summary>
    /// 卡券类型  和  卡券相关类型的包实体
    /// </summary>
    public class WxCardPackageMo
    {
        /// <summary>
        /// 卡券类型
        /// </summary>
        [JsonConverter(typeof (StringConverter))]
        public WxCardType card_type { get; set; }

        /// <summary>
        /// 通用券
        /// </summary>
        public WxCouponCardMo general_coupon { get; set; }

        /// <summary>
        /// 团购券
        /// </summary>
        public WxGroupCardMo groupon { get; set; }

        /// <summary>
        /// 礼品券
        /// </summary>
        public WxGiftCardMo gift { get; set; }

        /// <summary>
        /// 代金券
        /// </summary>
        public WxCashCardMo cash { get; set; }

        /// <summary>
        /// 折扣券
        /// </summary>
        public WxDiscountCardMo discount { get; set; }


        /// <summary>
        /// 会员卡
        /// </summary>
        public WxMemberCardMo member_card { get; set; }

        /// <summary>
        /// 会议门票
        /// </summary>
        public WxMeetingCardMo meeting_ticket { get; set; }

        /// <summary>
        /// 门票
        /// </summary>
        public WxScenicCardMo scenic_ticket { get; set; }

        /// <summary>
        /// 电影票
        /// </summary>
        public WxMovieCardMo movie_ticket { get; set; }

        /// <summary>
        /// 飞机票
        /// </summary>
        public WxBoardCardMo boarding_pass { get; set; }

    }

    #endregion

    #region  修改卡券相关实体

    /// <summary>
    ///   修改卡券的请求基类
    /// </summary>
    public class WxUpdateCardBaseReq
    {
        /// <summary>
        ///   基础信息
        /// </summary>
        public WxCardBasicBaseMo base_info { get; set; }
    }


    /// <summary>
    ///   修改响应实体
    /// </summary>
    public class WxUpdateCardResp : WxBaseResp
    {
        /// <summary>
        /// 是否提交审核，false为修改后不会重新提审，true为修改字段后重新提审，该卡券的状态变为审核中
        /// </summary>
        public bool send_check { get; set; }
    }

    #endregion

    #region 修改库存实体



    #endregion
}
