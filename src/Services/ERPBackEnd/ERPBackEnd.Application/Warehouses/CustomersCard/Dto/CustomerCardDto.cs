using AutoMapper;
using Common.Mapper;
using System;
using System.ComponentModel.DataAnnotations;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Dto
{
    public class CustomerCardDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CustomerGroupId { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? CustomerType { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ResponsiblePerson { get; set; }
        public string? AccountId { get; set; }
           
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public long? CountryId { get; set; }
        public string? TaxNumber { get; set; }
        public string? IdNumber { get; set; }

        public string? PostalCode { get; set; }
        public string? MailBox { get; set; }
        public double ? CreditLimit { get; set; }
        public double? InitialBalance { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Region { get; set; }
        public string? Street { get; set; }
        public string? AdditionalStreet { get; set; }
        public string? BuildingNo { get; set; }
        public string? AdditionalBuildingNo { get; set; }
        public string? CountryCode { get; set; }
        public string? RegisterationType { get; set; }
        public string? ZakaTaxCustomsAuthorityName { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Guid { get; set; }
        public long? PriceListId { get; set; }

        public string AccountName { get; set; }

        public string? Governorate { get; set; }
        public string? RegionCity { get; set; }

        public string? Floor { get; set; }

        public string? Room { get; set; }

        public string? LandMark { get; set; }

        public string? AdditionalInformation { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CustomerCard, CustomerCardDto>()
                          .ForMember(dest=> dest.AccountName , sec=> sec.MapFrom(x=>x.Account.NameAr));

            configuration.CreateMap<CustomerCardDto, CustomerCard>().ForMember(dest => dest.Account , src => src.Ignore());
        }
    }
}