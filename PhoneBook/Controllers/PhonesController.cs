using PhoneBook.Models;
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

namespace PhoneBook.Controllers
{
    public class PhonesController : BaseController
    {
        public ActionResult List()
        {
            PhonesServices phonesServises = new PhonesServices();
            PhonesListVM model = new PhonesListVM();
            TryUpdateModel(model);

            model.Phones = phonesServises.GetAll().Where(c => c.ContactID == model.ContactID.Value).ToList();
            model.Contact = phonesServises.GetContact(model.ContactID.Value);

            int pageSize = 2;
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
                if (phone==null)
                {
                    if (phonesServises.GetContact(model.ContactID) == null)
                    {
                        return RedirectToAction("List", "Contacts");
                    }
                }

                model.ContactID = phone.ContactID;
            }

            model.ID = phone.ID;
            model.PhoneNumber = phone.PhoneNumber;
            model.PhoneType = phone.PhoneType;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            PhonesServices phonesServises = new PhonesServices();
            PhonesEditVM model = new PhonesEditVM();
            TryUpdateModel(model);
            if (phonesServises.GetContact(model.ContactID)==null)
            {
                return RedirectToAction("List", "Contacts");
            }

            Phone phone;
            if (model.ID==0)
            {
                phone = new Phone();
            }
            else
            {
                phone = phonesServises.GetByID(model.ID);
                if (phone==null)
                {
                    return RedirectToAction("List", new { ContactID = phone.ContactID });
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            phone.ID = model.ID;
            phone.PhoneNumber = model.PhoneNumber;
            phone.ContactID = model.ContactID;
            phone.PhoneType = model.PhoneType;

            phonesServises.Save(phone);

            return RedirectToAction("List", new { ContactID = model.ContactID });
        }
        public ActionResult Delete(int? id)
        {
            PhonesServices phonesServises = new PhonesServices();
            int contactId = phonesServises.GetContactID(id.Value);
            if (id.HasValue)
            {
                phonesServises.Delete(id.Value);
            }

            return RedirectToAction("List", new {ContactId= contactId });
        }
    }
}