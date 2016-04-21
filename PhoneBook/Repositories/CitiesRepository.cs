using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Repositories
{
    public class CitiesRepository : BaseRepository<City>
    {
        public CitiesRepository() : base() { }

        public CitiesRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}