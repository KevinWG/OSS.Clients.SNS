#region Copyright (C) 2017 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：公号用户管理接口类
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2017
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Mos;

namespace OS.Social.WX.Offcial
{
    /// <summary>
    ///  公号用户管理接口类
    /// </summary>
    public class WxUserOffcialApi:WxBaseOffcialApi
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxUserOffcialApi(WxAppCoinfig config) : base(config)
        {
        }

        static WxUserOffcialApi()
        {
            #region  增加用户管理特殊 错误码(https://mp.weixin.qq.com/wiki)

            m_DicErrMsg.Add(45157, "标签名非法，请注意不能和其他标签重名");
            m_DicErrMsg.Add(45158, "标签名长度超过30个字节");
            m_DicErrMsg.Add(45056, "创建的标签数过多，请注意不能超过100个");

            m_DicErrMsg.Add(40003, "传入非法的openid");
            m_DicErrMsg.Add(45159, "非法的tag_id");

            m_DicErrMsg.Add(40032, "每次传入的openid列表个数不能超过50个");
            m_DicErrMsg.Add(45159, "非法的标签");
            m_DicErrMsg.Add(45059, "有粉丝身上的标签数已经超过限制");
            m_DicErrMsg.Add(40003, "传入非法的openid");
            m_DicErrMsg.Add(49003, "传入的openid不属于此AppID");
            #endregion
        }

        #region  标签管理


        /// <summary>
        /// 添加标签   最多添加一百个
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public AddTagResp AddTag(string name)
        {
            var req=new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/create");
            var param = new
            {
                tag=new { name = name }
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return RestCommonOffcial<AddTagResp>(req);
        }

        /// <summary>
        ///  修改标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public WxBaseResp UpdateTag(int id,string name)
        {
            var req = new OsHttpRequest
            {
                HttpMothed = HttpMothed.POST,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/update")
            };

            var param = new
            {
                tag = new { id=id, name = name }
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return RestCommonOffcial<WxBaseResp>(req);
        }

        /// <summary>
        ///  删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns></returns>
        public WxBaseResp DeleteTag(int id)
        {
            var req = new OsHttpRequest
            {
                HttpMothed = HttpMothed.POST,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/delete")
            };

            var param = new
            {
                tag = new { id = id }
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return RestCommonOffcial<WxBaseResp>(req);
        }

        /// <summary>
        ///   获取公众号已创建的标签
        /// </summary>
        /// <returns></returns>
        public GetTagListResp GetTagList()
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/get");
           
            return RestCommonOffcial<GetTagListResp>(req);
        }

        /// <summary>
        ///  获取标签下粉丝列表
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="next_openid">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public WxTagOpenIdsResp GetOpenIdListByTag(int tagId, string next_openid = "")
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/user/tag/get");
            req.CustomBody = JsonConvert.SerializeObject(new {tagid = tagId, next_openid = next_openid});

            var  idRes= RestCommonOffcial<WxTagOpenIdsResp>(req);
            if (idRes.IsSuccess)
            {
                idRes.openid_list = idRes.data!=null?((JToken)idRes.data)["openid"].Values<string>().ToList():new List<string>();
            }
            return idRes;
        }


        /// <summary>
        ///   给多个用户同时设置或者取消标签
        /// </summary>
        /// <param name="openIdList">要设置的用户列表，数量不能超过50个</param>
        /// <param name="tagId">标签Id</param>
        /// <param name="flag">标识  0. 增加标签     1.  取消标签</param>
        /// <returns></returns>
        public WxBaseResp SetOrCancleUsersTag(List<string> openIdList,int tagId,int flag)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;       
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/members/",flag==0? "batchtagging" : "batchuntagging");
            req.CustomBody = JsonConvert.SerializeObject(new { tagid = tagId, openid_list = openIdList });

            return RestCommonOffcial<WxBaseResp>(req);
         
        }
        /// <summary>
        ///  获取用户身上的标签列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public GetUserTagsResp GetUserTagsByOpenId(string openid)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/tags/getidlist");
            req.CustomBody = JsonConvert.SerializeObject(new { openid= openid });

            return RestCommonOffcial<GetUserTagsResp>(req);
        }

        #endregion

    }
}
