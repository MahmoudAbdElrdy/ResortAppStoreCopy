using Common.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Mapper;
using AutoMapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Inventory;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Dto
{
    public class WareHouseListDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public long? BranchId { get; set; }
        public long? CompanyId { get; set; }
        public string? NameAr { get; set; }  
        public string? NameEn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public long? CurrencyId { get; set; }
        public double? CurrencyValue { get; set; }
        public long? StoreId { get; set; }   
        public int? TypeWarehouseList { get; set; }
        public string? Notes { get; set; }
        public bool? IsCollection { get; set; }
        public string? WarehouseListIds { get; set; }
        public long FiscalPeriodId { get; set; }
        public List<WarehouseListsDetailDto> WarehouseListsDetail { get; set; }  
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<WareHouseListDto, InventoryList>()
                .ReverseMap();
            configuration.CreateMap<InventoryList, WareHouseListDto>().ReverseMap();

        }
    }
    public class WarehouseListsDetailDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? WarehouseListId { get; set; }
      
        public long ItemId { get; set; }
        public string? ItemCode { get; set; } 
      
        public string? ItemDescription { get; set; }
        public long? UnitId { get; set; }
        public double? Price { get; set; }
        public long? StoreId { get; set; }

        public double? TotalCostPrice { get; set; }
        public long? ProjectId { get; set; }
        public double? SellingPrice { get; set; }
        public double? MinSellingPrice { get; set; }
        public string? BarCode { get; set; }
        public double? Quantity { get; set; }
        public double? QuantityComputer { get; set; }
        public double? PriceComputer { get; set; }
        public long? ItemGroupId { get; set; }
        public string? Notes { get; set; }
        public double? Total { get; set; }
        public bool? IsApproved { get; set; }

        public virtual List<InsertInventoryDynamicDeterminantDto> InventoryDynamicDeterminants { get; set; } 
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<WarehouseListsDetailDto, InventoryListsDetail>()
                 .ReverseMap();
            configuration.CreateMap<InventoryListsDetail, WarehouseListsDetailDto>().ReverseMap();
        }

    }
    public class InsertInventoryDynamicDeterminantDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? InventoryListsDetailId { get; set; } 
        public double? AddedQuantity { get; set; }
        public double? IssuedQuantity { get; set; }
        public List<InventoryDeterminantDataDto> DeterminantsData { get; set; }
        public long? ItemCardId { get; set; }
        public List<InventoryDeterminantValue> DeterminantsValue { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
        public double? Quantity { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<InsertInventoryDynamicDeterminantDto, InventoryDynamicDeterminant>()
               .ReverseMap();
            configuration.CreateMap<InventoryDynamicDeterminant, InsertInventoryDynamicDeterminantDto>().ReverseMap();
        }
    }
    public class InventoryDynamicDeterminantList
    {
        public List<InsertInventoryDynamicDeterminantDto> DynamicDeterminantListDto { get; set; }
        public List<ItemCardDeterminantDto> ItemCardDeterminantListDto { get; set; }
    }
    public class InventoryDynamicDeterminantInput
    {
        public long? InventoryListsDetailId { get; set; } 
        public long? InventoryItemId { get; set; }
        public long? ItemCardId { get; set; }
    }
    public class InventoryDeterminantDataDto : IHaveCustomMapping
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public long? InventoryListDynamicDeterminantSerial { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<InventoryDeterminantDataDto, InventoryDeterminantData>()
               .ReverseMap();
            configuration.CreateMap<InventoryDeterminantData, InventoryDeterminantDataDto>().ReverseMap();
        }
    }
    public class InventoryDeterminantValueDto : IHaveCustomMapping
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<InventoryDeterminantValueDto, InventoryDeterminantValue>()
               .ReverseMap();
            configuration.CreateMap<InventoryDeterminantValue, InventoryDeterminantValueDto>().ReverseMap();
        }
    }

}
