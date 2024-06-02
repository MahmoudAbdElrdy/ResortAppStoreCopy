using Administration.API.Helpers;
using Common.BaseController;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Repository;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UserDetailsModuleController : BaseController
    {
        private readonly IUserDetailsModuleRepository _userDetailModuleRepository;
        public UserDetailsModuleController(IUserDetailsModuleRepository userDetailModuleRepository)
        {
            _userDetailModuleRepository = userDetailModuleRepository;
        }


        [HttpGet]
        [Route("getUserDetailsModuleByUser/{userId}")]
        public async Task<ActionResult> GetUserDetailsModuleByUser(string userId)
        {
            return Ok(await _userDetailModuleRepository.GetUserDetailsModulesByUserId(userId));
        }

        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<long>> Add([FromBody] CreateUserDetailsModulesCommand command)
        {
            return Ok((await _userDetailModuleRepository.CreateUserDetailsModules(command)));
        }
    }
}
