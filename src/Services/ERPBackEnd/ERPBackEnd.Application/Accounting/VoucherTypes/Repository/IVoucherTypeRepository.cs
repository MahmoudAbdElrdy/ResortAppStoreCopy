using System.Collections.Generic;
using System.Threading.Tasks;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Dto;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Repository
{
    public interface IVoucherTypeRepository
    {
      
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<VoucherTypeDto> FirstInclude(long id);
    }
}
