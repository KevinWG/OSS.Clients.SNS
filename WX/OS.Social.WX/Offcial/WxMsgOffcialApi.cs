#region Copyright (C) 2017 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：会话，客服，模板消息等管理
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-12
*       
*****************************************************************************/

#endregion

using OS.Http;
using OS.Http.Models;
using Newtonsoft.Json;

namespace OS.Social.WX.Offcial
{

    /// <summary>
    /// 会话，客服，模板消息等管理
    /// </summary>
    public class WxMsgOffcialApi:WxBaseOffcialApi
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxMsgOffcialApi(WxAppCoinfig config) : base(config)
        {
        }
        #region  模板功能
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">消息详情链接地址</param>
        /// <param name="data">消息数据</param>
        /// <returns></returns>
        public WxBaseResp SendTemplate(string openId, string templateId, string url, object data)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApuUrl, "/cgi-bin/message/template/send");
            var param = new
            {
                touser = openId,
                template_id = templateId,
                url = url,
                data = data
            };
            req.CustomBody = JsonConvert.SerializeObject(param);

            return RestCommonOffcial<WxBaseResp>(req);
        }

        #endregion

    }
}
