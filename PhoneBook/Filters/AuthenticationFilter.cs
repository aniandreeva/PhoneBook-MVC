using PhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Filters
{
    public class AuthenticationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["rememberMe"];

            if (cookie != null && AuthenticationService.LoggedUser == null)
            {
                //if (cookie.Expires.CompareTo(DateTime.Now) >= 0)
                //{
                    AuthenticationService.AuthenticateUserByCookie(cookie);
                //}
                //else
                //{
                //    filterContext.HttpContext.Response.Redirect("~/Account/Login?RedirectUrl=" + filterContext.HttpContext.Request.Url);
                //    filterContext.Result = new EmptyResult();
                //}
            }
            else
            {
                if (AuthenticationService.LoggedUser==null)
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/Login?RedirectUrl=" + filterContext.HttpContext.Request.Url);
                    filterContext.Result = new EmptyResult();
                }
            }
        }
    }
}