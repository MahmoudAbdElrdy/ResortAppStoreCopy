using AutoMapper;
using Common.Mapper;
using Warehouses.Entities;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Dto
{
    public class SupplierCardDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long SupplierGroupId { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ResponsiblePerson { get; set; }
        public string? AccountId { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public long? CountryId { get; set; }

        public string? TaxNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? MailBox { get; set; }
        public double? CreditLimit { get; set; }
        public double? InitialBalance { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Guid { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SupplierCard, SupplierCardDto>()
                          .ReverseMap();
        }
    }
}