using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.CostCenters.Dto
{
    public class CostCenterDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        [MaxLength(60)]
        public string NameAr { get; set; }
        [MaxLength(60)]
        public string? NameEn { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
    
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public long? CompanyId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CostCenter, CostCenterDto>()
                          .ReverseMap();
        }
    }
 
    public class CostCenterTreeDto
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public IEnumerable<CostCenterTreeDto> children { get; set; }
        public bool expanded { get; set; }
        public long? CompanyId { get; set; }
    }
    public class CostCenterSearch : Paging
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }

    }
    public class GetAllCostCenterTreeCommand
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }
    }
    public class GetLastCode
    {
        public long? ParentId { get; set; }
    }
    public class DeleteCostCenterCommand
    {
        public long Id { get; set; }
    }
}
