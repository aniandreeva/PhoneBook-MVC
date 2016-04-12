using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Users
{
    public class UsersEditVM
    {
        public int ID { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Usermane maximum length is 80 !")]
        [RegularExpression (@"^([A-z0-9.-_]+)$", ErrorMessage ="Username has to contain only lettsrs and '- . _'! ") ]
        public string Username { get; set; }

        [Required]
        [StringLength (20, MinimumLength =6, ErrorMessage ="Password minimum length is 6 and maximum length is 20")]
        [RegularExpression(@"^([A-z0-9]+)$", ErrorMessage = "Password has to contain only lettsrs and numbers!")]
        public string Password { get; set; }

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
    }
}