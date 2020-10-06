#region Copyright (C) 2017   kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：公号用户管理接口类
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Mos;


namespace OSS.Clients.Platform.WX.Basic
{
    /// <summary>
    ///  公号用户管理接口类
    /// </summary>
    public  class WXPlatUserApi:WXPlatBaseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WXBaseApi.DefaultConfig 属性赋值</param>
        public WXPlatUserApi(AppConfig config = null) : base(config)
        {
        }

        #region  用户管理

        /// <summary>
        /// 获取用户openid列表
        /// </summary>
        /// <param name="next_openid">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public async Task<WXOpenIdsResp> GetOpenIdListAsync(string next_openid = "")
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Get;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/user/get?next_openid=", next_openid);

            return await RestCommonPlatAsync<WXOpenIdsResp>(req);

        }

        /// <summary>
        ///   设置用户备注名
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="remark">备注名称</param>
        /// <returns></returns>
        public async Task<WXBaseResp> SetUserRemarkAsync(string openid,string remark)
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/user/info/updateremark");
            req.CustomBody = $"{{\"openid\":\"{openid}\",\"remark\":\"{remark}\"}}"; //JsonConvert.SerializeObject(new {openid = openid, remark = remark});

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }
        /// <summary>
        /// 获取用户基本信息(UnionID机制)
        /// </summary>
        /// <param name="userReq">请求参数</param>
        /// <returns></returns>
        public async Task<WXPlatUserInfoResp> GetUserInfoAsync(WXPlatUserInfoReq userReq)
        {

            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Get;
            req.AddressUrl = string.Concat(m_ApiUrl, $"/cgi-bin/user/info?openid={userReq.openid}&lang={userReq.lang}");

            return await RestCommonPlatAsync<WXPlatUserInfoResp>(req);
        }

        /// <summary>
        /// 获取用户基本信息(UnionID机制)列表
        /// </summary>
        /// <param name="userReq">请求参数</param>
        /// <returns></returns>
        public async Task<WXPlatUserListResp> GetUserInfoListAsync(IList<WXPlatUserInfoReq> userReq)
        {

            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/user/info/batchget");
            req.CustomBody = JsonConvert.SerializeObject(new
            {
                user_list = userReq
            });
            return await RestCommonPlatAsync<WXPlatUserListResp>(req);
        }


        #endregion

        #region  用户标签管理

        /// <summary>
        ///  获取标签下用户Openid列表
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="next_openid">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public async Task<WXOpenIdsResp> GetOpenIdListByTagAsync(int tagId, string next_openid = "")
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/user/tag/get");
            req.CustomBody = JsonConvert.SerializeObject(new { tagid = tagId, next_openid = next_openid });

            return await RestCommonPlatAsync<WXOpenIdsResp>(req);
        }

        /// <summary>
        /// 添加标签   最多添加一百个
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public async Task<WXAddTagResp> AddTagAsync(string name)
        {
            var req=new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/create");
            req.CustomBody = $"{{\"tag\":{{\"name\":\"{name}\"}}}}";// JsonConvert.SerializeObject(param);

            return await RestCommonPlatAsync<WXAddTagResp>(req);
        }

        /// <summary>
        ///  修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public async Task<WXBaseResp> UpdateTagAsync(int id,string name)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/update")
            };
            
            req.CustomBody = $"{{\"tag\":{{\"id\":{id},\"name\":\"{name}\"}}}}";// JsonConvert.SerializeObject(param);

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        /// <summary>
        ///  删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns></returns>
        public async Task<WXBaseResp> DeleteTagAsync(int id)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/delete")
            };

            req.CustomBody = $"{{\"tag\":{{\"id\":{id}}}}}";// JsonConvert.SerializeObject(param);

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        /// <summary>
        ///   获取公众号已创建的标签
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetTagListResp> GetTagListAsync()
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Get;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/get");
           
            return await RestCommonPlatAsync<WXGetTagListResp>(req);
        }



        /// <summary>
        ///   给多个用户同时设置或者取消标签
        /// </summary>
        /// <param name="openIdList">要设置的用户列表，数量不能超过50个</param>
        /// <param name="tagId">标签Id</param>
        /// <param name="flag">标识  0. 增加标签     1.  取消标签</param>
        /// <returns></returns>
        public async Task<WXBaseResp> SetOrCancleUsersTagAsync(List<string> openIdList,int tagId,int flag)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;       
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/members/",flag==0? "batchtagging" : "batchuntagging");
            req.CustomBody = JsonConvert.SerializeObject(new { tagid = tagId, openid_list = openIdList });

            return await RestCommonPlatAsync<WXBaseResp>(req);
         
        }

        /// <summary>
        ///  获取用户身上的标签列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<WXGetUserTagsResp> GetUserTagsByOpenId(string openid)
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/getidlist");
            req.CustomBody = $"{{\"openid\":\"{openid}\"}}";// JsonConvert.SerializeObject(new { openid= openid });

            return await RestCommonPlatAsync<WXGetUserTagsResp>(req);
        }

        #endregion
        
        #region  黑名单管理

        /// <summary>
        ///   获取黑名单用户Openid列表
        /// </summary>
        /// <param name="next_openid"></param>
        /// <returns></returns>
        public async Task<WXOpenIdsResp> GetBlackOpenIdListAsync(string next_openid)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/members/getblacklist");
            req.CustomBody = $"{{\"begin_openid\":\"{next_openid}\"}}";// JsonConvert.SerializeObject(new { begin_openid = next_openid });

            return await RestCommonPlatAsync<WXOpenIdsResp>(req);
        }


        /// <summary>
        ///   批量拉黑用户
        /// </summary>
        /// <param name="openids">openid列表</param>
        /// <returns></returns>
        public async Task<WXBaseResp>  BatchBlackOpenIds(IList<string> openids)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/members/batchblacklist"),
                CustomBody = JsonConvert.SerializeObject(new {opened_list = openids})
            };
            
            return await RestCommonPlatAsync<WXBaseResp>(req);
        }


        /// <summary>
        ///   批量取消拉黑用户
        /// </summary>
        /// <param name="openids">openid列表</param>
        /// <returns></returns>
        public async Task<WXBaseResp> BatchUnBlackOpenIds(IList<string> openids)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/members/batchunblacklist"),
                CustomBody = JsonConvert.SerializeObject(new {opened_list = openids})
            };

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        #endregion
        
    }
}
