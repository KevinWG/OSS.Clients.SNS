using System.Collections.Generic;
using System.Net.Http;

namespace OSS.Clients.Platform.WX.User
{
    /// <summary>
    ///  获取用户列表
    /// </summary>
    public class WechatOpenIdListReq : WechatBaseTokenReq<WechatOpenIdListResp>
    {
        public WechatOpenIdListReq() : base(HttpMethod.Get)
        {
        }

        public string next_openid { get; set; }

        /// <summary>
        ///  标签id
        /// </summary>
        public int tag_id { get; set; }

        public override string GetApiPath()
        {
            return tag_id > 0 ? "/cgi-bin/user/tag/get" : string.Concat("/cgi-bin/user/get?next_openid=", next_openid);
        }


        protected override void PrepareSend()
        {
            if (tag_id > 0)
            {
                // 使用tag搜索的请求类型变化
                http_method = HttpMethod.Post;
                custom_body = $"{{ \"tagid\" = {tag_id}, \"next_openid\" = \"{next_openid}\" }}";
            }
            else
            {
                http_method = HttpMethod.Get;
            }
        }
    }

    /// <summary>
    ///  获取openid列表的响应实体
    /// </summary>
    public class WechatOpenIdListResp : WechatBaseResp
    {
        /// <summary>
        /// 这次获取的粉丝数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 拉取列表最后一个用户的openid
        /// </summary>
        public string next_openid { get; private set; }

        /// <summary>
        /// 微信返回的包含openid的对象
        /// </summary>
        public WechatOpenIdListBody data
        {
            get;
            set;
        }
    }


    public class WechatOpenIdListBody
    {
        /// <summary>
        /// openid 列表
        /// </summary>
        public List<string> openid { get; set; }
    }
}
