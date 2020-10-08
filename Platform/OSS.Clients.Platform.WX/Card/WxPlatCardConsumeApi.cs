#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券核销接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-24
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Threading.Tasks;
using OSS.Clients.Platform.WX.Card.Mos;
using OSS.Tools.Http.Mos;

namespace OSS.Clients.Platform.WX.Card
{
    public partial class WXPlatCardApi
    {
        #region 线下核销卡券

        /// <summary>
        ///   查询cardcode 核销状态
        /// </summary>
        /// <param name="code">单张卡券的唯一标准</param>
        /// <param name="cardId">卡券ID代表一类卡券。自定义code卡券必填</param>
        /// <param name="checkConsume">是否校验code核销状态，填入true和false时的code异常状态返回数据不同</param>
        /// <returns></returns>
        public async Task<WXGetCardCodeConsumeResp> GetCardCodeConsumeState(string code, string cardId = "",
            bool checkConsume = false)
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/get");
            req.CustomBody = $"{{\"code\":\"{code}\",\"card_id\":\"{cardId}\",\"check_consume\":{checkConsume}}}";

            return await RestCommonPlatAsync<WXGetCardCodeConsumeResp>(req);
        }



        #endregion

        /// <summary>
        ///  解密链接中的code
        /// </summary>
        /// <param name="encryptCode"></param>
        /// <returns></returns>
        public async Task<WXCardCodeDecryptResp> DecryptCodeAsync(string encryptCode)
        {
            var req = new OssHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/card/code/decrypt"),
                CustomBody = $"{{\"encrypt_code\":\"{encryptCode}\"}}"
            };

            return await RestCommonPlatAsync<WXCardCodeDecryptResp>(req);
        }


        /// <summary>
        ///  核销卡券
        /// </summary>
        /// <param name="code">需核销的Code码</param>
        /// <param name="cardId">卡券ID。创建卡券时use_custom_code填写true时必填。非自定义Code不必填写</param>
        /// <returns></returns>
        public async Task<WXCardConsumeResp> ConsumeCardCodeAsync(string code, string cardId = "")
        {
            var req = new OssHttpRequest();

            req.HttpMethod = HttpMethod.Post;
            req.AddressUrl = string.Concat(m_ApiUrl, "/card/code/consume");
            req.CustomBody = $"{{\"code\":\"{code}\",\"card_id\":\"{cardId}\"}}";

            return await RestCommonPlatAsync<WXCardConsumeResp>(req);
        }
    }
}
