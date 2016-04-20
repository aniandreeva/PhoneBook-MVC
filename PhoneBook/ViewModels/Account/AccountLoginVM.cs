﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string RedirectUrl { get; set; }

        [Display(Name = "Remember Me")]
        public bool IsRemembered { get; set; }
    }
}