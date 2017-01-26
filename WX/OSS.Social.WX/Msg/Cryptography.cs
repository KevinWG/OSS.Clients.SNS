using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using OSS.Common.Encrypt;

namespace OSS.Social.WX.Msg
{

   /// <summary>
   /// 微信加密模式下使用的
   /// </summary>
    internal class Cryptography
    {
        public static Int32 HostToNetworkOrder(Int32 inval)
        {
            Int32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i*8)) & 255);
            return outval;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Input">密文</param>
        /// <param name="encodingAesKey"></param>
        /// <returns></returns>
        public static string WxAesDecrypt(String Input, string encodingAesKey)
        {
            byte[] Key;
            Key = Convert.FromBase64String(encodingAesKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            
            byte[] xXml = Convert.FromBase64String(Input);
            byte[] xBuff = AesRijndael.Decrypt(Key, xXml, Iv, 256, 128, CipherMode.CBC, PaddingMode.None);
            byte[] btmpMsg = Decode(xBuff);

            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);
            
            byte[] bMsg = new byte[len];
            //byte[] bAppid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            //Array.Copy(btmpMsg, 20 + len, bAppid, 0, btmpMsg.Length - 20 - len);
            string oriMsg = Encoding.UTF8.GetString(bMsg);
            //appid = Encoding.UTF8.GetString(bAppid);
            return oriMsg;
        }

        /// <summary>
        /// 加密返回的串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encodingAesKey"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string AesEncrypt(String input, string encodingAesKey, string appid)
        {
            byte[] Key;
            Key = Convert.FromBase64String(encodingAesKey + "=");
            byte[] Iv = new byte[16];
            Array.Copy(Key, Iv, 16);

            string randCode = CreateRandCode(16);
            byte[] bRand = Encoding.UTF8.GetBytes(randCode);
            byte[] bAppid = Encoding.UTF8.GetBytes(appid);
            byte[] btmpMsg = Encoding.UTF8.GetBytes(input);
            byte[] bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(btmpMsg.Length));
            byte[] bMsg = new byte[bRand.Length + bMsgLen.Length + bAppid.Length + btmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(btmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, btmpMsg.Length);
            Array.Copy(bAppid, 0, bMsg, bRand.Length + bMsgLen.Length + btmpMsg.Length, bAppid.Length);

            #region  对消息进行PKCS7补位

            byte[] msg = new byte[bMsg.Length + 32 - bMsg.Length % 32];
            Array.Copy(bMsg, msg, bMsg.Length);
            byte[] pad = Kcs7Encoder(bMsg.Length);
            Array.Copy(pad, 0, msg, bMsg.Length, pad.Length);

            #endregion

            byte[] xBuff = AesRijndael.Encrypt(Key, msg, Iv, 256, 128, CipherMode.CBC, PaddingMode.None);

            String output = Convert.ToBase64String(xBuff);
            return output;

        }

        private static byte[] Kcs7Encoder(int textLength)
        {
            int block_size = 32;
            // 计算需要填充的位数
            int amount_to_pad = block_size - (textLength % block_size);
            if (amount_to_pad == 0)
            {
                amount_to_pad = block_size;
            }
            // 获得补位所用的字符
            char pad_chr = Chr(amount_to_pad);
            string tmp = "";
            for (int index = 0; index < amount_to_pad; index++)
            {
                tmp += pad_chr;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }
        static char Chr(int a)
        {
            byte target = (byte)(a & 0xFF);
            return (char)target;
        }

        private static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int) DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decrypted"></param>
        /// <returns></returns>
        private static byte[] Decode(byte[] decrypted)
        {
            int pad = (int) decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}
