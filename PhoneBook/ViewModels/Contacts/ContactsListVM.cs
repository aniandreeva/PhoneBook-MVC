using PagedList;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Contacts
{
    public class ContactsListVM:ListVM
    {
        public List<Contact> Contacts { get; set; }

        public IPagedList<Contact> PagedContacts { get; set; }
        public int? Page { get; set; }
    }
}