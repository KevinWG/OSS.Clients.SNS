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


        /// <summary>
        ///   添加卡券接口
        /// </summary>
        /// <param name="cardReq"></param>
        /// <returns></returns>
        public WxAddCardResp AddCard(WxAddCardBaseReq cardReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/create");
            req.CustomBody = JsonConvert.SerializeObject(new {card = cardReq});

            return RestCommonOffcial<WxAddCardResp>(req);
        }
    }
}
