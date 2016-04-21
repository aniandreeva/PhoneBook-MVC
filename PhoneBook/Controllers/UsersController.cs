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
using System.Web.Mvc.Expressions;
using AutoMapper;

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
                case "lname_desc":
                    model.Users = model.Users.OrderByDescending(u => u.LastName).ToList();
                    break;
                case "fname_asc":
                default:
                    model.Users = model.Users.OrderBy(u => u.FirstName).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = model.Page ?? 1;
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
                if (user == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            Mapper.Map(user, model);

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
                    return this.RedirectToAction(c => c.List());
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Mapper.Map(model, user);

            usersServices.Save(user);

            return this.RedirectToAction(c => c.List());
        }

        public ActionResult Delete(int? id)
        {
            UsersServices usersServices = new UsersServices();

            if (id.HasValue)
            {
                usersServices.Delete(id.Value);
            }
            return this.RedirectToAction(c => c.List());
        }
    }
}