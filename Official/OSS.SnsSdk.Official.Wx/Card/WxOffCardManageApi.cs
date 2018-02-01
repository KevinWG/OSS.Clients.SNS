#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Card.Mos;



namespace OSS.SnsSdk.Official.Wx.Card
{
    /// <summary>
    ///   卡券接口
    /// </summary>
    public partial class WxOffCardApi : WxOffBaseApi
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOffCardApi(AppConfig config=null) : base(config)
        {
        }


        #region  创建卡券接口

        /// <summary>
        ///   添加现金卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddCashCardAsync(WxCashCardMo cardReq)
        {
            var data = new {card_type = WxCardType.CASH, cash = cardReq};
            return await AddCardAsync(data);
        }

        /// <summary>
        ///   添加优惠卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddCoupnCardAsync(WxCouponCardMo cardReq)
        {
            var data = new {card_type = WxCardType.GENERAL_COUPON, general_coupon = cardReq};
            return await AddCardAsync(data);
        }


        /// <summary>
        ///   添加折扣卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddDiscountCardAsync(WxDiscountCardMo cardReq)
        {
            var data = new {card_type = WxCardType.DISCOUNT, discount = cardReq};
            return await AddCardAsync(data);
        }


        /// <summary>
        ///   添加礼品卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddGiftCardAsync(WxGiftCardMo cardReq)
        {
            var data = new {card_type = WxCardType.GIFT, gift = cardReq};
            return await AddCardAsync(data);
        }

        /// <summary>
        ///   添加团购卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddGrouponCardAsync(WxGroupCardMo cardReq)
        {
            var data = new {card_type = WxCardType.GROUPON, groupon = cardReq};
            return await AddCardAsync(data);
        }

        /// <summary>
        ///   添加团会员卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddMemberCardAsync(WxMemberCardMo cardReq)
        {
            var data = new {card_type = WxCardType.MEMBER_CARD, member_card = cardReq};
            return await AddCardAsync(data);
        }


        /// <summary>
        ///   添加朋友的卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddFriendCard(WxFriendCardBigMo cardReq)
        {
            return await AddCardAsync(cardReq);
        }


        /// <summary>
        ///   添加会议门票卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddMeetingCardAsync(WxMeetingCardMo cardReq)
        {
            var data = new {card_type = WxCardType.MEETING_TICKET, meeting_ticket = cardReq};
            return await AddCardAsync(data);
        }


        /// <summary>
        ///   添加 景点门票 卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddScenicCardAsync(WxScenicCardMo cardReq)
        {
            var data = new {card_type = WxCardType.SCENIC_TICKET, scenic_ticket = cardReq};
            return await AddCardAsync(data);
        }


        /// <summary>
        ///   添加 电影票 卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddMovieCardAsync(WxMovieCardMo cardReq)
        {
            var data = new {card_type = WxCardType.MOVIE_TICKET, movie_ticket = cardReq};
            return await AddCardAsync(data);
        }

        /// <summary>
        ///   添加 飞机票 卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public async Task<WxAddCardResp> AddBoardCardAsync(WxBoardCardMo cardReq)
        {
            var data = new {card_type = WxCardType.BOARDING_PASS, boarding_pass = cardReq};
            return await AddCardAsync(data);
        }


