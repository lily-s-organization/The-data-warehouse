using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mooc.Common.Utils
{
     public class ProductConfigHelper
    {

        public static readonly string UploadPathCorpus = ConfigHelper.GetConfigValue("UploadPathDirectory");
        public static string Scheme
        {
            get
            {
                string mScheme = "";
                try
                {
                    mScheme = (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null) ? HttpContext.Current.Request.Url.Scheme : "http";
                    if (string.IsNullOrEmpty(mScheme))
                        mScheme = "http";
                    mScheme += "://";
                }
                catch (Exception)
                {
                    mScheme = "//";
                }
                return mScheme;
            }
        }
        public static string DomainWebPath
        {
            get { return ConfigHelper.GetConfigValue("DomainWebUrl").Replace("http://", Scheme); }
        }

        public static string DomainResourcePath
        {
            get { return ConfigHelper.GetConfigValue("DomainResourceUrl").Replace("http://", Scheme); }
        }

    }
}
