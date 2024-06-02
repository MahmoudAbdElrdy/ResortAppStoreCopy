using Common.Infrastructures;

using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Services
{
    public interface IPermissionService
    {
         Task<PaginatedList<GetAllPermissionDTO>> GetAllPermissions(GetAllPermissionWithPaginationCommand request);

    }
}
