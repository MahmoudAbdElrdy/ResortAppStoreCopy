using Common.Infrastructures;
using ResortAppStore.Services.Administration.Application.Auth.Roles.Dto;
using ResortAppStore.Services.Administration.Application.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Roles.Repository
{
    public interface IRoleRepository
    {
        Task<RoleDto> CreateRoleCommand(CreateRoleCommand request);
        Task<RoleDto> EditRoleCommand(EditRoleCommand request);
        Task<int> DeleteListRoleCommand(DeleteListRoleCommand request);
        Task<RoleDto> DeleteRoleCommand(DeleteRoleCommand request);
        Task<List<RoleDto>> GetAllRolesQuery();
        Task<PaginatedList<RoleDto>> GetAllRolesWithPaginationCommand(GetAllRolesWithPaginationCommand request);
        Task<RoleDto> GetByRoleId(GetByRoleId request);
        Task<PermissionDtoCodeRole> GetLastCode();
    }
}
