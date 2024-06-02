using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository
{
    public class VoucherDetailsRepository : GMappRepository<VoucherDetail, VoucherDetailDto, long>, IVoucherDetailsRepository
    {
        public VoucherDetailsRepository(IGRepository<VoucherDetail> mainRepos, IMapper mapper, DeleteService deleteService) : base(mainRepos, mapper, deleteService)
        {
        }

       
        public  async Task<int> DeleteAsync(long id)
        {
            return await base.SoftDeleteAsync(id, "VoucherDetails", "Id");
        }
        public Task<int> DeleteListAsync(List<object> ids)
        {
            return base.SoftDeleteListAsync(ids, "VoucherDetails", "Id");
        }
    }
}
