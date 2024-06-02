using Common.Infrastructures;
using ResortAppStore.Services.Administration.Application.Auth.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Services
{
    public interface IPermissionService
    {
         Task<PaginatedList<GetAllPermissionDTO>> GetAllPermissions(GetAllPermissionWithPaginationCommand request);

    }
}
