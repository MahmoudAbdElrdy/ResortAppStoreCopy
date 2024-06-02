using AutoMapper;
using Common.Mapper;
using MediatR;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;

namespace ResortAppStore.Services.Administration.Application.Features.Customers.Dto
{
    public class CustomerSubscriptionDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? CustomerId { get; set; }

        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? Applications { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CustomerSubscription, CustomerSubscriptionDto>().ReverseMap();

        }
    }
    public class CreateCustomerSubscriptionCommand
    {
        public CustomerSubscriptionDto InputDto { get; set; }
    }
    public class DeleteCustomerSubscriptionCommand
    {
        public long Id { get; set; }
    }
    public class DeleteListCustomerSubscriptionCommand
    {
        public long[] Ids { get; set; }
    }
    public class EditCustomerSubscriptionCommand
    {
        public CustomerSubscriptionDto InputDto { get; set; }
    }
    public class GetByCustomerSubscriptionId
    {
        public long Id { get; set; }
    }
    public class CustomerSubDomain
    {
        public string SubDomain { get; set; }
    }
}