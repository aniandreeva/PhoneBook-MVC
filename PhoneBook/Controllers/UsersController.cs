using PhoneBook.Models;
using PhoneBook.Repositories;
using PhoneBook.Services.ModelsServices;
using PhoneBook.ViewModels.Users;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class UsersController : BaseController
    {
        public ActionResult List()
        {
            UsersServices usersServices = new UsersServices();
            UsersListVM model = new UsersListVM();
            TryUpdateModel(model);

            model.Users = usersServices.GetAll();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Users = model.Users.Where(u => u.Username.ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch (model.SortOrder)
            {
                case "fname_desc":
                    model.Users = model.Users.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "lanme_asc":
                    model.Users = model.Users.OrderBy(u => u.LastName).ToList();
                    break;
                    case"lname_desc":
                    model.Users = model.Users.OrderByDescending(u => u.LastName).ToList();
                    break;
                case "fname_asc":
                default:
                    model.Users = model.Users.OrderBy(u => u.FirstName).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = model.Page??1;
            model.PagedUsers = model.Users.ToPagedList(pageNumber, pageSize);

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            UsersServices usersServices = new UsersServices();
            UsersEditVM model = new UsersEditVM();

            User user;

            if (!id.HasValue)
            {
                user = new User();
            }
            else
            {
                user = usersServices.GetByID(id.Value);
                if (user==null)
                {
                    return RedirectToAction("List");
                }
            }
            model.ID = user.ID;
            model.Username = user.Username;
            model.Password = user.Password;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            UsersServices usersServices = new UsersServices();
            UsersEditVM model = new UsersEditVM();
            TryUpdateModel(model);

            User user;

            if (model.ID == 0)
            {
                user = new User();
            }
            else
            {
                user = usersServices.GetByID(model.ID);
                if (user == null)
                {
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.ID = model.ID;
            user.Username = model.Username;
            user.Password = model.Password;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            usersServices.Save(user);

            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            UsersServices usersServices = new UsersServices();

            if (id.HasValue)
            {
                usersServices.Delete(id.Value);
            }
            return RedirectToAction("List");
        }
    }
}