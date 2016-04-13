using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.ViewModels.Contacts
{
    public class ContactsEditVM
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        [Required]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "First name has to contain only lettsrs and '-' !")]
        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "Last name has to contain only lettsrs and '-' !")]
        [StringLength(10, MinimumLength =2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[^_^&<>!@#%/?*()]+$", ErrorMessage ="Invalid address!")]
        public string Address { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }
        public string[] SelectedGroups { get; set; }
    }
}