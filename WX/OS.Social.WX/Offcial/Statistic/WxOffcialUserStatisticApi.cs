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

using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Statistic.Mos;

namespace OS.Social.WX.Offcial.Statistic
{

    /// <summary>
    /// 微信统计接口
    /// </summary>
   public partial class WxOffcialStatisticApi:WxOffcialBaseApi
    {
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="config"></param>
       public WxOffcialStatisticApi(WxAppCoinfig config) : base(config)
       {
       }
    }



    /// <summary>
    /// 微信用户统计接口
    /// </summary>
    public partial class WxOffcialStatisticApi 
    {

        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度   7
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        public WxUserStatisticResp GetUserSummary(WxStatisticReq statisticReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusersummary");
            req.CustomBody =
                $"{{\"begin_date\": \"{statisticReq.begin_date.ToString("yyyy-MM-dd")}\", \"end_date\": \"{statisticReq.end_date.ToString("yyyy-MM-dd")}\"}}";

            return RestCommonOffcial<WxUserStatisticResp>(req);
        }



        /// <summary>
        /// 获取累计用户数据
        /// 最大时间跨度   7
        /// </summary>
        /// <param name="statisticReq"></param>
        /// <returns></returns>
        public WxUserStatisticResp GetUserCumulate(WxStatisticReq statisticReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/datacube/getusercumulate");
            req.CustomBody =
                $"{{\"begin_date\": \"{statisticReq.begin_date.ToString("yyyy-MM-dd")}\", \"end_date\": \"{statisticReq.end_date.ToString("yyyy-MM-dd")}\"}}";

            return RestCommonOffcial<WxUserStatisticResp>(req);
        }

    }
}
