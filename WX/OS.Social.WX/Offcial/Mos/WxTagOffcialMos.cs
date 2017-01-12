using System.Collections.Generic;

namespace OS.Social.WX.Offcial.Mos
{
    #region  标签实体
    /// <summary>
    /// 添加标签返回请求
    /// </summary>
    public class AddTagResp:WxBaseResp
    {
        /// <summary>
        /// 返回标签实体
        /// </summary>
        public TagInfo tag { get; set; }
    }

    /// <summary>
    /// 获取公众号已创建的标签
    /// </summary>
    public class GetTagListResp:WxBaseResp
    {
        /// <summary>
        ///   标签列表
        /// </summary>
        public List<TagInfo> tags { get; set; }
    }


    /// <summary>
    /// 标签实体
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        ///    标签Id
        /// </summary>
        public int id { get; set; }


        /// <summary>
        /// 标签名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    ///  标签下粉丝列表响应实体
    /// </summary>
    public class WxTagOpenIdsResp:WxBaseResp
    {
        /// <summary>
        /// 这次获取的粉丝数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 拉取列表最后一个用户的openid
        /// </summary>
        public string next_openid { get;private set; }

        /// <summary>
        /// openid 列表
        /// </summary>
        public List<string> openid_list { get; internal set; }

        /// <summary>
        /// 微信返回的包含openid的对象，解析出openid_list之后就不用了
        /// </summary>
        public object data { get; set; }
    }


    /// <summary>
    /// 用户对应的标签列表
    /// </summary>
    public class GetUserTagsResp:WxBaseResp
    {
        /// <summary>
        /// 被置上的标签列表
        /// </summary>
        public List<int> tagid_list { get; set; }
    }

    #endregion
}
