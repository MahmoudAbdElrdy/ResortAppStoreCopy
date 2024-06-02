using AutoMapper;
using Common.Mapper;
using System;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Dto
{
    public class ManualInventoryApprovalDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long FiscalPeriodId { get; set; }

        public long WarehouseListId { get; set; }
        public DateTime Date { get; set; }
        public long? InputBillTypeId { get; set; }
        public long? OutputBillTypeId { get; set; }
        public Guid? Guid { get; set; }


        public bool? IsActive { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ManualInventoryApproval, ManualInventoryApprovalDto>()
                          .ReverseMap();
        }
    }
}