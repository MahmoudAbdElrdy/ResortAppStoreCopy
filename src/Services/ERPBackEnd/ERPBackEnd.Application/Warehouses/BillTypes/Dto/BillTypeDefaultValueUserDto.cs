using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Dto
{
    public class BillTypeDefaultValueUserDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillTypeId { get; set; }
        public string UserId { get; set; }
        public long? DefaultCurrencyId { get; set; }
        public long? StoreId { get; set; }
        public long? SecondStoreId { get; set; }
        public long? CostCenterId { get; set; }
        public long? InputCostCenterId { get; set; }
        public long? OutputCostCenterId { get; set; }
        public int? PaymentMethodType { get; set; }
        public long? DefaultPaymentMethodId { get; set; }
        public long? SalesPersonId { get; set; }
        public long? ProjectId { get; set; }
        public int? DefaultPrice { get; set; }
        public string? CashAccountId { get; set; }
        public string? SalesAccountId { get; set; }
        public string? SalesReturnAccountId { get; set; }
        public string? PurchasesAccountId { get; set; }
        public string? PurchasesReturnAccountId { get; set; }
        public string? SalesCostAccountId { get; set; }
        public string? InventoryAccountId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillTypeDefaultValueUserDto, BillTypeDefaultValueUser>().ReverseMap();
            configuration.CreateMap<BillTypeDefaultValueUser, BillTypeDefaultValueUserDto>().ReverseMap();


        }
       
    }
}