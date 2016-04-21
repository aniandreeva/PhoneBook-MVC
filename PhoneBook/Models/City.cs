using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class City : BaseModel
    {
        public int CountryID { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual List<Contact> Contacts { get; set; }
    }
}