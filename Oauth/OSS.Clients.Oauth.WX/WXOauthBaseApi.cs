#region Copyright (C) 2016 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：微信接口SDK基类
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Oauth.WX.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Extention;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Oauth.WX
{
    /// <summary>
    /// 微信接口SDK基类
    /// </summary>
    public class WXOauthBaseApi: BaseMetaImpl<AppConfig>
    {
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";

        /// <summary>
        /// 构造函数
        /// </summary>
        public WXOauthBaseApi(IMetaProvider<AppConfig> configProvider):base(configProvider)
        {
        }

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <returns>实体类型</returns>
        public async Task<T> RestCommonJson<T>(OssHttpRequest request)
            where T : WXBaseResp, new()
        {
            var response = await request.RestSend(WXOauthConfigProvider.ClientFactory?.Invoke());
            using (response)
            {
                if (!response.IsSuccessStatusCode)
                    return new T()
                    {
                        ret = -(int) response.StatusCode,
                        msg = response.ReasonPhrase
                    };
                var contentStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contentStr);
            }
        }

        /// <inheritdoc />
        protected override AppConfig GetDefaultMeta()
        {
            return WXOauthConfigProvider.DefaultConfig;
        }
    }

}
