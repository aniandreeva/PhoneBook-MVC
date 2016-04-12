﻿using PhoneBook.Filters;
using PhoneBook.Models;
using PhoneBook.Repositories;
using PhoneBook.Services;
using PhoneBook.Services.ModelsServices;
using PhoneBook.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace PhoneBook.Controllers
{
    [AuthenticationFilter]
    public class ContactsController : BaseController
    {
        public ActionResult List()
        {
            ContactsServices contactsServises = new ContactsServices();
            ContactsListVM model = new ContactsListVM();
            TryUpdateModel(model);

            model.Contacts = contactsServises.GetAll().Where(c=>c.UserID==AuthenticationService.LoggedUser.ID).ToList();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Contacts = model.Contacts.Where(c => c.FirstName.ToLower().Contains(model.Search.ToLower()) || c.LastName.ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch (model.SortOrder)
            {
                case "fname_desc":
                    model.Contacts = model.Contacts.OrderByDescending(c => c.FirstName).ToList();
                    break;
                case "lname_asc":
                    model.Contacts = model.Contacts.OrderBy(c => c.LastName).ToList();
                    break;
                case "lname_desc":
                    model.Contacts = model.Contacts.OrderByDescending(c => c.LastName).ToList();
                    break;
                case "fname_asc":
                default:
                    model.Contacts = model.Contacts.OrderBy(c => c.FirstName).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = model.Page ?? 1;
            model.PagedContacts = model.Contacts.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            ContactsServices contactsServises = new ContactsServices();
            ContactsEditVM model = new ContactsEditVM();

            Contact contact;

            if (!id.HasValue)
            {
                contact = new Contact();
            }
            else
            {
                contact = contactsServises.GetByID(id.Value);
                if (contact==null)
                {
                    return RedirectToAction("List");
                }
            }

            model.ID = contact.ID;
            model.UserID = contact.UserID;
            model.FirstName = contact.FirstName;
            model.LastName = contact.LastName;
            model.Address = contact.Address;

            model.Groups = contactsServises.GetSelectedGroups(contact.Groups);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ContactsServices contactsServises = new ContactsServices(unitOfWork);
            ContactsEditVM model = new ContactsEditVM();
            TryUpdateModel(model);

            Contact contact;

            if (model.ID==0)
            {
                contact = new Contact();
            }
            else
            {
                contact = contactsServises.GetByID(model.ID);
                if (contact==null)
                {
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            contact.ID = model.ID;
            contact.UserID = AuthenticationService.LoggedUser.ID;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Address = model.Address;

            contactsServises.UpdateContactGroups(contact, model.SelectedGroups);
            contactsServises.Save(contact);
            
            return RedirectToAction("List");
        }



        public ActionResult Delete(int? id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ContactsServices contactsServices = new ContactsServices(unitOfWork);
            if (id.HasValue)
            {
                contactsServices.UpdateContactGroups(contactsServices.GetByID(id.Value), null);
                //contactsServices.GetByID(id.Value).Groups.Clear();
                contactsServices.Delete(id.Value);
            }
            return RedirectToAction("List");
        }
    }
}