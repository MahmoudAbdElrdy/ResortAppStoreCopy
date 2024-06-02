using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository
{
    public interface IVoucherDetailsRepository
    {
      
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids); 
    }
}
