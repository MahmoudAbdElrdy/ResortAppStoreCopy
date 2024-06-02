using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using Common.Infrastructures;
using Common.Exceptions;
using ERPBackEnd.API.Helpers;
using ResortAppStore.Services.ERPBackEnd.Application.Configurations.Permissions.Repository;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class PermissionController : BaseController
    {

        private IPermissionRepository _permissionRepository;
       public  PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        //[AllowAnonymous]
        [HttpGet("all")] 
        public async Task<ActionResult<PageList<GetAllPermissionDTO>>> Show([FromQuery] GetAllPermissionWithPaginationCommand query)
        {
            return Ok((await _permissionRepository.GetAllPermissionWithPaginationCommand(query)));
        }
       //// [AllowAnonymous]
        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<List<CreatePermissionDto>>> Add([FromBody] CreatePermissionCommand command)
        {
            return Ok((await _permissionRepository.CreatePermissionCommand(command)));
        }
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            return Ok(await _permissionRepository.DeletePermissionCommand(new DeletePermissionCommand() { Id = id }));
        }
        //[AllowAnonymous]


    }
}
