using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Account
{
    public class AccountRegistrationVM
    {
        public int ID { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Usermane maximum length is 80 !")]
        [RegularExpression(@"^([A-z0-9.-_]+)$", ErrorMessage = "Username has to contain only lettsrs and '- . _'! ")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "First name has to contain only lettsrs and '-' !")]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "Last name has to contain only lettsrs and '-' !")]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string RedirectUrl { get; set; }
    }
}