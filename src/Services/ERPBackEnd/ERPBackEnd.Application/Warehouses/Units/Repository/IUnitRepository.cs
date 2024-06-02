using ResortAppStore.Services.ERPBackEnd.Application.Features.Units.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Repository
{
    public interface IUnitRepository
    {
        Task<UnitTransactionDto> CreateUnitTransaction(CreateUnitTransaction request);
        Task<UnitTransactionDto> DeleteUnitTransaction(DeleteUnitTransaction request);
        Task<int> DeleteListUnitTransaction(DeleteListUnitTransaction request);
        Task<UnitTransactionDto> EditUnitTransaction(EditUnitTransaction request);
        Task<UnitTransactionDto> GetByUnitTransactionId(GetByUnitTransactionId request);
        Task<List<UnitTransactionDto>> GetAllUnitsTransactionsQueries();
        Task<UnitDto> GetByUnitId(GetByUnitId request);

    }
}
