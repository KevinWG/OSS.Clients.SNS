#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using OSS.Common.BasicMos;

namespace OSS.Clients.Platform.Wechat
{
    public class WechatAccessTokenReq:WechatBaseReq<WechatAccessTokenResp>
    {
        private IAppSecret m_appConfig;
        public WechatAccessTokenReq(IAppSecret appconfig) : base(HttpMethod.Get)
        {
            m_appConfig = appconfig;
        }

        public override string GetApiPath()
        {
            return
                $"/cgi-bin/token?grant_type=client_credential&appid={m_appConfig.app_id}&secret={m_appConfig.app_secret}";
        }


    }
    
    /// <summary>
    ///   公众号功能接口accesstoken信息
    /// </summary>
    public class WechatAccessTokenResp : WechatBaseResp
    {
        /// <summary>
        ///   token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 	凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        ///// <summary>
        ///// 【UTC】过期时间，接口获取数据后根据expires_in 计算的值(可以扣除十分钟，作为中间的缓冲值)
        ///// </summary>
        //public long expires_date { get; set; }
    }
}
