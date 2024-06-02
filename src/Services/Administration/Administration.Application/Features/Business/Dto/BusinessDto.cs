using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;


namespace ResortAppStore.Services.Administration.Application.Businesss.Dto
{
    public class BusinessDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public long Id { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Business,BusinessDto>().ReverseMap();

        }
    }
}