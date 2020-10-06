#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券投放接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.Platform.WX.Base.Mos;
using OSS.Clients.Platform.WX.Card.Mos;
using OSS.Common.BasicMos.Resp;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Card
{
    public  partial class WXPlatCardApi
    {
        #region   投放卡券

        /// <summary>
        ///   生成单卡券投放二维码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expireSeconds"></param>
        /// <param name="cardQrMo"></param>
        /// <returns></returns>
        public async Task<WXCardQrCodeResp> CreateCardQrCodeAsync(WXQrCodeType type, int expireSeconds, WXCardQrMo cardQrMo)
        {
            var actionInfo = new WXCreateCardQrReq()
            {
                expire_seconds = expireSeconds,
                action_name = type.ToString(),
                action_info = new { card = cardQrMo }
            };
            return await CreateCardQrCodeAsync(actionInfo);
        }

        /// <summary>
        ///   生成多卡券投放二维码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expireSeconds"></param>
        /// <param name="cardList"></param>
        /// <returns></returns>
        public async Task<WXCardQrCodeResp> CreateMultiCardQrCode(WXQrCodeType type, int expireSeconds, List<WXCardQrMo> cardList)
        {
            if (cardList == null || cardList.Count > 5)
                return new WXCardQrCodeResp() { ret = (int)RespTypes.ParaError, msg = "卡券数目不和要求，请不要为空或超过五个！" };

            var actionInfo = new WXCreateCardQrReq()
            {
                expire_seconds = expireSeconds,
                action_name = type.ToString(),
                action_info = new { multiple_card = new { card_list = cardList } }
            };
            return await CreateCardQrCodeAsync(actionInfo);
        }


        /// <summary>
        /// 生成卡券投放二维码
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        private async Task<WXCardQrCodeResp> CreateCardQrCodeAsync(WXCreateCardQrReq actionInfo)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/qrcode/create");
            req.CustomBody = JsonConvert.SerializeObject(actionInfo);

            return await RestCommonPlatAsync<WXCardQrCodeResp>(req);
        }



        /// <summary>
        ///   导入卡券code
        /// </summary>
        /// <param name="cardId">需要进行导入code的卡券ID</param>
        /// <param name="codes">需导入微信卡券后台的自定义code，上限为100个</param>
        /// <returns></returns>
        public async Task<WXImportCardCodeResp> ImportCardCodeAsync(string cardId, List<string> codes)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/deposit");
            req.CustomBody = JsonConvert.SerializeObject(new { card_id = cardId, code = codes });

            return await RestCommonPlatAsync<WXImportCardCodeResp>(req);
        }

        /// <summary>
        ///   查询导入code数目接口
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<WXGetImportCodeCountResp> GetImportCodeCountAsync(string cardId)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/getdepositcount");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\"}}";

            return await RestCommonPlatAsync<WXGetImportCodeCountResp>(req);
        }

        /// <summary>
        ///   验证已经导入的code信息
        /// </summary>
        /// <param name="cardId">需要进行导入code的卡券ID</param>
        /// <param name="codes">需导入微信卡券后台的自定义code，上限为100个</param>
        /// <returns></returns>
        public async Task<WXCheckImportCodeResp> CheckImportCodeAsync(string cardId, List<string> codes)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/getdepositcount");
            req.CustomBody = JsonConvert.SerializeObject(new { card_id = cardId, code = codes });

            return await RestCommonPlatAsync<WXCheckImportCodeResp>(req);
        }


        /// <summary>
        ///   获取图文推送的卡券信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<WXGetCardArticleContentResp> GetArticleContentAsync(string cardId)
        {
            var req = new OssHttpRequest();
            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/mpnews/gethtml");
            req.CustomBody = $"{{\"card_id\":\"{cardId}\"}}";

            return await RestCommonPlatAsync<WXGetCardArticleContentResp>(req);
        }


        /// <summary>
        ///  创建卡券投放货架接口
        /// </summary>
        /// <param name="pageReq"></param>
        /// <returns></returns>
        public async Task<WXCreateCardLandPageResp> CreateLandPageAsync(WXCreateCardLandPageReq pageReq)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/landingpage/create");
            req.CustomBody = JsonConvert.SerializeObject(pageReq);

            return await RestCommonPlatAsync<WXCreateCardLandPageResp>(req);
        }

        #endregion



        #region  设置白名单

       /// <summary>
       /// 设置卡券测试白名单
       /// </summary>
       /// <param name="openIds"> 可选 openid列表 </param>
       /// <param name="names">可选  微信号列表  二者必填其一</param>
       /// <returns></returns>
       public async Task<WXBaseResp> SetTestWhiteListAsync(List<string> openIds, List<string> names)
       {
           var req = new OssHttpRequest();

           req.HttpMethod = HttpMethod.Post;
           req.AddressUrl = string.Concat(m_ApiUrl, "/card/testwhitelist/set");
           req.CustomBody = JsonConvert.SerializeObject(new {openid = openIds, username = names});

           return await RestCommonPlatAsync<WXBaseResp>(req);
       }

       #endregion


    }
}
