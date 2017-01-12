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

using Newtonsoft.Json;
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
            req.AddressUrl = string.Concat(m_ApuUrl, "/cgi-bin/tags/create");
            var param = new
            {
                tag=new { name = name }
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return RestCommonOffcial<AddTagResp>(req);
        }

        #endregion

    }
}
