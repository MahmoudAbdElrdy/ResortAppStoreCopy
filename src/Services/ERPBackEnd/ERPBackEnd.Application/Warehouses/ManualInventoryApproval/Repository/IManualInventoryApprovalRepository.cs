using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Repository
{
    public interface IManualInventoryApprovalRepository
    {
        Task<ManualInventoryApprovalDto> CreateManualInventoryApproval(ManualInventoryApprovalDto input);
        Task<ManualInventoryApprovalDto> UpdateManualInventoryApproval(ManualInventoryApprovalDto input);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
    }
}
