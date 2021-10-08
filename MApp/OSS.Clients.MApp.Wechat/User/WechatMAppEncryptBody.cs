using OSS.Common.Encrypt;
using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace OSS.Clients.MApp.Wechat
{
    public class WechatMAppEncryptBody
    {
        public string encrypt_data { get; set; }

        public string iv { get; set; }
    }

    public static class WechatMAppEncryptBodyMaps
    {
        public static WechatMAppUserInfo ToUserInfo(this WechatMAppEncryptBody encryptBody, string sessionKey)
        {
            return encryptBody.DecryptTo<WechatMAppUserInfo>(sessionKey);
        }

        public static WechatUserPhoneInfo ToUserPhone(this WechatMAppEncryptBody encryptBody, string sessionKey)
        {
            return encryptBody.DecryptTo<WechatUserPhoneInfo>(sessionKey);
        }

        private static TRes DecryptTo<TRes>(this WechatMAppEncryptBody encryptBody, string sessionKey)
        {
            try
            {
                var encryptDataBytes = Convert.FromBase64String(encryptBody.encrypt_data);
                var sesssionKeyBytes = Convert.FromBase64String(sessionKey);
                var ivBytes          = Convert.FromBase64String(encryptBody.iv);

                var result = Encoding.UTF8.GetString(AesRijndael.Decrypt(sesssionKeyBytes, encryptDataBytes, ivBytes, 128,
                    128, CipherMode.CBC));

                return JsonConvert.DeserializeObject<TRes>(result);

            }
            catch
            {
             
            }

            return default;
        }
    }
}
