using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services.ModelsServices
{
    public class CitiesServices:BaseService<City>
    {
        public CitiesServices() : base() { }

        public CitiesServices(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}