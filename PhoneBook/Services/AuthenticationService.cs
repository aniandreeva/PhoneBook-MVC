using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services
{
    public class AuthenticationService
    {
        public User CurrentUser { get; set; }

        public void AuthenticateUser(string username, string password)
        {
            UsersRepository usersRep = new UsersRepository();
            CurrentUser = usersRep.GetAll().FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        //public static void Logout()
        //{
        //    CookieService.DeleteCookie();
        //    AuthenticateUser(null, null);
        //}

        public void AuthenticateUserByCookie(HttpCookie cookie)
        {
            UsersRepository usersRep = new UsersRepository();
            CurrentUser = usersRep.GetAll().FirstOrDefault(u => u.RememberMeHash == cookie.Value);
        }
    }
}