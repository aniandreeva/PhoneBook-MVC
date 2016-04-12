using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services.ModelsServices
{
    public class PhonesServices:BaseService<Phone>
    {
        public PhonesServices():base(){}

        public PhonesServices(UnitOfWork unitOfWork):base(unitOfWork){ }

        public int GetContactID(int id)
        {
            return GetByID(id).ContactID;
        }
        public Contact GetContact(int id)
        {
            return new ContactsRepository().GetByID(id);
        }
    }
}