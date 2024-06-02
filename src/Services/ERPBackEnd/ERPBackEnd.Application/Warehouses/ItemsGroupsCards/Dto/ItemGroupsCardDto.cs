using AutoMapper;
using Common.Enums;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCard.Dto
{
    public class ItemGroupsCardDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        [MaxLength(60)]
        public string NameAr { get; set; }
        [MaxLength(60)]
        public string? NameEn { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
        public bool? IsBelongsToPOS { get; set; }
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public int? ItemType { get; set; }
        public int? CostCalculation { get; set; }
        public long? UnitId { get; set; } 
        public string? Notes { get; set; }
        public string? SalesAccountId { get; set; }
        public string? SalesReturnsAccountId { get; set; }
        public string? PurchasesAccountId { get; set; }
        public string? PurchasesReturnsAccountId { get; set; }
        public string? SalesCostAccountId { get; set; }
        public string? InventoryAccountId { get; set; }
        public long? DefaultStoreId { get; set; }
        public string? GPCCode { get; set; }




        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ItemGroupsCard, ItemGroupsCardDto>()
                 .ForMember(x => x.ItemType, expression => expression.MapFrom(x => Enum.GetValues(typeof(ItemType))))
                .ForMember(x => x.CostCalculation, expression => expression.MapFrom(x => Enum.GetValues(typeof(CostCalculation))))

                .ReverseMap();
            configuration.CreateMap<ItemGroupsCardDto, ItemGroupsCard>()
                              .ReverseMap();

        }
    }
    public class ItemGroupsCardTreeDto
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public int? ItemType { get; set; }
        public int? CostCalculation { get; set; }
        public long? WarehousesUnitID { get; set; }
        public IEnumerable<ItemGroupsCardTreeDto> children { get; set; }
        public bool expanded { get; set; }
        public IEnumerable<ItemCardDto> ItemCards { get; set; } 
    }

    public class GetAllItemGroupsCardTree
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }
    }
    public class GetLastCode
    {
        public long? ParentId { get; set; }
    }
    public class DeleteItemGroupsCard
    {
        public long Id { get; set; }
    }
}
