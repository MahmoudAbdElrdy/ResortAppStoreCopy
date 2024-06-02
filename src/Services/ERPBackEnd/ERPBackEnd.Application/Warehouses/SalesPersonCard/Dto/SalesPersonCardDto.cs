using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class SalesPersonCardDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ResponsiblePerson { get; set; }
        public string AccountId { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public long? CountryId { get; set; }
        public bool? IsActive { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.SalesPersonCard, SalesPersonCardDto>()
                          .ReverseMap();
        }
    }
}
