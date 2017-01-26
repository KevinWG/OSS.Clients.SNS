#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 统计模块
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System;
using OSS.Http;
using OSS.Http.Models;
using OSS.Social.WX.Offcial.Statistic.Mos;

namespace OSS.Social.WX.Offcial.Statistic
{

    /// <summary>
    /// 微信统计接口
    /// </summary>
   public partial class WxOffStatApi:WxOffBaseApi
    {
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="config"></param>
       public WxOffStatApi(WxAppCoinfig config) : base(config)
       {
       }
    }



    /// <summary>
    /// 微信用户统计接口
    /// </summary>
    public partial class WxOffStatApi 
    {

        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度   7
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        public WxUserStatResp GetUserSummary(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusersummary");
            req.CustomBody = GetRequestBody(statisticReq);

            return RestCommonOffcial<WxUserStatResp>(req);
        }



        /// <summary>
        /// 获取累计用户数据
        /// 最大时间跨度   7
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        public WxUserStatResp GetUserCumulate(WxStatReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusercumulate");
            req.CustomBody = GetRequestBody(statisticReq);

            return RestCommonOffcial<WxUserStatResp>(req);
        }

        /// <summary>
        ///   返回请求参数json串
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        private string GetRequestBody(WxStatReq statisticReq)
        {
            return $"{{\"begin_date\": \"{statisticReq.begin_date.ToString("yyyy-MM-dd")}\", \"end_date\": \"{statisticReq.end_date.ToString("yyyy-MM-dd")}\"}}";
        }

    }
}
