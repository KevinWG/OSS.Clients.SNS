
#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 图文统计接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Statistic.Mos;

namespace OS.Social.WX.Offcial.Statistic
{
    public partial class WxOffcialStatisticApi
    {
        /// <summary>
        ///  获取指定某天 【群发】文章组合 在当天的统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxArticleStatisResp<WxArticleStatisticSendMo> GetSendAticleStatistic(DateTime date)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getarticlesummary");
            req.CustomBody = GetRequestBody(new WxStatisticReq() {end_date = date, begin_date = date});

            return RestCommonOffcial<WxArticleStatisResp<WxArticleStatisticSendMo>>(req);
        }

        /// <summary>
        ///  获取指定某天【群发】文章组合连续最多七天的统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxArticleStatisResp<WxArticleStatisticSendTotalMo> GetSendAticleStatisticTotal(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getarticletotal");
            req.CustomBody = GetRequestBody(new WxStatisticReq() { end_date = date, begin_date = date });

            return RestCommonOffcial<WxArticleStatisResp<WxArticleStatisticSendTotalMo>>(req);
        }



        /// <summary>
        ///  获取指定某段时间 微信文章 的统计数据
        /// </summary>
        /// <param name="statisticReq">时间跨度最大【三天】</param>
        /// <returns></returns>
        public WxArticleStatisResp<WxArticleStatisticDaliyMo> GetAticleUserReadStatistic(WxStatisticReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getuserread");
            req.CustomBody = GetRequestBody(statisticReq);

            return RestCommonOffcial<WxArticleStatisResp<WxArticleStatisticDaliyMo>>(req);
        }

        /// <summary>
        ///  获取指定某天 微信文章 的分时统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxArticleStatisResp<WxArticleStatisticHourMo> GetAticleUserReadHourStatistic(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getuserreadhour");
            req.CustomBody = GetRequestBody(new WxStatisticReq() { end_date = date, begin_date = date });

            return RestCommonOffcial<WxArticleStatisResp<WxArticleStatisticHourMo>>(req);
        }



        /// <summary>
        ///  获取指定某段时间 微信文章 的分享统计数据
        /// </summary>
        /// <param name="statisticReq">时间跨度最大【七天】</param>
        /// <returns></returns>
        public WxArticleStatisResp<WxArticleStatisticShareMo> GetAticleUserShareStatistic(WxStatisticReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusershare");
            req.CustomBody = GetRequestBody(statisticReq);

            return RestCommonOffcial<WxArticleStatisResp<WxArticleStatisticShareMo>>(req);
        }


        /// <summary>
        ///  获取指定某天 微信文章 的分享分时统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxArticleStatisResp<WxArticleStatisticShareHourMo> GetAticleShareHourStatistic(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusersharehour");
            req.CustomBody = GetRequestBody(new WxStatisticReq() { end_date = date, begin_date = date });

            return RestCommonOffcial<WxArticleStatisResp<WxArticleStatisticShareHourMo>>(req);
        }

    }
}
