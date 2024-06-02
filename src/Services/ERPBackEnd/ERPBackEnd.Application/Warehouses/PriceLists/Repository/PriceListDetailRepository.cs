using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Repository
{
    public class PriceListDetailRepository : GMappRepository<PriceListDetail, PriceListDetailDto, long>, IPriceListDetailRepository
    {
        public PriceListDetailRepository(IGRepository<PriceListDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
        }

       
        public  async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "PriceListDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "PriceListDetails", "Id");
        }
    }
}
