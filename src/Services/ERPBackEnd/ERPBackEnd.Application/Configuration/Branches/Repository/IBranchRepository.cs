using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Branches.Repository
{
    public interface IBranchRepository
    {
        Task<PaginatedList<BranchDto>> GetAllIncluding(Paging paging);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        //Task<BranchDto> Add(BranchDto input);
        Task<List<BranchPermissionDto>> AllBranchPermission(List<object> branchIds);
        Task<List<BranchPermissionListDto>> AllBranchPermissionList(List<object> branchIds, string userId);
        Task<bool> EditBranchPermission(List<BranchPermissionDto> branchPermissions);
    }
}
