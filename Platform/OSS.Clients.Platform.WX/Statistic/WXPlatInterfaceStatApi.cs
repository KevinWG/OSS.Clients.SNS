#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using System.Net.Http;
using System.Threading.Tasks;


using OSS.SocialSDK.WX.Offcial.Statistic.Mos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Statistic
{
    /// <summary>
    /// 接口统计接口
    /// </summary>
    public partial class WXPlatStatApi
    {
        /// <summary>
        ///   接口调用分析接口
        ///     最大时间宽度【三十天】
        /// </summary>
        /// <param name="statReq"> 最大时间宽度【三十天】</param>
        /// <returns></returns>
        public async Task<WXInterfaceStatResp> GetInterfaceStatisticAsync(WXStatReq statReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getinterfacesummary"),
                CustomBody = GetRequestBody(statReq)
            };
            return await RestCommonPlatAsync<WXInterfaceStatResp>(req);
        }


        /// <summary>
        ///   接口调用分析接口
        /// </summary>
        /// <param name="date"> </param>
        /// <returns></returns>
        public async Task<WXInterfaceStatResp> GetInterfaceHourStatisticAsync(DateTime date)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getinterfacesummaryhour"),
                CustomBody = GetRequestBody(new WXStatReq() {begin_date = date, end_date = date})
            };
            
            return await RestCommonPlatAsync<WXInterfaceStatResp>(req);
        }
    }
   
}
