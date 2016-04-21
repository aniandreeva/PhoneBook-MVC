using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Repositories
{
    public class PhonesRepository : BaseRepository<Phone>
    {
        public PhonesRepository() : base() { }

        public PhonesRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}