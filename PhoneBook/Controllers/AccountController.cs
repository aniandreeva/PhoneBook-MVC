using AutoMapper;
using PhoneBook.Filters;
using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.Services.ModelsServices;
using PhoneBook.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            AuthenticationManager.AuthenticateUser(model.Username, model.Password);

            if (AuthenticationManager.LoggedUser != null)
            {
                if (model.IsRemembered)
                {
                    CookieService.CreateCookie();
                }
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
            AuthenticationManager.Logout();
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
                user = new User();
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

            Mapper.Map(model, user);
            user.Password = Guid.NewGuid().ToString();

            usersServices.Save(user);

            Task.Run(() => EmailService.SendEmail(user, ControllerContext));

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
            user = usersServices.GetByID(model.UserID);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "User noe exist");
            }

            if (user.ID == model.UserID && user.Password != model.Key)
            {
                Guid validKey;

                if (Guid.TryParse(model.Key, out validKey))
                {
                    return View("InactiveConfirmationLink");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.Password = model.Password;

            usersServices.Save(user);

            return this.RedirectToAction(c => c.Login());
        }

        public ActionResult ResetPassword()
        {
            AccountResetPasswordVM model = new AccountResetPasswordVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(AccountResetPasswordVM model)
        {
            UsersServices usersServises = new UsersServices();

            User user = usersServises.GetAll().FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "User nto exist");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Task.Run(() => EmailService.SendEmail(user, ControllerContext));

            return View("WaitForConfirmation");
        }
    }
}