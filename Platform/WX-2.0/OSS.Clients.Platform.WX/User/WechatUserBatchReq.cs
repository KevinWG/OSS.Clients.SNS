﻿using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace OSS.Clients.Platform.WX.User
{
    public class WechatUserBatchReq:WechatBaseTokenReq<WechatUserBatchResp>
    {
        public WechatUserBatchReq(IList<WechatUserBatchReqItem> users) : base(HttpMethod.Post)
        {
            _users = users;
        }
        
        private IList<WechatUserBatchReqItem> _users;
        public override string GetApiPath()
        {
            return "/cgi-bin/user/info/batchget";
        }
        
        protected override void PrepareSend()
        {
            custom_body = JsonConvert.SerializeObject(new
            {
                user_list = _users
            });
        }
    }

    public class WechatUserBatchReqItem
    {
        /// <summary>   
        ///   必填    用户的标识，对当前公众号唯一
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///   可空    国家地区语言版本，zh_CN简体，zh_TW繁体，en英语，默认为zh-CN
        /// </summary>  
        public string lang { get; set; }
    }

    /// <summary>
    /// 批量获取用户信息响应实体
    /// </summary>
    public class WechatUserBatchResp : WechatBaseResp
    {
        /// <summary>
        ///  用户信息列表
        /// </summary>
        public IList<WechatUserInfoResp> user_info_list { get; set; }
    }
}
