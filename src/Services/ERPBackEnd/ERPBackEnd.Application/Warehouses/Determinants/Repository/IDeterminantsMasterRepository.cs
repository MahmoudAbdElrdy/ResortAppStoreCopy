using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Repository
{
    public interface IDeterminantsMasterRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<DeterminantsMasterDto> CreateDeterminant(DeterminantsMasterDto input);
        Task<DeterminantsMasterDto> UpdateDeterminant(DeterminantsMasterDto input);
        Task<DeterminantsMasterDto> FirstInclude(long id);
    }
}
