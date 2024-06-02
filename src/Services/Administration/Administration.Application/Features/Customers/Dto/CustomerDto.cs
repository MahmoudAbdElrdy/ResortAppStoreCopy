using AutoMapper;
using Common.Enums;
using Common.Infrastructures;
using Common.Mapper;
using Common.ValidationAttributes;
using MediatR;
using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Features.Customers.Dto
{
    public class CustomerDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public long CountryId { get; set; }
        public long? BusinessId { get; set; }
        public string? CompanySize { get; set; }
        public bool? MultiCompanies { get; set; }
        public bool? MultiBranches { get; set; }
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
        public string? VerifyCode { get; set; }
        public string? ServerName { get; set; }
        public string? DatabaseName { get; set; }
        public string? SubDomain { get; set; }
        public List<CustomerSubscriptionDto> SubscriptionDto { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Customer, CustomerDto>()
                .ReverseMap();

        }
    }
    public class CreateCustomerCommand
    {
        public long Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public long CountryId { get; set; }
        public long? BusinessId { get; set; }
        public string? CompanySize { get; set; }
        public bool? MultiCompanies { get; set; }
        public bool? MultiBranches { get; set; }
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
    }
    public class DeleteCustomerCommand
    {
        public long Id { get; set; }
    }
    public class DeleteListCustomerCommand
    {
        public long[] Ids { get; set; }
    }
    public class EditCustomerCommand
    {

        public long Id { get; set; }
        [ValidationLocalizedData("NameEn")]
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public long CountryId { get; set; }
        public long? BusinessId { get; set; }
        public string? CompanySize { get; set; }
        public bool? MultiCompanies { get; set; }
        public bool? MultiBranches { get; set; }
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
        public string? Applications { get; set; }
    }
    public class VerifyCodeCommand
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
    public class GetAllCustomersWithPaginationCommand : Paging
    {
    }
    public class GetByCustomerId
    {
        public long Id { get; set; }
    }
}