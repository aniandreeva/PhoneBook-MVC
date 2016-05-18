using PagedList;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Users
{
    public class UsersListVM : ListVM
    {
        public List<User> Users { get; set; }

        public IPagedList<User> PagedUsers { get; set; }
    }
}