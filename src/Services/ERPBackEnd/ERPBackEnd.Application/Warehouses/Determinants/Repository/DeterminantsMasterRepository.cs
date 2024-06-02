using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
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
    public class DeterminantsMasterRepository : GMappRepository<DeterminantsMaster, DeterminantsMasterDto, long>, IDeterminantsMasterRepository
    {
        private readonly IGRepository<DeterminantsMaster> _determinantsMasterRepos;
        private IGRepository<DeterminantsDetail> _determinantsDetailRepos { get; set; }
        private IMapper _mpper;


        public DeterminantsMasterRepository(IGRepository<DeterminantsMaster> mainRepos, IMapper mapper, DeleteService deleteService
            , IGRepository<DeterminantsDetail> DeterminantsDetailRepos
            ) : base(mainRepos, mapper, deleteService)
        {
            _determinantsMasterRepos = mainRepos;
            _mpper = mapper;
            _determinantsDetailRepos = DeterminantsDetailRepos;

        }
        public async Task<DeterminantsMasterDto> FirstInclude(long id)
        {
            var item = await _determinantsMasterRepos.GetAllIncluding(c => c.DeterminantsDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mpper.Map<DeterminantsMasterDto>(item);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {

            var determinantResult = await FirstInclude(id);

            if (determinantResult?.DeterminantsDetails != null)
            {
                foreach (var item in determinantResult?.DeterminantsDetails)
                { 
                    await _determinantsDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "DeterminantsDetails" }, "DeterminantsMaster", "Id");


        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var determinantResult = await FirstInclude(Convert.ToInt64(id));

                if (determinantResult?.DeterminantsDetails != null)
                {
                    foreach (var item in determinantResult?.DeterminantsDetails)
                    {
                        await _determinantsDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "DeterminantsDetails" }, "DeterminantsMaster", "Id");
        }
        public async Task<DeterminantsMasterDto> CreateDeterminant(DeterminantsMasterDto input)
        {
            var result = await base.Create(input);
            return result;
        }

     
        public async Task<DeterminantsMasterDto> UpdateDeterminant(DeterminantsMasterDto input)
        {
           

                var determinantsItem = await _determinantsMasterRepos.GetAllIncluding(c => c.DeterminantsDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == input.Id);
                var determinantsResult = _mpper.Map<DeterminantsMasterDto>(determinantsItem);
                if (determinantsResult?.DeterminantsDetails != null)
                {
                    foreach (var item in determinantsResult?.DeterminantsDetails)
                    {
                        var entity = _mpper.Map<DeterminantsDetail>(item);
                        await _determinantsDetailRepos.SoftDeleteAsync(entity);
                    }

                }
                var result = await base.Update(input);
                return input;
           

        }
    }
}
