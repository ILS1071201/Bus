using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bus.Security
{
    public class HMAC_SHA1
    {
        private static string _APPID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
        private static string _APPKey = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

        private static string Signature(string xDate, string inputCharset = "utf-8")
        {
            Encoding _encode = Encoding.GetEncoding(inputCharset);
            byte[] _byteData = Encoding.GetEncoding(inputCharset).GetBytes(xDate);
            HMACSHA1 _hmac = new HMACSHA1(_encode.GetBytes(_APPKey));

            using (CryptoStream _cs = new CryptoStream(Stream.Null, _hmac, CryptoStreamMode.Write))
            {
                _cs.Write(_byteData, 0, _byteData.Length);
            }
            return Convert.ToBase64String(_hmac.Hash);
        }

        public static string GetAuth(string xdate)
        {
            string SignDate = "x-date: " + xdate;
            string Signature = HMAC_SHA1.Signature(SignDate);
            string sAuth = "hmac username=\"" + _APPID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + Signature + "\"";

            return sAuth;
        }
    }
}