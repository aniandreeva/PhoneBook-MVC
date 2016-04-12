using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Repositories
{
    public class GroupsRepository:BaseRepository<Group>
    {
        public GroupsRepository():base(){ }

        public GroupsRepository(UnitOfWork unitOfWork):base(unitOfWork){}
    }
}