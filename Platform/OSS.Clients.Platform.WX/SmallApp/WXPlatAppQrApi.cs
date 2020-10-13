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
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.SmallApp
{
    /// <summary>
    ///  微信小程序相关接口
    /// </summary>
    public class WXPlatAppApi:WXPlatBaseApi
    {
        /// <inheritdoc />
        public WXPlatAppApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
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
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/wxaapp/createwxaqrcode"),
                CustomBody = $"{{\"path\":\"{path}\",\"width\":{width}}}"
            };

            return await DownLoadFileAsync(req);
        }
    }
}
