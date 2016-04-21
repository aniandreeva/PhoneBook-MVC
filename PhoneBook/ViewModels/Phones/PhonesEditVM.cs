using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.ViewModels.Phones
{
    public class PhonesEditVM
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"([0-9]+)$", ErrorMessage = "The Phone Number must only contains numbers!")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public int ContactID { get; set; }

        [Display(Name = "Phone Type")]
        public PhoneTypeEnum PhoneType { get; set; }
    }
}