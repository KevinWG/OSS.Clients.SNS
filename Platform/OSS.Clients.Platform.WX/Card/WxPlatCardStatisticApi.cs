#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using System.Net.Http;
using System.Threading.Tasks;

using OSS.Clients.Platform.WX.Card.Mos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Card
{
    /// <summary>
    ///  卡券接口统计部分
    /// </summary>
    public partial class WXPlatCardApi
    {
        /// <summary>
        /// 拉取本商户的总体数据情况，包括时间区间内的各指标总量。
        /// 查询时间区间需 小于 62 天
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public async Task<WXCardStatResp<WXCardStatMo>> GetCardStatisticAsync(DateTime beginDate,DateTime endTime,int source)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardbizuininfo"),
                CustomBody =
                    $"{{\"begin_date\":\"{beginDate:yyyy-MM-dd}\",\"end_date\":\"{endTime:yyyy-MM-dd}\",\"cond_source\":{source}}}"
            };
            return await RestCommonPlatAsync<WXCardStatResp<WXCardStatMo>>(req);
        }

        /// <summary>
        /// 开发者调用该接口拉取减免卡券（优惠券、团购券、折扣券、礼品券）在固定时间区间内的相关数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <param name="cardId">可空，卡券ID。填写后，指定拉出该卡券的相关数据</param>
        /// <returns></returns>
        public async Task<WXCardStatResp<WXCardItemStatMo>> GetCardItemStatisticAsync(DateTime beginDate, DateTime endTime, int source,string cardId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardcardinfo"),
                CustomBody =
                    $"{{\"begin_date\":\"{beginDate:yyyy-MM-dd}\",\"end_date\":\"{endTime:yyyy-MM-dd}\",\"cond_source\":{source},\"card_id\":\"{cardId}\"}}"
            };
            
            return await RestCommonPlatAsync<WXCardStatResp<WXCardItemStatMo>>(req);
        }


        /// <summary>
        /// 拉取公众平台创建的会员卡相关数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="source">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public async Task<WXCardStatResp<WXMemberCardStatMo>> GetMemberCardStatisticAsync(DateTime beginDate, DateTime endTime, int source)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardmembercardinfo");
            req.CustomBody = $"{{\"begin_date\":\"{beginDate:yyyy-MM-dd}\",\"end_date\":\"{endTime:yyyy-MM-dd}\",\"cond_source\":{source}}}";

            return await RestCommonPlatAsync<WXCardStatResp<WXMemberCardStatMo>>(req);
        }


        /// <summary>
        /// 开发者调用该接口拉取减免卡券（优惠券、团购券、折扣券、礼品券）在固定时间区间内的相关数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endTime">查询数据的截至时间</param>
        /// <param name="cardId">卡券ID</param>
        /// <returns></returns>
        public async Task<WXCardStatResp<WXMemberCardDetailStatMo>> GetMemberCardDetailStatisticAsync(DateTime beginDate, DateTime endTime, string cardId)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getcardmembercarddetail");
            req.CustomBody = $"{{\"begin_date\":\"{beginDate:yyyy-MM-dd}\",\"end_date\":\"{endTime:yyyy-MM-dd}\",\"card_id\":\"{cardId}\"}}";

            return await RestCommonPlatAsync<WXCardStatResp<WXMemberCardDetailStatMo>>(req);
        }



    }
}
