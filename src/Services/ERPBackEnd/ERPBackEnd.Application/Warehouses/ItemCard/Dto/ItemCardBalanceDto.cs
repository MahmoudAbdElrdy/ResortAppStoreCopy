using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class ItemCardBalanceDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long ItemCardId { get; set; }
        public long StoreId { get; set; }
        public string? Position { get; set; }
        public double? MinLimit { get; set; }
        public double? MaxLimit { get; set; }
        public double? ReorderLimit { get; set; }
        public double? CostPrice { get; set; }
        public double? OpeningCostPrice { get; set; }
        public double? SellingPrice { get; set; }
        public double? MinSellingPrice { get; set; }

        public void CreateMappings(Profile configuration)
        {
           
            configuration.CreateMap<ItemCardBalanceDto, ItemCardBalance>()
               .ReverseMap();
            configuration.CreateMap<ItemCardBalance, ItemCardBalanceDto>().ReverseMap();
        }
    }
}
