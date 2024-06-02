using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.StoresCard.Dto
{
    public class StoreCardDto : IHaveCustomMapping
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
        public string? Storekeeper { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<StoreCard, StoreCardDto>()
                          .ReverseMap();
        }
    }
    public class StoreCardTreeDto
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public string? Storekeeper { get; set; }
        public string? Address { get; set; }
        public IEnumerable<StoreCardTreeDto> children { get; set; }
        public bool expanded { get; set; }
    }

    public class GetAllStoreCardTree
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }
    }
    public class GetLastCode
    {
        public long? ParentId { get; set; }
    }
    public class DeleteStoreCard
    {
        public long Id { get; set; }
    }
}
