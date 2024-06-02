using ResortAppStore.Services.ERPBackEnd.Application.Accounting.CostCenters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.CostCenters.Repository
{
    public interface ICostCenterRepository
    {
        Task<List<CostCenterTreeDto>> GetTrees(GetAllCostCenterTreeCommand request);
        Task<string> GetLastCode(GetLastCode request);
        Task<CostCenterDto> Create(CostCenterDto request);
        Task<CostCenterDto> Update(CostCenterDto request);
        Task<CostCenterDto> DeleteCostCenter(DeleteCostCenterCommand request);
    }
}
