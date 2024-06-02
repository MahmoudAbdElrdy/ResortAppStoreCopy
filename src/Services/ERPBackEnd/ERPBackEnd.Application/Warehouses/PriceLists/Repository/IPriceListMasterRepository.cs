using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Repository
{
    public interface IPriceListMasterRepository
    {
      
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<PriceListMasterDto> CreatePriceList(PriceListMasterDto input);
        Task<PriceListMasterDto> UpdatePriceList(PriceListMasterDto input);
        Task<PriceListMasterDto> FirstInclude(long id);
    }
}
