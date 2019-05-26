using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mooc.Common.Utils
{

    public class BrowserHelper
    {
        public static string S_MSIE = @"(msie\s|trident.*rv:)([\w.]+)";
        public static string S_MSIELess11 = @"(?<name>msie\s)(?<version>[\w.]+)"; //只适用于IE11以下的
        public static string S_MSIELess12 = @"(?<name>msie\s)(?<version>[\w.]+)|(?<name>trident).*rv:(?<version>[\w.]+)";
        public static string S_EDGE = @"(edge)\/([\w.]+)";
        public static string S_Filefox = @"(firefox)\/([\w.]+)";
        public static string S_Filefox2 = @"(?<name>(firefox))\/(?<version>[\w.]+)";
        public static string S_Chrome = @"(chrome)\/([\w.]+)";
        public static string S_Chrome2 = @"(?<name>chrome)\/(?<version>[\w.]+)";
        public static string S_Opera = @"(opera).+version|(opr)\/([\w.]+)";
        public static string S_Opera2 = @"(?<name>opera).+(?<version>version)|(?<name>opr)\/(?<version>[\w.]+)";
        public static string S_Safari = @"version\/([\w.]+).*(safari)";
        public static string S_Safari2 = @"version\/(?<version>[\w.]+).*(?<name>safari)";
        public static string S_Baidu2 = @"(?<name>bidubrowser)\/(?<version>[\w.]+)";  //BIDUBrowser/8.4
        public static string S_QQ2 = @"(?<name>qqbrowser)\/(?<version>[\w.]+)"; //QQBrowser/9.4.8309.400
        public static string S_Liebao = "lbbrowser";
        public static string S_Sougo = @"(?<name>se) (?<version>[\w.]+)";// @"(?<name>se) (?<version>[\w.]+)"; //SE 2.X MetaSr 1.0
        public static string S_TencentTraveler = @"(?<name>tencenttraveler) (?<version>[\w.]+)"; //TencentTraveler 4.0
        public static string S_360se = @"360se"; //360se
        public static string S_Maxthon = @"(?<name>maxthon)\/(?<version>[\w.]+)"; //Maxthon/3.0
        public static string S_TheWorld = @"(?<name>theworld) (?<version>[\w.]+)"; //TheWorld 7
        public static string S_AvantBrowser = @"avant browser";
        public static string S_UC = @"(?<name>ucweb)\/(?<version>[\w.]+)";   //UCWEB7.0.2.37/28/999

        public const string C_MSIE = "MSIE";
        public const string C_FIREFOX = "Firefox";
        public const string C_CHROME = "Chrome";
        public const string C_OPERA = "Opera";
        public const string C_SAFARI = "Safari";
        public const string C_OTHER = "Other";
        public const string C_EDGE = "Edge";
        public const string C_BAIDU = "Baidu";
        public const string C_QQ = "QQ";
        public const string C_Tencent = "TencentTT";
        public const string C_Liebao = "Liebao";
        public const string C_SouGo = "Sougo";
        public const string C_360se = "360se";
        public const string C_Maxthon = "Maxthon";
        public const string C_TheWorld = "TheWorld";
        public const string C_AvantBrowser = @"AvantBrowser";
        public const string C_UC = @"UCWEB"; 

        /// <summary>
        /// 判断浏览器
        /// </summary>
        /// <param name="isFireFoxOrSafari"></param>
        /// <returns></returns>
        public static bool IsIEBrowser(out bool isFireFoxOrSafari)
        {
            bool isIe = false;
            isFireFoxOrSafari = true;
            string strNowBrowser = HttpContext.Current.Request.Browser.Browser;
            if (string.Compare(strNowBrowser, "ie", true) == 0 || string.Compare(strNowBrowser, "InternetExplorer", true) == 0 || string.Compare(strNowBrowser, "Edge") == 0)
            {
                isIe = true;
                isFireFoxOrSafari = false;
            }
            else
            {
                BrowserInfo info = BrowserHelper.GetBrowser(HttpContext.Current.Request.UserAgent);
                //20140619 对IE浏览器的判断条件进行宽松处理,没有匹配的也默认为IE
                if (info == null || info.Name == BrowserHelper.C_MSIE || info.Name == "Other" || info.Name == BrowserHelper.C_EDGE)
                {
                    isIe = true;
                    isFireFoxOrSafari = false;
                }
                if (!isIe)
                {
                    //特殊处理
                    string strTemp = "Mozilla/4.0 (Windows; U; Windows NT 5.1; zh-TW; rv:1.9.0.11)";
                    if (string.Compare(HttpContext.Current.Request.UserAgent, strTemp, true) == 0)
                    {
                        isIe = true;
                        isFireFoxOrSafari = false;
                    }
                }
            }
            //Safari,Chrome,FireFox
            //else if (string.Compare(strNowBrowser, "mozilla", true) == 0 || string.Compare(strNowBrowser, "firefox", true) == 0 || string.Compare(strNowBrowser, "AppleMAC-Safari", true) == 0)
            //{
            //    isFireFoxOrSafari = true;
            //}
            return isIe;
        }

        /// <summary>
        /// 判断是否为FireFox浏览器
        /// </summary>
        /// <returns></returns>
        public static bool IsFireFoxBrowser()
        {
            return HttpContext.Current.Request.Browser.Browser.Equals("Firefox", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsFormMacOS()
        {
            string strUserAgent = HttpContext.Current.Request.UserAgent;
            if (strUserAgent != null && strUserAgent.ToLower().IndexOf("Mac OS") > 0)
            {
                return true;
            }
            else
                return false;
        }

        #region 判断当前访问是否来自浏览器软件
        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox", "safari", "chrome", "internetexplorer", "edge" };
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            if (browser == null)
                return false;
            string strBrowserName = browser.Browser.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (strBrowserName.IndexOf(BrowserName[i]) >= 0)
                    return true;
            }
            return false;
        }
        #endregion

        public static BrowserInfo GetBrowser(string strUserAgent)
        {
            BrowserInfo info = null;
            if (string.IsNullOrEmpty(strUserAgent))
                return info;
            strUserAgent = strUserAgent.ToLower();

            string strName = string.Empty;
            string strOtherName = string.Empty;
            string strVersion = "0";

            Regex regMsie = new Regex(S_MSIE);
            Regex regMsie12 = new Regex(S_MSIELess12);
            Regex regFirefox = new Regex(S_Filefox2);
            Regex regChrome = new Regex(S_Chrome2);
            Regex regOpera = new Regex(S_Opera2);
            Regex regSafari = new Regex(S_Safari2);
            Regex regEdge = new Regex(S_EDGE);

            Match m = regMsie.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_MSIE;
                if (m.Groups.Count >= 2)
                    strOtherName = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strVersion = m.Groups[2].Value;
            }
            m = regMsie12.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_MSIE;
                if (m.Groups.Count >= 2)
                    strOtherName = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strVersion = m.Groups[2].Value;
            }
            m = regFirefox.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_FIREFOX;
                if (m.Groups.Count >= 2)
                    strOtherName = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strOtherName = m.Groups[2].Value;
                if (m.Groups.Count >= 4)
                    strVersion = m.Groups[3].Value;
            }
            m = regChrome.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_CHROME;
                if (m.Groups.Count >= 2)
                    strOtherName = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strVersion = m.Groups[2].Value;
            }
            m = regOpera.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_OPERA;
                if (m.Groups.Count >= 2)
                    strOtherName = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strVersion = m.Groups[2].Value;
            }
            m = regSafari.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_SAFARI;
                if (m.Groups.Count >= 2)
                    strVersion = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strOtherName = m.Groups[2].Value;
            }
            m = regEdge.Match(strUserAgent);
            if (m.Success)
            {
                strName = BrowserHelper.C_EDGE;
                if (m.Groups.Count == 2)
                    strOtherName = m.Groups[1].Value;
                if (m.Groups.Count >= 3)
                    strVersion = m.Groups[2].Value;
            }
            if (string.IsNullOrEmpty(strName))
                strName = "Other";
            float fVersion = 0.0f;
            if (!string.IsNullOrEmpty(strVersion))
            {
                string[] versionArr = strVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (versionArr.Length > 2)
                    fVersion = string.Format("{0}.{1}", versionArr[0], versionArr[1]).ToFloat(0.0f);
                else
                    fVersion = strVersion.ToFloat(0.0f);
            }
            string strCNName = string.Empty;
            string strCNVersion = string.Empty;
            if (strUserAgent.IndexOf("bidubrowser") != -1)
            {
                Regex regBaidu = new Regex(S_Baidu2);
                m = regBaidu.Match(strUserAgent);
                if (m.Success)
                {
                    strCNName = BrowserHelper.C_BAIDU;
                    //if (m.Groups.Count >= 2)
                    //    strOtherName = m.Groups[1].Value;
                    if (m.Groups.Count >= 3)
                        strCNVersion = m.Groups[2].Value;
                }
            }
            else if (strUserAgent.IndexOf("qqbrowser") != -1)
            {
                Regex regQQ = new Regex(S_QQ2);
                m = regQQ.Match(strUserAgent);
                if (m.Success)
                {
                    strCNName = BrowserHelper.C_QQ;
                    //if (m.Groups.Count >= 2)
                    //    strOtherName = m.Groups[1].Value;
                    if (m.Groups.Count >= 3)
                        strCNVersion = m.Groups[2].Value;
                }
            }
            else if (strUserAgent.IndexOf(BrowserHelper.S_Liebao) != -1)
            {
                strCNName = BrowserHelper.C_Liebao;
                strCNVersion = "0";
            }
            else if (strUserAgent.IndexOf("se ") != -1 && strUserAgent.IndexOf("metasr ") != -1)  //SE 2.X MetaSr 1.0
            {
                strCNName = BrowserHelper.C_SouGo;
                strCNVersion = "0";
                //SE 2.X MetaSr 1.0
                Regex regSougo = new Regex(BrowserHelper.S_Sougo);
                m = regSougo.Match(strUserAgent);
                if (m.Success)
                {
                    if (m.Groups.Count >= 3)
                        strCNVersion = m.Groups[2].Value;
                }
            }
            else if (strUserAgent.IndexOf("tencenttraveler") != -1)
            {
                strCNName = BrowserHelper.C_Tencent;
                strCNVersion = "0";
            }
            else if (strUserAgent.IndexOf("360se") != -1)
            {
                strCNName = BrowserHelper.C_360se;
                strCNVersion = "0";
            }
            else if (strUserAgent.IndexOf("maxthon") != -1) //Maxthon/3.0
            {
                Regex regMaxthon = new Regex(S_Maxthon);
                strCNVersion = "0";
                m = regMaxthon.Match(strUserAgent);
                if (m.Success)
                {
                    strCNName = BrowserHelper.C_Maxthon;
                    //if (m.Groups.Count >= 2)
                    //    strOtherName = m.Groups[1].Value;
                    if (m.Groups.Count >= 3)
                        strCNVersion = m.Groups[2].Value;
                }
            }
            else if (strUserAgent.IndexOf("theworld") != -1)
            {
                strCNName = BrowserHelper.C_TheWorld;
                strCNVersion = "0";
                Regex reg = new Regex(S_TheWorld);
                m = reg.Match(strUserAgent);
                if (m.Success)
                {
                    if (m.Groups.Count >= 3)
                        strCNVersion = m.Groups[2].Value;
                }
            }
            else if (strUserAgent.IndexOf(BrowserHelper.S_AvantBrowser) != -1)
            {
                strCNName = BrowserHelper.C_AvantBrowser;
                strCNVersion = "0";
            }
            else if (strUserAgent.IndexOf("ucweb") != -1)
            {
                Regex regUC = new Regex(S_UC);
                m = regUC.Match(strUserAgent);
                if (m.Success)
                {
                    strCNName = BrowserHelper.C_UC;
                    //if (m.Groups.Count >= 2)
                    //    strOtherName = m.Groups[1].Value;
                    if (m.Groups.Count >= 3)
                        strCNVersion = m.Groups[2].Value;
                }
            }
            info = new BrowserInfo { Name = strName, OtherName = strOtherName, Version = strVersion, UserAgent = strUserAgent, VersionNumber = fVersion, CNName = strCNName, CNVersion = strCNVersion };
            return info;
        }

        public static string GetBrowser1(string strUserAgent)
        {
            if (string.IsNullOrEmpty(strUserAgent))
                return string.Empty;
            strUserAgent = strUserAgent.ToLower();
            BrowserInfo info = new BrowserInfo();

            Regex regMsie = new Regex(S_MSIE);
            Regex regMsie10 = new Regex(S_MSIELess11);
            Regex regMsie12 = new Regex(S_MSIELess12);
            Regex regFirefox = new Regex(S_Filefox2);
            Regex regChrome = new Regex(S_Chrome2);
            Regex regOpera = new Regex(S_Opera2);
            Regex regSafari = new Regex(S_Safari2);

            string str = string.Empty;
            str = strUserAgent + "\r\n";
            Match m = regMsie.Match(strUserAgent);
            if (m.Success)
            {
                str += "regMsie\r\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    str += i + ":" + m.Groups[i] + ", ";
                }
                str += "\r\n";
            }
            //m = regMsie10.Match(strUserAgent);
            //if (m.Success)
            //{
            //    str += "regMsie10\r\n";
            //    for (int i = 0; i < m.Groups.Count; i++)
            //    {
            //        str += i + ":" + m.Groups[i] + ", ";
            //    }
            //    str += "\r\n";
            //}
            m = regMsie12.Match(strUserAgent);
            if (m.Success)
            {
                str += "regMsie12\r\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    str += i + ":" + m.Groups[i] + ", ";
                }
                str += "\r\n";
            }
            m = regFirefox.Match(strUserAgent);
            if (m.Success)
            {
                str += "regFirefox\r\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    str += i + ":" + m.Groups[i] + ", ";
                }
                str += "\r\n";
            }
            m = regChrome.Match(strUserAgent);
            if (m.Success)
            {
                str += "regChrome\r\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    str += i + ":" + m.Groups[i] + ", ";
                }
                str += "\r\n";
            }
            m = regOpera.Match(strUserAgent);
            if (m.Success)
            {
                str += "regOpera\r\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    str += i + ":" + m.Groups[i] + ", ";
                }
                str += "\r\n";
            }
            m = regSafari.Match(strUserAgent);
            if (m.Success)
            {
                str += "regSafari\r\n";
                for (int i = 0; i < m.Groups.Count; i++)
                {
                    str += i + ":" + m.Groups[i] + ", ";
                }
                str += "\r\n";
            }
            return str;
        }
    }

    public class BrowserInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string OtherName { get; set; }
        public string UserAgent { get; set; }
        public float VersionNumber { get; set; }
        public string CNName { get; set; }
        public string CNVersion { get; set; }
    }
}
