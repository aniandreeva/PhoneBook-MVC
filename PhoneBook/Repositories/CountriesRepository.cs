using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Repositories
{
    public class CountriesRepository: BaseRepository<Country>
    {
        public CountriesRepository() : base() { }

        public CountriesRepository(UnitOfWork unitOfWork):base(unitOfWork){ }
    }
}