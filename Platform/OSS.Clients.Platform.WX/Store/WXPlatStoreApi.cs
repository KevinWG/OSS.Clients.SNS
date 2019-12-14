#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Platform.WX;
using OSS.Common.ComModels;

using OSS.Clients.Platform.WX.Store.Mos;
using OSS.Tools.Http.Mos;


namespace OSS.Clients.Platform.WX.Store
{
    /// <summary>
    /// 微信门店管理接口
    /// </summary>
    public class WXPlatStoreApi:WXPlatBaseApi
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WXBaseApi.DefaultConfig 属性赋值</param>
        public WXPlatStoreApi(AppConfig config=null) : base(config)
        {
        }

        //static WXPlatStoreApi()
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
        public async Task<WXAddStoreResp> AddStoreAsync(WXStoreBasicSmallMo storeMo)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/addpoi");
            req.CustomBody = JsonConvert.SerializeObject(new {business = new {base_info = storeMo}});

            return await RestCommonOffcialAsync<WXAddStoreResp>(req);
        }

        /// <summary>
        ///   获取门店分店信息
        /// </summary>
        /// <param name="poiId"></param>
        /// <returns></returns>
        public async Task<WXGetStoreResp> GetStoreAsync(long poiId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/poi/getpoi"),
                CustomBody = $"{{\"poi_id\":{poiId}}}"
            };
            return await RestCommonOffcialAsync<WXGetStoreResp>(req);
        }

        /// <summary>
        ///   获取门店分店列表信息
        /// </summary>
        /// <param name="begin">开始位置，0 即为从第一条开始查询</param>
        /// <param name="limit">返回数据条数，最大允许50，默认为20</param>
        /// <returns></returns>
        public async Task<WXGetStoreListResp> GetStoreListAsync(int begin = 0, int limit = 20)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/getpoilist"),
                CustomBody = $"{{\"begin\":{begin},\"limit\":{limit}}}"
            };
            
            return await RestCommonOffcialAsync<WXGetStoreListResp>(req);
        }



        /// <summary>
        ///   修改门店基础信息
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> UpdateStoreBaicServiceAsync(WXUpdateStoreBasicServiceReq serviceReq)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/updatepoi");
            req.CustomBody = JsonConvert.SerializeObject(serviceReq);

            return await RestCommonOffcialAsync<WXGetStoreResp>(req);
        }

        /// <summary>
        ///   删除门店分店信息
        /// </summary>
        /// <param name="poiId"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> DeleteStoreAsync(long poiId)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/delpoi");
            req.CustomBody = $"{{\"poi_id\":{poiId}}}";

            return await RestCommonOffcialAsync<WXBaseResp>(req);
        }

        /// <summary>
        ///  获取门店类目列表
        /// </summary>
        /// <returns></returns>
        public async Task<WXStoreCategoryResp> GetStoreCategoryAsync()
        {
            var req=new OssHttpRequest();

            req.HttpMethod = HttpMethod.Get;
            req.AddressUrl = String.Concat(m_ApiUrl, "/cgi-bin/poi/getwxcategory");

            return await RestCommonOffcialAsync<WXStoreCategoryResp>(req);
        }

        #endregion



    }
}
