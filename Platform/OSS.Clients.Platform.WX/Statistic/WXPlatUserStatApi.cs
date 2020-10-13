#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 统计模块
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Threading.Tasks;
using OSS.Clients.Platform.WX.Base;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.SocialSDK.WX.Offcial.Statistic.Mos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Statistic
{

    /// <summary>
    /// 微信统计接口
    /// </summary>
    public partial class WXPlatStatApi:WXPlatBaseApi
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WXPlatStatApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
       {
       }
    }



    /// <summary>
    /// 微信用户统计接口
    /// </summary>
    public partial class WXPlatStatApi 
    {

        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度   7
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        public async Task<WXUserStatResp> GetUserSummaryAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusersummary"),
                CustomBody = GetRequestBody(statisticReq)
            };
            
            return await RestCommonPlatAsync<WXUserStatResp>(req);
        }



        /// <summary>
        /// 获取累计用户数据
        /// 最大时间跨度   7
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        public async Task<WXUserStatResp> GetUserCumulateAsync(WXStatReq statisticReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusercumulate"),
                CustomBody = GetRequestBody(statisticReq)
            };
            
            return await RestCommonPlatAsync<WXUserStatResp>(req);
        }

        /// <summary>
        ///   返回请求参数json串
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        private string GetRequestBody(WXStatReq statisticReq)
        {
            return $"{{\"begin_date\": \"{statisticReq.begin_date.ToString("yyyy-MM-dd")}\", \"end_date\": \"{statisticReq.end_date.ToString("yyyy-MM-dd")}\"}}";
        }

    }
}
