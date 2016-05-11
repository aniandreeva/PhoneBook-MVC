using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.Groups
{
    public class GroupsEditVM
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^([A-z 0-9.]+)$", ErrorMessage = "Username has to contain only lettsrs and digits! ")]
        public string Name { get; set; }

        public int UserID { get; set; }

    }
}