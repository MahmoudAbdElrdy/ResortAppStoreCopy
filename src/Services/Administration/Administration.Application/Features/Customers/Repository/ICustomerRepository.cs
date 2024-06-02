using Common.Infrastructures;
using ResortAppStore.Services.Administration.Application.Features.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Features.Customers.Repository
{
    public interface ICustomerRepository
    {
        Task<CustomerDto> CreateCustomerCommand(CreateCustomerCommand request);
        Task<CustomerSubscriptionDto> CreateCustomerSubscriptionCommand(CreateCustomerSubscriptionCommand request);
        Task<CustomerDto> DeleteCustomerCommand(DeleteCustomerCommand request);
        Task<CustomerSubscriptionDto> DeleteCustomerSubscriptionCommand(DeleteCustomerSubscriptionCommand request);
        Task<int> DeleteListCustomerCommand(DeleteListCustomerCommand request);
        Task<int> DeleteListCustomerSubscriptionCommand(DeleteListCustomerSubscriptionCommand request);
        Task<CustomerDto> EditCustomerCommand(EditCustomerCommand request);
        Task<CustomerSubscriptionDto> EditCustomerSubscriptionCommand(EditCustomerSubscriptionCommand request);
        Task<string> VerifyCodeCommand(VerifyCodeCommand request);
        Task<List<CustomerDto>> GetAllCustomersQuery();
        Task<PaginatedList<CustomerDto>> GetAllCustomersWithPaginationCommand(GetAllCustomersWithPaginationCommand request);
        Task<CustomerSubscriptionDto> GetByCustomerSubscriptionId(GetByCustomerSubscriptionId request);
        Task<CustomerDto> GetByCustomerId(GetByCustomerId request);
        Task<string> GetLastCode();
        Task<CustomerDto> GetCustomer(CustomerSubDomain request);
    }
}
