using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Repository
{
    public class DeterminantsDetailsRepository : GMappRepository<DeterminantsDetail, DeterminantsDetailsDto, long>, IDeterminantsDetailsRepository
    {
        public DeterminantsDetailsRepository(IGRepository<DeterminantsDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
        }


        public async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "DeterminantsDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "DeterminantsDetails", "Id");
        }
    }
    

    
}
