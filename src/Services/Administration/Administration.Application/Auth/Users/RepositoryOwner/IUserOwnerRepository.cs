using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto;
namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository
{
    public interface IUserOwnerRepository 
    {
         Task<UserOwnerDto> GetUserOwner(GetById request);
        Task<bool> EditUserPayment(EditUserPaymentDto request);
        Task<UserDetailsPackageDto> GetUserDetailsPackage(GetUserDetailsPackageById request);
        Task<List<UserDetailsModuleDto>> GetUserDetailsModule(GetUserDetailsModuleByCode request);


    }
}
