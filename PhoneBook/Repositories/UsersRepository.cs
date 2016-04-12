using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Repositories
{
    public class UsersRepository:BaseRepository<User>
    {
        public UsersRepository() : base() { }

        public UsersRepository(UnitOfWork unitOfWork): base(unitOfWork){ }
    }
}