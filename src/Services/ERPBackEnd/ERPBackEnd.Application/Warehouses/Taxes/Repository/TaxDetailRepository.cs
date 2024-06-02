using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Repository
{
    public class TaxDetailRepository : GMappRepository<TaxDetail, TaxDetailDto, long>, ITaxDetailRepository
    {
        public TaxDetailRepository(IGRepository<TaxDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
        }

       
        public  async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "TaxDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "TaxDetails", "Id");
        }
    }
}
