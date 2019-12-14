#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

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
using OSS.Common.Resp;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Tools.Http.Extention;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.SmallApp
{
    /// <summary>
    ///  微信小程序相关接口
    /// </summary>
    public class WXPlatAppApi:WXPlatBaseApi
    {
        /// <summary>
        /// 小程序接口
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WXBaseApi.DefaultConfig 属性赋值</param>
        public WXPlatAppApi(AppConfig config=null) : base(config)
        {
        }

        /// <summary>
        ///  获取小程序二维码
        /// 生成二维码的总数不限，但接口调用每天上限 10000 次
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public async Task<WXFileResp> DownloadMediaAsync(string path, int width)
        {
            var accessToken = await GetAccessTokenFromCacheAsync();
            if (!accessToken.IsSuccess())
                return  new WXFileResp().WithResp(accessToken);// accessToken.ConvertToResultInherit<WXFileResp>();

            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/wxaapp/createwxaqrcode?access_token=",
                    accessToken.access_token),
                CustomBody = $"{{\"path\":\"{path}\",\"width\":{width}}}"
            };

            var resp= await req.RestSend();
            return await DownLoadFileAsync(resp);
        }
    }
}
