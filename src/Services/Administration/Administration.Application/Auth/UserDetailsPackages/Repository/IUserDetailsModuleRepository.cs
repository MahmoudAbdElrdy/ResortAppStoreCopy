using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Repository
{
    public interface IUserDetailsPackageRepository
    {
        Task<List<UserDetailsPackageDto>> GetUserDetailsPackageByUserId(string userId);
        Task<long> CreateUserDetailsPackage(CreateUserDetailsPackageCommand command);
    }
}
