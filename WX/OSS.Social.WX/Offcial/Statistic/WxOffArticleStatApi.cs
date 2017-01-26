
#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口   统计  —— 图文统计接口
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
using OSS.Social.WX.Offcial.Statistic.Mos;

namespace OSS.Social.WX.Offcial.Statistic
{
    public partial class WxOffStatApi
    {
        /// <summary>
        ///  获取指定某天 【群发】文章组合 在当天的统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxStatResp<WxArticleStatSendMo> GetSendAticleStatistic(DateTime date)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getarticlesummary");
            req.CustomBody = GetRequestBody(new WxStatReq() {end_date = date, begin_date = date});

            return RestCommonOffcial<WxStatResp<WxArticleStatSendMo>>(req);
        }

        /// <summary>
        ///  获取指定某天【群发】文章组合连续最多七天的统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxStatResp<WxArticleStatSendTotalMo> GetSendAticleStatisticTotal(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getarticletotal");
            req.CustomBody = GetRequestBody(new WxStatReq() { end_date = date, begin_date = date });

            return RestCommonOffcial<WxStatResp<WxArticleStatSendTotalMo>>(req);
        }



        /// <summary>
        ///  获取指定某段时间 微信文章 的统计数据
        /// </summary>
        /// <param name="statisticReq">时间跨度最大【三天】</param>
        /// <returns></returns>
        public WxStatResp<WxArticleStatDaliyMo> GetAticleUserReadStatistic(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getuserread");
            req.CustomBody = GetRequestBody(statisticReq);

            return RestCommonOffcial<WxStatResp<WxArticleStatDaliyMo>>(req);
        }

        /// <summary>
        ///  获取指定某天 微信文章 的分时统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxStatResp<WxArticleStatHourMo> GetAticleUserReadHourStatistic(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getuserreadhour");
            req.CustomBody = GetRequestBody(new WxStatReq() { end_date = date, begin_date = date });

            return RestCommonOffcial<WxStatResp<WxArticleStatHourMo>>(req);
        }



        /// <summary>
        ///  获取指定某段时间 微信文章 的分享统计数据
        /// </summary>
        /// <param name="statisticReq">时间跨度最大【七天】</param>
        /// <returns></returns>
        public WxStatResp<WxArticleStatShareMo> GetAticleUserShareStatistic(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusershare");
            req.CustomBody = GetRequestBody(statisticReq);

            return RestCommonOffcial<WxStatResp<WxArticleStatShareMo>>(req);
        }


        /// <summary>
        ///  获取指定某天 微信文章 的分享分时统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WxStatResp<WxArticleStatShareHourMo> GetAticleShareHourStatistic(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusersharehour");
            req.CustomBody = GetRequestBody(new WxStatReq() { end_date = date, begin_date = date });

            return RestCommonOffcial<WxStatResp<WxArticleStatShareHourMo>>(req);
        }

    }
}
