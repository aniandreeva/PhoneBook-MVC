using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PhoneBook.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToViewModelMappingProfile>();
                x.AddProfile<ViewModelToModelMappingProfile>();
            });
        }
    }
}