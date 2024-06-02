using Common.Infrastructures;
using ResortAppStore.Services.Administration.Application.Auth.Payment.Dto;
using ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;
using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizedUserDTO = ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto.AuthorizedUserDTO;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository
{
    public interface IUserRepository
    {
        
        Task<AuthorizedUserDTO> LoginCommand(LoginCommand request);
        Task<AuthorizedUserDTO> LoginWithGoogleCommand(string request);
        Task<UserFbInfo> SignUpWithGoogleCommand(string request);
        Task<AuthorizedUserDTO> LoginWithFacebookCommand(string request);
        Task<TechnicalSupportDto> Register(TechnicalSupportCommand request);
        Task<CreateUserDetailsPackageCommand> CreateUserPackageDetails(CreateUserDetailsPackageCommand command);
        Task<List<CreateUserDetailsModulesCommand>> CreateUserDetailsModules(List<CreateUserDetailsModulesCommand> command);
        Task<long> CreateUserCompanies(UserCompaniesDto command);
        Task<long> CreateUserOrganizations(OrganizationDto command);
        Task<List<OrganizationDto>> GetUserOrganizations(string command);
        Task<List<OrganizationDto>> GetUserCompanies(GetUserCompanyRequest command);
        Task<AuthorizedUserDTO> RegisterOwner(TechnicalSupportCommand request);
        Task<List<UserDto>> GetAllUserQuery();
        Task<PaginatedList<UserDto>> GetAllUsersWithPaginationCommand(GetAllUsersWithPaginationCommand request);
        Task<UserDto> CreateUserCommand(CreateUserCommand request);
        Task<UserDto> EditUserCommand(EditUserCommand request);
        Task<UserDto> AddPasswordUserCommand(AddPasswordUserCommand request);
        Task<UserDto> ForgetPasswordUserCommand(ForgetPasswordUserCommand request);
        Task<UserDto> ChangePasswordCommand(ChangePasswordCommand request);
        Task<UserDto> DeleteUserCommand(DeleteUserCommand request);
        Task<UserDto> GetById(GetById request);
        Task<long> AddTrialCommand(TrialDto request);
        Task<string> GetLastCode();
        Task<int> DeleteListUserCommand(DeleteListUserCommand request);
        Task<UserDto> EditUserOwner(UserOwnerDto request);
        Task<bool> LogOut();
        Task<bool> CheckSession(); 

        Task<PaymentResponseLinkDto> GetPaymentRequestLinkDto(long orgId);
    }
}
