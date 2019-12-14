
#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using System.Net.Http;
using System.Threading.Tasks;


using OSS.SocialSDK.WX.Offcial.Statistic.Mos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Statistic
{
    public partial class WXPlatStatApi
    {
        /// <summary>
        ///  获取指定某天 【群发】文章组合 在当天的统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<WXStatResp<WXArticleStatSendMo>> GetSendAticleStatisticAsync(DateTime date)
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getarticlesummary");
            req.CustomBody = GetRequestBody(new WXStatReq() {end_date = date, begin_date = date});

            return await RestCommonOffcialAsync<WXStatResp<WXArticleStatSendMo>>(req);
        }

        /// <summary>
        ///  获取指定某天【群发】文章组合连续最多七天的统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<WXStatResp<WXArticleStatSendTotalMo>> GetSendAticleStatisticTotalAsync(DateTime date)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getarticletotal"),
                CustomBody = GetRequestBody(new WXStatReq() {end_date = date, begin_date = date})
            };
            
            return await RestCommonOffcialAsync<WXStatResp<WXArticleStatSendTotalMo>>(req);
        }



        /// <summary>
        ///  获取指定某段时间 微信文章 的统计数据
        /// </summary>
        /// <param name="statisticReq">时间跨度最大【三天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXArticleStatDaliyMo>> GetAticleUserReadStatisticAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getuserread"),
                CustomBody = GetRequestBody(statisticReq)
            };
            
            return await RestCommonOffcialAsync<WXStatResp<WXArticleStatDaliyMo>>(req);
        }

        /// <summary>
        ///  获取指定某天 微信文章 的分时统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<WXStatResp<WXArticleStatHourMo>> GetAticleUserReadHourStatisticAsync(DateTime date)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getuserreadhour"),
                CustomBody = GetRequestBody(new WXStatReq() {end_date = date, begin_date = date})
            };
            
            return await RestCommonOffcialAsync<WXStatResp<WXArticleStatHourMo>>(req);
        }



        /// <summary>
        ///  获取指定某段时间 微信文章 的分享统计数据
        /// </summary>
        /// <param name="statisticReq">时间跨度最大【七天】</param>
        /// <returns></returns>
        public async Task<WXStatResp<WXArticleStatShareMo>> GetAticleUserShareStatisticAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusershare"),
                CustomBody = GetRequestBody(statisticReq)
            };
            return await RestCommonOffcialAsync<WXStatResp<WXArticleStatShareMo>>(req);
        }


        /// <summary>
        ///  获取指定某天 微信文章 的分享分时统计数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<WXStatResp<WXArticleStatShareHourMo>> GetAticleShareHourStatisticAsync(DateTime date)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusersharehour"),
                CustomBody = GetRequestBody(new WXStatReq() {end_date = date, begin_date = date})
            };
            
            return await RestCommonOffcialAsync<WXStatResp<WXArticleStatShareHourMo>>(req);
        }

    }
}
