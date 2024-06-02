using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.CashPayment.Dto
{
    public class CashPaymentInfoDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string CompanyAddress { get; set; }
        public string Email { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CashPaymentInfoDto, CashPaymentInfo>().ReverseMap();
        }
    }

}
