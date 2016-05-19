using PhoneBook.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PhoneBook.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutoMapperConfiguration.Configure();
            //LocationConfiguration.Configure();
        }
    }
}