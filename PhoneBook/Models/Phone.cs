using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class Phone:BaseModel
    {
        public int ContactID { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneTypeEnum PhoneType { get; set; }

        public virtual Contact Contacts { get; set; }
    }
    public enum PhoneTypeEnum { Home, Mobile, Work }
}