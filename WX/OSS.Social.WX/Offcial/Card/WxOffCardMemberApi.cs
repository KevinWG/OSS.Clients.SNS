#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券接口 ==  会员卡特有接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Http.Mos;
using OSS.Social.WX.Offcial.Card.Mos;

namespace OSS.Social.WX.Offcial.Card
{
    public partial class WxOffCardApi
    {



        /// <summary>
        /// 6.1  接口激活会员卡
        /// </summary>
        /// <param name="activeReq"></param>
        /// <returns></returns>
        public async Task<WxBaseResp> ActiveMemberCardAsync(WxActiveMemberCardReq activeReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/activate");
            req.CustomBody = JsonConvert.SerializeObject(activeReq);

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        /// 设置激活开卡表单
        /// </summary>
        /// <param name="setReq"></param>
        /// <returns></returns>
        public async Task<WxBaseResp> SetActiveFormAsync(WxSetActiveFormReq setReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/activateuserform/set");
            req.CustomBody = JsonConvert.SerializeObject(setReq);

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        /// 根据CardID和Code查询会员信息。
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<WxGetMemberCardUserInfoResp> GetMemberCardUserInfoAsync(string code,string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/userinfo/get");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\",\"code\":\"{code}\"}}";

            return await RestCommonOffcialAsync<WxGetMemberCardUserInfoResp>(req);
        }

        /// <summary>
        /// 根据CardID和Code查询会员信息。
        /// </summary>
        /// <param name="activateTicket">临时票据</param>
        /// <returns></returns>
        public async Task<WxGetActiveTempInfoResp> GetMemberActiveTempInfoAsync(string activateTicket)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/userinfo/get");
            req.CustomBody = $"{{\"activate_ticket\":\"{activateTicket}\"}}";

            return await RestCommonOffcialAsync<WxGetActiveTempInfoResp>(req);
        }


        /// <summary>
        /// 当会员持卡消费后，开发者调用该接口更新会员信息。
        /// </summary>
        /// <param name="updateReq">更新信息</param>
        /// <returns></returns>
        public async Task<WxUpdateMemberCardUserInfoResp> UpdateMemberCardUserInfoAsync(WxUpdateMemberCardUserInfoReq updateReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/updateuser");
            req.CustomBody = JsonConvert.SerializeObject(updateReq);

            return await RestCommonOffcialAsync<WxUpdateMemberCardUserInfoResp>(req);
        }
        
    }
}
