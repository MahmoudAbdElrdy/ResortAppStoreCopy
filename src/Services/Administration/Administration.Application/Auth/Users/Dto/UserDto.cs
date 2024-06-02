using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Enums;
using Common.Infrastructures;
using Common.Mapper;
using ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class UserDto: IHaveCustomMapping
    {
        public  string Id { get; set; }
        public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; } 
        public string? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; } 
        public string[]? Roles { get; set; }
        public bool? IsActive { get; set; }
        public string? FullName { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserDto>()
                  .ForMember(x => x.UserType, expression => expression.MapFrom(x => Enum.GetName(typeof(UserType), x.UserType)))
                 // .ForMember(x => x.Roles, expression => expression.MapFrom(x => x.UserRoles.Select(c=>c.Role.Id).ToList()))

                .ReverseMap();

        }
    }
    public class AuthorizedUserDTO
    {
        public AuthorizedUserDTO()
        {

        }


        public UserDto User { get; set; }
        public string Token { get; set; }

    }
    public class GetAllUsersWithPaginationCommand : Paging
    {
        public UserType? UserType { get; set; }
    }
    public class CreateUserCommand
    {
        //  public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string[]? Roles { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
       
    }
    public class EditUserCommand
    {
        //  public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public string? Id { get; set; }
        public string[]? Roles { get; set; }
        public string? Password { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
     
    }
    public class AddPasswordUserCommand
    {
        //   public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Email { get; set; }
    }
    public class ForgetPasswordUserCommand
    {
        public string Email { get; set; }
    }
    public class ChangePasswordCommand
    {
        //   public string OldPassword { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
    public class DeleteUserCommand
    {
        public string Id { get; set; }
    }
    public class GetById
    {
        public string Id { get; set; }
    }
    public class GetUserDetailsPackageById 
    {
        public int? Id { get; set; }
    }
    public class GetUserDetailsModuleByCode 
    {
        public long? Code { get; set; } 
    }
    public class DeleteListUserCommand
    {
        public string[] Ids { get; set; }
    }
    public class LoginCommand
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
    public class LogOutCommand
    {
        public string UserId { get; set; } 
       
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

    public class CreateUserClient 
    {
        //  public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string[]? Roles { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public List<UserDetailsPackageDto> UserDetailsPackageDtos { get; set; }
        public List<UserDetailsModuleDto> UserDetailsModuleDtos{ get; set; } 

    }
    public class UserOwnerDto : IHaveCustomMapping
    {
        //  public string? UserName { get; set; }
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public string? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; }
        public string[]? Roles { get; set; }
        public bool? IsActive { get; set; }
        public string? FullName { get; set; }
        public List<UserDetailsPackageDto> UserDetailsPackageDtos { get; set; } = new List<UserDetailsPackageDto>();
        public List<UserDetailsModuleDto> UserDetailsModuleDtos { get; set; } = new List<UserDetailsModuleDto>();
        public List<MergedUserDetailsDto> MergedUserDetailsDto { get; set; } = new List<MergedUserDetailsDto>();
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserOwnerDto>()
                  .ForMember(x => x.UserType, expression => expression.MapFrom(x => Enum.GetName(typeof(UserType), x.UserType)))
                // .ForMember(x => x.Roles, expression => expression.MapFrom(x => x.UserRoles.Select(c=>c.Role.Id).ToList()))

                .ReverseMap();

        }
    }
}
