#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 门店管理
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-20
*       
*****************************************************************************/

#endregion

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Store.Mos;



namespace OSS.SnsSdk.Official.Wx.Store
{
    /// <summary>
    /// 微信门店管理接口
    /// </summary>
    public class WxOffStoreApi:WxOffBaseApi
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOffStoreApi(AppConfig config=null) : base(config)
        {
        }

        //static WxOffStoreApi()
        //{
        //    #region  增加全局错误码
        //    RegisteErrorCode(40009, "图片大小为0或者超过1M");
        //    RegisteErrorCode(40097, "参数不正确，请参考字段要求检查json 字段");
        //    RegisteErrorCode(65104, "门店的类型不合法，必须严格按照附表的分类填写");
        //    RegisteErrorCode(65105, "图片url 不合法，必须使用接口1 的图片上传接口所获取的url");
        //    RegisteErrorCode(65106, "门店状态必须未审核通过");
        //    RegisteErrorCode(65107, "扩展字段为不允许修改的状态");
        //    RegisteErrorCode(65109, "门店名为空");
        //    RegisteErrorCode(65110, "门店所在详细街道地址为空");
        //    RegisteErrorCode(65111, "门店的电话为空");
        //    RegisteErrorCode(65112, "门店所在的城市为空");
        //    RegisteErrorCode(65113, "门店所在的省份为空");
        //    RegisteErrorCode(65114, "图片列表为空");
        //    RegisteErrorCode(65115, "poi_id 不正确");
        //    #endregion
        //}



        #region  门店接口

        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="storeMo"></param>
        /// <returns></returns>
        public async Task<WxAddStoreResp> AddStoreAsync(WxStoreBasicSmallMo storeMo)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/addpoi");
            req.CustomBody = JsonConvert.SerializeObject(new {business = new {base_info = storeMo}});

            return await RestCommonOffcialAsync<WxAddStoreResp>(req);
        }

        /// <summary>
        ///   获取门店分店信息
        /// </summary>
        /// <param name="poiId"></param>
        /// <returns></returns>
        public async Task<WxGetStoreResp> GetStoreAsync(long poiId)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl=String.Concat(m_ApiUrl, "/cgi-bin/poi/getpoi");
            req.CustomBody = $"{{\"poi_id\":{poiId}}}";

            return await RestCommonOffcialAsync<WxGetStoreResp>(req);
        }

        /// <summary>
        ///   获取门店分店列表信息
        /// </summary>
        /// <param name="begin">开始位置，0 即为从第一条开始查询</param>
        /// <param name="limit">返回数据条数，最大允许50，默认为20</param>
        /// <returns></returns>
        public async Task<WxGetStoreListResp> GetStoreListAsync(int begin = 0, int limit = 20)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/getpoilist");
            req.CustomBody = $"{{\"begin\":{begin},\"limit\":{limit}}}";

            return await RestCommonOffcialAsync<WxGetStoreListResp>(req);
        }



        /// <summary>
        ///   修改门店基础信息
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <returns></returns>
        public async Task<WxBaseResp> UpdateStoreBaicServiceAsync(WxUpdateStoreBasicServiceReq serviceReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/updatepoi");
            req.CustomBody = JsonConvert.SerializeObject(serviceReq);

            return await RestCommonOffcialAsync<WxGetStoreResp>(req);
        }

        /// <summary>
        ///   删除门店分店信息
        /// </summary>
        /// <param name="poiId"></param>
        /// <returns></returns>
        public async Task<WxBaseResp> DeleteStoreAsync(long poiId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/delpoi");
            req.CustomBody = $"{{\"poi_id\":{poiId}}}";

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        ///  获取门店类目列表
        /// </summary>
        /// <returns></returns>
        public async Task<WxStoreCategoryResp> GetStoreCategoryAsync()
        {
            var req=new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/getwxcategory");

            return await RestCommonOffcialAsync<WxStoreCategoryResp>(req);
        }

        #endregion



    }
}
