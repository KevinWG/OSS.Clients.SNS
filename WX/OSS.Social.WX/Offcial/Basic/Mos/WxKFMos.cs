#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 客服管理接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-2
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;

namespace OSS.Social.WX.Offcial.Basic.Mos
{

    /// <summary>
    ///   获取客服列表响应实体
    /// </summary>
    public class WxGetKFAccountListResp:WxBaseResp
    {
        public List<WxKFAccountMo> kf_list { get; set; }
    }

    public class WxKFAccountMo
    {
        /// <summary>   
        ///    完整客服账号，格式为：账号前缀@公众号微信号
        /// </summary>  
        public string kf_account { get; set; }

        /// <summary>   
        ///    客服昵称
        /// </summary>  
        public string kf_nick { get; set; }

        /// <summary>   
        ///     客服工号
        /// </summary>  
        public string kf_id { get; set; }

        /// <summary>   
        ///    客服头像
        /// </summary>  
        public string kf_headimgurl { get; set; }

    }
}
