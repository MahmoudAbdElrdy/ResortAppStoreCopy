using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain;

namespace ResortAppStore.Services.Administration.Application.Subscription
{
    public class ModuleDto :IHaveCustomMapping
    {

        public long Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal MonthlySubscriptionPrice { get; set; }
        public decimal YearlySubscriptionPrice { get; set; }
        public decimal FullBuyingSubscriptionPrice { get; set; }
        public decimal OtherUserMonthlySubscriptionPrice { get; set; }
        public decimal OtherUserYearlySubscriptionPrice { get; set; }
        public decimal OtherUserFullBuyingSubscriptionPrice { get; set; }
        public bool IsFree { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        public decimal? InstrumentPattrenPrice { get; set; }

        public decimal? BillPattrenPrice { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Module, ModuleDto>()
                .ForMember(x=>x.Id,src=>src.MapFrom(x=>x.Id))
                .ReverseMap();
        }
    }

    public class EditModuleCommand
    {
        public long Id { get; set; }
        public ModuleDto InputDto { get; set; }
    }
}
