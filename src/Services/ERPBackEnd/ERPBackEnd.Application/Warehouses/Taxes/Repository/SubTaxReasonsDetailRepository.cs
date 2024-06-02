using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Repository
{
    public class SubTaxReasonsDetailRepository : GMappRepository<SubTaxReasonsDetail, SubTaxReasonsDetailDto, long>, ISubTaxReasonsDetailRepository
    {
        public SubTaxReasonsDetailRepository(IGRepository<SubTaxReasonsDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
        }


        public async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "SubTaxReasonsDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "SubTaxReasonsDetails", "Id");
        }
    }

}