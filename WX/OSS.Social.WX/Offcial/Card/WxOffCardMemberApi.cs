#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券接口 ==  会员卡特有接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using Newtonsoft.Json;
using OSS.Http;
using OSS.Http.Models;
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
        public WxBaseResp ActiveMemberCard(WxActiveMemberCardReq activeReq)
        {
            var req=new OsHttpRequest();

            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/activate");
            req.CustomBody = JsonConvert.SerializeObject(activeReq);

            return RestCommonOffcial<WxBaseResp>(req);
        }

        /// <summary>
        /// 设置激活开卡表单
        /// </summary>
        /// <param name="setReq"></param>
        /// <returns></returns>
        public WxBaseResp SetActiveForm(WxSetActiveFormReq setReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/activateuserform/set");
            req.CustomBody = JsonConvert.SerializeObject(setReq);

            return RestCommonOffcial<WxBaseResp>(req);
        }

        /// <summary>
        /// 根据CardID和Code查询会员信息。
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public WxGetMemberCardUserInfoResp GetMemberCardUserInfo(string code,string cardId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/userinfo/get");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\",\"code\":\"{code}\"}}";

            return RestCommonOffcial<WxGetMemberCardUserInfoResp>(req);
        }

        /// <summary>
        /// 根据CardID和Code查询会员信息。
        /// </summary>
        /// <param name="activateTicket">临时票据</param>
        /// <returns></returns>
        public WxGetActiveTempInfoResp GetMemberActiveTempInfo(string activateTicket)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/membercard/userinfo/get");
            req.CustomBody = $"{{\"activate_ticket\":\"{activateTicket}\"}}";

            return RestCommonOffcial<WxGetActiveTempInfoResp>(req);
        }
    }
}
