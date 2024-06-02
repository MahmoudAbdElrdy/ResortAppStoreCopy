using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto;

using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Dto
{
    public class ShiftMasterDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public long Code { get; set; }
        public virtual List<ShiftDetail>? ShiftDetails { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ShiftMasterDto, ShiftMaster>()
                .ReverseMap();
            configuration.CreateMap<ShiftMaster, ShiftMasterDto>().ReverseMap();

        }
    }
}
