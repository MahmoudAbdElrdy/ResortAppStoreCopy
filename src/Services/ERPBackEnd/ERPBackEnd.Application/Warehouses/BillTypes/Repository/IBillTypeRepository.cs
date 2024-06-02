using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Repository
{
    public interface IBillTypeRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<BillTypeDto> CreateBillType(BillTypeDto input);
        Task<BillTypeDto> UpdateBillType(BillTypeDto input);
        Task<BillTypeDto> FirstInclude(long id);

    }
}
