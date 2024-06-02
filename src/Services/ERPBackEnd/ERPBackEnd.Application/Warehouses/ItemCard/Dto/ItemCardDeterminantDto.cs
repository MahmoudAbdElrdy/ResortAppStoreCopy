using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class ItemCardDeterminantDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long ItemCardId { get; set; }
        public long DeterminantId { get; set; }
        public DeterminantsMasterDto DeterminantsMaster { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<ItemCardDeterminantDto, ItemCardDeterminant>()
               .ReverseMap();
            configuration.CreateMap<ItemCardDeterminant, ItemCardDeterminantDto>().ReverseMap();
        }
    }
}
