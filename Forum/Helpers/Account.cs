using Core.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Forum.Helpers
{

    /// <summary>
    /// Account Helper Class
    /// </summary>
    public static class Account
    {

        /// <summary>
        /// Determines whether role or status belongs to Admin.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="role">The user role.</param>
        /// <param name="status">The user status.</param>
        public static bool IsRoleOrStatus(this HtmlHelper helper, string role = null, string status = null)
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                UserDto currentUser = JsonConvert.DeserializeObject<UserDto>(ticket.UserData);

                if (currentUser != null && currentUser.Role == role || currentUser.Status == status)
                    return true;
                return false;
            }
            else
                return false;
        }

        public static bool IsBanned(this HtmlHelper helper)
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                UserDto currentUser = JsonConvert.DeserializeObject<UserDto>(ticket.UserData);

                if (currentUser != null && currentUser.IsBanned)
                    return true;
                return false;
            }
            else
                return false;
        }

    }
}