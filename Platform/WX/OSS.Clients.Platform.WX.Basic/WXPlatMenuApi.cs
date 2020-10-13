#region Copyright (C) 2016 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公众号功能接口 ——菜单管理部分
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-18
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Platform.WX.Base;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Clients.Platform.WX.Basic.Mos;
using OSS.Common.BasicImpls;
using OSS.Common.BasicMos;
using OSS.Tools.Http.Mos;


namespace OSS.Clients.Platform.WX.Basic
{
    /// <summary>
    /// 公号管理
    /// </summary>
    public class WXPlatMenuApi:WXPlatBaseApi
    {


        #region 正常菜单管理

        /// <inheritdoc />
        public WXPlatMenuApi(IMetaProvider<AppConfig> configProvider = null) : base(configProvider)
        {
        }

        /// <summary>
        ///    添加或更新公号菜单
        /// </summary>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> AddOrUpdateMenuAsync(List<WXMenuButtonMo>  buttons)
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/menu/create");
            req.CustomBody = JsonConvert.SerializeObject(new {button= buttons } , Formatting.Indented,
                new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});


            return await RestCommonPlatAsync<WXBaseResp>(req);
        }


        /// <summary>
        /// 获取菜单设置
        /// </summary>
        /// <returns></returns>
        public async Task<WXGetMenuResp> GetMenuAsync()
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Get;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/menu/get");

            return await RestCommonPlatAsync<WXGetMenuResp>(req);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        public async Task<WXBaseResp> DeleteMenuAsync()
        {
            var  req=new OssHttpRequest();
            req.HttpMethod=HttpMethod.Get;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/menu/delete");

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }
        #endregion


        #region 个性化菜单
        /// <summary>
        ///   添加定制个性化菜单
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public async Task<WXAddCustomMenuResp> AddCustomMenuAsync(List<WXMenuButtonMo> buttons,WXMenuMatchRuleMo rule )
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/menu/addconditional"),
                CustomBody = JsonConvert.SerializeObject(new {button = buttons, matchrule = rule},
                    Formatting.Indented, new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore})
            };



            return await RestCommonPlatAsync<WXAddCustomMenuResp>(req);
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuid">菜单Id</param>
        /// <returns></returns>
        public async Task<WXBaseResp> DeleteCustomMenuAsync(long menuid)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/menu/delconditional");
            req.CustomBody = $"{{\"menuid\":\"{menuid}\"}}";

            return await RestCommonPlatAsync<WXBaseResp>(req);
        }

        /// <summary>
        /// 获取某个用户下的菜单信息
        /// </summary>
        /// <param name="userId">用户openid，或者微信号</param>
        /// <returns></returns>
        public async Task<WXUserMenuResp> GetUserMenuAsync(string userId)
        {
            var req=new OssHttpRequest();

            req.HttpMethod= HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/menu/trymatch");
            req.CustomBody = $"{{\"user_id\":\"{userId}\"}}";

            return await RestCommonPlatAsync<WXUserMenuResp>(req);
        }


        #endregion
    }
}
