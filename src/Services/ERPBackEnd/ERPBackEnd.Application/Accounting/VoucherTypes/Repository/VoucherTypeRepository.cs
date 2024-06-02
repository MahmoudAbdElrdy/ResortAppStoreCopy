using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Repository
{
    public class VoucherTypeRepository : GMappRepository<VoucherType, VoucherTypeDto, long>, IVoucherTypeRepository
    {
        private readonly IGRepository<VoucherType> _voucherTypeRepos;
        private readonly IGRepository<VouchersRolesPermissions> _vouchersRolesPermissionsRepo;
        private IMapper _mpper;

        public VoucherTypeRepository(IGRepository<VoucherType> mainRepos, IGRepository<VouchersRolesPermissions> vouchersRolesPermissionsRepo, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
            _voucherTypeRepos = mainRepos;
            _vouchersRolesPermissionsRepo = vouchersRolesPermissionsRepo;
            _mpper = mapper;
        }

       
        public  async Task<int> DeleteAsync(long id)
        {
            var vouchersRolesPermissions = _vouchersRolesPermissionsRepo.GetAll().Where(x => x.VoucherTypeId == id).ToList();
            if(vouchersRolesPermissions.Count()>0)
            {

                _vouchersRolesPermissionsRepo.DeleteAll(vouchersRolesPermissions);
                _vouchersRolesPermissionsRepo.SaveChanges();   
            }
           

            return await base.SoftDeleteAsync(id, "VoucherTypes", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "VoucherTypes", "Id");
        }
        public async Task<VoucherTypeDto> FirstInclude(long id)
        {
            var item = await _voucherTypeRepos.GetAll().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);

            var result = _mpper.Map<VoucherTypeDto>(item);
            return result;
        }
    }
}
