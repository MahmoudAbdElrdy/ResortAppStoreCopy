using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Roles.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Roles.Dto;
using ERPBackEnd.API.Helpers;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Roles.Repository;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class RoleController : BaseController
    {
        private readonly IRoleRepository _roleRepository; 
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [HttpGet("get-ddl")]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            return Ok((await _roleRepository.GetAllRolesQuery()));
        }

      // [AllowAnonymous]
        [HttpGet("all")] 
        public async Task<ActionResult<PageList<RoleDto>>> Show([FromQuery] GetAllRolesWithPaginationCommand query)
        {
            return Ok((await _roleRepository.GetAllRolesWithPaginationCommand(query)));
        }
        //// [AllowAnonymous]
        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<RoleDto>> Add([FromBody] CreateRoleCommand command)
        {
            return Ok((await _roleRepository.CreateRoleCommand(command)));
        }
      // [AllowAnonymous]
        [HttpPost("edit")]
       [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<RoleDto>> Edit([FromBody] EditRoleCommand command)
        {

            return Ok((await _roleRepository.EditRoleCommand(command)));
        }
      // [AllowAnonymous]
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<RoleDto>> Delete( string id)
        {
            return Ok(await _roleRepository.DeleteRoleCommand(new DeleteRoleCommand() { Id = id }));
        }
      // [AllowAnonymous]
        [HttpGet("getById")]

        public async Task<ActionResult<RoleDto>> GetById( string id)
        {
            return Ok(await _roleRepository.GetByRoleId(new GetByRoleId() { Id = id }));
        }
      // [AllowAnonymous]
        [HttpGet("getLastCode")]

        public async Task<ActionResult<PermissionDtoCodeRole>> GetLastCode()
        {
            return Ok(await _roleRepository.GetLastCode());
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<RoleDto>> Delete([FromBody] DeleteListRoleCommand command)
        {
            return Ok(await _roleRepository.DeleteListRoleCommand(command));
        }
    }
}
