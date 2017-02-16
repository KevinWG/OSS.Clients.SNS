#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券统计接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-29
*       
*****************************************************************************/

#endregion

using System;
using System.Threading.Tasks;
using OSS.Http.Mos;
using OSS.Social.WX.Offcial.Card.Mos;

namespace OSS.Social.WX.Offcial.Card
{
    /// <summary>
    ///  卡券接口统计部分
    /// </summary>
    public partial class WxOffCardApi
    {
        /// <summary>
        /// 拉取本商户的总体数据情况，包括时间区间内的各指标总量。
        /// 查询时间区间需 小于 62 天
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public async Task<WxCardStatResp<WxCardStatMo>> GetCardStatisticAsync(DateTime beginDate,DateTime endTime,int source)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardbizuininfo");
            req.CustomBody=$"{{\"begin_date\":\"{beginDate.ToString("yyyy-MM-dd")}\",\"end_date\":\"{endTime.ToString("yyyy-MM-dd")}\",\"cond_source\":{source}}}";

            return await RestCommonOffcialAsync<WxCardStatResp<WxCardStatMo>>(req);
        }

        /// <summary>
        /// 开发者调用该接口拉取减免卡券（优惠券、团购券、折扣券、礼品券）在固定时间区间内的相关数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <param name="cardId">可空，卡券ID。填写后，指定拉出该卡券的相关数据</param>
        /// <returns></returns>
        public async Task<WxCardStatResp<WxCardItemStatMo>> GetCardItemStatisticAsync(DateTime beginDate, DateTime endTime, int source,string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardcardinfo");
            req.CustomBody = $"{{\"begin_date\":\"{beginDate.ToString("yyyy-MM-dd")}\",\"end_date\":\"{endTime.ToString("yyyy-MM-dd")}\",\"cond_source\":{source},\"card_id\":\"{cardId}\"}}";

            return await RestCommonOffcialAsync<WxCardStatResp<WxCardItemStatMo>>(req);
        }


        /// <summary>
        /// 拉取公众平台创建的会员卡相关数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public async Task<WxCardStatResp<WxMemberCardStatMo>> GetMemberCardStatisticAsync(DateTime beginDate, DateTime endTime, int source)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardmembercardinfo");
            req.CustomBody = $"{{\"begin_date\":\"{beginDate.ToString("yyyy-MM-dd")}\",\"end_date\":\"{endTime.ToString("yyyy-MM-dd")}\",\"cond_source\":{source}}}";

            return await RestCommonOffcialAsync<WxCardStatResp<WxMemberCardStatMo>>(req);
        }


        /// <summary>
        /// 开发者调用该接口拉取减免卡券（优惠券、团购券、折扣券、礼品券）在固定时间区间内的相关数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="cardId">卡券ID</param>
        /// <returns></returns>
        public async Task<WxCardStatResp<WxMemberCardDetailStatMo>> GetMemberCardDetailStatisticAsync(DateTime beginDate, DateTime endTime, string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardmembercarddetail");
            req.CustomBody = $"{{\"begin_date\":\"{beginDate.ToString("yyyy-MM-dd")}\",\"end_date\":\"{endTime.ToString("yyyy-MM-dd")}\",\"card_id\":\"{cardId}\"}}";

            return await RestCommonOffcialAsync<WxCardStatResp<WxMemberCardDetailStatMo>>(req);
        }



    }
}
