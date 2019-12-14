#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscore

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
using OSS.Clients.Platform.WX;


namespace OSS.Clients.Platform.WX.Basic.Mos
{
    #region  标签实体
    /// <summary>
    /// 添加标签返回请求
    /// </summary>
    public class WXAddTagResp : WXBaseResp
    {
        /// <summary>
        /// 返回标签实体
        /// </summary>
        public WXTagInfoMo tag { get; set; }

    }

    /// <summary>
    /// 获取公众号已创建的标签
    /// </summary>
    public class WXGetTagListResp:WXBaseResp
    {
        /// <summary>
        ///   标签列表
        /// </summary>
        public List<WXTagInfoMo> tags { get; set; }
    }


    /// <summary>
    /// 标签实体
    /// </summary>
    public class WXTagInfoMo
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
    public class WXGetUserTagsResp:WXBaseResp
    {
        /// <summary>
        /// 被置上的标签列表
        /// </summary>
        public List<int> tagid_list { get; set; }
    }

    #endregion
}
