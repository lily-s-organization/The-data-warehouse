using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Mooc.Common.Utils
{
    public class HttpHelper
    {
        private static int timeOut = 10000;//设置连接超时时间，默认10秒，用户可以根据具体需求适当更改timeOut的值

        public static string HttpPostMath(string url, string paramsValue, ContentTypeEnum ContentType = ContentTypeEnum.Form, Dictionary<string, string> headerDic = null)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                switch (ContentType)
                {
                    default:
                    case ContentTypeEnum.Form:
                        request.ContentType = "application/x-www-form-urlencoded";
                        break;
                    case ContentTypeEnum.Json:
                        request.ContentType = "application/json";
                        break;
                    case ContentTypeEnum.Xml:
                        request.ContentType = "text/xml";
                        break;

                }

                request.ContentLength = byteArray.Length;
                if (headerDic != null && headerDic.Count > 0)
                {
                    foreach (var item in headerDic)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
            }

            return result;

        }
        public static string HttpGetMath(string url, string paramsValue)
        {
            string result = string.Empty;
            Uri uri = new Uri(url);

            HttpWebRequest request = null;
            if (string.IsNullOrEmpty(paramsValue))
            {
                request = (HttpWebRequest)WebRequest.Create(uri);
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(uri + "?" + paramsValue);
            }

            request.Accept = "*/*";
            request.KeepAlive = true;
            request.Timeout = timeOut;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)";
            request.Method = "Get";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            result = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return result;
        }
    }
    public enum ContentTypeEnum
    {
        Xml,
        Json,
        Form
    }
}
