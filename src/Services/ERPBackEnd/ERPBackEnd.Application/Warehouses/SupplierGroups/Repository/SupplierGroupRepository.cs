using AutoMapper;
using ResortAppStore.Repositories;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SupplierGroups.Repository
{
    public class SupplierGroupRepository : ISupplierGroupRepository
    {
        public SupplierGroupRepository(IMapper mapper,
            IGRepository<SupplierGroup> context)
        {
     
        }

       
    }
}
