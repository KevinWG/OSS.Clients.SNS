#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券接口 ==  会员卡特有接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Platform.WX;

using OSS.Clients.Platform.WX.Card.Mos;
using OSS.Tools.Http.Mos;


namespace OSS.Clients.Platform.WX.Card
{
    public partial class WXPlatCardApi
    {

        /// <summary>
        /// 6.1  接口激活会员卡
        /// </summary>
        /// <param name="activeReq"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> ActiveMemberCardAsync(WXActiveMemberCardReq activeReq)
        {
            var req=new OssHttpRequest();

            req.HttpMethod=HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/activate");
            req.CustomBody = JsonConvert.SerializeObject(activeReq);

            return await RestCommonOffcialAsync<WXBaseResp>(req);
        }

        /// <summary>
        /// 设置激活开卡表单
        /// </summary>
        /// <param name="setReq"></param>
        /// <returns></returns>
        public async Task<WXBaseResp> SetActiveFormAsync(WXSetActiveFormReq setReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/activateuserform/set"),
                CustomBody = JsonConvert.SerializeObject(setReq)
            };
            
            return await RestCommonOffcialAsync<WXBaseResp>(req);
        }

        /// <summary>
        /// 根据CardID和Code查询会员信息。
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<WXGetMemberCardUserInfoResp> GetMemberCardUserInfoAsync(string code,string cardId)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/userinfo/get"),
                CustomBody = $"{{\"card_id\":\"{cardId}\",\"code\":\"{code}\"}}"
            };

            return await RestCommonOffcialAsync<WXGetMemberCardUserInfoResp>(req);
        }

        /// <summary>
        /// 根据CardID和Code查询会员信息。
        /// </summary>
        /// <param name="activateTicket">临时票据</param>
        /// <returns></returns>
        public async Task<WXGetActiveTempInfoResp> GetMemberActiveTempInfoAsync(string activateTicket)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/userinfo/get"),
                CustomBody = $"{{\"activate_ticket\":\"{activateTicket}\"}}"
            };

            return await RestCommonOffcialAsync<WXGetActiveTempInfoResp>(req);
        }


        /// <summary>
        /// 当会员持卡消费后，开发者调用该接口更新会员信息。
        /// </summary>
        /// <param name="updateReq">更新信息</param>
        /// <returns></returns>
        public async Task<WXUpdateMemberCardUserInfoResp> UpdateMemberCardUserInfoAsync(WXUpdateMemberCardUserInfoReq updateReq)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/updateuser"),
                CustomBody = JsonConvert.SerializeObject(updateReq)
            };

            return await RestCommonOffcialAsync<WXUpdateMemberCardUserInfoResp>(req);
        }
        
    }
}
