#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using Newtonsoft.Json;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Card.Mos;

namespace OS.Social.WX.Offcial.Card
{
    /// <summary>
    ///   卡券接口
    /// </summary>
    public partial class WxOffCardApi : WxOffBaseApi
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxOffCardApi(WxAppCoinfig config) : base(config)
        {
        }


        #region  创建卡券接口

        /// <summary>
        ///   添加现金卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCashCard(WxAddCashCardReq cardReq)
        {
            return AddCard(cardReq);
        }

        /// <summary>
        ///   添加优惠卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCoupnCard(WxCouponCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }


        /// <summary>
        ///   添加折扣卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddDiscountCard(WxDiscountCardBig cardReq)
        {
            return AddCard(cardReq);
        }


        /// <summary>
        ///   添加礼品卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCoupnCard(WxGiftCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }



        /// <summary>
        ///   添加团购卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCoupnCard(WxGroupCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }
        
        /// <summary>
        ///   添加团会员卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddMemberCard(WxMemberCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }
        

        /// <summary>
        ///   添加朋友的卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddFriendCard(WxFriendCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }
        

        /// <summary>
        ///   添加会议门票卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddMeetingCard(WxMeetingCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }
        

        /// <summary>
        ///   添加 景点门票 卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCoupnCard(WxScenicCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }


        /// <summary>
        ///   添加 电影票 卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddMovieCard(WxMovieCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }

        /// <summary>
        ///   添加 飞机票 卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddBoardCard(WxBoardCardBigMo cardReq)
        {
            return AddCard(cardReq);
        }


        /// <summary>
        ///   添加卡券
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        private WxAddCardResp AddCard(WxCardTypeBaseMo cardReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/create");
            req.CustomBody = JsonConvert.SerializeObject(new {card = cardReq}, Formatting.Indented,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            return RestCommonOffcial<WxAddCardResp>(req);
        }
        
        #endregion

        /// <summary>
        ///  获取用户的卡券code列表
        /// </summary>
        /// <param name="openId">需要查询的用户openid</param>
        /// <param name="cardId">卡券ID。不填写时默认查询当前appid下的卡券</param>
        /// <returns></returns>
        public WxGetUserCardCodeListResp GetUserCardCodeList(string openId,string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/user/getcardlist");
            req.CustomBody = $"{{\"openid\":\"{openId}\",\"card_id\":\"{cardId}\"}}";

            return RestCommonOffcial<WxGetUserCardCodeListResp>(req);
        }

        /// <summary>
        ///   获取卡券详情
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public WxGetCardDetailRsp GetCardDetail(string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/get");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\"}}";

            return RestCommonOffcial<WxGetCardDetailRsp>(req);
        }

        /// <summary>
        ///   获取卡券id列表
        /// </summary>
        /// <param name="offset">查询卡列表的起始偏移量，从0开始，即offset: 5是指从从列表里的第六个开始读取</param>
        /// <param name="count"> 需要查询的卡片的数量（数量最大50</param>
        /// <param name="status"> 可空 支持开发者拉出指定状态的卡券列表</param>
        /// <returns></returns>
        public WxGetCardIdListResp GetCardDetail(int offset, int count, List<string> status)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/batchget");
            req.CustomBody = JsonConvert.SerializeObject(new {offset = offset, count = count, status_list = status});

            return RestCommonOffcial<WxGetCardIdListResp>(req);
        }

    }
}
