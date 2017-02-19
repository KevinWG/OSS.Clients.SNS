#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

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
using System.Threading.Tasks;
using OSS.Http.Mos;
using OSS.Social.WX.Offcial.Statistic.Mos;

namespace OSS.Social.WX.Offcial.Statistic
{
   public partial class WxOffStatApi
   {
       /// <summary>
       /// 获取消息统计概览
       ///  时间宽度最多 【七天】
       /// </summary>
       /// <param name="statisticReq">时间跨度最多【七天】</param>
       /// <returns></returns>
       public async Task<WxStatResp<WxMsgUpStatMo>> GetUpMsgStatisticAsync(WxStatReq statisticReq)
       {
            var req=new OsHttpRequest();
            req.HttpMothed=HttpMothed.POST;
           req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsg");
           req.CustomBody = GetRequestBody(statisticReq);

           return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatMo>>(req);
       }


        /// <summary>
        /// 获取【分时】消息统计概览
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<WxStatResp<WxMsgUpStatHourMo>> GetUpMsgHourStatisticAsync(DateTime date)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsghour");
            req.CustomBody = GetRequestBody(new WxStatReq() {begin_date = date, end_date = date});

            return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatHourMo>>(req);
        }


        /// <summary>
        /// 获取【周】消息统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WxStatResp<WxMsgUpStatMo>> GetUpMsgWeekStatistic(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgweek");
            req.CustomBody = GetRequestBody(statisticReq);

            return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatMo>>(req);
        }


        /// <summary>
        /// 获取【月】消息统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WxStatResp<WxMsgUpStatMo>> GetUpMsgMonthStatisticAsync(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgmonth");
            req.CustomBody = GetRequestBody(statisticReq);

            return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatMo>>(req);
        }



        /// <summary>
        /// 获取消息【分布】统计概览
        ///  时间宽度最多 【十五天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【十五天】</param>
        /// <returns></returns>
        public async Task<WxStatResp<WxMsgUpStatDistMo>> GetUpMsgDistStatisticAsync(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgdist");
            req.CustomBody = GetRequestBody(statisticReq);

            return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatDistMo>>(req);
        }

        /// <summary>
        /// 获取【周】消息【分布】统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WxStatResp<WxMsgUpStatDistMo>> GetUpMsgDistWeekStatistic(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgdistweek");
            req.CustomBody = GetRequestBody(statisticReq);

            return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatDistMo>>(req);
        }


        /// <summary>
        /// 获取【月】消息【分布】统计概览
        ///  时间宽度最多 【三十天】
        /// </summary>
        /// <param name="statisticReq">时间跨度最多【三十天】</param>
        /// <returns></returns>
        public async Task<WxStatResp<WxMsgUpStatDistMo>> GetUpMsgDistMonthStatisticAsync(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getupstreammsgdistmonth");
            req.CustomBody = GetRequestBody(statisticReq);

            return await RestCommonOffcialAsync<WxStatResp<WxMsgUpStatDistMo>>(req);
        }
    }
}
