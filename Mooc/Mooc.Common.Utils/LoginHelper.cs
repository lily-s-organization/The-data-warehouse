using System.Web;

namespace Mooc.Common.Utils
{
    public class LoginHelper
    {
        private static string SessionDomain = ConfigHelper.GetConfigValue("DomainAdminUrl");
        /// <summary>
        /// Login UserId
        /// </summary>
        public static long UserId
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookieID).ToLong(0); }
        }
        /// <summary>
        /// Login UserGuid
        /// </summary>
        public static string UserGuid
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookieGuid); }
        }
        /// <summary>
        /// Login UserName
        /// </summary>
        public static string UserName
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookieName); }
        }
        /// <summary>
        /// Login UserNameType
        /// </summary>
        public static string UserType
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookieType); }
        }
        /// <summary>
        /// Login UserNameEmail
        /// </summary>
        public static string UserEmail
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookieEmail); }
        }
        /// <summary>
        /// Login UserNickName
        /// </summary>
        public static string UserRealName
        {
            get { return CookieHelper.GetCookie(CommonVariables.LoginCookieRealName); }
        }

        public static void Logout()
        {
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookieID);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookieGuid);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookieType);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookieName);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookieEmail);
            CookieHelper.DelSessionCookie(SessionDomain, CommonVariables.LoginCookieRealName);

            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieID);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieGuid);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieType);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieName);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieEmail);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieRealName);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieRealName);

            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            }
        }

        public static void Logout2()
        {
            CookieHelper.DelCookie(CommonVariables.LoginCookieID);
            CookieHelper.DelCookie(CommonVariables.LoginCookieGuid);
            CookieHelper.DelCookie(CommonVariables.LoginCookieType);
            CookieHelper.DelCookie(CommonVariables.LoginCookieName);
            CookieHelper.DelCookie(CommonVariables.LoginCookieEmail);
            CookieHelper.DelCookie(CommonVariables.LoginCookieRealName);

            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieID);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieGuid);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieType);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieName);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieEmail);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieRealName);
            CookieHelper.DelWithCurrentDomain(CommonVariables.LoginCookieRealName);

            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            }
        }

    }
}