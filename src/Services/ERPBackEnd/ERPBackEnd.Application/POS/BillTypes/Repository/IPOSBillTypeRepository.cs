using ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Repository
{
    public interface IPOSBillTypeRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<POSBillTypeDto> CreateBillType(POSBillTypeDto input);
        Task<POSBillTypeDto> UpdateBillType(POSBillTypeDto input);
        Task<POSBillTypeDto> FirstInclude(long id);

    }
}
