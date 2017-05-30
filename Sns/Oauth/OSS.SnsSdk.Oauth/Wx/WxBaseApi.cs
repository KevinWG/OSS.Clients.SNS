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

using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.Common.Plugs;
using OSS.Http.Extention;
using OSS.Http.Mos;

namespace OSS.SnsSdk.Oauth.Wx
{
    /// <summary>
    /// 微信接口SDK基类
    /// </summary>
    public class WxBaseApi:BaseRestApi<WxBaseApi>
    {
        public WxBaseApi():this(null)
        {

        }
        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxBaseApi(AppConfig config):base(config)
        {
            ModuleName = ModuleNames.SocialCenter;
        }

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <param name="funcFormat">获取实体格式化方法</param>
        /// <returns>实体类型</returns>
        public override async Task<T> RestCommon<T>(OsHttpRequest request,
            Func<HttpResponseMessage, Task<T>> funcFormat = null)
        {
            var t = await base.RestCommon(request, funcFormat);

            if (!t.IsSuccess())
                t.message = GetErrMsg(t.ret);

            return t;
        }

        #region   全局错误处理

        /// <summary>
        /// 基本错误信息字典，基类中继续完善
        /// </summary>
        protected static ConcurrentDictionary<int, string> m_DicErrMsg = new ConcurrentDictionary<int, string>();
        
        /// <summary>
        /// 注册错误码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected static void RegisteErrorCode(int code, string message) => m_DicErrMsg.TryAdd(code, message);

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        protected static string GetErrMsg(int errCode)
            => m_DicErrMsg.ContainsKey(errCode) ? m_DicErrMsg[errCode] : string.Empty;

        #endregion


    }




}
