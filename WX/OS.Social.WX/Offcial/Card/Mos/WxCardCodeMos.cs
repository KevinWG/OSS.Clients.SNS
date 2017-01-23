#region Copyright (C) 2017 Kevin (OS系列开源项目)
/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券Code导入校验实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-23
*       
*****************************************************************************/
#endregion

using System.Collections.Generic;

namespace OS.Social.WX.Offcial.Card.Mos
{

    public class WxImportCardCodeResp:WxBaseResp
    {
        /// <summary>   
        ///   成功个数
        /// </summary>  
        public int succ_code { get; set; }

        /// <summary>   
        ///   重复导入的code会自动被过滤。
        /// </summary>  
        public int duplicate_code { get; set; }

        /// <summary>   
        ///   失败个数。
        /// </summary>  
        public int fail_code { get; set; }

    }



    public class WxGetImportCodeCountResp:WxBaseResp
    {
        /// <summary>
        /// 已经成功存入的code数目。
        /// </summary>
        public int count { get; set; }
    }



    public class WxCheckCodeResp : WxBaseResp
    {
        /// <summary>
        /// 已经成功存入的code。
        /// </summary>
        public List<string> exist_code { get; set; }

        /// <summary>
        /// 没有存入的code。
        /// </summary>
        public List<string> not_exist_code { get; set; }
    }


    public class WxGetCardArticleContentResp:WxBaseResp
    {
        /// <summary>
        /// 返回一段html代码，可以直接嵌入到图文消息的正文里。即可以把这段代码嵌入到上传图文消息素材接口中的content字段里
        /// </summary>
        public string content { get; set; }
    }
}
