using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class ItemCardUnitDto: IHaveCustomMapping
    {
        public long Id { get; set; }
        public long ItemCardId { get; set; }
    
        public long UnitId { get; set; }

        public Double? TransactionFactor { get; set; }
        public Double? SellingPrice { get; set; }
        public Double? MinSellingPrice { get; set; }
        public Double? OpeningCostPrice { get; set; }
        public void CreateMappings(Profile configuration)
        {
           
            configuration.CreateMap<ItemCardUnitDto, ItemCardUnit>()
               .ReverseMap();
            configuration.CreateMap<ItemCardUnit, ItemCardUnitDto>().ReverseMap();
        }
    }
}
