using AutoMapper;
using PhoneBook.Models;
using PhoneBook.ViewModels.Contacts;
using PhoneBook.ViewModels.Phones;
using PhoneBook.ViewModels.Groups;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.ViewModels.Users;

namespace PhoneBook.Mappings
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile() : base(nameof(ModelToViewModelMappingProfile)) { }

        protected override void Configure()
        {
            Mapper.CreateMap<Contact, ContactsEditVM>()
                .ForMember(x => x.Countries, opt => opt.Ignore())
                .ForMember(x => x.Cities, opt => opt.Ignore())
                .ForMember(x => x.Groups, opt => opt.Ignore())
                .ForMember(x => x.CountryID, opt => opt.MapFrom(y => y.City.CountryID));

            Mapper.CreateMap<Phone, PhonesEditVM>();

            Mapper.CreateMap<Group, GroupsEditVM>();

            Mapper.CreateMap<User, UsersEditVM>();

        }


    }
}