
using AutoMapper;
using Common.Enums;
using Common.Mapper;
using Configuration.Entities;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Profiler;

namespace ResortAppStore.Services.ERPBackEnd.Application.Auth.UserLogin.Dto
{
    public class UserLoginDto : IHaveCustomMapping
    {
        public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? LoginCount { get; set; }
        // public string[]? Roles { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserLoginDto>()
                  .ForMember(x => x.UserType, expression => expression.MapFrom(x => Enum.GetName(typeof(UserType), x.UserType)))
                //.ForMember(x => x.Roles, expression => expression.MapFrom(x => x.UserRoles.Select(c=>c.Role.Name)))

                .ReverseMap();

        }
    }
    public class AuthorizedUserDTO
    {
        public AuthorizedUserDTO()
        {

        }


        public UserLoginDto User { get; set; }
        public string Token { get; set; }
        public List<CompanyDto> Companies { get; set; }
        public List<BranchDto> Branches { get; set; }
    }
    public class LoginCommand
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
    public class LoginCompanyCommand
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
    }
    public class UserLoginCommand
    {
        public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public UserType? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        //  public string[]? Roles { get; set; }
        public string? Password { get; set; }
    }
}