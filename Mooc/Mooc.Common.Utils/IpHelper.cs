using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mooc.Common.Utils
{
    public class IPHelper
    {
        /// <summary>  
        /// 获取远程访问用户的Ip地址  
        /// </summary>  
        /// <returns>返回Ip地址</returns>  
        public static string GetLoginIp(HttpRequest Request=null)
        {
            if (Request == null)
                Request = System.Web.HttpContext.Current.Request;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] t = ip.Split(',');
                if (t.Length > 1 && !string.IsNullOrEmpty(t[t.Length - 1]))
                    ip = t[t.Length - 1];
            }

            if (string.IsNullOrEmpty(ip))
                ip = Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(ip))
                ip = Request.UserHostAddress;

            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }
            return ip.Trim();
            //return "192.168.21.77";
        }

        public static string GetLoginIp_2(HttpRequest Request = null)
        {
            if (Request == null)
                Request = HttpContext.Current.Request;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ips = ip.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string _ip in ips)
                {
                    if (_ip.Equals(":1") || IsPrivateIPAddress(_ip))
                        continue;
                    ip = _ip;
                    break;
                }
                //if (ips.Length > 2)
                //    ip = ips[0];
                //else
                //    ip = ips[ips.Length - 1];
            }

            if (string.IsNullOrEmpty(ip) || IsPrivateIPAddress(ip))
                ip = Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(ip))
                ip = Request.UserHostAddress;
            //#if DEBUG
            //            ip = "211.151.93.221";
            //#endif
            //logger.DebugFormat("客户端IP：{0}", ip.Trim());
            if (ip == "::1")
                ip = "127.0.0.1";
            return ip.Trim();
        }
        private static bool IsPrivateIPAddress(string ip)
        {
            return (System.Text.RegularExpressions.Regex.IsMatch(ip, @"^10\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$") || System.Text.RegularExpressions.Regex.IsMatch(ip, @"^172\.((1[6-9]{1})|(2[0-9]{1})|(3[0-1]{1}))\.[0-9]{1,3}\.[0-9]{1,3}$") || System.Text.RegularExpressions.Regex.IsMatch(ip, @"^192\.168\.[0-9]{1,3}\.[0-9]{1,3}$"));
        }

        public static string GetServerIP(HttpRequest Request = null)
        {
            if (Request == null)
                Request = HttpContext.Current.Request;
            return string.Format("{0}", Request.ServerVariables.Get("Local_Addr"));
        }
    }
}
