using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Dto
{
    public class InsertPOSBillDynamicDeterminantDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? BillItemId { get; set; }
        public double? AddedQuantity { get; set; }
        public double? IssuedQuantity { get; set; }
        public List<POSDeterminantDataDto> DeterminantsData { get; set; }
        public long? ItemCardId { get; set; }
        public List<POSDeterminantValueDto> DeterminantsValue { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<InsertPOSBillDynamicDeterminantDto, POSBillDynamicDeterminant>()
               .ReverseMap();
            configuration.CreateMap<POSBillDynamicDeterminant, InsertPOSBillDynamicDeterminantDto>().ReverseMap();
        }
    }
    public class POSBillDynamicDeterminantList
    {
       public List<InsertPOSBillDynamicDeterminantDto> DynamicDeterminantListDto{ get; set; } 
       public List<ItemCardDeterminantDto> ItemCardDeterminantListDto { get; set; } 
    }
    public class POSBillDynamicDeterminantInput
    {
        public long? BillId { get; set; }
        public long? BillItemId { get; set; }
        public long? ItemCardId { get; set; }
    }
   
    public class POSDeterminantDataDto :IHaveCustomMapping 
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public long? BillDynamicDeterminantSerial { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSDeterminantDataDto, POSDeterminantData>()
               .ReverseMap();
            configuration.CreateMap<POSDeterminantData, POSDeterminantDataDto>().ReverseMap();
        }
    }   
    public class POSDeterminantValueDto : IHaveCustomMapping
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
       

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSDeterminantValueDto, POSDeterminantValue>()
               .ReverseMap();
            configuration.CreateMap<POSDeterminantValue, POSDeterminantValueDto>().ReverseMap();
        }
    }
    public class POSDeterminantQueryOutput
    {
        public long? ItemCardId { get; set; }
        public double? ConvertedAddedQuantitySum { get; set; }
        public double? ItemSum { get; set; }

    }
    public class POSDeterminantQueryInput
    {
        public virtual List<InsertPOSBillDynamicDeterminantDto> BillDynamicDeterminants { get; set; }
    }
}
