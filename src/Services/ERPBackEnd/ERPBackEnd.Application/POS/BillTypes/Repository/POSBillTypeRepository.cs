using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.POSBillTypes.Repository
{

    public class POSBillTypeRepository : GMappRepository<POSBillType, POSBillTypeDto, long>, IPOSBillTypeRepository
    {
        private readonly IGRepository<POSBillType> _billTypeRepos;
        private IMapper _mpper;
        private readonly IGRepository<POSBillsRolesPermissions> _billsRolesPermissionsRepos;
        private IGRepository<POSBillTypeDefaultValueUser> _billTypeDefaultValueUserRepos { get; set; }

        public POSBillTypeRepository(IGRepository<POSBillType> mainRepos, IGRepository<POSBillsRolesPermissions> billsRolesPermissionsRepos,
                        IGRepository<POSBillTypeDefaultValueUser> billTypeDefaultValueUserRepos,

            IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _billTypeRepos = mainRepos;
            _billsRolesPermissionsRepos = billsRolesPermissionsRepos;
            _mpper = mapper;
            _billTypeDefaultValueUserRepos = billTypeDefaultValueUserRepos;
        }
        public async Task<POSBillTypeDto> FirstInclude(long id)
        {
            var item = await _billTypeRepos.GetAll().Include(c => c.POSBillTypeDefaultValueUsers).FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);

            var result = _mpper.Map<POSBillTypeDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {
            var billsRolesPermissions = _billsRolesPermissionsRepos.GetAll().Where(x => x.BillTypeId == id).ToList();
            if (billsRolesPermissions.Count() > 0)
            {

                _billsRolesPermissionsRepos.DeleteAll(billsRolesPermissions);
                _billsRolesPermissionsRepos.SaveChanges();
            }

            var billTypeResult = await FirstInclude(id);

            if (billTypeResult?.POSBillTypeDefaultValueUsers != null)
            {
                foreach (var item in billTypeResult?.POSBillTypeDefaultValueUsers)
                {
                    await _billTypeDefaultValueUserRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }


            return await base.ScriptCheckDeleteWithDetails(id, new List<string> {  "POSBillTypeDefaultValueUsers" }, "POSBillTypes", "Id");

        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
                var billsRolesPermissions = _billsRolesPermissionsRepos.GetAll().Where(x => x.BillTypeId == Convert.ToInt64(id)).ToList();
                if (billsRolesPermissions.Count() > 0)
                {

                    _billsRolesPermissionsRepos.DeleteAll(billsRolesPermissions);
                    _billsRolesPermissionsRepos.SaveChanges();
                }

                var billTypeResult = await FirstInclude(Convert.ToInt64(id));

                if (billTypeResult?.POSBillTypeDefaultValueUsers != null)
                {
                    foreach (var item in billTypeResult?.POSBillTypeDefaultValueUsers)
                    {
                        await _billTypeDefaultValueUserRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

            }
            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "POSBillTypeDefaultValueUsers" }, "POSBillTypes", "Id");
        }

        public async Task<POSBillTypeDto> CreateBillType(POSBillTypeDto input)
        {
            var entity = _mpper.Map<POSBillType>(input);
            var result = await base.CreateTEntity(entity);
            return input;

        }
        public async Task<POSBillTypeDto> UpdateBillType(POSBillTypeDto input)
        {
            var billTypeResult = await FirstInclude(input.Id);

            if (billTypeResult?.POSBillTypeDefaultValueUsers != null)
            {
                foreach (var item in billTypeResult?.POSBillTypeDefaultValueUsers)
                {
                    var entity = _mpper.Map<POSBillTypeDefaultValueUser>(item);
                    await _billTypeDefaultValueUserRepos.SoftDeleteAsync(entity);
                }

            }
            var result = await base.UpdateWithoutCheckCode(input);
            return input;


        }







    }


}
