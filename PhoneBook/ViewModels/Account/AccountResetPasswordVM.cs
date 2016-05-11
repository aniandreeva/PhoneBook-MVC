using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Account
{
    public class AccountResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}