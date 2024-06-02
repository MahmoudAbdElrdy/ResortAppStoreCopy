using AutoMapper;
using Common.Mapper;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PaymentMethods.Dto
{
    public class PaymentMethodDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string AccountId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCash { get; set; }



        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PaymentMethod, PaymentMethodDto>()
                          .ReverseMap();
        }
    }
}