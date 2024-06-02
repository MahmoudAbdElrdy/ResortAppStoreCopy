using AutoMapper;
using Common.Mapper;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Dto
{
    public class SubscriptionDto:IHaveCustomMapping
    {
        public long Id { set; get; }
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? Applications { get; set; }
        public bool? MultiCompanies { get; set; }
        public bool? MultiBranches { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Subscriptions, SubscriptionDto>().ReverseMap();

        }
    }
    public class GetLastSubscription 
    {
        public long Id { get; set; }
    }
}
