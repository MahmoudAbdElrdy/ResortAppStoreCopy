
using AutoMapper;
using Common.Enums;
using Common.Infrastructures;
using Common.Mapper;
using Configuration.Entities;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResortAppStore.Services.ERPBackEnd.Application.Auth.Users.Dto
{
    public class UserDto : IHaveCustomMapping
    {
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
        public long[]? Companies { get; set; }
        public long[]? Branches { get; set; }
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
    public class CompaniesUserDto
    {
        public long CompanyId { get; set; }
        public long[]? Branches { get; set; }
    }
    public class GetAllUsersWithPaginationCommand : Paging
    {
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
        //public long[]? Companies { get; set; }
        //public long[]? Branches { get; set; }
        public List<CompaniesUserDto> CompaniesUserDtos { get; set; }
        public List<BranchPermissionDto> branchPermissions { get; set; }
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
        public List<CompaniesUserDto> CompaniesUserDtos { get; set; }
        public List<BranchPermissionDto> branchPermissions { get; set; }
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
    public class DeleteListUserCommand
    {
        public string[] Ids { get; set; }
    }
}