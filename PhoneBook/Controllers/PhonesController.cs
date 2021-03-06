﻿using PhoneBook.Models;
using PhoneBook.Repositories;
using PhoneBook.Services;
using PhoneBook.Services.ModelsServices;
using PhoneBook.ViewModels.Phones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc.Expressions;
using AutoMapper;

namespace PhoneBook.Controllers
{
    public class PhonesController : BaseController
    {
        public ActionResult List()
        {
            PhonesServices phonesServises = new PhonesServices();
            PhonesListVM model = new PhonesListVM();
            TryUpdateModel(model);

            if (!model.ContactID.HasValue || phonesServises.GetContact(model.ContactID.Value) == null)
            {
                return this.RedirectToAction<ContactsController>(c => c.List());
            }
            
            model.Phones = phonesServises.GetAll().Where(c => c.ContactID == model.ContactID.Value).ToList();

            model.Contact = phonesServises.GetContact(model.ContactID.Value);

            int pageSize = 2;
            if (model.PageSize!=0)
            {
                pageSize = model.PageSize;
            }

            int pageNumber = model.Page ?? 1;
            model.PagedPhons = model.Phones.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            PhonesServices phonesServises = new PhonesServices();
            PhonesEditVM model = new PhonesEditVM();
            TryUpdateModel(model);

            Phone phone;
            if (!id.HasValue)
            {
                phone = new Phone();
            }
            else
            {
                phone = phonesServises.GetByID(id.Value);
                if (phone == null)
                {
                    if (phonesServises.GetContact(model.ContactID) == null)
                    {
                        return this.RedirectToAction<ContactsController>(c => c.List());
                    }
                }

                model.ContactID = phone.ContactID;
            }

            Mapper.Map(phone, model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            PhonesServices phonesServises = new PhonesServices();
            PhonesEditVM model = new PhonesEditVM();
            TryUpdateModel(model);

            if (phonesServises.GetContact(model.ContactID) == null)
            {
                return this.RedirectToAction<ContactsController>(c => c.List());
            }

            Phone phone;
            if (model.ID == 0)
            {
                phone = new Phone();
            }
            else
            {
                phone = phonesServises.GetByID(model.ID);
                if (phone == null)
                {
                    return this.RedirectToAction(c => c.List(), new { ContactID = phone.ContactID });
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Mapper.Map(model, phone);

            phonesServises.Save(phone);

            return this.RedirectToAction(c => c.List(), new { ContactID = phone.ContactID });
        }
        public ActionResult Delete(int? id)
        {
            PhonesServices phonesServises = new PhonesServices();
            int contactId = phonesServises.GetContactID(id.Value);

            if (id.HasValue)
            {
                phonesServises.Delete(id.Value);
            }

            return this.RedirectToAction(c => c.List(), new { ContactID = contactId });
        }
    }
}