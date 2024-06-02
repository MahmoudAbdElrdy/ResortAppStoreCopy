using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Repository
{
    public class ShiftMasterRepository : GMappRepository<ShiftMaster, ShiftMasterDto, long>, IShiftMasterRepository
    {
        private readonly IGRepository<ShiftMaster> _shiftMasterRepos;
        private IGRepository<ShiftDetail> _shiftDetailsRepos { get; set; }
        private IMapper _mpper;
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;



        public ShiftMasterRepository(IGRepository<ShiftMaster> mainRepos, IMapper mapper, DeleteService deleteService
            , IGRepository<ShiftDetail> shiftDetailRepos,IGRepository<GeneralConfiguration> generalConfiguration
            ) : base(mainRepos, mapper, deleteService)
        {
            _shiftMasterRepos = mainRepos;
            _mpper = mapper;
            _shiftDetailsRepos = shiftDetailRepos;
            _generalConfiguration = generalConfiguration;



        }
        public async Task<ShiftMasterDto> FirstInclude(long id)
        {
            var item = await _shiftMasterRepos.GetAllIncluding(c => c.ShiftDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            var result = _mpper.Map<ShiftMasterDto>(item);
            return result;
        }

        public async Task<ShiftMasterDto> GetShiftDetailsOfDefaultShift()
        {

            var ShiftTypeResult = await _generalConfiguration.GetAll()
                .Where(X => X.Code == "17")  // Ensure X.Code is a property of type string
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(ShiftTypeResult.Value))
            {
                long ShiftId = long.Parse(ShiftTypeResult.Value);
                var item = await _shiftMasterRepos.GetAllIncluding(c => c.ShiftDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == ShiftId);
                var result = _mpper.Map<ShiftMasterDto>(item);
                return result;



            }


            return null;
        }


        public async Task<int> DeleteAsync(long id)
        {

            var shiftResult = await FirstInclude(id);

            if (shiftResult?.ShiftDetails != null)
            {
                foreach (var item in shiftResult?.ShiftDetails)
                {
                    await _shiftDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }

            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "ShiftDetails" }, "ShiftMaster", "Id");


        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var shiftResult = await FirstInclude(Convert.ToInt64(id));

                if (shiftResult?.ShiftDetails != null)
                {
                    foreach (var item in shiftResult?.ShiftDetails)
                    {
                        await _shiftDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }


            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "ShiftDetails" }, "ShiftMaster", "Id");
        }
        public async Task<ShiftMasterDto> CreateShift(ShiftMasterDto input)
        {
            var result = await base.Create(input);
            return result;
        }

        public async Task<ShiftMasterDto> UpdateShift(ShiftMasterDto input)
        {
            var shiftResult = await FirstInclude(input.Id);


            if (shiftResult?.ShiftDetails != null)
            {
                foreach (var item in shiftResult?.ShiftDetails)
                {
                    var entity = _mpper.Map<ShiftDetail>(item);
                    await _shiftDetailsRepos.SoftDeleteAsync(entity);
                }

            }


            var result = await base.Update(input);
            return input;

        }



    }
}
