using PhoneBook.Models;
using PhoneBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Services.ModelsServices
{
    public class GroupsServices : BaseService<Group>
    {
        public GroupsServices() : base() { }

        public GroupsServices(UnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}