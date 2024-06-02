using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Dto;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Dto;
using System.Collections.Generic;

namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class RegisterUserDto
    {
        public UserDto user { get; set; }

        public CreateUserDetailsPackageCommand packages { get; set; }

        public List<UserCompaniesDto>  userCompaniesDtos { get; set; }
        public List<CreateUserDetailsModulesCommand> modules { get; set; }

        public UserPromoCodeDto userPromoCodeDto { get; set; }

        //public List<UserDetailsModuleDto> module { get; set; }


    }
}
