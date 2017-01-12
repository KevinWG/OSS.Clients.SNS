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

    #endregion
}
