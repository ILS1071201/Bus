using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bus.Security
{
    public class PtxAuth
    {
        // PTX APPID and APPKey
        private static readonly string _AppID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
        private static readonly string _AppKey = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

        // 將APP Key 與當下時間（格式請使用GMT時間）做HMAC-SHA1 運算後轉成Base64 格式
        private static string Signature(string xDate, string inputCharset = "utf-8")
        {
            // 編碼格式
            Encoding encode = Encoding.GetEncoding(inputCharset);

            // SHA1加密，Hash返回Base64格式
            using (HMACSHA1 hmac = new HMACSHA1(encode.GetBytes(_AppKey)))
            {
                byte[] hashDate = hmac.ComputeHash(encode.GetBytes(xDate));
                return Convert.ToBase64String(hashDate);
            }

            //HMACSHA1 hmac = new HMACSHA1(encode.GetBytes(_AppKey));

            //using (CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
            //{
            //    cs.Write(xDateByteData, 0, xDateByteData.Length);
            //}
            //return Convert.ToBase64String(hmac.Hash);
        }

        public static string GetAuth(string xdate)
        {
            string signDate = "x-date: " + xdate;
            string signature = PtxAuth.Signature(signDate);
            string sAuth = "hmac username=\"" + _AppID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + signature + "\"";

            return sAuth;
        }
    }
}