        /// <summary>
        ///   添加卡券
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        private async Task<WxAddCardResp> AddCardAsync(object cardReq)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/create");
            req.CustomBody = JsonConvert.SerializeObject(new {card = cardReq}, Formatting.Indented,
                new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});

            return await RestCommonOffcialAsync<WxAddCardResp>(req);
        }

        #endregion
        
        #region 获取卡券相关信息接口

        /// <summary>
        ///  获取用户的卡券code列表
        /// </summary>
        /// <param name="openId">需要查询的用户openid</param>
        /// <param name="cardId">卡券ID。不填写时默认查询当前appid下的卡券</param>
        /// <returns></returns>
        public async Task<WxGetUserCardCodeListResp> GetUserCardCodeListAsync(string openId, string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/user/getcardlist");
            req.CustomBody = $"{{\"openid\":\"{openId}\",\"card_id\":\"{cardId}\"}}";

            return await RestCommonOffcialAsync<WxGetUserCardCodeListResp>(req);
        }

        /// <summary>
        ///   获取卡券详情
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<WxGetCardDetailRsp> GetCardDetailAsync(string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/get");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\"}}";

            return await RestCommonOffcialAsync<WxGetCardDetailRsp>(req);
        }

        /// <summary>
        ///   获取卡券id列表
        /// </summary>
        /// <param name="offset">查询卡列表的起始偏移量，从0开始，即offset: 5是指从从列表里的第六个开始读取</param>
        /// <param name="count"> 需要查询的卡片的数量（数量最大50</param>
        /// <param name="status"> 可空 支持开发者拉出指定状态的卡券列表</param>
        /// <returns></returns>
        public async Task<WxGetCardIdListResp> GetCardIdListAsync(int offset, int count, List<string> status)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/batchget");
            req.CustomBody = JsonConvert.SerializeObject(new {offset = offset, count = count, status_list = status});

            return await RestCommonOffcialAsync<WxGetCardIdListResp>(req);
        }

        #endregion
        
        #region  修改卡券

        /// <summary>
        ///   修改现金卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改的基础信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateCashCardAsync(string cardId, WxUpdateCardBaseReq cardMo)
        {
            var data = new { card_id = cardId, cash = cardMo };
            return await UpdateCardAsync(data);
        }

        /// <summary>
        ///   修改 优惠卡 券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改的基础信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateCoupnCardAsync(string cardId, WxUpdateCardBaseReq cardMo)
        {
            var data = new { card_id = cardId, general_coupon = cardMo };
            return await UpdateCardAsync(data);
        }


        /// <summary>
        ///  修改折扣卡券接口
        /// </summary>  
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateDiscountCardAsync(string cardId, WxUpdateCardBaseReq cardMo)
        {
            var data = new {card_id = cardId, discount = cardMo};
            return await UpdateCardAsync(data);
        }


        /// <summary>
        ///   修改礼品卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateGiftCardAsync(string cardId, WxUpdateCardBaseReq cardMo)
        {
            var data = new { card_id = cardId, gift = cardMo };
            return await UpdateCardAsync(data);
        }

        /// <summary>
        ///   添加团购卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateGrouponCardAsync(string cardId, WxUpdateCardBaseReq cardMo)
        {
            var data = new { card_id = cardId, groupon = cardMo };
            return await UpdateCardAsync(data);
        }

        /// <summary>
        ///   修改 会员卡券 接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateMemberCardAsync(string cardId, WxUpdateMemberCardReq cardMo)
        {
            var data = new { card_id = cardId, member_card = cardMo };
            return await UpdateCardAsync(data);
        }
        

        /// <summary>
        ///   修改 会议门票卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateMeetingCardAsync(string cardId, WxUpdateMeetingCardReq cardMo)
        {
            var data = new { card_id = cardId, meeting_ticket = cardMo };
            return await UpdateCardAsync(data);
        }


        /// <summary>
        ///   修改 景点门票 卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateScenicCardAsync(string cardId, WxScenicCardMo cardMo)
        {
            var data = new { card_id = cardId, scenic_ticket = cardMo };
            return await UpdateCardAsync(data);
        }


        /// <summary>
        ///   修改 电影票 卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateMovieCardAsync(string cardId, WxUpdateMovieCardReq cardMo)
        {
            var data = new { card_id = cardId, movie_ticket = cardMo };
            return await UpdateCardAsync(data);
        }

        /// <summary>
        ///   修改 飞机票 卡券接口
        /// </summary>
        /// <param name="cardId">卡券Id</param>
        /// <param name="cardMo">修改对应的卡券相关信息</param>
        /// <returns></returns>
        public async Task<WxUpdateCardResp> UpdateBoardCardAsync(string cardId, WxUpdateBoardCardReq cardMo)
        {
            var data = new { card_id = cardId, boarding_pass = cardMo };
            return await UpdateCardAsync(data);
        }
        

        /// <summary>
        ///  修改卡券信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task<WxUpdateCardResp> UpdateCardAsync(object obj)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/update"),
                CustomBody = JsonConvert.SerializeObject(obj, Formatting.Indented,
                    new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore})
            };
            
            return await RestCommonOffcialAsync<WxUpdateCardResp>(req);
        }

        #endregion

        #region  修改库存，更换code，删除卡券, 设置卡券失效

        /// <summary>
        ///  更新库存
        /// </summary>
        /// <param name="cardId">卡Id</param>
        /// <param name="increaseCount">增加的数量</param>
        /// <param name="reduceCount">减少的数量</param>
        /// <returns></returns>
        public async Task<WxBaseResp> UpdateStockAsync(string cardId,int increaseCount,int reduceCount)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/modifystock"),
                CustomBody =
                    $"{{\"card_id\":\"{cardId}\",\"increase_stock_value\":{increaseCount},\"reduce_stock_value\":{reduceCount}}}"
            };
            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        ///  修改卡券code
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="cardId">卡Id</param>
        /// <param name="newCode">新code</param>
        /// <returns></returns>
        public async Task<WxBaseResp> UpdateCardCodeAsync(string code, string cardId,string newCode)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/update");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\",\"code\":\"{code}\",\"new_code\":\"{newCode}\"}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }
        
        /// <summary>
        ///  删除卡券
        /// </summary>
        /// <param name="cardId">卡Id</param>
        /// <returns></returns>
        public async Task<WxBaseResp> DeleteCardAsync( string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/delete");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\"}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }


        /// <summary>
        /// 废弃指定code卡券
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cardId">卡Id，自定义code时 必填</param>
        /// <param name="reason">废弃理由，自定义code时 可空</param>
        /// <returns></returns>
        public async Task<WxBaseResp> AbandonCardCodeAsync(string code,string cardId,string reason)
        {
            var req = new OsHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/unavailable");
            req.CustomBody = $"{{\"code\":\"{code}\",\"card_id\":\"{cardId}\",\"reason\":\"{reason}\"}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }
        #endregion


    }
}
