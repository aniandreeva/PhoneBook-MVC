using PhoneBook.Filters;
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
using System.IO;
using System.Web.Mvc.Expressions;
using AutoMapper;

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

            model.Contacts = contactsServises.GetAll().Where(c => c.UserID == AuthenticationService.LoggedUser.ID).ToList();

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Contacts=model.Contacts.Where(c => (c.FirstName.ToLower() + c.LastName.ToLower()).Contains(model.Search.ToLower().Replace(" ", ""))).ToList();
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

            int pageSize = 5;
            if (model.PageSize!=0)
            {
                pageSize = model.PageSize;
            }

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
                model.CountryID = int.Parse(contactsServises.GetSelectedCountries().FirstOrDefault().Value);
            }
            else
            {

                contact = contactsServises.GetByID(id.Value);
                if (contact == null)
                {
                    return this.RedirectToAction(c => c.List());
                }

                model.CountryID = contact.City.CountryID;
            }

            Mapper.Map(contact, model);

            model.Countries = contactsServises.GetSelectedCountries();
            model.Cities = contactsServises.GetCitiesByCountryID(model.CountryID);
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
            if (model.ID == 0)
            {
                contact = new Contact();
            }
            else
            {
                contact = contactsServises.GetByID(model.ID);
                if (contact == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
            {
                if (String.IsNullOrEmpty(Path.GetExtension(model.ImageUpload.FileName)) || !Path.GetExtension(model.ImageUpload.FileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(String.Empty, "Wrong Image Format!");
                }
                else
                {
                    string uploadDir = Server.MapPath("~/Uploads/");
                    model.ImagePath = model.ImageUpload.FileName;
                    model.ImageUpload.SaveAs(uploadDir + model.ImagePath);
                }
            }

            if (!ModelState.IsValid)
            {
                model.Countries = contactsServises.GetSelectedCountries();
                model.Cities = contactsServises.GetCitiesByCountryID(model.CountryID);
                model.Groups = contactsServises.GetSelectedGroups(contact.Groups, model.SelectedGroups);
                return View(model);
            }

            Mapper.Map(model, contact);
            contact.UserID = AuthenticationService.LoggedUser.ID;

            contactsServises.UpdateContactGroups(contact, model.SelectedGroups);
            contactsServises.Save(contact);

            return this.RedirectToAction(c => c.List());
        }

        public ActionResult Delete(int? id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ContactsServices contactsServices = new ContactsServices(unitOfWork);
            if (id.HasValue)
            {
                contactsServices.GetByID(id.Value).Groups.Clear();
                contactsServices.Delete(id.Value);
            }
            return this.RedirectToAction(c => c.List());
        }

        public JsonResult DeleteImage(int contactId)
        {
            ContactsServices contactsServices = new ContactsServices();

            Contact contact = contactsServices.GetByID(contactId);
            contact.ImagePath = "default.jpg";

            contactsServices.Save(contact);

            return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(int countryId)
        {
            ContactsServices contactsServices = new ContactsServices();

            List<SelectListItem> cities = contactsServices.GetCitiesByCountryID(countryId).ToList();

            return Json(cities.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveLocations(object[] locs)
        {
            return Json(new object[] { }, JsonRequestBehavior.AllowGet);
        }
    }
}