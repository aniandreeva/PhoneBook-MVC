using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services.ModelsServices
{
    public class CountriesServices: BaseService<Country>
    {
        public CountriesServices() : base() { }

        public CountriesServices(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}