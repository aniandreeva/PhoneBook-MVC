using PagedList;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Phones
{
    public class PhonesListVM : ListVM
    {
        public List<Phone> Phones { get; set; }

        public int? ContactID { get; set; }
        public Contact Contact { get; set; }

        public IPagedList<Phone> PagedPhons { get; set; }
    }
}