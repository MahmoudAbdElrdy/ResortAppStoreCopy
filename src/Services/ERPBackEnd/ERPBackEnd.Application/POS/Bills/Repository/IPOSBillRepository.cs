using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Repository
{
    public interface IPOSBillRepository
    {
        Task<POSBillDto> CreatePOSBill(POSBillDto input);
        Task<POSBillDto> UpdatePOSBill(POSBillDto input);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<POSBillDto> FirstInclude(long id);
        Task<string> getLastPOSBillCodeByTypeId(long typeId);
        Task<POSBillDynamicDeterminantList> GetPOSBillDynamicDeterminantList(POSBillDynamicDeterminantInput input);




    }
}
