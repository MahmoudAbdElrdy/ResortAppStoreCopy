using Administration.API.Helpers;
using Common.BaseController;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Repository;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UserDetailsPackageController : BaseController
    {
        private readonly IUserDetailsPackageRepository _userDetailPackageRepository;
        public UserDetailsPackageController(IUserDetailsPackageRepository userDetailPackageRepository)
        {
            _userDetailPackageRepository = userDetailPackageRepository;
        }


        [HttpGet]
        [Route("getUserDetailsPackageByUser/{userId}")]
        public async Task<ActionResult> GetUserDetailsPackageByUser(string userId)
        {
            return Ok(await _userDetailPackageRepository.GetUserDetailsPackageByUserId(userId));
        }

        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<long>> Add([FromBody] CreateUserDetailsPackageCommand command)
        {
            return Ok((await _userDetailPackageRepository.CreateUserDetailsPackage(command)));
        }
    }
}
