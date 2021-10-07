using System.Collections.Generic;
using System.Net.Http;

namespace OSS.Clients.Platform.WX.User
{
    /// <summary>
    ///  获取关注的用户详情
    /// </summary>
    public class WechatUserInfoReq:WechatBaseTokenReq<WechatUserInfoResp>
    {
        /// <summary>
        ///  获取关注的用户详情
        /// </summary>
        /// <param name="openid"></param>
        public WechatUserInfoReq(string openid) : base(HttpMethod.Get)
        {
            this.openid = openid;
        }

        public override string GetApiPath()
        {
            return string.Concat($"/cgi-bin/user/info?openid={openid}&lang={lang}");
        }

        /// <summary>   
        ///   必填    用户的标识，对当前公众号唯一
        /// </summary>  
        public string openid { get; private set; }

        /// <summary>   
        ///   可空    国家地区语言版本，zh_CN简体，zh_TW繁体，en英语，默认为zh-CN
        /// </summary>  
        public string lang { get; set; } = "zh-CN";
    }

    /// <summary>
    ///  微信公号关注用户信息
    /// </summary>
    public class WechatUserInfoResp : WechatBaseResp
    {
        /// <summary>   
        ///   用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>  
        public int subscribe { get; set; }

        /// <summary>   
        ///   用户的标识，对当前公众号唯一
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///   用户的昵称
        /// </summary>  
        public string nickname { get; set; }

        /// <summary>   
        ///   用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>  
        public int sex { get; set; }

        /// <summary>   
        ///   用户所在城市
        /// </summary>  
        public string city { get; set; }

        /// <summary>   
        ///   用户所在国家
        /// </summary>  
        public string country { get; set; }

        /// <summary>   
        ///   用户所在省份
        /// </summary>  
        public string province { get; set; }

        /// <summary>   
        ///   用户的语言，简体中文为zh_CN
        /// </summary>  
        public string language { get; set; }

        /// <summary>   
        ///   用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>  
        public string headimgurl { get; set; }

        /// <summary>   
        ///   用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间  例如：1434093047
        /// </summary>  
        public long subscribe_time { get; set; }

        /// <summary>   
        ///   只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>  
        public string unionid { get; set; }

        /// <summary>   
        ///   公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>  
        public string remark { get; set; }

        /// <summary>   
        ///   用户所在的分组ID（兼容旧的用户分组接口）
        /// </summary>  
        public string groupid { get; set; }

        /// <summary>   
        ///   用户被打上的标签ID列表
        /// </summary>  
        public List<int> tagid_list { get; set; }


    }
}
