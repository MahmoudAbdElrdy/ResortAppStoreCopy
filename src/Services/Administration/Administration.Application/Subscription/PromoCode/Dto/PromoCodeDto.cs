using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto
{
    public class PromoCodeDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string PromoCode { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PromoCodes, PromoCodeDto>().ReverseMap();
        }
    }
}
