#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：小程序接口 —— 用户模块接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Http;
using OSS.Http.Models;
using OSS.Social.WX.Offcial.Basic.Mos;

namespace OSS.Social.WX.Offcial.SmallApp
{
    /// <summary>
    ///  微信小程序相关接口
    /// </summary>
    public class WxOffAppApi:WxOffBaseApi
    {
        public WxOffAppApi(WxAppCoinfig config) : base(config)
        {
        }

        /// <summary>
        ///  获取小程序二维码
        /// 生成二维码的总数不限，但接口调用每天上限 10000 次
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public WxFileResp DownloadMedia(string path,int width)
        {
            var accessToken = GetOffcialAccessToken();
            if (!accessToken.IsSuccess)
                return accessToken.ConvertToResult<WxFileResp>();

            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl,"/cgi-bin/wxaapp/createwxaqrcode?access_token=", accessToken.access_token);
            req.CustomBody = $"{{\"path\":\"{path}\",\"width\":{width}}}";

            return RestCommon(req, resp =>
            {
                if (!resp.ContentType.Contains("application/json"))
                    return new WxFileResp() { content_type = resp.ContentType, file = resp.RawBytes };
                return JsonConvert.DeserializeObject<WxFileResp>(resp.Content);
            });
        }
    }
}
