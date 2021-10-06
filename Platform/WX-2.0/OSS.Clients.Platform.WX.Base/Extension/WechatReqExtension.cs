using OSS.Common.BasicMos;

namespace OSS.Clients.Platform.WX
{
    public static class WechatReqExtension
    {
        /// <summary>
        ///  设置当前请求对应的秘钥配置信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="payConfig"></param>
        /// <returns></returns>
        public static TReq SetContextConfig<TReq>(this TReq req, IAppSecret appConfig)
            where TReq : WechatBaseReq
        {
            req.app_config = appConfig;
            return req;
        }

    }
}
