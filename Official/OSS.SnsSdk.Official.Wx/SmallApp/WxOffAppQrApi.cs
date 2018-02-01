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

using System.Net.Http;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Basic.Mos;
using OSS.Http.Extention;

namespace OSS.SnsSdk.Official.Wx.SmallApp
{
    /// <summary>
    ///  微信小程序相关接口
    /// </summary>
    public class WxOffAppApi:WxOffBaseApi
    {
        /// <summary>
        /// 小程序接口
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOffAppApi(AppConfig config=null) : base(config)
        {
        }

        /// <summary>
        ///  获取小程序二维码
        /// 生成二维码的总数不限，但接口调用每天上限 10000 次
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public async Task<WxFileResp> DownloadMediaAsync(string path, int width)
        {
            var accessToken = await GetAccessTokenFromCacheAsync();
            if (!accessToken.IsSuccess())
                return accessToken.ConvertToResult<WxFileResp>();

            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/wxaapp/createwxaqrcode?access_token=",
                    accessToken.access_token),
                CustomBody = $"{{\"path\":\"{path}\",\"width\":{width}}}"
            };

            return await req.RestCommon(DownLoadFileAsync);
        }
    }
}
