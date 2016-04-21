using PhoneBook.Models;
using PhoneBook.Repositories;
using PhoneBook.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services.ModelsServices
{
    public class UsersServices : BaseService<User>
    {
        public UsersServices() : base() { }

        public UsersServices(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUserExists(AccountRegistrationVM model)
        {
            User verifyUser = GetAll().FirstOrDefault(u => u.Username.ToLower() == model.Username.ToLower() || u.Email == model.Email);

            if (verifyUser != null)
            {
                return true;
            }

            return false;
        }
    }
}