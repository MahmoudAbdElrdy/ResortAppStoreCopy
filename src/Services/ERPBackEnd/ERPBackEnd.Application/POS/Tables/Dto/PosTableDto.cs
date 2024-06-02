using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Tables.Dto
{
    public class POSTableDto : IHaveCustomMapping
    {

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public long FloorId { get; set; }
        public string Color { get; set; }
        public int NumberOfSeats { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Guid { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSTable, POSTableDto>().ReverseMap();

        }
    }

}

