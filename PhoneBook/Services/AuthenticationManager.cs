using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services
{
    public static class AuthenticationManager
    {
        public static AuthenticationService AuthInstanse
        {
            get
            {
                if (HttpContext.Current!=null&& HttpContext.Current.Session[typeof(AuthenticationService).Name]==null)
                {
                    HttpContext.Current.Session[typeof(AuthenticationService).Name] = new AuthenticationService();
                }
                return (AuthenticationService)HttpContext.Current.Session[typeof(AuthenticationService).Name];
            }
        }

       public static User LoggedUser
        {
            get { return AuthInstanse.CurrentUser; }
        }

        public static void AuthenticateUser(string username, string password)
        {
            AuthInstanse.AuthenticateUser(username, password);
        }

        public static void AuthenticateUserByCookie(HttpCookie cookie)
        {
            AuthenticateUserByCookie(cookie);
        }

        public static void Logout()
        {
            CookieService.DeleteCookie();
            AuthenticateUser(null, null);
        }

    }
}