using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bus.Security
{
    public class HMAC_SHA1
    {
        // PTX APPID and APPKey
        private static readonly string _APPID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
        private static readonly string _APPKey = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

        private static string Signature(string xDate, string inputCharset = "utf-8")
        {
            Encoding encode = Encoding.GetEncoding(inputCharset);
            byte[] byteData = Encoding.GetEncoding(inputCharset).GetBytes(xDate);
            HMACSHA1 hmac = new HMACSHA1(encode.GetBytes(_APPKey));

            using (CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
            {
                cs.Write(byteData, 0, byteData.Length);
            }
            return Convert.ToBase64String(hmac.Hash);
        }

        public static string GetAuth(string xdate)
        {
            string signDate = "x-date: " + xdate;
            string signature = HMAC_SHA1.Signature(signDate);
            string sAuth = "hmac username=\"" + _APPID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + signature + "\"";

            return sAuth;
        }
    }
}