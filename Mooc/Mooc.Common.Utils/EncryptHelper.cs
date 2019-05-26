using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;

namespace Mooc.Common.Utils
{
    public class EncryptHelper
    {

        private static byte[] Keys = { 0x16, 0x34, 0x56, 0x58, 0x88, 0xAB, 0xCD, 0xEF };
        /**/
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="NewPopedom">加密后的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        /// 
        public static int EncryptPopedom(string encryptString, out string NewPopedom)
        {
            NewPopedom = null;
            try
            {
                //加密密钥,要求为8位
                string encryptKey = ConfigurationManager.AppSettings["EncryptKey_XueWen"].ToString();
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                NewPopedom = Convert.ToBase64String(mStream.ToArray());
                NewPopedom = EncryptEncoding(NewPopedom);
                return 1;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        /**/
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="OldPopedom">解密后的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static int UnEncryptPopedom(string decryptString, out string OldPopedom)
        {
            OldPopedom = null;
            decryptString = UnEncoding(decryptString);
            try
            {
                //byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                //解密密钥,要求为8位,和加密密钥相同
                string decryptKey = ConfigurationManager.AppSettings["EncryptKey_XueWen"].ToString();
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                OldPopedom = Encoding.UTF8.GetString(mStream.ToArray());
                return 1;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        /// <summary>
        /// 加密替换特殊符号
        /// </summary>
        /// <param name="OldStr"></param>
        /// <returns></returns>
        public static string EncryptEncoding(string OldStr)
        {
            return OldStr.Replace("+", "%mmd2B").Replace("\"", "%mmd22").Replace("'", "%mmd27").Replace("/", "%mmd2F");

        }

        /// <summary>
        /// 解密还原特殊符号
        /// </summary>
        /// <param name="OldStr"></param>
        /// <returns></returns>
        public static string UnEncoding(string OldStr)
        {
            return OldStr.Replace("%mmd2B", "+").Replace("%mmd22", "\"").Replace("%mmd27", "'").Replace("%mmd2F", "/");
        }


        public static string DecryptBase64Encoding(string encryptstr)
        {
            try
            {
                encryptstr = encryptstr.Replace(" ", "+");
                int totalLen = encryptstr.Length, tempLen = encryptstr.Length;
                int equalItemCount = 0;
                char[] source = encryptstr.ToCharArray();
                char[] result = new char[totalLen];
                while (source[tempLen - 1] != '\0')
                {
                    if (source[tempLen - 1] != '\0' && source[tempLen - 1] == '=')
                    {
                        equalItemCount++;
                        tempLen--;
                    }
                    else
                    {
                        break;
                    }
                }
                string subSecretStr = encryptstr.Substring(0,totalLen - equalItemCount);
                string keySecret = string.Empty;
                if (subSecretStr.Length > 5) {
                    keySecret = subSecretStr.Substring(1, 5);
                }
                for (int i = 0; i < totalLen; i++) {
                    if (i == 0 || i >= (totalLen - equalItemCount)) {
                        result[i]=source[i];
                    }
                    else if (i > 0 && i < totalLen - equalItemCount-5) { 
                        result[i]=source[i+5];
                    }
                    else {
                        result[i] = source[6 + i - totalLen + equalItemCount];
                    }
                }
                string decryptstr = new string(result);
                return Encoding.UTF8.GetString(Convert.FromBase64String(decryptstr));
            }
            catch
            {
                return string.Empty;
            }
        }
        
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Code(string Message)
        {
            byte[] bytes = Encoding.Default.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.Default.GetString(bytes);
        }

        #region 简单加密
        public static char[] EncryChars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'G', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '@', '#', '$' };
        public static string EnBase64String(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            try
            {
                Random random = new Random();
                if (str.Length > 2)
                {
                    str = str.Substring(0, 2) + EncryChars[random.Next(0, EncryChars.Length - 1)] + EncryChars[random.Next(0, EncryChars.Length - 1)] + str.Substring(2, str.Length - 2);
                }
                if (str.Length > 8)
                {
                    str = str.Substring(0, 8) + EncryChars[random.Next(0, EncryChars.Length - 1)] + EncryChars[random.Next(0, EncryChars.Length - 1)] + str.Substring(8, str.Length - 8);
                }

                Console.WriteLine("加密前的临时源:{0}", str);
                StringBuilder sb = new StringBuilder();
                int iLength = str.Length;
                int iEven = 0;
                int iOdd = 1;
                if (iLength % 2 == 0)
                {
                    iEven = iLength - 1;
                    iOdd = iLength - 2;
                }
                else
                {
                    iEven = iLength - 2;
                    iOdd = iLength - 1;
                }
                //倒序并奇偶颠倒
                for (int i = iEven; i >= 0; i--)
                {
                    sb.Append(str[i]);
                    i--;
                }
                for (int j = iOdd; j >= 0; j--)
                {
                    sb.Append(str[j]);
                    j--;
                }

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(sb.ToString());
                return Convert.ToBase64String(bytes, Base64FormattingOptions.None);
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }

        public static string UnBase64String(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
                return string.Empty;
            try
            {
                byte[] bytes = Convert.FromBase64String(base64Str);
                string str = System.Text.Encoding.ASCII.GetString(bytes);
                Console.WriteLine("解出的源:{0}", str);
                StringBuilder sb = new StringBuilder();
                int iLength = str.Length;
                int iHalf = 0;
                if (iLength % 2 == 0)
                {
                    iHalf = (int)Math.Floor(iLength * 0.5);
                    for (int i = iLength - 1; i >= iHalf; i--)
                    {
                        sb.Append(str[i]);
                        sb.Append(str[i - iHalf]);
                    }
                }
                else
                {
                    iHalf = (int)Math.Ceiling(iLength * 0.5);
                    for (int i = iLength - 1; i >= iHalf - 1; i--)
                    {
                        sb.Append(str[i]);
                        if (i - iHalf >= 0)
                            sb.Append(str[i - iHalf]);
                    }
                }
                string str2 = sb.ToString();
                Console.WriteLine("解密出的临时源:{0}", str2);
                if (str2.Length > 10)
                {
                    str2 = str2.Substring(0, 8) + str2.Substring(10, str2.Length - 10);
                }
                if (str2.Length > 4)
                {
                    str2 = str2.Substring(0, 2) + str2.Substring(4, str2.Length - 4);
                }

                return str2;
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }
        #endregion

    }
}
