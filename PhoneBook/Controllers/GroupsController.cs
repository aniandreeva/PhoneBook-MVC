using PagedList;
using PagedList.Mvc;
using PhoneBook.Models;
using PhoneBook.Repositories;
using PhoneBook.Services;
using PhoneBook.Services.ModelsServices;
using PhoneBook.ViewModels.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class GroupsController : BaseController
    {
        public ActionResult List()
        {
            GroupsServices groupsServices = new GroupsServices();
            GroupsListVM model = new GroupsListVM();
            TryUpdateModel(model);

            model.Groups = groupsServices.GetAll().Where(g => g.UserID == AuthenticationService.LoggedUser.ID).ToList();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Groups = model.Groups.Where(g => g.Name.ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch (model.SortOrder)
            {
                case "name_desc":
                    model.Groups = model.Groups.OrderByDescending(g => g.Name).ToList();
                    break;
                case "name_asc":
                default:
                    model.Groups = model.Groups.OrderBy(g => g.Name).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = model.Page ?? 1;
            model.PagedGroups = model.Groups.ToPagedList(pageNumber, pageSize);

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            GroupsServices groupServices = new GroupsServices();
            GroupsEditVM model = new GroupsEditVM();

            Group group;
            if (!id.HasValue)
            {
                group = new Group();
            }
            else
            {
                group = groupServices.GetByID(id.Value);
                if (group==null)
                {
                    return RedirectToAction("List");
                }
            }
            model.ID = group.ID;
            model.Name = group.Name;
            model.UserID = group.UserID;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            GroupsServices groupsServices = new GroupsServices();
            GroupsEditVM model = new GroupsEditVM();
            TryUpdateModel(model);

            Group group;

            if (model.ID==0)
            {
                group = new Group();
            }
            else
            {
                group = groupsServices.GetByID(model.ID);
                if (group==null)
                {
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            group.ID = model.ID;
            group.Name = model.Name;
            group.UserID = AuthenticationService.LoggedUser.ID;

            groupsServices.Save(group);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            GroupsServices groupsServices = new GroupsServices(unitOfWork);
            if (id.HasValue)
            {
                groupsServices.GetByID(id.Value).Contacts.Clear();
                groupsServices.Delete(id.Value);
            }

            return RedirectToAction("List");
        }

        public JsonResult Remove(int contactId, int groupId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            GroupsServices groupsServices = new GroupsServices(unitOfWork);

            Group group = groupsServices.GetByID(groupId);

            group.Contacts = group.Contacts.Where(c => c.ID != contactId).ToList();
            groupsServices.Save(group);

            return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
        }
    }
}