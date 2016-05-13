using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Services.ModelsServices
{
    public class GroupsServices : BaseService<Group>
    {
        public GroupsServices() : base() { }

        public GroupsServices(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public IEnumerable<SelectListItem> GetSelectedContacts(Group group)
        {
            List<string> selectedIds = group.Contacts.Select(c => c.ID.ToString()).ToList();

            return new ContactsRepository().GetAll().Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.ID.ToString(),
                Selected = selectedIds.Contains(c.ID.ToString())
            });
        }



    }
}