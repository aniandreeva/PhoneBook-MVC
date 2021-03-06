﻿using PagedList;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.ViewModels.Groups
{
    public class GroupsListVM : ListVM
    {
        public Dictionary<Group, List<SelectListItem>> Groups { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        //public IPagedList<Group> PagedGroups { get; set; }
    }
}