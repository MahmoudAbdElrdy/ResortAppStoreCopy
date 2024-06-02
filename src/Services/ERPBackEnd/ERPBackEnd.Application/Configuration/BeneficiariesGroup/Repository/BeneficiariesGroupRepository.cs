using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Dto;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Configuration.Repository
{
    public class BeneficiariesGroupRepository : GMappRepository<BeneficiariesGroup, BeneficiariesGroupDto, long>, IBeneficiariesGroupRepository
    {
        private readonly IGRepository<BeneficiariesGroup> _beneficiariesGroupRepos;
        private IGRepository<BeneficiariesGroupDetails> _beneficiariesGroupDetailsRepos { get; set; }
        private IMapper _mpper;


        public BeneficiariesGroupRepository(IGRepository<BeneficiariesGroup> mainRepos, IMapper mapper, DeleteService deleteService
            , IGRepository<BeneficiariesGroupDetails> BeneficiariesGroupDetailsRepos
            ) : base(mainRepos, mapper, deleteService)
        {
            _beneficiariesGroupRepos = mainRepos;
            _mpper = mapper;
            _beneficiariesGroupDetailsRepos = BeneficiariesGroupDetailsRepos;

        }
        public async Task<BeneficiariesGroupDto> FirstInclude(long id)
        {
            var item = await _beneficiariesGroupRepos.GetAllIncluding(c => c.BeneficiariesGroupDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mpper.Map<BeneficiariesGroupDto>(item);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {

            var beneficiariesGroupResult = await FirstInclude(id);

            if (beneficiariesGroupResult?.BeneficiariesGroupDetails != null)
            {
                foreach (var item in beneficiariesGroupResult?.BeneficiariesGroupDetails)
                {
                    await _beneficiariesGroupDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "BeneficiariesGroupDetails" }, "BeneficiariesGroup", "Id");


        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var beneficiariesResult = await FirstInclude(Convert.ToInt64(id));

                if (beneficiariesResult?.BeneficiariesGroupDetails != null)
                {
                    foreach (var item in beneficiariesResult?.BeneficiariesGroupDetails)
                    {
                        await _beneficiariesGroupDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "BeneficiariesGroupDetails" }, "BeneficiariesGroup", "Id");
        }
        public async Task<BeneficiariesGroupDto> CreateBeneficiariesGroup(BeneficiariesGroupDto input)
        {
            var result = await base.Create(input);
            return input;
        }


        public async Task<BeneficiariesGroupDto> UpdateBeneficiariesGroup(BeneficiariesGroupDto input)
        {
            var items = await _beneficiariesGroupRepos.GetAllIncluding(c => c.BeneficiariesGroupDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == input.Id);
            var beneficiariesResult = _mpper.Map<BeneficiariesGroupDto>(items);
            if (beneficiariesResult?.BeneficiariesGroupDetails != null)
            {
                foreach (var item in beneficiariesResult?.BeneficiariesGroupDetails)
                {
                    var entity = _mpper.Map<BeneficiariesGroupDetails>(item);
                    await _beneficiariesGroupDetailsRepos.SoftDeleteAsync(entity);
                }

            }
            var result = await base.Update(input);
            return input;

        }
    }
}
