using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Repositories;

namespace PhoneBook.Repositories
{
    public class ContactsRepository:BaseRepository<Contact>
    {
        public ContactsRepository() : base() { }

        public ContactsRepository(UnitOfWork unitOfWork):base(unitOfWork){}
    }
}