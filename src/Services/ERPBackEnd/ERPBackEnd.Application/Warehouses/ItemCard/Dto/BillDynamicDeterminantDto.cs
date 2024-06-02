using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class InsertBillDynamicDeterminantDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? BillItemId { get; set; }
        public double? AddedQuantity { get; set; }
        public double? IssuedQuantity { get; set; }
        public List<DeterminantDataDto> DeterminantsData { get; set; }
        public long? ItemCardId { get; set; }
        public List<DeterminantValueDto> DeterminantsValue { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<InsertBillDynamicDeterminantDto, BillDynamicDeterminant>()
               .ReverseMap();
            configuration.CreateMap<BillDynamicDeterminant, InsertBillDynamicDeterminantDto>().ReverseMap();
        }
    }
    public class BillDynamicDeterminantList
    {
       public List<InsertBillDynamicDeterminantDto> DynamicDeterminantListDto{ get; set; } 
       public List<ItemCardDeterminantDto> ItemCardDeterminantListDto { get; set; } 
    }
    public class BillDynamicDeterminantInput
    {
        public long? BillId { get; set; }
        public long? BillItemId { get; set; }
        public long? ItemCardId { get; set; }
    }
    public class InventoryDynamicDeterminantInput 
    {
        public long? InventoryListsDetailId { get; set; }
        public long? ItemCardId { get; set; }
    }
    public class DeterminantDataDto :IHaveCustomMapping 
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public long? BillDynamicDeterminantSerial { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<DeterminantDataDto, DeterminantData>()
               .ReverseMap();
            configuration.CreateMap<DeterminantData, DeterminantDataDto>().ReverseMap();
        }
    }   
    public class DeterminantValueDto : IHaveCustomMapping
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
       

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<DeterminantValueDto, DeterminantValue>()
               .ReverseMap();
            configuration.CreateMap<DeterminantValue, DeterminantValueDto>().ReverseMap();
        }
    }
    public class DeterminantQueryOutput
    {
        public long? ItemCardId { get; set; }
        public double? ConvertedAddedQuantitySum { get; set; }
        public double? ItemSum { get; set; }

    }
    public class DeterminantQueryInput
    {
        public virtual List<InsertBillDynamicDeterminantDto> BillDynamicDeterminants { get; set; }
    }
}
