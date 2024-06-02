using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.UserLogin.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Users.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizedUserDTO = ResortAppStore.Services.ERPBackEnd.Application.Auth.UserLogin.Dto.AuthorizedUserDTO;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository
{
    public interface IUserRepository
    {
        Task<List<CompanyDto>> GetAllCompanyQuery();
        Task<List<BranchDto>> GetAllBranches(GetAllBranchesQuery request);
        Task<AuthorizedUserDTO> LoginCommand(LoginCommand request);
        Task<AuthorizedUserDTO> LoginCompanyCommand(LoginCompanyCommand request);
        Task<UserLoginDto> Register(UserLoginCommand request);
        Task<List<UserDto>> GetAllUserQuery();
        Task<PaginatedList<UserDto>> GetAllUsersWithPaginationCommand(GetAllUsersWithPaginationCommand request);
        Task<UserDto> CreateUserCommand(CreateUserCommand request);
        Task<UserDto> EditUserCommand(EditUserCommand request);
        Task<UserDto> AddPasswordUserCommand(AddPasswordUserCommand request);
        Task<UserDto> ForgetPasswordUserCommand(ForgetPasswordUserCommand request);
        Task<UserDto> ChangePasswordCommand(ChangePasswordCommand request);
        Task<UserDto> DeleteUserCommand(DeleteUserCommand request);
        Task<UserDto> GetById(GetById request);
        Task<List<BranchDto>> GetBranchesByUserId(string userId,long companyId);

        Task<string> GetLastCode();
        Task<int> DeleteListUserCommand(DeleteListUserCommand request);
        string GetRoleByUserId(string userId);
        Task<List<CompanyDto>> GetAllCompanyByOrganization(long organizationId);
    }
}
