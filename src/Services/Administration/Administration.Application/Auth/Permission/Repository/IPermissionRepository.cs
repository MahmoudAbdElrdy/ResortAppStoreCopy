using Common.Extensions;
using Common.Infrastructures;
using ResortAppStore.Services.Administration.Application.Auth.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Auth.Permissions.Repository   
{
    public interface IPermissionRepository
    {
        Task<PaginatedList<GetAllPermissionDTO>> GetAllPermissionWithPaginationCommand(GetAllPermissionWithPaginationCommand request);
        Task<List<CreatePermissionDto>> CreatePermissionCommand(CreatePermissionCommand request);
        Task<bool> DeletePermissionCommand(DeletePermissionCommand request);
     
    }
}
