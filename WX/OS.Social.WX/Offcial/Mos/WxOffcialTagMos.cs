#region Copyright (C) 2016 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：  用户管理的 标签管理系列实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-1
*       
*****************************************************************************/

#endregion
using System.Collections.Generic;

namespace OS.Social.WX.Offcial.Mos
{
    #region  标签实体
    /// <summary>
    /// 添加标签返回请求
    /// </summary>
    public class WxAddTagResp : WxBaseResp
    {
        /// <summary>
        /// 返回标签实体
        /// </summary>
        public WxTagInfo tag { get; set; }

    }

    /// <summary>
    /// 获取公众号已创建的标签
    /// </summary>
    public class WxGetTagListResp:WxBaseResp
    {
        /// <summary>
        ///   标签列表
        /// </summary>
        public List<WxTagInfo> tags { get; set; }
    }


    /// <summary>
    /// 标签实体
    /// </summary>
    public class WxTagInfo
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
    /// 用户对应的标签列表
    /// </summary>
    public class WxGetUserTagsResp:WxBaseResp
    {
        /// <summary>
        /// 被置上的标签列表
        /// </summary>
        public List<int> tagid_list { get; set; }
    }

    #endregion
}
