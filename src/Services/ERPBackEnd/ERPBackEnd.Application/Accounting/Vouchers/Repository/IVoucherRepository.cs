using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository
{
    public interface IVoucherRepository
    {
      
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<VoucherDto> CreateVoucher(VoucherDto input);
        Task<VoucherDto> UpdateVoucher(VoucherDto input);
        Task<VoucherDto> FirstInclude(long id);
        Task generateEntry(List<long> ids);
        Task<string> getLastVoucherCodeByTypeId(long typeId);
        Task<List<NotGenerateEntryVoucherDto>> GetNotGenerateEntryVouchers();
        Task<ResponseResult<List<BillPaymentDto>>> GetVouchersBillPays(long VoucherId);
    }
}
