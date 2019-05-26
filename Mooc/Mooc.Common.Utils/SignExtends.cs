using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Mooc.Common.Utils
{
    /// <summary>
    /// 签名类
    /// </summary>
    public class SignExtends
    {
        public static string Signature(SortedDictionary<string, Object> dict, string appid, string secret, string host, string api)
        {
            try
            {
                if (!dict.ContainsKey("SecretId"))
                    dict.Add("SecretId", appid);
                if (!dict.ContainsKey("Nonce"))
                {
                    Random rand = new Random();
                    dict.Add("Nonce", rand.Next(Int32.MaxValue).ToString());

                }
                if (!dict.ContainsKey("Timestamp"))
                {
                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
                    DateTime nowTime = DateTime.Now;
                    long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
                    dict.Add("Timestamp", (unixTime / 1000).ToString());
                }
                dict.Add("RequestClient", "SDK_1.0.0");
                string plainText = MakeSignPlainText(dict, "GET", host, api);
                string signatureMethod = "HmacSHA1";
                if (dict.ContainsKey("SignatureMethod") && dict["SignatureMethod"].ToString().Equals("HmacSHA256", StringComparison.InvariantCultureIgnoreCase))
                {
                    signatureMethod = "HmacSHA256";
                }

                dict.Add("Signature", Signature(plainText, secret, signatureMethod));


                string str = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

                return Convert.ToBase64String(Encoding.UTF8.GetBytes(str)).Replace(" ", "$2B$").Replace("+", "$2C$").Replace("=", "$2D$");
            }
            catch (Exception e)
            {
                return "";
            }
        }
        ///<summary>生成签名</summary>
        ///<param name="signStr">被加密串</param>
        ///<param name="secret">加密密钥</param>
        ///<returns>签名</returns>
        private static string Signature(string signStr, string secret, string SignatureMethod)
        {
            if (SignatureMethod == "HmacSHA256")
                using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                    return Convert.ToBase64String(hash);
                }
            else
                using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                    return Convert.ToBase64String(hash);
                }
        }

        private static string BuildParamStr(SortedDictionary<string, object> requestParams, string requestMethod = "GET")
        {
            string retStr = "";
            foreach (string key in requestParams.Keys)
            {
                if (key == "Signature")
                {
                    continue;
                }
                if (requestMethod == "POST" && requestParams[key].ToString().Substring(0, 1).Equals("@"))
                {
                    continue;
                }
                retStr += string.Format("{0}={1}&", key.Replace("_", "."), requestParams[key]);
            }
            return "?" + retStr.TrimEnd('&');
        }

        private static string MakeSignPlainText(SortedDictionary<string, object> requestParams, string requestMethod = "GET",
            string requestHost = "xyz.cnki.net", string requestPath = "/file/kfd/download/single/pdf.action")
        {
            string retStr = "";
            retStr += requestMethod;
            retStr += requestHost;
            retStr += requestPath;
            retStr += BuildParamStr(requestParams);
            return retStr;
        }
    }
}
