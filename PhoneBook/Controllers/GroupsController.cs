using AutoMapper;
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
using System.Web.Mvc.Expressions;

namespace PhoneBook.Controllers
{
    public class GroupsController : BaseController
    {
        public ActionResult List()
        {
            GroupsServices groupsServices = new GroupsServices();
            GroupsListVM model = new GroupsListVM();
            TryUpdateModel(model);

            model.Groups = new Dictionary<Group, List<SelectListItem>>();

            foreach (var group in groupsServices.GetAll().Where(g => g.UserID == AuthenticationService.LoggedUser.ID))
            {
                List<SelectListItem> contacts = groupsServices.GetSelectedContacts(group).ToList();
                model.Groups.Add(group, contacts);
            }

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Groups = model.Groups.Where(g => g.Key.Name.ToLower().Contains(model.Search.ToLower())).ToDictionary(v => v.Key, v => v.Value);
            }

            switch (model.SortOrder)
            {
                case "name_desc":
                    model.Groups = model.Groups.OrderByDescending(g => g.Key.Name).ToDictionary(v => v.Key, v => v.Value);
                    break;
                case "name_asc":
                default:
                    model.Groups = model.Groups.OrderBy(g => g.Key.Name).ToDictionary(v => v.Key, v => v.Value);
                    break;
            }

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
                if (group == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            Mapper.Map(group, model);

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
            if (model.ID == 0)
            {
                group = new Group();
            }
            else
            {
                group = groupsServices.GetByID(model.ID);
                if (group == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Mapper.Map(model, group);
            group.UserID = AuthenticationService.LoggedUser.ID;

            groupsServices.Save(group);

            return this.RedirectToAction(c => c.List());
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

            return this.RedirectToAction(c => c.List());
        }

        public JsonResult Add(int[] contactsIds, int groupId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            GroupsServices groupsServices = new GroupsServices(unitOfWork);

            Group group = groupsServices.GetByID(groupId);
            group.Contacts.Clear();
            group.Contacts = new List<Contact>();

            if (contactsIds == null)
            {
                contactsIds = new int[0];
            }

            foreach (var id in contactsIds)
            {
                Contact contact = new ContactsServices(unitOfWork).GetByID(id);

                group.Contacts.Add(contact);
            }

            groupsServices.Save(group);

            var contacts = group.Contacts.Select(c => new
            {
                id = c.ID,
                firstName = c.FirstName,
                lastName = c.LastName
            });

            return Json(contacts, JsonRequestBehavior.AllowGet);
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