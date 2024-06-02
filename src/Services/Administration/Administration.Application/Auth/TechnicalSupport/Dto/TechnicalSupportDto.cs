using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Enums;
using Common.Mapper;
using MediatR;
using System;
using System.Linq;
using Web.Profiler;

namespace ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto
{
    public class TechnicalSupportDto : IHaveCustomMapping
    {
        public string UserName { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; } 
        public bool IsSubscriped { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, TechnicalSupportDto>()
                  .ForMember(x => x.UserType, expression => expression.MapFrom(x => Enum.GetName(typeof(UserType), x.UserType)))
                  .ForMember(x => x.UserId, expression => expression.MapFrom(x =>x.Id))
                //.ForMember(x => x.Roles, expression => expression.MapFrom(x => x.UserRoles.Select(c=>c.Role.Name)))

                .ReverseMap();

        }
    }
    public class AuthorizedUserDTO
    {
        public AuthorizedUserDTO()
        {

        }


        public TechnicalSupportDto User { get; set; }
        public string Token { get; set; }

    }
    public class TechnicalSupportCommand
    {
        public string UserName { get; set; }

        public string FullName { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public UserType UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}