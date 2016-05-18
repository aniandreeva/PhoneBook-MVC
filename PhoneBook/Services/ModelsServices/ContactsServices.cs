using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Services.ModelsServices
{
    public class ContactsServices : BaseService<Contact>
    {
        public ContactsServices() : base() { }

        public ContactsServices(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public User GetUser(int id)
        {
            return new UsersRepository().GetByID(id);
        }

        public IEnumerable<SelectListItem> GetSelectedCountries()
        {
            return new CountriesRepository().GetAll().OrderBy(c => c.Name).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
        }

        //public IEnumerable<SelectListItem> GetSelectedCities()
        //{
        //    return new CitiesRepository().GetAll().OrderBy(c => c.Name).Select(c => new SelectListItem
        //    {
        //        Text = c.Name,
        //        Value = c.ID.ToString()
        //    });
        //}

        public IEnumerable<SelectListItem> GetSelectedGroups(List<Group> groups, string[] selectedGroups = null)
        {
            if (groups == null)
            {
                groups = new List<Group>();
            }

            List<string> selectedIds = groups.Select(g => g.ID.ToString()).ToList();

            if (selectedGroups != null)
            {
                selectedIds.AddRange(selectedGroups);
            }

            return new GroupsRepository().GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.ID.ToString(),
                Selected = selectedIds.Contains(g.ID.ToString())
            });
        }

        public void UpdateContactGroups(Contact contact, string[] selectedIds)
        {
            if (selectedIds == null)
            {
                selectedIds = new string[0];
            }

            if (contact.Groups == null)
            {
                contact.Groups = new List<Group>();
            }

            contact.Groups.Clear();
            foreach (Group group in new GroupsRepository(base.unitOfWork).GetAll())
            {
                if (selectedIds.Contains(group.ID.ToString()))
                {
                    contact.Groups.Add(group);
                }
            }
        }

        public IEnumerable<SelectListItem> GetCitiesByCountryID(int countryId)
        {
            return new CitiesRepository().GetAll().OrderBy(c=>c.Name).Where(c => c.CountryID == countryId).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
        }
    }
}