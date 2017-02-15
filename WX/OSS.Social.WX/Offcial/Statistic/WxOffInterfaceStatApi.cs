#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 统计 ——  接口调用统计接口
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
    /// <summary>
    /// 接口统计接口
    /// </summary>
    public partial class WxOffStatApi
    {
        /// <summary>
        ///   接口调用分析接口
        ///     最大时间宽度【三十天】
        /// </summary>
        /// <param name="statReq"> 最大时间宽度【三十天】</param>
        /// <returns></returns>
        public async Task<WxInterfaceStatResp> GetInterfaceStatisticAsync(WxStatReq statReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getinterfacesummary");
            req.CustomBody = GetRequestBody(statReq);

            return await RestCommonOffcialAsync<WxInterfaceStatResp>(req);
        }


        /// <summary>
        ///   接口调用分析接口
        /// </summary>
        /// <param name="date"> </param>
        /// <returns></returns>
        public async Task<WxInterfaceStatResp> GetInterfaceHourStatisticAsync(DateTime date)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getinterfacesummaryhour");
            req.CustomBody = GetRequestBody(new WxStatReq() {begin_date = date, end_date = date});

            return await RestCommonOffcialAsync<WxInterfaceStatResp>(req);
        }
    }
   
}
