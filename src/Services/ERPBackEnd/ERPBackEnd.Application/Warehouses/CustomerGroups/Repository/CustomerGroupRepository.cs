using AutoMapper;
using ResortAppStore.Repositories;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomerGroups.Repository
{
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        public CustomerGroupRepository(IMapper mapper,
            IGRepository<CustomerGroup> context)
        {
     
        }

       
    }
}
