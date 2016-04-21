using PagedList;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Groups
{
    public class GroupsListVM : ListVM
    {
        public List<Group> Groups { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        public IPagedList<Group> PagedGroups { get; set; }
        public int? Page { get; set; }
    }
}