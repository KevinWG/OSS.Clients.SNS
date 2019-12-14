#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：OSS - 微信加密相关方法
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using OSS.Common.Encrypt;

namespace OSS.Clients.Chat.WX
{

   /// <summary>
   /// 微信加密模式下使用的
   /// </summary>
    internal class Cryptography
    {
        public static Int32 HostToNetworkOrder(Int32 inval)
        {
            var outval = 0;
            for (var i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i*8)) & 255);
            return outval;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Input">密文</param>
        /// <param name="encodingAesKey"></param>
        /// <returns></returns>
        public static string WXAesDecrypt(String Input, string encodingAesKey)
        {
            var Key = Convert.FromBase64String(encodingAesKey + "=");
            var Iv = new byte[16];
            Array.Copy(Key, Iv, 16);
            
            var xXml = Convert.FromBase64String(Input);
            var xBuff = AesRijndael.Decrypt(Key, xXml, Iv, 256, 128, CipherMode.CBC, PaddingMode.None);
            var btmpMsg = Decode(xBuff);

            var len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);
            
            var bMsg = new byte[len];
            //byte[] bAppid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            //Array.Copy(btmpMsg, 20 + len, bAppid, 0, btmpMsg.Length - 20 - len);
            var oriMsg = Encoding.UTF8.GetString(bMsg);
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
            var Key = Convert.FromBase64String(encodingAesKey + "=");
            var Iv = new byte[16];
            Array.Copy(Key, Iv, 16);

            var randCode = CreateRandCode(16);
            var bRand = Encoding.UTF8.GetBytes(randCode);
            var bAppid = Encoding.UTF8.GetBytes(appid);
            var btmpMsg = Encoding.UTF8.GetBytes(input);
            var bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(btmpMsg.Length));
            var bMsg = new byte[bRand.Length + bMsgLen.Length + bAppid.Length + btmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(btmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, btmpMsg.Length);
            Array.Copy(bAppid, 0, bMsg, bRand.Length + bMsgLen.Length + btmpMsg.Length, bAppid.Length);

            #region  对消息进行PKCS7补位

            var msg = new byte[bMsg.Length + 32 - bMsg.Length % 32];
            Array.Copy(bMsg, msg, bMsg.Length);
            var pad = Kcs7Encoder(bMsg.Length);
            Array.Copy(pad, 0, msg, bMsg.Length, pad.Length);

            #endregion

            var xBuff = AesRijndael.Encrypt(Key, msg, Iv, 256, 128, CipherMode.CBC, PaddingMode.None);
            return Convert.ToBase64String(xBuff);
        }

        private static byte[] Kcs7Encoder(int textLength)
        {
            const int block_size = 32;

            // 计算需要填充的位数
            var amount_to_pad = block_size - (textLength % block_size);
            if (amount_to_pad == 0)
            {
                amount_to_pad = block_size;
            }

            // 获得补位所用的字符
            var pad_chr = Chr(amount_to_pad);
            var tmp = "";
            for (var index = 0; index < amount_to_pad; index++)
            {
                tmp += pad_chr;
            }

            return Encoding.UTF8.GetBytes(tmp);
        }

        private static char Chr(int a)
        {
            var target = (byte)(a & 0xFF);
            return (char)target;
        }

        private static string CreateRandCode(int codeLen)
        {
            const string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }

            var arr = codeSerial.Split(',');
            var code = new StringBuilder(codeLen);

            var rand = new Random(unchecked((int) DateTime.Now.Ticks));
            for (var i = 0; i < codeLen; i++)
            {
                var randValue = rand.Next(0, arr.Length - 1);
                code.Append(arr[randValue]);
            }
            return code.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decrypted"></param>
        /// <returns></returns>
        private static byte[] Decode(byte[] decrypted)
        {
            var pad = (int) decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }

            var res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}
