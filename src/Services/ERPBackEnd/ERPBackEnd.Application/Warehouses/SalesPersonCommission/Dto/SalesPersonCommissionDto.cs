using AutoMapper;
using Common.Mapper;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class SalesPersonCommissionDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long Code { get; set; }
        public long SalesPersonId { get; set; }
        public int CalculationMethod { get; set; }
        public int Type { get; set; }
        public double Target { get; set; }
        public int CommissionOn { get; set; }
        public double AchievedTargetRatio { get; set; }
        public double NotAchievedTargetRatio { get; set; }
        public bool? IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<SalesPersonCommissionDto, SalesPersonCommission>()
               .ReverseMap();
            configuration.CreateMap<SalesPersonCommission, SalesPersonCommissionDto>().ReverseMap();
        }
    }
}
