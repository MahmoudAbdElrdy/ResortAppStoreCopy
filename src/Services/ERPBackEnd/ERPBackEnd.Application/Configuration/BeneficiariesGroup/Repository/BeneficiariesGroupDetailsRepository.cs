using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Dto;
using Configuration.Entities;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.BeneficiariesGroup.Repository
{
    public class BeneficiariesGroupDetailsRepository : GMappRepository<BeneficiariesGroupDetails, BeneficiariesGroupDetailsDto, long>,
        IBeneficiariesGroupDetailsRepository
    {
        public BeneficiariesGroupDetailsRepository(IGRepository<BeneficiariesGroupDetails> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
    {
    }


    public async Task<int> DeleteAsync(long id)
    {
        return await base.SoftDeleteAsync(id, "BeneficiariesGroupDetails", "Id");
    }
    public Task<int> DeleteListAsync(List<object> ids)
    {
        return base.SoftDeleteListAsync(ids, "BeneficiariesGroupDetails", "Id");
    }
}
    

    
}