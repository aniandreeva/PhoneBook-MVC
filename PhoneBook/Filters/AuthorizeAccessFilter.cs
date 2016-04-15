using PhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Filters
{
    public class AuthorizeAccessFilter:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthenticationService.LoggedUser!=null)
            {
                HttpContext.Current.Response.Redirect("~/Contacts/List");
                filterContext.Result = new EmptyResult();
            }
        }
    }
}