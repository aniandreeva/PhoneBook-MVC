using AutoMapper;
using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.ViewModels.Account;
using PhoneBook.ViewModels.Contacts;
using PhoneBook.ViewModels.Groups;
using PhoneBook.ViewModels.Phones;
using PhoneBook.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Mappings
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile() : base(nameof(ViewModelToModelMappingProfile)) { }

        protected override void Configure()
        {
            Mapper.CreateMap<ContactsEditVM, Contact>()
                .ForMember(x => x.UserID, opt => opt.Ignore())
                .ForMember(x => x.Phones, opt => opt.Ignore());

            Mapper.CreateMap<PhonesEditVM, Phone>();

            Mapper.CreateMap<GroupsEditVM, Group>()
                .ForMember(x => x.UserID, opt => opt.Ignore());

            Mapper.CreateMap<UsersEditVM, User>()
                .ForMember(x => x.RememberMeHash, opt => opt.Ignore())
                .ForMember(x => x.RememberMeExpiryDate, opt => opt.Ignore());

            Mapper.CreateMap<AccountRegistrationVM, User>();
        }
    }
}