using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Repository
{
    public interface ITaxMasterRepository
    {
      
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<TaxMasterDto> CreateTax(TaxMasterDto input);
        Task<TaxMasterDto> UpdateTax(TaxMasterDto input);
        Task<TaxMasterDto> FirstInclude(long id);
    }
}
