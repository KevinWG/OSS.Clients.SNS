#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券核销接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-24
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using OSS.Http.Extention;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Card.Mos;


namespace OSS.SnsSdk.Official.Wx.Card
{
    public partial class WxOffCardApi
    {
        #region  线下核销卡券
        /// <summary>
        ///   查询cardcode 核销状态
        /// </summary>
        /// <param name="code">单张卡券的唯一标准</param>
        /// <param name="cardId">卡券ID代表一类卡券。自定义code卡券必填</param>
        /// <param name="checkConsume">是否校验code核销状态，填入true和false时的code异常状态返回数据不同</param>
        /// <returns></returns>
        public async Task<WxGetCardCodeConsumeResp> GetCardCodeConsumeState(string code, string cardId = "",
            bool checkConsume = false)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(BaseRestApi<WxBaseApi>.m_ApiUrl, "/card/code/get");
            req.CustomBody = $"{{\"code\":\"{code}\",\"card_id\":\"{cardId}\",\"check_consume\":{checkConsume}}}";

            return await RestCommonOffcialAsync<WxGetCardCodeConsumeResp>(req);
        }

 

        #endregion

         /// <summary>
         ///  解密链接中的code
         /// </summary>
         /// <param name="encryptCode"></param>
         /// <returns></returns>
        public async Task<WxCardCodeDecryptResp> DecryptCodeAsync(string encryptCode)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(BaseRestApi<WxBaseApi>.m_ApiUrl, "/card/code/decrypt");
            req.CustomBody = $"{{\"encrypt_code\":\"{encryptCode}\"}}";

            return await RestCommonOffcialAsync<WxCardCodeDecryptResp>(req);
        }

       
        /// <summary>
        ///  核销卡券
        /// </summary>
        /// <param name="code">需核销的Code码</param>
        /// <param name="cardId">卡券ID。创建卡券时use_custom_code填写true时必填。非自定义Code不必填写</param>
        /// <returns></returns>
        public async Task<WxCardConsumeResp> ConsumeCardCodeAsync(string code, string cardId = "")
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(BaseRestApi<WxBaseApi>.m_ApiUrl, "/card/code/consume");
            req.CustomBody = $"{{\"code\":\"{code}\",\"card_id\":\"{cardId}\"}}";

            return await RestCommonOffcialAsync<WxCardConsumeResp>(req);
        }
    }
}
