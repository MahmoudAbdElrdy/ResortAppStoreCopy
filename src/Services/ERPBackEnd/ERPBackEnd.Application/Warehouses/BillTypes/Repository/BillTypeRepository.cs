using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Repository
{

    public class BillTypeRepository : GMappRepository<BillType, BillTypeDto, long>, IBillTypeRepository
    {
        private readonly IGRepository<BillType> _billTypeRepos;
        private IMapper _mpper;
        private readonly IGRepository<BillsRolesPermissions> _billsRolesPermissionsRepos;
        private IGRepository<BillTypeDefaultValueUser> _billTypeDefaultValueUserRepos { get; set; }

        public BillTypeRepository(IGRepository<BillType> mainRepos, IGRepository<BillsRolesPermissions> billsRolesPermissionsRepos,
                        IGRepository<BillTypeDefaultValueUser> billTypeDefaultValueUserRepos,

            IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _billTypeRepos = mainRepos;
            _billsRolesPermissionsRepos = billsRolesPermissionsRepos;
            _mpper = mapper;
            _billTypeDefaultValueUserRepos = billTypeDefaultValueUserRepos;
        }
        public async Task<BillTypeDto> FirstInclude(long id)
        {
            var item = await _billTypeRepos.GetAll().Include(c => c.BillTypeDefaultValueUsers).FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);

            var result = _mpper.Map<BillTypeDto>(item);
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

            if (billTypeResult?.BillTypeDefaultValueUsers != null)
            {
                foreach (var item in billTypeResult?.BillTypeDefaultValueUsers)
                {
                    await _billTypeDefaultValueUserRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }


            return await base.ScriptCheckDeleteWithDetails(id, new List<string> {  "BillTypeDefaultValueUsers" }, "BillTypes", "Id");

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

                if (billTypeResult?.BillTypeDefaultValueUsers != null)
                {
                    foreach (var item in billTypeResult?.BillTypeDefaultValueUsers)
                    {
                        await _billTypeDefaultValueUserRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

            }
            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "BillTypeDefaultValueUsers" }, "BillTypes", "Id");
        }

        public async Task<BillTypeDto> CreateBillType(BillTypeDto input)
        {
            var entity = _mpper.Map<BillType>(input);
            var result = await base.CreateTEntity(entity);
            return input;

        }
        public async Task<BillTypeDto> UpdateBillType(BillTypeDto input)
        {
            var billTypeResult = await FirstInclude(input.Id);

            if (billTypeResult?.BillTypeDefaultValueUsers != null)
            {
                foreach (var item in billTypeResult?.BillTypeDefaultValueUsers)
                {
                    var entity = _mpper.Map<BillTypeDefaultValueUser>(item);
                    await _billTypeDefaultValueUserRepos.SoftDeleteAsync(entity);
                }

            }
            var result = await base.UpdateWithoutCheckCode(input);
            return input;


        }







    }


}
