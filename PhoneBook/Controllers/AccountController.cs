using PhoneBook.Filters;
using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.Services.ModelsServices;
using PhoneBook.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace PhoneBook.Controllers
{
    public class AccountController : Controller
    {
        [AuthorizeAccessFilter]
        public ActionResult Login(string RedirectUrl)
        {
            AccountLoginVM model = new AccountLoginVM();
            model.RedirectUrl = RedirectUrl;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccessFilter]
        public ActionResult Login()
        {
            AccountLoginVM model = new AccountLoginVM();
            TryUpdateModel(model);

            AuthenticationService.AuthenticateUser(model.Username, model.Password);

            if (AuthenticationService.LoggedUser != null)
            {
                if (!String.IsNullOrEmpty(model.RedirectUrl))
                {
                    return Redirect(model.RedirectUrl);
                }
                
                return this.RedirectToAction<ContactsController>(c => c.List());
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Invalid Username or Password");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationService.Logout();
            return this.RedirectToAction(c => c.Login());
        }

        [AuthorizeAccessFilter]
        public ActionResult Registration(string redirectUrl)
        {
            AccountRegistrationVM model = new AccountRegistrationVM();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccessFilter]
        public ActionResult Registration()
        {
            UsersServices usersServices = new UsersServices();
            AccountRegistrationVM model = new AccountRegistrationVM();
            TryUpdateModel(model);

            User user;

            if (model.ID == 0)
            {
                user = new Models.User();
            }
            else
            {
                return this.RedirectToAction(c => c.Login());
            }

            if (usersServices.IsUserExists(model))
            {
                ModelState.AddModelError(String.Empty, "This email or username already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.ID = model.ID;
            user.Username = model.Username;
            user.Password = Guid.NewGuid().ToString();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            usersServices.Save(user);

            EmailService.SendEmail(user);

            return View("WaitForConfirmation");
        }
        [AuthorizeAccessFilter]
        public ActionResult Confirm(int userID, string key)
        {
            AccountConfirmVM model = new AccountConfirmVM();

            return View(model);
        }

        [HttpPost]
        [AuthorizeAccessFilter]
        public ActionResult Confirm()
        {
            UsersServices usersServices = new UsersServices();
            AccountConfirmVM model = new AccountConfirmVM();
            TryUpdateModel(model);

            User user;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            user = usersServices.GetByID(model.UserID);
            user.Password = model.Password;

            usersServices.Save(user);

            return this.RedirectToAction(c => c.Login());
        }
   }
}