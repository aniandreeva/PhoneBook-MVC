﻿using PhoneBook.Models;
using PhoneBook.Repositories;
using PhoneBook.Services.ModelsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services
{
    public class CookieService
    {
        public static void CreateCookie()
        {
            UsersServices usersService = new UsersServices();
            User user = usersService.GetByID(AuthenticationService.LoggedUser.ID);

            if (user!=null)
            {
                HttpCookie cookie = new HttpCookie("rememberMe");
                cookie.Name = "rememberMe";
                cookie.Value= Guid.NewGuid().ToString();
                cookie.Expires = DateTime.Now.AddMinutes(1);
                HttpContext.Current.Response.Cookies.Add(cookie);

                //user.RememberMeHash = new Guid().ToString();
                user.RememberMeExpiryDate = DateTime.Now.AddMinutes(1);
                user.RememberMeHash = cookie.Value;

                usersService.Save(user);
                
                //cookie.Value = user.RememberMeHash;
            }
        }
        public static void DeleteCookie()
        {
            HttpCookie cookie = new HttpCookie("rememberMe");
            cookie.Expires=DateTime.Now.AddMinutes(-10);
            HttpContext.Current.Response.Cookies.Set(cookie);

            UsersServices usersServices = new UsersServices();
            User user = usersServices.GetByID(AuthenticationService.LoggedUser.ID);
            user.RememberMeHash = null;
            user.RememberMeExpiryDate = null;

            usersServices.Save(user);
        }
    }
}