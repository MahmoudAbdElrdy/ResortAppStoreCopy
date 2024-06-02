using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Repository
{
    public interface IUserDetailsModuleRepository
    {
        Task<List<UserDetailsModuleDto>> GetUserDetailsModulesByUserId(string userId);
        Task<long> CreateUserDetailsModules(CreateUserDetailsModulesCommand command);
    }
}
