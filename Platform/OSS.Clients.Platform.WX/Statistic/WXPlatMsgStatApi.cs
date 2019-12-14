#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  消息统计接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System;
using System.Net.Http;
using System.Threading.Tasks;

using OSS.SocialSDK.WX.Offcial.Statistic.Mos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Statistic
{
   public partial class WXPlatStatApi
   {
       /// <summary>
       /// 获取消息统计概览
       ///  时间宽度最多 【七天】
       /// </summary>
       /// <param name="statisticReq">时间跨度最多【七天】</param>
       /// <returns></returns>
       public async Task<WXStatResp<WXChatUpStatMo>> GetUpMsgStatisticAsync(WXStatReq statisticReq)
       {
            var req=new OssHttpRequest();
            req.HttpMethod=HttpMethod.Post;
           req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsg");
           req.CustomBody = GetRequestBody(statisticReq);

           return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatMo>>(req);
       }


        /// <summary>
        /// 获取【分时】消息统计概览
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<WXStatResp<WXChatUpStatHourMo>> GetUpMsgHourStatisticAsync(DateTime date)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsghour");
            req.CustomBody = GetRequestBody(new WXStatReq() {begin_date = date, end_date = date});

            return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatHourMo>>(req);
        }


        /// <summary>
        /// 获取【周】消息统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXChatUpStatMo>> GetUpMsgWeekStatistic(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgweek"),
                CustomBody = GetRequestBody(statisticReq)
            };

            return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatMo>>(req);
        }


        /// <summary>
        /// 获取【月】消息统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXChatUpStatMo>> GetUpMsgMonthStatisticAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgmonth"),
                CustomBody = GetRequestBody(statisticReq)
            };

            return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatMo>>(req);
        }



        /// <summary>
        /// 获取消息【分布】统计概览
        ///  时间宽度最多 【十五天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【十五天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXChatUpStatDistMo>> GetUpMsgDistStatisticAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgdist"),
                CustomBody = GetRequestBody(statisticReq)
            };
            return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatDistMo>>(req);
        }

        /// <summary>
        /// 获取【周】消息【分布】统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXChatUpStatDistMo>> GetUpMsgDistWeekStatistic(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgdistweek"),
                CustomBody = GetRequestBody(statisticReq)
            };
            return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatDistMo>>(req);
        }


        /// <summary>
        /// 获取【月】消息【分布】统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXChatUpStatDistMo>> GetUpMsgDistMonthStatisticAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgdistmonth"),
                CustomBody = GetRequestBody(statisticReq)
            };

            return await RestCommonOffcialAsync<WXStatResp<WXChatUpStatDistMo>>(req);
        }
    }
}
