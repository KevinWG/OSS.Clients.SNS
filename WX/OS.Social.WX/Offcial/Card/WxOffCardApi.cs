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
using OS.Common.ComModels.Enums;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Basic.Mos;
using OS.Social.WX.Offcial.Card.Mos;
using OS.Social.WX.SysUtils.Mos;

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
        public WxAddCardResp AddCoupnCard(WxAddCouponCardReq cardReq)
        {
            return AddCard(cardReq);
        }


        /// <summary>
        ///   添加折扣卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddDiscountCard(WxAddDiscountCardReq cardReq)
        {
            return AddCard(cardReq);
        }


        /// <summary>
        ///   添加礼品卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCoupnCard(WxAddGiftCardReq cardReq)
        {
            return AddCard(cardReq);
        }



        /// <summary>
        ///   添加团购卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCoupnCard(WxAddGroupCardReq cardReq)
        {
            return AddCard(cardReq);
        }




        /// <summary>
        ///   添加卡券
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        private WxAddCardResp AddCard(WxAddCardBaseReq cardReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/create");
            req.CustomBody = JsonConvert.SerializeObject(new {card = cardReq}, Formatting.Indented,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            return RestCommonOffcial<WxAddCardResp>(req);
        }


        #endregion
        
        #region   投放卡券

        /// <summary>
        ///   生成单卡券投放二维码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expireSeconds"></param>
        /// <param name="cardQrMo"></param>
        /// <returns></returns>
        public WxCardQrCodeResp CreateCardQrCode(WxQrCodeType type, int expireSeconds, WxCardQrMo cardQrMo)
        {
            var actionInfo = new WxCreateCardQrReq()
            {
                expire_seconds = expireSeconds,
                action_name = type,
                action_info = new {card = cardQrMo}
            };
            return CreateCardQrCode(actionInfo);
        }

        /// <summary>
        ///   生成多卡券投放二维码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expireSeconds"></param>
        /// <param name="cardList"></param>
        /// <returns></returns>
        public WxCardQrCodeResp CreateMultiCardQrCode(WxQrCodeType type, int expireSeconds, List<WxCardQrMo> cardList)
        {
            if (cardList == null || cardList.Count > 5)
                return new WxCardQrCodeResp() {Ret = (int) ResultTypes.ParaNotMeet, Message = "卡券数目不和要求，请不要为空或超过五个！"};
            
            var actionInfo = new WxCreateCardQrReq()
            {
                expire_seconds = expireSeconds,
                action_name = type,
                action_info = new {multiple_card = new {card_list = cardList}}
            };
            return CreateCardQrCode(actionInfo);
        }


        /// <summary>
        /// 生成卡券投放二维码
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        private WxCardQrCodeResp CreateCardQrCode(WxCreateCardQrReq actionInfo)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/qrcode/create");
            req.CustomBody = JsonConvert.SerializeObject(actionInfo);

            return RestCommonOffcial<WxCardQrCodeResp>(req);
        }

        #endregion

    }
}
