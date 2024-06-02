using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class ItemCardAlternativeDto: IHaveCustomMapping
    {
        public long Id { get; set; }
        public long ItemCardId { get; set; }
        public long AlternativeItemId { get; set; }
        public Double? CostPrice { get; set; }
        public Double? SellingPrice { get; set; }
        public int? AlternativeType { get; set; }
        public Double? CurrentBalance { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<ItemCardAlternativeDto, ItemCardAlternative>()
               .ReverseMap();
            configuration.CreateMap<ItemCardAlternative, ItemCardAlternativeDto>().ReverseMap();
        }
    }
}
