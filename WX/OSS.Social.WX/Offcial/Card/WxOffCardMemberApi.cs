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
        /// 激活会员卡
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
        


    }
}
